using System;

namespace TagGame
{
	public partial class SummaryRound : Round
	{
		public override string name => "Summary";

		public override int length => 30;

		public override void OnTick()
		{
			if (TimeLeft <= 0)
			{
				Tag.Instance.SetRound( new WaitRound() );
			}
		}
	}
}
