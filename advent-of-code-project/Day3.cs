using System.Text.RegularExpressions;

namespace advent_of_code_project
{

    public class Day3
    {

        String str = "(mul)\\([0-9]*,[0-9]*\\)";
        public static void GetDay3()
        {
            String pattern = "(mul)\\([0-9]*,[0-9]*\\)";

            StreamReader streamReader = new("C:\\Users\\peter\\Documents\\advent-of-code\\advent-of-code-project\\resources\\Day3Input.txt");

            MatchCollection matches = Regex.Matches(streamReader.ReadToEnd(), pattern);

            List<string> sums = matches.Cast<Match>().Select(match => match.Value).ToList(); //https://stackoverflow.com/questions/12730251/convert-result-of-matches-from-regex-into-list-of-string

            //Console.WriteLine(sums.Count);
            //sums.ForEach(s => Console.WriteLine(s));

            int total = 0;

            for (int i = 0; i < sums.Count; i++)
            {

                String numberPattern = "([0-9]+)";

                MatchCollection numMatches = Regex.Matches(sums[i], numberPattern);

                // List<string> nums = numMatches.Cast<Match>().Select(match => match.Value).ToList();

                // nums.ForEach(s => Console.WriteLine(s));
                int sum = int.Parse(numMatches[0].Value) * int.Parse(numMatches[1].Value);

                total += sum;
            }

            Console.WriteLine("Total for part 1: " + total);

        }


        public static void GetDay3Part2(){


            String pattern = "(mul)\\([0-9]*,[0-9]*\\)|((do)\\(\\))|((don't)\\(\\))";

            bool adding = true; 

            StreamReader streamReader = new("C:\\Users\\peter\\Documents\\advent-of-code\\advent-of-code-project\\resources\\Day3Input.txt");

            MatchCollection matches = Regex.Matches(streamReader.ReadToEnd(), pattern);

            List<string> instructions = matches.Cast<Match>().Select(match => match.Value).ToList(); //https://stackoverflow.com/questions/12730251/convert-result-of-matches-from-regex-into-list-of-string

            //Console.WriteLine(sums.Count);
            //instructions.ForEach(s => Console.WriteLine(s));

            int total = 0;

            for (int i = 0; i < instructions.Count; i++)
            {
                if(instructions[i] == "do()") adding = true; 
                else if(instructions[i] == "don't()") adding = false; 
                else{
                    if(adding == true){
                        String numberPattern = "([0-9]+)";

                        MatchCollection numMatches = Regex.Matches(instructions[i], numberPattern);

                        // nums.ForEach(s => Console.WriteLine(s));
                        int sum = int.Parse(numMatches[0].Value) * int.Parse(numMatches[1].Value);

                        total += sum;
                    }
                }

            }

            Console.WriteLine("Total for part 2: " + total); 

        }
    }



}