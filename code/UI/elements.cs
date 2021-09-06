using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;

namespace TagGame
{
	public partial class UI : Panel { }
	public partial class CD : Panel { }

	public partial class UITeam : Panel
	{
		public Label label;
		public Label label2;
		public Label stam;
		public UITeam()
		{
			label2 = Add.Label( "🕒", "icon" );
			label = Add.Label( "Waiting for Players", "value" );
		}
		public override void Tick()
		{
			TagPlayer player = Local.Pawn as TagPlayer;
			if ( player is null ) return;
			label.Text = $"{(player.Team is not null ? player.Team.teamName : "Waiting for Players")}";
			
			TimeSpan timespan = TimeSpan.FromSeconds( (double)Tag.Instance?.currentRound?.TimeLeft );
			string timeleft = $"{timespan.Minutes:D2}:{timespan.Seconds:D2}";
			label2.Text = $"{(Tag.Instance.currentRound is TagRound ? timeleft : "🕒")}";
			label2.SetClass("active", Tag.Instance.currentRound is TagRound);
		}
	}
}
