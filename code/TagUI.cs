using Sandbox;
using Sandbox.UI;
using System;

namespace TagGame
{
	public partial class TagUI : HudEntity<RootPanel>
	{
		public TagUI()
		{
			if ( !IsClient ) return;
			RootPanel.StyleSheet.Load( "/ui/styles.scss" );
			RootPanel.AddChild<ChatBox>();
			RootPanel.AddChild<NameTags>();
			RootPanel.AddChild<Scoreboard<ScoreboardEntry>>();
			Panel UI = RootPanel.AddChild<UI>();
			UI.AddChild<UITeam>();
			TagPlayer player = Local.Pawn as TagPlayer;
			Panel StamUI = RootPanel.AddChild<StamUI>();
			StamUI.AddChild<Stamina>();
			Panel PersonUI = RootPanel.AddChild<PersonUI>();
			PersonUI.AddChild<Person>();
			Panel NameUI = RootPanel.AddChild<NameUI>();
			NameUI.AddChild<Name>();
		}
	}
}
