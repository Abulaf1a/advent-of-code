using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code_project
{
    internal class Day5
    {
        public static void Day5Method()
        {
            StreamReader streamReader = new(ResourceLocator.address + "day5input.txt");
            
            //get the constraints -- 21 is the lines in the TEST 
            Dictionary<int,List<int>> constraints = GetConstraints(streamReader, 1176); 

            List<List<int>> manualUpdateNums = GetManualUpdateNumbers(streamReader, 1176, 1377);

            int total = 0; 

            for(int i = 0; i < manualUpdateNums.Count; i++){
                List<int> currentRow = manualUpdateNums[i];
                bool inOrder = true; 

                currentRow.ForEach(s => Console.Write(s + ","));
                Console.Write("\n");

                for(int j = 0; j < currentRow.Count; j++){
                    if(constraints.ContainsKey(currentRow[j])){
                        List<int> valuesToCheck = constraints[currentRow[j]];

                        for(int k = 0; k < valuesToCheck.Count; k ++){
                            if(currentRow.Contains(valuesToCheck[k]) && currentRow.IndexOf(valuesToCheck[k]) < j){
                                //the sequence is not in order 
                                inOrder = false;  
                            }
                        }
                    }
                }
                if(inOrder){
                    if(currentRow.Count > 0){
                        int middle = currentRow[currentRow.Count/2];

                        total += middle; 
                    }
                }
            }

            Console.WriteLine("The total for day 5 part 1 is : " + total); 
           
        }

        public static Dictionary<int,List<int>> GetConstraints(StreamReader streamReader, int lines){

            Dictionary<int, List<int>> constraints = new Dictionary<int, List<int>>();

             int lineCount = 0; 
            //make dictionary from input text
            while (lineCount < lines)
            {
                string line = streamReader.ReadLine();

                string[] intStrs  = line.Trim().Split("|");

                int[] ints = Array.ConvertAll(intStrs, s => 
                int.Parse(s)
                );          

                if (constraints.TryGetValue(ints[0], out List<int>? existingValues)) //cool generated syntax look at that optional! (or nullable?)
                {
                    existingValues.Add(ints[1]);
                    constraints[ints[0]] = existingValues;
                }
                else
                {
                    constraints.Add(ints[0], [ints[1]]);
                }

                lineCount++; 
            }

            foreach(KeyValuePair<int,List<int>> entry in constraints){
                Console.Write(entry.Key + ":");
                entry.Value.ForEach(s => Console.Write(s + ", ")); 
                Console.Write(Environment.NewLine); 
            }

            return constraints; 
        }
    
        public static List<List<int>> GetManualUpdateNumbers(StreamReader streamReader, int start, int end){

            int count = start;
            List<List<int>> manualUpdateNums = []; 
            while(count < end){
                string? line = streamReader.ReadLine();
                int i = 0; 
                if(line != null){
                    List<int> updateNums = (from s in line?
                .Split(",") where int
                .TryParse(s, out i) select i)
                .ToList(); 
                
                updateNums.ForEach(Console.WriteLine); 
                manualUpdateNums.Add(updateNums); 
                }
                count++; 
            }
            return manualUpdateNums; 
        }
    }
}
