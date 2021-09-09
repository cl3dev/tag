using Sandbox;
using System.Collections.Generic;

namespace TagGame
{
	public partial class ScoreSys : NetworkComponent
	{
		public static List<ScoreSys> Scores = new List<ScoreSys>();
		[Net] public int Overall
		{
			get {
				return 1;
			}
			set
			{
				// save
			}
		}
		[Net] public int Round { get; private set; }
		public ScoreSys()
		{
			Scores.Add( this );
		}
		public void Add(int amt)
		{
			Round += amt;
			//Overall += amt;
		}
		public static void ResetScores()
		{
			foreach (ScoreSys score in Scores )
			{
				score.Round = 0;
			}
		}
	}
}
