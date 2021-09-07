using Sandbox;
using System;

namespace TagGame
{
	public class TaggerTeam : Team
	{
		public override string teamName => "You're It";

		public override Color teamColor => new Color(1, 0, 0);

		public override void OnLeave( TagPlayer player )
		{
			base.OnLeave( player );
			if (Tag.Instance.currentRound is TagRound && players.Count == 0 )
			{
				Tag.Instance.SetRound( new WaitRound() );
			}
		}
	}
}
