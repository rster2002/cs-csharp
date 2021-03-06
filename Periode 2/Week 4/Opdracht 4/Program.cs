﻿using System;
using System.IO;

namespace Opdracht4 {
    class Program {
        Random random = new Random();
        const string saveFilePath = "./gameSave.txt";

        enum RegularCandy {
            JellyBean = 1,
            Lozenge,
            LemonDrop,
            GumSquare,
            LollipopHead,
            JujubeCluster
        }

        static void Main(string[] args) {
            Program program = new Program();
            program.start();

            //Main(args); // For testing
        }

        void start() {
            RegularCandy[,] candyGrid = new RegularCandy[9, 9];
              
            if (File.Exists(saveFilePath)) {
                try {
                    candyGrid = readPlayingField(saveFilePath);
                } catch {
                    Console.WriteLine("Het bestand kon niet worden ingelezen, waarschijnlijk omdat hij is aangepast.");
                    Console.WriteLine("Er word nu een nieuw speelveld gegenereerd en opgeslagen");
                    initAndSaveCandies(candyGrid, saveFilePath);
                }
            } else {
                initAndSaveCandies(candyGrid, saveFilePath);
            }

            printCandies(candyGrid);

            bool scoreRowPresent = checkScoreRowPresent(candyGrid);
            bool scoreColumnPresent = checkScoreColumnPresent(candyGrid);

            Console.WriteLine("{0}, er is {1}een score rij aanwezig", scoreRowPresent ? "Ja" : "Nee", scoreRowPresent ? "" : "GEEN ");
            Console.WriteLine("{0}, er is {1}een score kolom aanwezig", scoreColumnPresent ? "Ja" : "Nee", scoreColumnPresent ? "" : "GEEN ");

            Console.ReadKey();
        }

        void initAndSaveCandies(RegularCandy[,] grid, string fileName) {
            initCandies(grid);
            writePlayingField(grid, fileName);
        }

        void initCandies(RegularCandy[,] grid) {
            for (int r = 0; r < grid.GetLength(0); r++) {
                for (int c = 0; c < grid.GetLength(1); c++) {
                    int candyCountPossible = Enum.GetNames(typeof(RegularCandy)).Length;

                    grid[r, c] = (RegularCandy) random.Next(1, candyCountPossible + 1);
                }
            }
        }

        ConsoleColor getColorForCandy(RegularCandy candy) {
            int candyIndex = (int) candy;

            switch (candyIndex) {
                case 1:
                    return ConsoleColor.Red;

                case 2:
                    return ConsoleColor.Cyan;

                case 3:
                    return ConsoleColor.Yellow;

                case 4:
                    return ConsoleColor.Green;

                case 5:
                    return ConsoleColor.Blue;

                case 6:
                    return ConsoleColor.Magenta;

                default:
                    return ConsoleColor.White;
            }
        }

        void printCandy(RegularCandy candy) {
            Console.ForegroundColor = getColorForCandy(candy);
            Console.Write("#");
            Console.ResetColor();
        }

        void printCandies(RegularCandy[,] grid) {
            for (int r = 0; r < grid.GetLength(0); r++) {
                for (int c = 0; c < grid.GetLength(1); c++) {
                    printCandy(grid[r, c]);
                    Console.Write(" ");
                }

                Console.WriteLine();
            }
        }

        bool checkScoreRowPresent(RegularCandy[,] grid) {
            for (int r = 0; r < grid.GetLength(0); r++) {
                for (int c = 0; c < grid.GetLength(1) - 2; c++) {
                    RegularCandy comparisonCandy = grid[r, c];

                    if (grid[r, c + 1] == comparisonCandy && grid[r, c + 2] == comparisonCandy) {
                        return true;
                    }
                }
            }

            return false;
        }

        bool checkScoreColumnPresent(RegularCandy[,] grid) {
            for (int c = 0; c < grid.GetLength(0); c++) {
                for (int r = 0; r < grid.GetLength(1) - 2; r++) {
                    RegularCandy comparisonCandy = grid[r, c];

                    if (grid[r + 1, c] == comparisonCandy && grid[r + 1, c] == comparisonCandy) {
                        return true;
                    }
                }
            }

            return false;
        }

        void writePlayingField(RegularCandy[,] grid, string fileName) {
            StreamWriter streamWriter = new StreamWriter(fileName);

            streamWriter.WriteLine(grid.GetLength(0));
            streamWriter.WriteLine(grid.GetLength(1));

            for (int r = 0; r < grid.GetLength(0); r++) {
                string row = "";

                for (int c = 0; c < grid.GetLength(1); c++) {
                    row += (int) grid[r, c];

                    if (c < grid.GetLength(1) - 1) {
                        row += " ";
                    }
                }

                streamWriter.WriteLine(row);
            }

            streamWriter.Close();
        }
         
        RegularCandy[,] readPlayingField(string fileName) {
            StreamReader streamReader = null;

            try {
                streamReader = new StreamReader(fileName);

                int rows = int.Parse(streamReader.ReadLine());
                int columns = int.Parse(streamReader.ReadLine());

                RegularCandy[,] grid = new RegularCandy[rows, columns];

                int row = 0;
                while (!streamReader.EndOfStream) {
                    string line = streamReader.ReadLine();
                    string[] candies = line.Split(' ');

                    for (int i = 0; i < candies.Length; i++) {
                        int candyId = int.Parse(candies[i]);

                        if (Enum.IsDefined(typeof(RegularCandy), candyId)) {
                            RegularCandy candy = (RegularCandy) candyId;
                            grid[row, i] = candy;
                        } else {
                            throw new Exception("Candy id was not present in Enum");
                        }
                    }

                    row++;
                }

                return grid;
            } catch (IOException exception) {
                Console.WriteLine(exception.Message);
            } catch (Exception exception) {
                throw exception;
            } finally {
                streamReader.Close();
            }
        }
    }
}
