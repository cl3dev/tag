using Sandbox;
using System;

namespace TagGame
{
	public class WaitRound : Round
	{
		public override string name => "Waiting for players";
		public override int length => -1;

		public override void Start()
		{
			if ( !Host.IsServer ) return;
			foreach ( Client client in Client.All )
			{
				TagPlayer player = (TagPlayer)client.Pawn;
				Tag.Instance.MoveToSpawnpoint( player );
				player.CurrentTeam = null;
			}
		}
		public override void OnTick()
		{
			if (Client.All.Count >= 2 )
			{
				Tag.Instance.SetRound(new TagRound());
			}
		}
	}
}
