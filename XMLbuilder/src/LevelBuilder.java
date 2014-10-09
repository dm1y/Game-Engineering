import java.io.File;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.PrintWriter;
import java.nio.charset.Charset;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.NoSuchElementException;
import java.util.Scanner;

public class LevelBuilder {

	/**
	 * @param args
	 */
	public static void main(String[] args) throws NoSuchElementException {

		Scanner scan = null;

		try {
			scan = new Scanner(new File("test.txt"));
		} catch (FileNotFoundException e1) {
			System.out.println("What the fuck did you do?");
		}

		/* Data structures used to make things work */
		ArrayList<Tile> data = new ArrayList<>();
		String line = null;

		/* Ensures to fit the window size of 960 x 640 [CAPS] */
		// int width = 29;
		int height = 19;

		/* Used to actually give the positions of the map */
		// int x_pos = 0;
		// int y_pos = 0;

		/* Counter to begin from the bottom of the map */
		// int i = 0;
		int j = height;

		while (scan.hasNextLine()) {
			line = scan.nextLine();
			for (int k = 0; k < line.length(); k++) {
				String temp = "";
				temp += line.charAt(k);
				Tile tile = new Tile(temp);
				if (tile.isTile) {
					tile.setCoordinates(k, j);
				}
				data.add(tile);
			}
			j--;
		}

		try (PrintWriter writer = new PrintWriter(Files.newBufferedWriter(
				Paths.get("LevelOutput.XML"), Charset.forName("UTF-8")))) {
			writer.println("<?xml version=" + "\"1.0\"" + " encoding="
					+ "\"utf-8\"" + " ?>");
			writer.println("<XnaContent>");
			writer.println("  <Asset Type=" + "\"Project2.MapTileData[]\""
					+ ">");

			/* Iterates through the actual data and outputs it onto the xml file */
			for (Tile t : data) {

				if (t.isTile) {
					writer.println("    <Item>");
					writer.println("      <tileTexture>" + t.tileTexture
							+ "</tileTexture>");
					writer.println("      <isTrap>" + t.isTrap + "</isTrap>");
					writer.println("      <isBouncy>" + t.isBouncy
							+ "</isBouncy>");
					writer.println("      <isBreakable>" + t.isBreakable
							+ "</isBreakable>");
					writer.println("      <isCake>" + t.isCake + "</isCake>");
					writer.println("      <mapPosition>" + t.xpos + " "
							+ t.ypos + "</mapPosition>");
					writer.println("    </Item>");
				}
			}

			writer.println("  </Asset>");
			writer.println("</XnaContent>");
		} catch (IOException e) {
			System.out.println("Unable to write file.");
			// e.printStackTrace();
		}

	}
}
