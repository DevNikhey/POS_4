import java.io.*;
import java.util.*;

public class Main {

    static class Point {
        int r, c;
        Point(int r, int c) { this.r = r; this.c = c; }
    }

    public static void main(String[] args) throws Exception {

        String inputPath = "/data/level4_1_small.in";
        String outputPath = "/output/level4_output.txt";

        BufferedReader br = new BufferedReader(new FileReader(inputPath));
        PrintWriter outFile = new PrintWriter(new FileWriter(outputPath));

        int N = Integer.parseInt(br.readLine().trim());

        for (int asteroid = 0; asteroid < N; asteroid++) {

            String line;
            while ((line = br.readLine()) != null && line.isEmpty()) { }
            if (line == null) break;

            String[] parts = line.trim().split(" ");
            int H = Integer.parseInt(parts[0]);
            int W = Integer.parseInt(parts[1]);
            int limit = Integer.parseInt(parts[2]);

            char[][] grid = new char[H][W];
            Point S = null;

            for (int r = 0; r < H; r++) {
                String row = br.readLine();
                for (int c = 0; c < W; c++) {
                    grid[r][c] = row.charAt(c);
                    if (grid[r][c] == 'S')
                        S = new Point(r, c);
                }
            }

            char[][] result = new char[H][W];
            for (int r = 0; r < H; r++)
                result[r] = grid[r].clone();

            boolean widthDiv3 = (W % 3 == 0);
            boolean heightDiv3 = (H % 3 == 0);

            if (widthDiv3) {
                for (int c = 0; c < W; c += 3) {
                    digVerticalStripe(result, grid, c);
                }
            } else {
                for (int r = 0; r < H; r += 3) {
                    digHorizontalStripe(result, grid, r);
                }
            }

            connectToOutpost(result, grid, S);

            for (int r = 0; r < H; r++) {
                outFile.println(new String(result[r]));
            }

            if (asteroid < N - 1)
                outFile.println();
        }

        outFile.close();
        br.close();
    }

    private static void digVerticalStripe(char[][] out, char[][] grid, int c) {
        for (int r = 0; r < out.length; r++) {
            if (grid[r][c] == ':' || grid[r][c] == 'S')
                out[r][c] = 'X';
        }
    }

    private static void digHorizontalStripe(char[][] out, char[][] grid, int r) {
        for (int c = 0; c < out[0].length; c++) {
            if (grid[r][c] == ':' || grid[r][c] == 'S')
                out[r][c] = 'X';
        }
    }

    private static void connectToOutpost(char[][] out, char[][] grid, Point S) {
        int R = S.r, C = S.c;

        for (int c = C; c >= 0; c--) {
            if (grid[R][c] == ':' || grid[R][c] == 'S')
                out[R][c] = 'X';
        }

        for (int r = R; r >= 0; r--) {
            if (grid[r][C] == ':' || grid[r][C] == 'S')
                out[r][C] = 'X';
        }
    }
}
