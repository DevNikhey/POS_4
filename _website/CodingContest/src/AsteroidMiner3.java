import java.io.File;
import java.io.FileNotFoundException;
import java.io.PrintWriter;
import java.util.Scanner;

/**
 * Solves the Cloudflight Coding Contest Level 3: Asteroid Mining (Optimal Tunnel).
 * Reads minable rectangle dimensions (W H) and Dig Limit (D) from a file in the /data/ directory
 * and writes the character representation of the asteroid.
 * * * * Level 3 Rules:
 * - One minable dimension (W or H) is always 3, the other is 3-199.
 * - Outpost ('S') is given in the sample map's first row.
 * - Tunnel ('X') must discover EVERY cell (cell is 'X' or adjacent to 'X').
 * - Solution must be OPTIMAL (minimal 'X' cells) to meet the Dig Limit.
 * * * * Strategy:
 * 1. Find the Outpost 'S' location (column) from the sample input's first line.
 * 2. Construct the single optimal strip (vertical or horizontal) for full coverage (min(W,H) or max(W,H) cells).
 * 3. Add a minimal connecting path from the outpost (r=1, c_s) to the strip to ensure connectivity.
 */
public class AsteroidMiner3 {

    private static final String INPUT_PATH = "data/level3_2_large.in";
    private static final String OUTPUT_PATH = "output/3_b_output.txt";

    public static void main(String[] args) {
        // Ensure the output directory exists
        File outputFile = new File(OUTPUT_PATH);
        File outputDir = outputFile.getParentFile();
        if (outputDir != null && !outputDir.exists()) {
            if (!outputDir.mkdirs()) {
                System.err.println("Failed to create output directory: " + outputDir.getAbsolutePath());
                return;
            }
        }

        try (Scanner scanner = new Scanner(new File(INPUT_PATH));
             PrintWriter writer = new PrintWriter(outputFile)) {

            // 1. Read the number of asteroids (N)
            if (!scanner.hasNextInt()) {
                System.err.println("Input file does not start with the number of asteroids (N).");
                return;
            }
            int N = scanner.nextInt();

            // Consume the rest of the line after N (if any)
            scanner.nextLine();

            // Process N asteroids
            for (int i = 0; i < N; i++) {
                // 2. Read minable width (W), height (H), and Dig Limit (D)

                if (!scanner.hasNextInt()) { System.err.println("Error: Missing WIDTH for asteroid " + (i + 1)); break; }
                int W = scanner.nextInt();

                if (!scanner.hasNextInt()) { System.err.println("Error: Missing HEIGHT for asteroid " + (i + 1) + ". Read width: " + W); break; }
                int H = scanner.nextInt();

                if (!scanner.hasNextInt()) { System.err.println("Error: Missing DIG LIMIT for asteroid " + (i + 1) + ". Read dimensions: " + W + "x" + H); break; }
                int D = scanner.nextInt();

                // Consume the rest of the line containing W, H, D (important before reading map line)
                scanner.nextLine();

                // 3. Find Outpost Location & Skip Sample Map

                int outpostCol = -1;

                // Read the first row of the sample map to find 'S'
                if (scanner.hasNextLine()) {
                    String firstMapLine = scanner.nextLine();
                    outpostCol = firstMapLine.indexOf('S');
                    if (outpostCol == -1) {
                        System.err.println("Warning: Could not find 'S' in sample map for asteroid " + (i + 1) + ". Defaulting to center column.");
                        // Fallback to center if 'S' is not found (though it should be).
                        outpostCol = (W + 2) / 2;
                    }
                } else {
                    System.err.println("Error: Missing sample map lines for asteroid " + (i + 1) + ". Defaulting outpost to center.");
                    outpostCol = (W + 2) / 2;
                }

                // Skip the remaining H+1 lines of the sample asteroid representation
                // The sample map has H+2 rows total (Row 0 already read).
                for (int j = 0; j < H + 1; j++) {
                    if (scanner.hasNextLine()) {
                        scanner.nextLine();
                    }
                }

                // 4. Generate the asteroid representation
                String asteroidOutput = generateAsteroid(W, H, D, outpostCol);

                // 5. Write the output
                writer.print(asteroidOutput);

                // 6. Add an empty line separator (recommended in the output specification)
                if (i < N - 1) {
                    writer.println();
                }
            }

            System.out.println("Processing complete. Output written to " + outputFile.getAbsolutePath());

        } catch (FileNotFoundException e) {
            // ... (Error handling remains the same)
            System.err.println("--- FILE ACCESS ERROR ---");
            System.err.println("Could not find file at path: " + new File(INPUT_PATH).getAbsolutePath());
            System.err.println("Please ensure the data folder and input file exist in your project root.");
            System.err.println("Error detail: " + e.getMessage());
            System.err.println("-------------------------");
        } catch (Exception e) {
            System.err.println("An unexpected error occurred: " + e.getMessage());
        }
    }

    /**
     * Generates the character representation of the asteroid for a given
     * minable core size (W x H) and dig limit (D), ensuring connection to the outpost.
     *
     * @param W The width of the minable core.
     * @param H The height of the minable core.
     * @param D The dig limit.
     * @param outpostCol The column index of the Outpost 'S' (map index, 0 to W+1).
     * @return The complete asteroid representation as a String.
     */
    private static String generateAsteroid(int W, int H, int D, int outpostCol) {
        // Determine the dimensions of the full map (Bedrock + Core)
        int totalRows = H + 2;
        int totalCols = W + 2;

        char[][] map = new char[totalRows][totalCols];

        // 1. Initialize Map: Bedrock '#' and Minable ':'
        for (int r = 0; r < totalRows; r++) {
            for (int c = 0; c < totalCols; c++) {
                if (r == 0 || r == totalRows - 1 || c == 0 || c == totalCols - 1) {
                    map[r][c] = '#'; // Bedrock
                } else {
                    map[r][c] = ':'; // Minable Core
                }
            }
        }

        // 2. Place the Outpost 'S' at the found location
        if (outpostCol != -1) {
            map[0][outpostCol] = 'S';
        } else {
            // Fallback if 'S' was not found during reading
            outpostCol = totalCols / 2;
            map[0][outpostCol] = 'S';
        }


        // 3. Implement Optimal Tunnel ('X') Strategy and Connection

        // Determine the column/row index of the optimal strip.
        int stripCoord; // Map index (2 for 3-wide/high core)

        if (W == 3) {
            // Case 1: W=3, H is long. Vertical tunnel at center column (map index 2).
            stripCoord = 2;

            // Draw the optimal vertical strip (from row 1 to H)
            for (int r = 1; r <= H; r++) {
                map[r][stripCoord] = 'X';
            }

            // Connect the outpost (r=0, c=outpostCol) to the strip
            // The connection is at r=1, going from outpostCol to stripCoord

            // 3a. Vertical drop from outpost at r=1 (must always be dug)
            map[1][outpostCol] = 'X';

            // 3b. Horizontal path from outpostCol to stripCoord at r=1
            int c1 = Math.min(outpostCol, stripCoord);
            int c2 = Math.max(outpostCol, stripCoord);
            for (int c = c1; c <= c2; c++) {
                // Dig 'X' in minable cells only (r=1 is always minable if 1 <= c <= W)
                if (map[1][c] == ':') {
                    map[1][c] = 'X';
                }
            }


        } else if (H == 3) {
            // Case 2: H=3, W is long. Horizontal tunnel at center row (map index 2).
            stripCoord = 2;

            // Draw the optimal horizontal strip (from column 1 to W)
            for (int c = 1; c <= W; c++) {
                map[stripCoord][c] = 'X';
            }

            // Connect the outpost (r=0, c=outpostCol) to the strip

            // 3a. Vertical drop from outpost at r=1 (must always be dug)
            map[1][outpostCol] = 'X';

            // 3b. Vertical path from r=1 to stripCoord (r=2) at c=outpostCol
            int r1 = Math.min(1, stripCoord);
            int r2 = Math.max(1, stripCoord);
            for (int r = r1; r <= r2; r++) {
                // Dig 'X' in minable cells only (c=outpostCol is always minable if 1 <= c <= W)
                if (map[r][outpostCol] == ':') {
                    map[r][outpostCol] = 'X';
                }
            }

        } else {
            // Fallback for non 3xN or Nx3 asteroid (should not happen)
            System.err.println("Error: Asteroid is neither 3-wide nor 3-high: " + W + "x" + H);
            // Default to a single vertical strip down the center (minable index W/2, map index W/2 + 1)
            int tunnelCol = W / 2 + 1;
            for (int r = 1; r <= H; r++) {
                map[r][tunnelCol] = 'X';
            }
            // Connect to outpost by vertical drop
            map[1][outpostCol] = 'X';
            // Connect horizontally to the strip
            int c1 = Math.min(outpostCol, tunnelCol);
            int c2 = Math.max(outpostCol, tunnelCol);
            for (int c = c1; c <= c2; c++) {
                if (map[1][c] == ':') { map[1][c] = 'X'; }
            }
        }


        // 4. Convert the 2D map array to the final output string
        StringBuilder sb = new StringBuilder();
        for (int r = 0; r < totalRows; r++) {
            for (int c = 0; c < totalCols; c++) {
                sb.append(map[r][c]);
            }
            // Do not append newline after the very last row of the very last asteroid
            if (r < totalRows - 1) {
                sb.append("\n");
            }
        }

        return sb.toString();
    }
}