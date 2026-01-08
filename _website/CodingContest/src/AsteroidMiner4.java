import java.io.File;
import java.io.FileNotFoundException;
import java.io.PrintWriter;
import java.util.Scanner;
import java.util.Arrays;

/**
 * Löst Cloudflight Coding Contest Level 4: "You will need more tunnels".
 *
 * FINAL STRATEGIE:
 * 1. Berechnet die optimalen Tunnelzellen basierend auf der 1/3-Abdeckung.
 * 2. Nutzt die einfache, mathematisch optimale Streifenstrategie (volle Länge/Breite).
 * 3. NUTZT DAS ERLAUBTE +1 ZELLEN BUDGET, um den Außenposten 'S' minimal mit dem
 * Haupt-Tunnelnetzwerk zu verbinden, falls er nicht direkt darauf liegt.
 * 4. Priorität: Wenn W und H beide durch 3 teilbar sind, wird die Horizontal-Strategie
 * (H % 3 == 0) verwendet.
 */
public class AsteroidMiner4 {

    // >>> WICHTIG: Überprüfen und aktualisieren Sie diese Pfade <<<
    private static final String INPUT_PATH = "data/level4_1_small.in";
    private static final String OUTPUT_PATH = "output/level4_solution.txt";

    public static void main(String[] args) {
        File outputFile = new File(OUTPUT_PATH);
        File outputDir = outputFile.getParentFile();
        if (outputDir != null && !outputDir.exists()) {
            if (!outputDir.mkdirs()) {
                System.err.println("Fehler: Konnte das Ausgabe-Verzeichnis nicht erstellen: " + outputDir.getAbsolutePath());
                return;
            }
        }

        try (Scanner fileScanner = new Scanner(new File(INPUT_PATH));
             PrintWriter writer = new PrintWriter(outputFile)) {

            if (!fileScanner.hasNextLine()) {
                System.out.println("Input-Datei ist leer.");
                return;
            }

            String nLine = fileScanner.nextLine().trim();
            if (nLine.isEmpty()) {
                System.err.println("Fehler: Konnte N (Anzahl der Asteroiden) nicht lesen.");
                return;
            }
            int N = Integer.parseInt(nLine);

            for (int i = 0; i < N; i++) {

                String dataLine = "";
                while (fileScanner.hasNextLine()) {
                    dataLine = fileScanner.nextLine().trim();
                    if (!dataLine.isEmpty()) break;
                }

                if (dataLine.isEmpty()) {
                    System.err.println("Fehler: Vorzeitiges Ende der Datei bei der Suche nach W H D für Asteroid " + (i + 1));
                    break;
                }

                int W, H, D;
                try (Scanner lineScanner = new Scanner(dataLine)) {
                    W = lineScanner.nextInt();
                    H = lineScanner.nextInt();
                    D = lineScanner.nextInt();
                }

                int totalRows = H + 2;
                int totalCols = W + 2;
                char[][] grid = new char[totalRows][totalCols];
                int outpostC = -1; // Spaltenindex des Außenpostens 'S'

                for (int r = 0; r < totalRows; r++) {
                    if (!fileScanner.hasNextLine()) {
                        System.err.println("Fehler: Kartendaten fehlen für Zeile " + (r + 1) + " von Asteroid " + (i + 1));
                        return;
                    }
                    String mapLine = fileScanner.nextLine();

                    char[] lineChars = Arrays.copyOf(mapLine.toCharArray(), totalCols);
                    for (int c = 0; c < totalCols; c++) {
                        if (c < lineChars.length) {
                            grid[r][c] = lineChars[c];
                        } else {
                            grid[r][c] = '#';
                        }

                        if (r == 0 && c < lineChars.length && grid[r][c] == 'S') {
                            outpostC = c;
                        }
                    }
                }

                if (outpostC == -1) {
                    System.err.println("Warnung: Konnte 'S' in der Karte für Asteroid " + (i + 1) + " nicht finden.");
                    writeGrid(writer, grid);
                    if (i < N - 1) writer.println();
                    continue;
                }

                solveAsteroid(grid, W, H, outpostC);
                writeGrid(writer, grid);

                if (i < N - 1) {
                    writer.println();
                }
            }

            System.out.println("Verarbeitung abgeschlossen. Ausgabe geschrieben nach " + outputFile.getAbsolutePath());

        } catch (FileNotFoundException e) {
            System.err.println("FEHLER: Die Eingabedatei unter dem Pfad: " + new File(INPUT_PATH).getAbsolutePath() + " konnte nicht gefunden werden.");
            System.err.println("Bitte stellen Sie sicher, dass die Datei 'level4_input.txt' existiert und der Pfad korrekt ist.");
        } catch (Exception e) {
            System.err.println("Ein unerwarteter Fehler trat während der Verarbeitung auf: " + e.getMessage());
            e.printStackTrace();
        }
    }

    /**
     * Implementiert die optimalen Streifenmuster (H/3 oder W/3) und sorgt für
     * die Konnektivität zu 'S' mit minimalem Graben, um das +1 Budget zu nutzen.
     */
    private static void solveAsteroid(char[][] grid, int W, int H, int outpostC) {
        int minableStartR = 1; // Erste Zeile des abbaubaren Kerns
        int minableEndR = H;   // Letzte Zeile des abbaubaren Kerns

        // --- Fall A: H ist durch 3 teilbar (inkl. W auch durch 3 teilbar) ---
        if (H % 3 == 0) {

            // 1. Grabe die horizontalen Streifen (volle Breite)
            // Die optimalen Tunnelzeilen sind R=2, 5, 8, ... (Relativ zur Kerngröße)
            // Im Raster sind das die Zeilen-Indizes: minableStartR + 1, minableStartR + 4, ...
            for (int r = minableStartR + 1; r <= minableEndR; r += 3) {
                // Grabe den Streifen von Spalte 1 bis W (volle Breite)
                for (int c = 1; c <= W; c++) {
                    if (grid[r][c] == ':') {
                        grid[r][c] = 'X';
                    }
                }
            }

            // 2. Stelle die Konnektivität von 'S' (R=0, C=outpostC) sicher
            // 'S' liegt über dem Kern. Wir müssen es mit der ersten Tunnelzeile (R=2) verbinden.
            int firstTunnelR = minableStartR + 1; // R=2

            // Grabe eine einzelne Zelle direkt unter 'S' (R=1, C=outpostC)
            if (grid[minableStartR][outpostC] == ':') {
                grid[minableStartR][outpostC] = 'X';
            }

            // Sollte der Außenposten 'S' zufällig genau über einem horizontalen Streifen liegen (z.B. bei W=1, H=3),
            // dann würde der vertikale Schacht (R=1, C=outpostC) mit der vollen Tunnelzeile (R=2, C=outpostC) verbunden sein.
            // Da die horizontalen Streifen volle Breite haben, ist das Hauptnetzwerk verbunden.

            // --- Fall B: W ist durch 3 teilbar (und H ist nicht durch 3 teilbar) ---
        } else if (W % 3 == 0) {

            // 1. Grabe die vertikalen Streifen (volle Höhe)
            // Die optimalen Tunnelspalten sind C=2, 5, 8, ... (Relativ zur Kerngröße)
            for (int c = 2; c <= W; c += 3) {
                // Grabe den Streifen von Zeile 1 bis H (volle Höhe)
                for (int r = minableStartR; r <= minableEndR; r++) {
                    if (grid[r][c] == ':') {
                        grid[r][c] = 'X';
                    }
                }
            }

            // 2. Stelle die Konnektivität von 'S' (R=0, C=outpostC) sicher

            // Finde die nächstgelegene vertikale Tunnelspalte C_opt
            int nearestTunnelC = -1;
            int minDistance = Integer.MAX_VALUE;

            for (int c = 2; c <= W; c += 3) {
                int distance = Math.abs(c - outpostC);
                if (distance < minDistance) {
                    minDistance = distance;
                    nearestTunnelC = c;
                }
            }

            // Grabe den Verbindungstunnel von 'S' (R=1) zur nächsten optimalen Spalte
            int connectR = minableStartR; // Zeile direkt unter 'S'

            // Starte beim Außenposten und gehe horizontal zur nächstgelegenen vertikalen Linie
            int startC = Math.min(outpostC, nearestTunnelC);
            int endC = Math.max(outpostC, nearestTunnelC);

            for (int c = startC; c <= endC; c++) {
                // Grabe nur, wenn es eine abbaubare Zelle ist und nicht schon ein Tunnel ist
                // (Die vertikalen Streifen C=2, 5, ... sind bereits 'X')
                if (grid[connectR][c] == ':') {
                    grid[connectR][c] = 'X';
                }
            }

            // Da minDistance > 0 ist, wenn S nicht auf einer optimalen Spalte liegt (C=2, 5, ...),
            // wird mindestens eine Zelle gegraben, um die Verbindung herzustellen.
        } else {
            // Dieser Fall sollte nicht auftreten, da entweder W oder H durch 3 teilbar ist.
            System.err.println("Logikfehler: W und H sind beide nicht durch 3 teilbar.");
        }
    }

    /**
     * Hilfsfunktion zum Schreiben des 2D-Zeichenrasters in den PrintWriter.
     */
    private static void writeGrid(PrintWriter writer, char[][] grid) {
        for (char[] row : grid) {
            writer.println(new String(row));
        }
    }
}