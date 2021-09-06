using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System.Collections.Generic;
using System;

namespace TagGame
{
	public partial class UI : Panel { }
	public partial class CD : Panel { }
	public partial class StamUI : Panel { }
	public partial class PersonUI : Panel { }
	public partial class NameUI : Panel { }

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

	public partial class Stamina : Panel
	{
		public Label stam;

		public Stamina()
		{
			stam = Add.Label( "", "fillbar" );
		}

		public override void Tick()
		{
			TagPlayer player = Local.Pawn as TagPlayer;
			if ( player is null ) return;

			TagController controller = (TagController)player.Controller;
			float stamina = controller.Stamina;
			float a = 250 * stamina;

			stam.Style.BackgroundColor = new ColorHsv(stamina * 90, 1, 1, .5f);
			stam.Style.Top = 1;
			stam.Style.Height = 45;
			stam.Style.BorderBottomLeftRadius = 10;
			stam.Style.BorderBottomRightRadius = 10;
			stam.Style.BorderTopLeftRadius = 10;
			stam.Style.BorderTopRightRadius = 10;
			stam.Style.Width = a;
			stam.Style.Dirty();
		}
	}

	public partial class Person : Panel
	{
		public Image Avatar;

		public Person()
		{
			Avatar = Add.Image( $"avatar:{Local.SteamId}" );
		}
	}
	public partial class Name : Panel
	{
		public Label Label { get; internal set; }

		public Name()
		{
			Label = Add.Label( "Name", "name" );
		}
		public override void Tick()
		{
			var name = Local.DisplayName;
			var player = Local.Pawn;
			if ( player == null ) return;

			Label.Text = $"{name}";
		}
	}
}
