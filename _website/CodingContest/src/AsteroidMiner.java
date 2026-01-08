import java.io.File;
import java.io.FileNotFoundException;
import java.io.PrintWriter;
import java.util.Scanner;

/**
 * Solves the Cloudflight Coding Contest Level 1: Asteroid Mining.
 * Reads minable rectangle dimensions (W H) from a file in the /data/ directory
 * and writes the character representation of the full asteroid (W+2 x H+2)
 * to a file in the /output/ directory.
 * * Asteroid structure:
 * '#' represents bedrock.
 * ':' represents minable cells.
 */
public class AsteroidMiner {

    // IMPORTANT: Updated the input path based on your error message
    private static final String INPUT_PATH = "data/level1_2_large.in";
    private static final String OUTPUT_PATH = "output/xx_output.txt";

    public static void main(String[] args) {
        // Use try-with-resources for safe file handling

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
             PrintWriter writer = new PrintWriter(outputFile)) { // Use the File object here

            // 1. Read the number of asteroids (N)
            if (!scanner.hasNextInt()) {
                System.err.println("Input file does not start with the number of asteroids (N).");
                return;
            }
            int N = scanner.nextInt();

            // Process N asteroids
            for (int i = 0; i < N; i++) {
                // 2. Read minable width (W) and height (H)
                if (!scanner.hasNextInt()) {
                    System.err.println("Error: Missing width for asteroid " + (i + 1));
                    break;
                }
                int W = scanner.nextInt();

                if (!scanner.hasNextInt()) {
                    System.err.println("Error: Missing height for asteroid " + (i + 1));
                    break;
                }
                int H = scanner.nextInt();

                // 3. Generate the asteroid representation
                String asteroidOutput = generateAsteroid(W, H);

                // 4. Write the output
                writer.print(asteroidOutput);

                // 5. Add an empty line separator (recommended in the output specification)
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
     * minable core size (W x H).
     *
     * @param W The width of the minable core (':').
     * @param H The height of the minable core (':').
     * @return The complete asteroid representation as a String.
     */
    private static String generateAsteroid(int W, int H) {
        StringBuilder sb = new StringBuilder();

        // The total width of the asteroid, including bedrock borders
        int totalWidth = W + 2;

        // 1. Create the Top/Bottom Bedrock Row (e.g., "######" for W=4)
        String bedrockRow = "#".repeat(totalWidth);

        // 2. Create the Minable/Middle Row (e.g., "#::::#" for W=4)
        String minableCore = ":".repeat(W);
        String middleRow = "#" + minableCore + "#";

        // --- Build the full asteroid ---

        // Append Top Bedrock Row
        sb.append(bedrockRow).append("\n");

        // Append Middle Rows (H times)
        for (int row = 0; row < H; row++) {
            sb.append(middleRow).append("\n");
        }

        // Append Bottom Bedrock Row
        sb.append(bedrockRow);

        return sb.toString();
    }
}