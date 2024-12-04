using System.Security.Cryptography.X509Certificates;

namespace advent_of_code_project
{


    public class Day4
    {

        public static string pattern = "XMAS";

        public static void Day4Method()
        {
            StreamReader streamReader = new(ResourceLocator.address + "Day4Input.txt");

            int answer = 18;


            List<string> wordsearch = new List<string>();

            int count = 0;
            while (count < 140)
            {
                wordsearch.Add(streamReader.ReadLine());
                count++;
            }

            // wordsearch.ForEach(Console.WriteLine);
            // Console.WriteLine(wordsearch.Count);

            //for each orientation there is a first letter, either X or S depending on whether it's forward/down, or backwards/up
            //whenever we hit that letter, we check the appropriate other char positions, either the next three in the string, or the corresponding 
            //positions in

            int total = 0;

            for (int i = 0; i < wordsearch.Count; i++)
            {
                for (int j = 0; j < wordsearch[i].Length; j++)
                {
                    if (wordsearch[i][j] == 'X')
                    {
                        List<Direction> directions = CheckSurroundings(i, j, wordsearch);
                        if (directions[0] != Direction.NOT_FOUND)
                        {

                            //note i and j are NOT modified by CheckSurroundings, so CheckRemainder checks the M again! 
                            int newHits = CheckRemainder(i, j, wordsearch, directions);

                            Console.WriteLine("new hits at pos" + i + " " + j + "are: " + newHits);

                            total += newHits;

                        }
                    }
                }
            }

            Console.WriteLine("the total number of hits is " + total);

        }


        public static List<Direction> CheckSurroundings(int i, int j, List<string> wordsearch)
        {

            List<Direction> dirsToCheck = new List<Direction>();

            if (j + 1 < wordsearch[i].Length && wordsearch[i][j + 1] == 'M')
            {
                dirsToCheck.Add(Direction.RIGHT);
            }
            if (j + 1 < wordsearch[i].Length && i + 1 < wordsearch.Count && wordsearch[i + 1][j + 1] == 'M')
            {
                dirsToCheck.Add(Direction.DOWN_RIGHT);
            }
            if (i + 1 < wordsearch.Count && wordsearch[i + 1][j] == 'M')
            {
                dirsToCheck.Add(Direction.DOWN);
            }
            if (j - 1 >= 0 && i + 1 < wordsearch.Count && wordsearch[i + 1][j - 1] == 'M')
            {
                dirsToCheck.Add(Direction.DOWN_LEFT);
            }
            if (j - 1 >= 0 && wordsearch[i][j - 1] == 'M')
            {
                dirsToCheck.Add(Direction.LEFT);
            }
            if (j - 1 >= 0 && i - 1 >= 0 && wordsearch[i - 1][j - 1] == 'M')
            {
                dirsToCheck.Add(Direction.UP_LEFT);
            }
            if (i - 1 >= 0 && wordsearch[i - 1][j] == 'M')
            {
                dirsToCheck.Add(Direction.UP);
            }
            if (j + 1 < wordsearch[i].Length && i - 1 >= 0 && wordsearch[i - 1][j + 1] == 'M')
            {
                dirsToCheck.Add(Direction.UP_RIGHT);
            }

            if (dirsToCheck.Count > 0)
            {
                return dirsToCheck;
            }

            return new List<Direction>() { Direction.NOT_FOUND };
        }


        public static int CheckRemainder(int i, int j, List<string> wordsearch, List<Direction> directions)
        {
            bool found;

            Console.WriteLine("directions being checked at position " + i + ": " + j + " are " + directions.Count);
            Console.WriteLine("comprised of : ");
            directions.ForEach(s => Console.Write(s));
            Console.WriteLine(" ");

            int newXMAS = 0;
            foreach (Direction dir in directions)
            {
               
                switch (dir)
                {
                    case Direction.RIGHT:
                        found = true; 

                        for (int c = 0; c < pattern.Length; c++)
                        {
                            if (j + c < wordsearch[i].Length)
                            {
                                if (wordsearch[i][j + c] != pattern[c])
                                {
                                    found = false;
                                }
                            }
                            else
                            {
                                found = false;


                            }

                        }
                        if(found) newXMAS++;
                        break;

                    case Direction.DOWN_RIGHT:
                        found = true;


                        for (int c = 0; c < pattern.Length; c++)
                        {
                            if (j + c < wordsearch[i].Length && i + c < wordsearch.Count)
                            {
                                if (wordsearch[i + c][j + c] != pattern[c])
                                {
                                    found = false;

                                }
                            }
                            else
                            {
                                found = false;

                            }
                        }
                        if (found) newXMAS++;

                        break;

                    case Direction.DOWN:

                        found = true;


                        for (int c = 0; c < pattern.Length; c++)
                        {
                            if (i + c < wordsearch.Count)
                            {
                                if (wordsearch[i + c][j] != pattern[c])
                                {
                                    found = false;

                                }
                            }
                            else
                            {
                                found = false;

                            }

                        }
                        if (found) newXMAS++;

                        break;

                    case Direction.DOWN_LEFT:
                        found = true;


                        for (int c = 0; c < pattern.Length; c++)
                        {
                            if (i + c < wordsearch.Count && j - c >= 0)
                            {
                                if (wordsearch[i + c][j - c] != pattern[c])
                                {
                                    found = false;

                                }
                            }
                            else
                            {
                                found = false;

                            }
                        }
                        if (found) newXMAS++;

                        break;

                    case Direction.LEFT:

                        found = true;

                        for (int c = 0; c < pattern.Length; c++)
                        {
                            if (j - c >= 0)
                            {
                                if (wordsearch[i][j - c] != pattern[c])
                                {
                                    found = false;
                                    

                                }
                            }
                            else
                            {
                                found = false;

                            }

                        }
                        if (found) newXMAS++;
                        break;

                    case Direction.UP_LEFT:
                        found = true;


                        for (int c = 0; c < pattern.Length; c++)
                        {
                            if (i - c >= 0 && j - c >= 0)
                            {
                                if (wordsearch[i - c][j - c] != pattern[c])
                                {
                                    found = false;

                                }
                            }
                            else
                            {
                                found = false;

                            }

                        }
                        if (found) newXMAS++;

                        break;
                    case Direction.UP:

                        found = true;

                        for (int c = 0; c < pattern.Length; c++)
                        {
                            if (i - c >= 0)
                            {
                                if (wordsearch[i - c][j] != pattern[c])
                                {
                                    found = false;

                                }
                            }
                            else
                            {
                                found = false;

                            }

                        }
                        if (found) newXMAS++;

                        break;

                    case Direction.UP_RIGHT:
                        found = true;


                        for (int c = 0; c < pattern.Length; c++)
                        {
                            if (i - c >= 0 && j + c < wordsearch[i].Length)
                            {
                                if (wordsearch[i - c][j + c] != pattern[c])
                                {
                                    found = false;

                                }
                            }
                            else
                            {
                                found = false;

                            }

                        }
                        if (found) newXMAS++;
                        break;

                }
            }

            return newXMAS;

        }
    }

    public enum Direction
    {
        NOT_FOUND,
        LEFT,
        RIGHT,
        UP,
        DOWN,
        DOWN_LEFT,
        UP_LEFT,
        DOWN_RIGHT,
        UP_RIGHT

    }
}