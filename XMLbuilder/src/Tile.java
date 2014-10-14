public class Tile {

	/* Feel free to change this, but stay consistent in LevelBuilder */
	String tileTexture;
	String isTrap;
	String isBouncy;
	String isBreakable;
	String isUnstable;
	String isCake;

	/* Do not touch this part */
	int xpos;
	int ypos;
	boolean isTile;

	/* Sets the x, y positions to the correct spot on the map */
	public void setCoordinates(int x, int y) {
		xpos = x;
		ypos = y;
	}

	/* Used to create each tile */
	public Tile(String x) {

		/* Tweak this if you make changes */
		if (x.equals("x") || x.equals("t") || x.equals("b") || x.equals("c")
				|| x.equals("o") || x.equals("u")) {

			/* Makes sure tile is viewed as a tile */
			isTile = true;

			// If it is a normal tile
			if (x.equals("x")) {
				tileTexture = "cube";
				isTrap = "false";
				isBouncy = "false";
				isBreakable = "false";
				isCake = "false";
				isUnstable = "false";
			}

			// If it is a bouncy tile
			else if (x.equals("b")) {
				tileTexture = "bounce";
				isTrap = "false";
				isBouncy = "true";
				isBreakable = "false";
				isCake = "false";
				isUnstable = "false";
			}

			// If it is a trap tile
			else if (x.equals("t")) {
				tileTexture = "trap";
				isTrap = "true";
				isBouncy = "false";
				isBreakable = "false";
				isCake = "false";
				isUnstable = "false";
			}

			// If it is a breakable tile
			else if (x.equals("o")) {
				tileTexture = "breakable";
				isTrap = "false";
				isBouncy = "false";
				isBreakable = "true";
				isCake = "false";
				isUnstable = "false";
			}

			// if it is a cake
			else if (x.equals("c")) {
				tileTexture = "cake";
				isTrap = "false";
				isBouncy = "false";
				isBreakable = "false";
				isCake = "true";
				isUnstable = "false";
			}

			// if it is an unstable tile
			else if (x.equals("u")) {
				tileTexture = "unstable";
				isTrap = "false";
				isBouncy = "false";
				isBreakable = "false";
				isCake = "false";
				isUnstable = "true";
			}

		} else {
			/* It is not a tile so information will not get printed */
			isTile = false;
		}
	}

	@Override
	public String toString() {
		return tileTexture + " " + isTrap + " " + isBouncy + " " + isBreakable;
	}

}
