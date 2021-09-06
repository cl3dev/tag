using Sandbox;
using System.Threading.Tasks;

namespace TagGame
{
	public partial class TagController : WalkController
	{
		private float lastSprint = 0f;
		[Net, Predicted] public float Stamina { get; set; } = 1;

		public TagController()
		{
			_ = StamTick();
		}
		public override float GetWishSpeed()
		{
			//base.GetWishSpeed()
			float origin = 320f;
			TagPlayer ply = Pawn as TagPlayer;
			if (ply.Team is TaggerTeam && Input.Down(InputButton.Run) && Stamina > 0 && Pawn.Velocity.Length > 0 )
			{
				Stamina = MathX.Approach(Stamina, 0, Time.Delta);
				lastSprint = Time.Now;
				return origin + 350f;
			}
			else
			{
				return origin;
			}
		}
		private void RegenStam()
		{
			float now = Time.Now;
			if ( now > lastSprint + 5 && Stamina < 1 )
			{
				Stamina = MathX.Approach(Stamina, 1, 1f / 15f);
			}
		}
		private async Task StamTick()
		{
			if ( !Host.IsServer ) return;
			while ( true )
			{
				await GameTask.DelaySeconds( 1 );
				RegenStam();
			}
		}
	}
}
