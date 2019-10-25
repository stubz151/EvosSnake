using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoSnake
{
    //Enums are cool classes that contain information on certain variable names, so Direction.Up =0 this is useful as it gives us a way to easily check values.
    enum Direction
    {
        Up, Right, Down, Left
    }
    enum Box
    {
        Empty = 0,
        Wall = 5,
        SnakeBody = 1,
        Food = 2,

    }
    class SnakeGame
    {
        int score { get; set; }
        public Direction curDirection { get; set; }
        public Boolean gameOver { get; set; } = false;
        int headPosX { get; set; }
        int headPosY { get; set; }
        int gridWidth { get; set; }
        int gridHeight { get; set; }
        private List<int[]> snakeBody = new List<int[]>();
        //Please note its row, columns, so its [y,x] not [x,y]
        public Box[,] board;
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

            for (int i = 0; i < 3 ; i++)
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

            for (int i = 0; i < snakeBody.Count; i++)
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

        public void genNextFood()
        {

            while (true)
            {
                int x = rnd.Next(gridWidth);
                int y = rnd.Next(gridHeight);
                if (board[y, x] == Box.Empty)
                {
                    board[y, x] = Box.Food;
                    return;
                }
            }
        }
        public Box[,] getBoxAroundHead()
        {

            return board;
        }
        public void MakeMove(Direction move)
        {
            int[] headPos = snakeBody[0];
            headPosY = headPos[0];
            headPosX = headPos[1];
            int[] tailPos = snakeBody[snakeBody.Count - 1];
            curDirection = move;
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
                            genNextFood();
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
                            genNextFood();
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
                        board[tailPosY,tailPosX] = Box.Empty;
                            snakeBody.RemoveAt(snakeBody.Count - 1);
                            break;

                        case 1:
                            genNextFood();
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
                            genNextFood();
                            score++;
                            break;
                        case 2:
                            break;

                    }
            }
            
            updateBoard();
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
                int[] checkPos = snakeBody[i];
                int checkPosY = checkPos[0];
                int checkPosX = checkPos[1];
                
                if (x==checkPosX)
                {
                    if (y==checkPosY)
                    {
                     //   gameOver = true;
                        return 2;
                    }                 
                }
            }
            return -1;
        }



    }
}

