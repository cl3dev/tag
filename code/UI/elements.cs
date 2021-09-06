using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
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
		private AnimSceneObject modelObject;
		readonly ScenePanel scene;

		Angles CamAngles = new( 25.0f, 180.0f, 0.0f );
		float CamDistance = 50;
		Vector3 CamPos => Vector3.Up * 60 + CamAngles.Direction * -35;

		public Person()
		{
			Style.FlexWrap = Wrap.Wrap;
			Style.JustifyContent = Justify.Center;
			Style.AlignItems = Align.Center;
			Style.AlignContent = Align.Center;

			using ( SceneWorld.SetCurrent( new SceneWorld() ) )
			{
				// SceneObject.CreateModel( "models/citizen/citizen.vmdl", Transform.Zero );
				var model = Model.Load( "models/citizen/citizen.vmdl" );
				modelObject = new AnimSceneObject( model, Transform.Zero );
				modelObject.SetAnimInt( "idle_states", 2 );
				modelObject.Update( Time.Now );

				Light.Point( Vector3.Up * 150.0f, 200.0f, Color.Red * 5000.0f );
				Light.Point( Vector3.Up * 10.0f + Vector3.Forward * 100.0f, 200, Color.White * 15000.0f );
				Light.Point( Vector3.Up * 10.0f + Vector3.Backward * 100.0f, 200, Color.Magenta * 15000.0f );
				Light.Point( Vector3.Up * 10.0f + Vector3.Right * 100.0f, 200, Color.Blue * 15000.0f );
				Light.Point( Vector3.Up * 10.0f + Vector3.Left * 100.0f, 200, Color.Green * 15000.0f );

				scene = Add.ScenePanel( SceneWorld.Current, CamPos, Rotation.From( CamAngles ), 45 );
				scene.Style.Width = 116;
				scene.Style.Height = 116;
			}
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
