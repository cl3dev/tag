using Sandbox;
using System;
using System.Collections.Generic;

namespace TagGame {
	public abstract partial class Team : NetworkComponent
	{
		public List<TagPlayer> players = new List<TagPlayer>();
		[Net] public abstract string teamName { get; }
		public abstract Color teamColor { get; }

		public virtual void OnBecome( TagPlayer player )
		{
			players.Add( player );
			player.RenderColor = teamColor;
		}

		public virtual void OnLeave( TagPlayer player )
		{
			players.Remove( player );
		}
	}
}
