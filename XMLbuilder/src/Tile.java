public class Tile {

	String tileTexture;
	String isTrap;
	String isBouncy;
	String isBreakable;
	String isCake;
	int xpos;
	int ypos;
	boolean isTile;

	public void setCoordinates(int x, int y) {
		xpos = x;
		ypos = y;
	}

	public Tile(String x) {
		if (x.equals("x") || x.equals("t") || x.equals("d") || x.equals("c")
				|| x.equals("p")) {
			isTile = true;

			// If it is a normal tile
			if (x.equals("x")) {
				tileTexture = "cube";
				isTrap = "false";
				isBouncy = "false";
				isBreakable = "false";
				isCake = "false";
			}

			// If it is a bouncy tile
			else if (x.equals("d")) {
				tileTexture = "bounce";
				isTrap = "false";
				isBouncy = "true";
				isBreakable = "false";
				isCake = "false";
			}

			// If it is a trap tile
			else if (x.equals("t")) {
				tileTexture = "trap";
				isTrap = "true";
				isBouncy = "false";
				isBreakable = "false";
				isCake = "false";
			}

			// If it is a passable tile
			else if (x.equals("p")) {
				tileTexture = "passable";
				isTrap = "false";
				isBouncy = "false";
				isBreakable = "true";
				isCake = "false";
			}

			else if (x.equals("c")) {
				tileTexture = "cake";
				isTrap = "false";
				isBouncy = "false";
				isBreakable = "false";
				isCake = "true";
			}
		} else {
			isTile = false;
		}
	}

	@Override
	public String toString() {
		return tileTexture + " " + isTrap + " " + isBouncy + " " + isBreakable;
	}

}
