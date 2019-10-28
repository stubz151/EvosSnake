using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace EvoSnake
{
    //Enums are cool classes that contain information on certain variable names, so Direction.Up =0 this is useful as it gives us a way to easily check values.
    public enum Direction
    {
        Up,Down,Left,Right
    }
    public enum moves
    {
        Forward,Left,Right
    }
   public enum Box
    {
        Empty = 0,
        Wall = 1,
        SnakeBody = 2,
        Food = 3, 
        SnakeHead = 4
    }
    class SnakeGame :ICloneable
    {
        public int[] foodLocation { get; set; } = new int[2];
        public int[] posForward { get; set; } = new int[2];
        public int[] posLeft { get; set; } = new int[2];
        public int[] posRight { get; set; } = new int[2];
        public int score { get; set; }
        public Direction curDirection { get; set; }
        public Boolean gameOver { get; set; } = false;
        int[] checkPos { get; set; }
        int headPosX { get; set; }
        int headPosY { get; set; }
        int gridWidth { get; set; }
        int gridHeight { get; set; }
        public List<int[]> snakeBody { get; set; } = new List<int[]>();
        //Please note its row, columns, so its [y,x] not [x,y]
        public Box[,] board { get; set; }
        Random rnd = new Random();
        public SnakeGame(int gridWidth, int gridHeight)
        {
            this.score = 0;
            this.gridWidth = gridWidth;
            this.gridHeight = gridHeight;
            this.board = new Box[gridHeight, gridWidth];
            buildGrid();
            genSnake();
            genNextFood();
        }
        public SnakeGame(int s, int gridW, int gridH, Box[,] b, int[] fl, List<int[]> sb , Direction tempDirection)
        {
            score = s;
            gridWidth = gridW;
            gridHeight = gridH;
            snakeBody = sb;
            curDirection = tempDirection;
            board = b;
            foodLocation = fl;
            updateBoard();
        }
        public SnakeGame(SnakeGame sg)
        {
            score = sg.score;
            gridWidth = sg.gridWidth;
            gridHeight = sg.gridHeight;
            snakeBody = sg.snakeBody;
            board = (Box[,])sg.board.Clone();
            foodLocation = (int[])sg.foodLocation.Clone();
            updateBoard();
        }
        //use this to get inputs
        public double[] getInputs()
        {
            double[] inputs = new double[6];
            inputs[0] = (double)board[posForward[0] , posForward[1]];
            inputs[1] = (double)board[posLeft[0], posRight[1]];
            inputs[2] = (double)board[posRight[0], posRight[1]];
            inputs[3] = resultOfMove(moves.Forward);
            inputs[4] = resultOfMove(moves.Left);
            inputs[5] = resultOfMove(moves.Right);
            return inputs;
        }

        public double resultOfMove(moves move)
        {         
            int distanceBefore = distanceToFood();
            int scoreBefore = score;
            moves closestMove = moves.Forward;           
            SnakeGame temp = (SnakeGame)this.Clone();
            temp.moveHead(move);
            int scoreAfter = temp.score;
            if (temp.gameOver==true)
            {
                return -1.00;
            }
            if (scoreAfter> scoreBefore)
            {
                return 5.00;
            }
            if (temp.distanceToFood() < distanceBefore)
            {
                return 1.00;
            }
            
            return 0;                    
        }

        public void setAreaAroundHead()
        {
            switch (curDirection)
            {
                case Direction.Up:
                    posForward[0] = headPosY - 1;
                    posForward[1] = headPosX;
                    posLeft[0] = headPosY;
                    posLeft[1] = headPosX - 1;
                    posRight[0] = headPosY;
                    posRight[1] = headPosX + 1;
                    break;
                case Direction.Down:
                    posForward[0] = headPosY + 1;
                    posForward[1] = headPosX;
                    posLeft[0] = headPosY;
                    posLeft[1] = headPosX + 1;
                    posRight[0] = headPosY;
                    posRight[1] = headPosX - 1;
                   break;
                case Direction.Right:
                    posForward[0] = headPosY;
                    posForward[1] = headPosX + 1;
                    posLeft[0] = headPosY - 1;
                    posLeft[1] = headPosX;
                    posRight[0] = headPosY + 1;
                    posRight[1] = headPosX;
                    break;
                case Direction.Left:
                    posForward[0] = headPosY;
                    posForward[1] = headPosX - 1;
                    posLeft[0] = headPosY + 1;
                    posLeft[1] = headPosX;
                    posRight[0] = headPosY - 1;
                    posRight[1] = headPosX;
                    break;
            }
        }

        //Call this method with the appropraite move and it will do all the heavy lifting :)
        public void moveHead(moves move)
        {
            int[] headPos = snakeBody[0];
            headPosY = headPos[0];
            headPosX = headPos[1];
            int[] tailPos = snakeBody[snakeBody.Count - 1];
            Direction newDirection = curDirection;
            switch(curDirection)
            {
                case Direction.Up:
                    
                    if (move == moves.Forward)
                    {
                        newDirection = curDirection;
                    }
                    if (move == moves.Right)
                    {
                        newDirection = Direction.Right;
                    }
                    if (move == moves.Left)
                    {
                        newDirection = Direction.Left;
                    }
                    break;
                case Direction.Down:
                   
                    if (move == moves.Forward)
                    {
                        newDirection = Direction.Down;
                    }
                    if (move == moves.Right)
                    {
                        newDirection = Direction.Left;
                    }
                    if (move == moves.Left)
                    {
                        newDirection = Direction.Right;
                    }
                    break;
                case Direction.Right:
                    

                    if (move == moves.Forward)
                    {
                        newDirection = curDirection;
                    }
                    if (move == moves.Right)
                    {
                        newDirection = Direction.Down;
                    }
                    if (move == moves.Left)
                    {
                        newDirection = Direction.Up;
                    }
                    break;
                case Direction.Left:
                    
                    if (move == moves.Forward)
                    {
                        newDirection = curDirection;
                    }
                    if (move == moves.Right)
                    {
                        newDirection = Direction.Up;
                    }
                    if (move == moves.Left)
                    {
                        newDirection = Direction.Down;
                    }
                    break;                
            }
            MakeMove(newDirection);
            setAreaAroundHead();
                    
        }
        public void MakeMove(Direction move)
        {
            int[] headPos = snakeBody[0];
            headPosY = headPos[0];
            headPosX = headPos[1];
            int[] tailPos = snakeBody[snakeBody.Count - 1];
            
            curDirection = move;
            Boolean eated = false;
            if (move == Direction.Up)
            {
                int newHeadPosY = headPosY - 1;
                int[] newHead = new int[2];
                newHead[0] = newHeadPosY;
                newHead[1] = headPosX;
                snakeBody.Insert(0, newHead);
                switch (checkCollision())
                {
                    case 0:
                        int[] tailpos = snakeBody[snakeBody.Count - 1];
                        int tailPosY = tailpos[0];
                        int tailPosX = tailpos[1];
                        board[tailPosY, tailPosX] = Box.Empty;
                        snakeBody.RemoveAt(snakeBody.Count - 1);
                        break;

                    case 1:
                        eated = true;
                        score++;
                        break;
                    case 2:
                        break;
                }
            }
            if (move == Direction.Down)
            {
                int newHeadPosY = headPosY + 1;
                int[] newHead = new int[2];
                newHead[0] = newHeadPosY;
                newHead[1] = headPosX;
                snakeBody.Insert(0, newHead);
                switch (checkCollision())
                {
                    case 0:
                        int[] tailpos = snakeBody[snakeBody.Count - 1];
                        int tailPosY = tailpos[0];
                        int tailPosX = tailpos[1];
                        board[tailPosY, tailPosX] = Box.Empty;
                        snakeBody.RemoveAt(snakeBody.Count - 1);
                        break;
                    case 1:
                        eated = true;
                        score++;
                        break;
                    case 2:
                        break;

                }
            }
            if (move == Direction.Left)
            {
                int newHeadPosX = headPosX - 1;
                int[] newHead = new int[2];
                newHead[0] = headPosY;
                newHead[1] = newHeadPosX;
                snakeBody.Insert(0, newHead);
                switch (checkCollision())
                {
                    case 0:
                        int[] tailpos = snakeBody[snakeBody.Count - 1];
                        int tailPosY = tailpos[0];
                        int tailPosX = tailpos[1];
                        board[tailPosY, tailPosX] = Box.Empty;
                        snakeBody.RemoveAt(snakeBody.Count - 1);
                        break;

                    case 1:
                        eated = true;
                        score++;
                        break;
                    case 2:
                        break;

                }
            }
            if (move == Direction.Right)
            {
                int newHeadPosX = headPosX + 1;
                int[] newHead = new int[2];
                newHead[0] = headPosY;
                newHead[1] = newHeadPosX;
                snakeBody.Insert(0, newHead);
                switch (checkCollision())
                {
                    case 0:
                        int[] tailpos = snakeBody[snakeBody.Count - 1];
                        int tailPosY = tailpos[0];
                        int tailPosX = tailpos[1];
                        board[tailPosY, tailPosX] = Box.Empty;
                        snakeBody.RemoveAt(snakeBody.Count - 1);
                        break;

                    case 1:
                        eated = true;
                        score++;
                        break;
                    case 2:
                        break;

                }
            }

            updateBoard();
            if (eated)
            {
                genNextFood();
            }
        }


        public void buildGrid()
        {
            for (int i = 0; i < gridHeight; i++)
            {
                for (int j = 0; j < gridWidth; j++)
                {

                    if (i == 0 || j == 0 || i == gridHeight - 1 || j == gridWidth - 1)
                    {
                        board[i, j] = Box.Wall;
                    }
                    else
                    {
                        board[i, j] = Box.Empty;
                    }

                }
            }
        }
        public void genSnake()
        {
            int x = gridWidth / 2;
            curDirection = Direction.Up;
            int y = gridHeight / 2;
            int[] pos = new int[2];
            pos[0] = y;
            pos[1] = x;
            snakeBody.Add(pos);

            for (int i = 0; i < 4; i++)
            {
                int[] pos2 = new int[2];
                y--;
                pos2[0] = y;
                pos2[1] = x;
                snakeBody.Add(pos2);
            }
            updateBoard();
        }
        public void updateBoard()
        {
            int[] headpos = snakeBody[0];
            board[headpos[0], headpos[1]] = Box.SnakeHead;
            for (int i = 1; i < snakeBody.Count; i++)
            {
                int[] pos = snakeBody[i];
                board[pos[0], pos[1]] = Box.SnakeBody;
            }
        }
        
        public void DisplayBoard()
        {
            for (int i = 0; i < gridHeight; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < gridWidth; j++)
                {
                    Box b = board[i, j];
                    int ele = (int)b;
                    Console.Write(ele);
                }
            }

            Console.WriteLine();
            Console.WriteLine("The score is : " + score);
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
        }
        /*
        public Box[,] getInputs()
        {
            Box[]
        }
        */
        //This method returns the area around the head,
        public double[] outputBox()
        {
            double[] outBox = new double[6];
            int[] headPos = snakeBody[0];
            headPosY = headPos[0];
            headPosX = headPos[1];
            //inputs
            double eleUp = (double)board[headPosY--, headPosX];
            double eleDown = (double)board[headPosY++, headPosX];
            double eleLeft = (double)board[headPosY, headPosX--];
            double eleRight = (double)board[headPosY, headPosX++];
            double distX = distanceToFoodX();
            double distY = distanceToFoodY();

            outBox[0] = eleUp;
            outBox[1] = eleDown;
            outBox[2] = eleLeft;
            outBox[3] = eleRight;
            outBox[4] = distX;
            outBox[5] = distY;

            return outBox;
        }
        public int distanceToFood()
        {
            int[] headPos = snakeBody[0];
            headPosY = headPos[0];
            headPosX = headPos[1];
            int distance = 0;
            distance += Math.Abs(headPosY - foodLocation[0]);
            distance += Math.Abs(headPosX - foodLocation[1]);
            return distance;

        }

        public int distanceToFoodY()
        {
            int[] headPos = snakeBody[0];
            headPosY = headPos[0];           
            int yDist = headPosY - foodLocation[0];        
            return yDist;
        }
        public int distanceToFoodX()
        {
            int[] headPos = snakeBody[0];
            headPosX = headPos[1];
            int xDist = headPosX - foodLocation[1];
            return xDist;
        }
           

        public void genNextFood()
        {

            while (true)
            {
                int x = rnd.Next(gridWidth);
                int y = rnd.Next(gridHeight);
                if (board[y, x] == Box.Empty)
                {
                    board[y, x] = Box.Food;
                    foodLocation[0] = y;
                    foodLocation[1] = x;
                    return;
                }
            }
        }
        public Box[,] getBoxAroundHead()
        {
            
            return board;
        }
        


        public int checkCollision()
        {

            int[] pos = snakeBody[0];
            int y = pos[0];
            int x = pos[1];

            if (board[y, x] == Box.Empty)
            {
                return 0;
            }
            if (board[y, x] == Box.Food)
            {
                board[y, x] = Box.Empty;
               
                return 1;
            }
           
            if (board[y, x] == Box.Wall)
            {
                gameOver = true;
                return 2;
            }
            for (int i = 1; i< snakeBody.Count-1;i++)
            {              
                checkPos = snakeBody[i];
                int checkPosY = checkPos[0];
                int checkPosX = checkPos[1];
                
                if (x==checkPosX)
                {
                    if (y==checkPosY)
                    {                      
                        gameOver = true;
                        return 2;
                    }                 
                }
            }
            return -1;
        }
       /*
        public SnakeGame ShallowClone()
        {
            return (SnakeGame)this.MemberwiseClone();
        }
        */
        public SnakeGame DeepCopy()
        {
            
            SnakeGame other = (SnakeGame)this.MemberwiseClone();
            other.score = score;
            other.gridWidth = gridWidth;
            other.gridHeight = gridHeight;
            
            List<int[]> newList = new List<int[]>(snakeBody.Count);
            foreach (int[] item in snakeBody)
                newList.Add((int[])item.Clone());
            other.snakeBody = newList;

            other.board = (Box[,])board.Clone();
            other.foodLocation = (int[])foodLocation.Clone();
            
            return other;
        }
        
        public Object Clone()
        {
            int Tempscore = score;
            int tempWidth = gridWidth;
            int tempHeight = gridHeight;
            Box[,] tempBoard = (Box[,])board.Clone();
            int[] foodpos = (int[])foodLocation.Clone();
            List<int[]> sb = snakeBody;
            List<int[]> newList = new List<int[]>(snakeBody.Count);
            foreach (int[] item in snakeBody)
            {
                newList.Add((int[])item.Clone());
            }
             
            Direction tempDirection = curDirection;
        
            return new SnakeGame(Tempscore, tempWidth, tempHeight, tempBoard, foodpos, newList, tempDirection);
        }
        
    }
}

