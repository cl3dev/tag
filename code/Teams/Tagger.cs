using Sandbox;
using System;

namespace TagGame
{
	public class TaggerTeam : Team
	{
		public override string teamName => "You're It";

		public override Color teamColor => new Color(1, 0, 0);
	}
}
