import java.io.File;
import java.io.FileNotFoundException;
import java.io.PrintWriter;
import java.util.Scanner;

/**
 * Solves the Cloudflight Coding Contest Level 2: Asteroid Mining.
 * Reads minable rectangle dimensions (W H) from a file in the /data/ directory
 * and writes the character representation of the full asteroid (W+2 x H+2)
 * to a file in the /output/ directory.
 * * NOTE: The input reading loop has been updated to SKIP the sample character
 * representation (H + 2 lines of text) that follows each dimension pair,
 * allowing the program to process all asteroids in the provided input format.
 * * * Level 2 Rules:
 * - Minable width (W) is always 3.
 * - An outpost ('S') is placed in the center of the top bedrock row.
 * - A tunnel ('X') is dug straight down in the center of the minable core.
 * * * Asteroid structure:
 * '#' represents bedrock.
 * ':' represents a minable cell.
 * 'S' represents the outpost.
 * 'X' represents a tunnel cell.
 */
public class AsteroidMiner2 {

    private static final String INPUT_PATH = "data/level2_2_large.in";
    private static final String OUTPUT_PATH = "output/2_large_output.txt";

    // Constant for the fixed minable width for Level 2
    private static final int MINABLE_WIDTH = 3;

    public static void main(String[] args) {
        // ** FIX 1: Ensure the output directory exists before creating the file **
        File outputFile = new File(OUTPUT_PATH);
        File outputDir = outputFile.getParentFile();
        if (outputDir != null && !outputDir.exists()) {
            // Attempt to create the directory and all necessary parent directories
            if (outputDir.mkdirs()) {
                System.out.println("Created output directory: " + outputDir.getAbsolutePath());
            } else {
                System.err.println("Failed to create output directory: " + outputDir.getAbsolutePath());
                return; // Stop if we can't create the directory
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
            if (scanner.hasNextLine()) {
                scanner.nextLine();
            }

            // Process N asteroids
            for (int i = 0; i < N; i++) {
                // 2. Read minable width (W_in) and height (H)

                // Read Width (W_in)
                if (!scanner.hasNextInt()) {
                    System.err.println("Error: Missing WIDTH for asteroid " + (i + 1) + ". Expected " + N + " pairs of dimensions.");
                    break;
                }
                int W_in = scanner.nextInt(); // Read W, which should be 3

                // Read Height (H)
                if (!scanner.hasNextInt()) {
                    System.err.println("Error: Missing HEIGHT for asteroid " + (i + 1) + ". Read width: " + W_in + ". Expected " + N + " pairs of dimensions.");
                    break;
                }
                int H = scanner.nextInt(); // Read H

                // 3. ** NEW LOGIC: Skip the sample character map provided in the input **
                // The sample map takes up H+2 lines (Top bedrock, H middle rows, Bottom bedrock).

                // Consume the rest of the line after H (if W and H were on the same line)
                if (scanner.hasNextLine()) {
                    scanner.nextLine();
                }

                // Skip the H+2 lines of the sample asteroid representation
                for (int j = 0; j < H + 2; j++) {
                    if (scanner.hasNextLine()) {
                        scanner.nextLine();
                    }
                }

                // 4. Generate the asteroid representation
                String asteroidOutput = generateAsteroid(MINABLE_WIDTH, H);

                // 5. Write the output
                writer.print(asteroidOutput);

                // 6. Add an empty line separator (recommended in the output specification)
                if (i < N - 1) {
                    writer.println();
                }
            }

            System.out.println("Processing complete. Output written to " + outputFile.getAbsolutePath());

        } catch (FileNotFoundException e) {
            // Handle case where input file is missing
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
     * minable core size (W x H). For Level 2, W is always 3.
     *
     * @param W The width of the minable core (should be 3).
     * @param H The height of the minable core (':').
     * @return The complete asteroid representation as a String.
     */
    private static String generateAsteroid(int W, int H) {
        // Total width of the asteroid: W minable cells + 2 bedrock borders
        int totalWidth = W + 2; // For W=3, totalWidth = 5

        // Build the middle row string: "#X#"
        String middleRow = "#:X:#";

        // --- Build the full asteroid ---
        StringBuilder sb = new StringBuilder();

        // 1. Create the Top Bedrock Row with Outpost 'S'
        // Example: ##S##
        String topRowWithOutpost = new StringBuilder()
                .append("#".repeat(totalWidth / 2)) // left bedrock (e.g., "##")
                .append("S")                        // outpost
                .append("#".repeat(totalWidth / 2)) // right bedrock (e.g., "##")
                .toString();

        sb.append(topRowWithOutpost).append("\n");

        // 2. Append Middle Rows (H times) with Tunnel 'X'
        for (int row = 0; row < H; row++) {
            sb.append(middleRow).append("\n");
        }

        // 3. Create the Bottom Bedrock Row (e.g., "#####")
        String bedrockRow = "#".repeat(totalWidth);
        sb.append(bedrockRow);

        return sb.toString();
    }
}