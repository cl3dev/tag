using Sandbox;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TagGame
{
	public class TagRound : Round
	{
		public override string name => "Tagging";

		public override int length => 5 * 60;

		public override void Start()
		{
			if ( !Host.IsServer ) return;
			ScoreSys.ResetScores();
			int taggerNum = Math.Clamp( (int)Math.Ceiling( (decimal)(Client.All.Count / 3) ), 1, 4 );
			List<Client> players = new List<Client>( Client.All );
			for ( int i = 0; i < taggerNum; i++ )
			{
				Client tagger = players[Rand.Int( players.Count - 1 )];
				players.Remove( tagger );
				TagPlayer tagPawn = (TagPlayer)tagger.Pawn;
				tagPawn.Team = Tag.Instance.TagTeam;
				Log.Info( $"{tagger} -> {tagPawn.Team.teamName}" );
			}
			foreach (Client player in players )
			{
				TagPlayer pawn = (TagPlayer)player.Pawn;
				pawn.Team = Tag.Instance.RunTeam;
			}
		}

		public override void PlayerTouch( TagPlayer player , TagPlayer other )
		{
			float now = Time.Now;
			if ( player.Team is not TaggerTeam || now < other.nextTouch ) return;
			player.nextTouch = now + 10;
			player.Team = Tag.Instance.RunTeam;
			other.Team = Tag.Instance.TagTeam;
		}
		public override void OnTick()
		{
			if ( TimeLeft <= 0 )
			{
				Tag.Instance.SetRound( new SummaryRound() );
			}
		}
	}
}
