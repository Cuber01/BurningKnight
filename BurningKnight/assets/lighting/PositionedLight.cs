using Microsoft.Xna.Framework;

namespace BurningKnight.assets.lighting {
	public class PositionedLight : Light {
		public Vector2 Position;

		public override Vector2 GetPosition() {
			return Position;
		}
	}
}