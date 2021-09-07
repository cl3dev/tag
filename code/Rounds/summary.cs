using Sandbox;
using System;

namespace TagGame
{
	public partial class SummaryRound : Round
	{
		public override string name => "Summary";

		public override int length => 10;

		public override void Start()
		{
			if ( !Host.IsServer ) return;
			foreach ( Client client in Client.All )
			{
				TagPlayer player = client.Pawn as TagPlayer;
				player.CurrentTeam = null;
			}
		}

		public override void OnTick()
		{
			if (TimeLeft <= 0)
			{
				Tag.Instance.SetRound( new WaitRound() );
			}
		}
	}
}
