

namespace advent_of_code_project
{


    class Day2
    {
        static readonly String address = ResourceLocator.address;
        public static void GetDay2()
        {
            StreamReader streamReader = new(address + "Day2Input.txt");

            List<List<int>> reactorOutputs = new List<List<int>>();

            int count = 0;
            //read input stream to create formatted array
            while (count < 1000)
            {
                String[] strings = streamReader.ReadLine().Split(" ");

                List<int> singleOutput = Array.ConvertAll(strings, int.Parse).ToList();
                reactorOutputs.Add(singleOutput);
                count++;
                //Checking it works, kept as another example of lambda expressions in C# 
                Console.Write(count + ": ");
                singleOutput.ForEach(s => Console.Write(s + " "));
                Console.Write("\n");
            }

            Console.WriteLine(reactorOutputs.Count);
            //So, a report only counts as safe if both of the following are true:
            // - The levels are either all increasing or all decreasing.
            // - Any two adjacent levels differ by at least one and at most three.
            //iterate through array and count safe 
            int safeCount = 0;
            bool safe;
            foreach (List<int> output in reactorOutputs)
            {
                safe = checkSafe(output, -1);
                if (safe)
                {
                    safeCount++;
                }
            }

            Console.WriteLine(safeCount);
        }



        public static bool checkSafe(List<int> output, int pos)
        {
            List<int> subOutput = new List<int>(output);

            if (pos > -1) subOutput.RemoveAt(pos); 

            bool safe = true;
            for (int i = 0; i < subOutput.Count - 1; i++)
            {
                int x = subOutput[i];
                int y = subOutput[i + 1];

                if (subOutput[1] > subOutput[2])
                {
                    if (x - y > 3 || x <= y)
                    {
                        //I had my recursive method call in an if statement here last night!!
                        safe = false;
                    }
                }

                else
                {
                    if (y - x > 3 || y <= x)
                    {
                        safe = false;
                    }
                }
            }

            if (safe == false && pos < output.Count -1)
            {
                safe = checkSafe(output, pos + 1);
            }

            return safe;
        }
    }
}