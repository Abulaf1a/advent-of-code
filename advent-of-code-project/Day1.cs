

namespace advent_of_code_project
{
    public class Day1()
    {

        public static void GetDay1()
        {
            //Import file
            StreamReader streamReader = new("C:\\Users\\peter\\Documents\\advent-of-code\\advent-of-code-project\\resources\\Day1Input.txt");

            List<int> firstList = [];
            List<int> secondList = [];

            int linePtr = 0;
            while (linePtr < 1000)
            {
                String[] line = streamReader.ReadLine().Split("   ");
                firstList.Add(int.Parse(line[0]));
                secondList.Add(int.Parse(line[1]));
                linePtr++;
            }


            //Calc similarity index
            int similarityIdx = 0;

            for (int i = 0; i < firstList.Count; i++)
            {
                int pos = firstList[i];

                int secondCount = 0;

                for (int j = 0; j < secondList.Count; j++)
                {

                    if (secondList[j] == pos)
                    {
                        secondCount++;
                    }

                }
                pos *= secondCount;
                similarityIdx += pos;
            }

            Console.WriteLine("similarityIdx = " + similarityIdx);

            int totalDist = 0;

            while (firstList.Count > 0)
            {
                //are there repeats? 
                int lastPos = int.Max(firstList.IndexOf(firstList.Min()), secondList.IndexOf(secondList.Min()));
                int firstPos = int.Min(firstList.IndexOf(firstList.Min()), secondList.IndexOf(secondList.Min()));

                if (firstList.Min() > secondList.Min())
                {
                    totalDist += firstList.Min() - secondList.Min();
                }
                else
                {
                    totalDist += secondList.Min() - firstList.Min();
                }

                firstList.Remove(firstList.Min());
                secondList.Remove(secondList.Min());
            }

            Console.WriteLine("the answer is : " + totalDist);
        }
    }
}