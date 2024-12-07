using System.Collections;
using System.Runtime.CompilerServices;

namespace advent_of_code_project{

    //Day 6 part one
    public class GuardGallivant{

        public static void GetGallivantingGuard(){

            Lab lab = GetInputData();

            lab.ToString(); 
            
            bool exitedArea = lab.Move();

            while(!exitedArea){
                exitedArea = lab.Move();
            }

            lab.ToString(); 

            Console.WriteLine("positions gallivanted = " + lab.positionsGallivanted); 
            
        }

        public static Lab GetInputData(){

             StreamReader streamReader = new StreamReader(ResourceLocator.address + "Day6Input.txt");

            int xPos = 0;
            int yPos = 0;
            List<List<char>> lab = new List<List<char>>();
            int count = 0;
            //NOTE 10 magic number of lines in file!
            while(count < 130){
                String line = streamReader.ReadLine();

                List<char> charList = line.ToList(); 

                lab.Add(new List<char>()); 

                for(int i = 0; i < charList.Count; i ++ ){
                    lab[count].Add(charList[i]);  

                    if(charList[i] == '^'){
                        xPos = i;
                        yPos = count; 
                    }
                }
                count++;        
            }
            Guard guard = new Guard(xPos, yPos); 
            return new Lab(lab, guard);  
        }
    }

    public class Lab{ 
        public int positionsGallivanted = 0; 
        private List<List<char>> layout;
        private Guard guard; 
        public Lab(List<List<char>> layout, Guard guard){
            this.layout = layout;
            this.guard = guard; 
        }

        //returns false when leaving area
        public bool Move(){
            int nextXPos = guard.GetNextX();
            int nextYPos = guard.GetNextY();
            //x = along the rows horizontally
            //y = up and down 
            //so y is first dimension, x is second dimension

            try{
                if(layout[nextYPos][nextXPos] != '#'){ // 
                guard.Move(); //move the guard, sets the guard's x and y to next x and y
                if(layout[guard.GetY()][guard.GetX()] != 'X'){
                    positionsGallivanted++; //this stops double counting of Xs that have been crossed multiple times. 
                }
                layout[guard.GetY()][guard.GetX()] = 'X'; //set current pos to a 'seen' pos
                //layout[guard.GetNextY()][guard.GetNextX()] = '@';
            }
            else{
                guard.RotateRight(); 
                //layout[guard.GetNextY()][guard.GetNextX()] = '@';
            }
            //I don't think I need to set the guard's current position to a < > or ^ ?
            //print out lab each time! 
            //this.ToString(); 

            Console.WriteLine(); 
            return false; 

            }
            catch{
                Console.WriteLine("positions Gallivanted = " + positionsGallivanted); 
                return true; //outside the area (index out of range!)
            }
        
        }

        public List<List<char>> GetLayout(){
            return layout;
        }

        public void SetLayout(List<List<char>> layout){
            this.layout = layout;
        }

        public void SetGuard(Guard guard){
            this.guard = guard; 
        }
        public Guard GetGuard(){
            return guard; 
        }
        
        public void ToString(){
            List<List<char>> layout = this.GetLayout();
            for(int i = 0; i < layout.Count; i++){
                for(int j = 0; j< layout[i].Count; j++){
                    Console.Write(layout[i][j]);
                }
                Console.Write("\n");
            }
        }
    }

    public class Guard
    {
        private Cardinal cardinal; 
        private int xPos;
        private int yPos;
        private int nextXPos;
        private int nextYPos;

        public Guard(int xPos, int yPos){

            this.xPos = xPos;
            this.yPos = yPos;

            this.nextXPos = xPos; 
            this.nextYPos = yPos--; //this would produce a BIG BUUG if the guard was facing a #?
            //or deal with checking and rotating somewhere else?  
            cardinal = Cardinal.NORTH; //the guard is always facing up to star
        }
      

        public void RotateRight(){ //also have to reset pos
            //reset next pos to remove additional move. 
            nextXPos = xPos;
            nextYPos = yPos;

            switch(cardinal){
                case Cardinal.NORTH:
                    cardinal = Cardinal.EAST;
                    nextXPos++; 
                break; 
                case Cardinal.EAST:
                    cardinal = Cardinal.SOUTH;
                    nextYPos++;
                break;
                case Cardinal.SOUTH:
                    cardinal = Cardinal.WEST;
                    nextXPos--;
                break;
                case Cardinal.WEST:
                    cardinal = Cardinal.NORTH;
                    nextYPos--;
                break;
            }
        }

        public void Move(){
            switch(cardinal){
                case Cardinal.NORTH:
                    yPos = nextYPos;
                    nextYPos --;
                break;
                case Cardinal.EAST:
                    xPos = nextXPos;
                    nextXPos ++;
                break;
                case Cardinal.SOUTH:
                    yPos = nextYPos;
                    nextYPos ++;
                break;
                case Cardinal.WEST:
                    xPos = nextXPos;
                    nextXPos --; 
                break; 
            }
        }

        public void SetX(int x){
            xPos = x;
        }

        public int GetX(){
            return xPos;
        }

        public void SetY(int y){
            yPos = y;
        }

        public int GetY(){
            return yPos;
        }

        public int GetNextX(){
            return nextXPos;
        }

        public int GetNextY(){
            return nextYPos;
        }
    }

    public enum Cardinal{
        NORTH,
        SOUTH,
        EAST,
        WEST
    }
}