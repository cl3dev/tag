using Sandbox;
using System;

namespace TagGame
{
	public class RunnerTeam : Team
	{
		public override string teamName => "You're Running";

		public override Color teamColor => new Color(0, 0, 1);

		public override void OnLeave( TagPlayer player )
		{
			base.OnLeave( player );
			if ( Tag.Instance.currentRound is TagRound && players.Count == 0 )
			{
				Tag.Instance.SetRound( new WaitRound() );
			}
		}
	}
}
