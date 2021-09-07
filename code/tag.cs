using Sandbox;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TagGame
{
	public partial class Tag : Sandbox.Game
	{
		public static Tag Instance => (Current as Tag);
		public TaggerTeam TagTeam = new TaggerTeam();
		public RunnerTeam RunTeam = new RunnerTeam();
		[Net] public Round currentRound { get; private set; }

		public Tag()
		{
			if ( !IsServer ) return;
			SetRound( new WaitRound() );
			_ = Tick();
			new TagUI();
		}
		public override void ClientJoined( Client cl )
		{
			TagPlayer player = new TagPlayer();
			cl.Pawn = player;
			player.Respawn();
			if ( Tag.Instance?.currentRound is TagRound )
			{
				player.CurrentTeam = Tag.Instance?.RunTeam;
			}
			SetupNewPVS( player );
			base.ClientJoined( cl );
		}
		public override void ClientDisconnect( Client cl, NetworkDisconnectionReason reason )
		{
			TagPlayer pawn = cl.Pawn as TagPlayer;
			pawn?.CurrentTeam?.OnLeave( pawn );
			base.ClientDisconnect( cl, reason );
		}
		public void SetRound( Round round )
		{
			currentRound?.End();
			currentRound = round;
			currentRound.Start();
		}
		private async Task Tick()
		{
			while ( true )
			{
				await Task.DelaySeconds( 1 );
				currentRound?.OnTick();
			}
		}
		private void SetupNewPVS(TagPlayer player)
		{
			Client me = player.GetClientOwner();
			if ( player is null || me is null ) return;
			foreach (Client client in Client.All )
			{
				me.Pvs.Add( client?.Pawn );
				client.Pvs.Add( player );
			}
		}
	}
}
