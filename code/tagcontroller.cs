using Sandbox;
using System.Threading.Tasks;

namespace TagGame
{
	public partial class TagController : WalkController
	{
		private float lastSprint = 0f;
		[Net, Predicted] public float Stamina { get; set; } = 1;

		public override float GetWishSpeed()
		{
			//base.GetWishSpeed()
			float origin = 320f;
			TagPlayer ply = Pawn as TagPlayer;
			if (ply.Team is TaggerTeam && Input.Down(InputButton.Run) && Stamina > 0 && Pawn.Velocity.Length > 0 )
			{
				Stamina = MathX.Approach(Stamina, 0, Time.Delta);
				lastSprint = Time.Now;
				return origin + 350f;
			}
			else
			{
				return origin;
			}
		}
		public override void Simulate()
		{
			base.Simulate();
			if ( !Host.IsServer ) return;
			if (Time.Now > lastSprint + 5 && Stamina < 1 )
			{
				Stamina = MathX.Approach( Stamina, 1, (1f / 15f) * Time.Delta );
			}
		}
	}
}
