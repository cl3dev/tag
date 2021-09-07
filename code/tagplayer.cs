using Sandbox;
using System;
using System.Threading.Tasks;

namespace TagGame
{
	public partial class TagPlayer : Player
	{
		public float nextTouch = 0;
		private Team previousTeam;
		[Net, OnChangedCallback] public Team CurrentTeam { get; set; }
		[Net] public ScoreSys Score { get; set; } = new ScoreSys();
		private void OnCurrentTeamChanged()
		{
			previousTeam?.OnLeave( this );
			CurrentTeam?.OnBecome( this );
			previousTeam = CurrentTeam;
			if ( CurrentTeam is null ) RenderColor = new Color( 1, 1, 1 );
		}
		public TagPlayer()
		{
			_ = PointsTick();
		}
		public override void Respawn()
		{
			SetModel( "models/citizen/citizen.vmdl" );
			Animator = new StandardPlayerAnimator();
			Controller = new TagController();
			Camera = new ThirdPersonCamera();
			EnableAllCollisions = false;
			EnableTouch = true;
			EnableTouchPersists = true;
			EnableHideInFirstPerson = true;
			EnableShadowInFirstPerson = true;
			base.Respawn();
		}

		public override void Simulate( Client cl )
		{
			base.Simulate( cl );
			if ( Input.Pressed( InputButton.View ) )
			{
				if (Camera is FirstPersonCamera )
				{
					Camera = new ThirdPersonCamera();
				}
				else
				{
					Camera = new FirstPersonCamera();
				}
			}
		}
		public override void Touch( Entity other )
		{
			base.StartTouch( other );
			if ( other is not TagPlayer ) return;
			Tag.Instance.currentRound?.PlayerTouch( this, other as TagPlayer );
		}
		private async Task PointsTick()
		{
			while ( true )
			{
				await GameTask.DelaySeconds( 5 );
				if ( Tag.Instance?.currentRound is not TagRound || CurrentTeam is not RunnerTeam ) return;
				Score.Add(1);
			}
		}
	}
}
