using System;

namespace TicTacToe
{
    public class Game
    {
        //Board is declared as a 2d array
        private Mark[,] Board = new Mark[3, 3];

        public int Turn { get; private set; } = 0;
        public Player? Winner { get; private set; }

        public bool IsGameInProgress
        {
            get
            {
                return Turn > 0 && !Winner.HasValue;
            }
        }

        public Game()
        {
            ClearBoard();
        }

        public void MakeMark(Player player, int x, int y)
        {
            //game must be playable
            if (Winner == null)
            {
                //must be on the board
                if (x < 3 && y < 3)
                {
                    Mark PreviousMark = Board[x, y];
                    //x's turn
                    if (Turn % 2 == 0)
                    {
                        if (player == Player.Player1)
                        {
                            if (PreviousMark == Mark._)
                            {
                                Board[x, y] = Mark.X;
                                if (CheckWin(Mark.X))
                                {
                                    Winner = Player.Player1;
                                }
                                Turn++;
                            }
                            else
                            {
                                throw new AlreadyMarkedException();
                            }
                        }
                        else
                        {
                            throw new OutOfTurnException();
                        }
                    }
                    else
                    {
                        if (player == Player.Player2)
                        {
                            if (PreviousMark == Mark._)
                            {
                                Board[x, y] = Mark.O;
                                if (CheckWin(Mark.O))
                                {
                                    Winner = Player.Player2;
                                }
                                Turn++;
                            }
                            else
                            {
                                throw new AlreadyMarkedException();
                            }
                        }
                        else
                        {
                            throw new OutOfTurnException();
                        }
                    }
                }
                else
                {
                    throw new MarkedOffBoardException();
                }
            }
            else
            {
                throw new GameOverException();
            }
        }

        public bool CheckWin(Mark m)
        {
            //Test for three way equality a little easier
            Func<Mark, Mark, Mark, bool> Equal = (x, y, z) =>
            {
                return (x == y && y == z) && (z == m);
            };

            //a b c
            //d e f
            //g h i
            Mark a = Board[0, 0], b = Board[0, 1], c = Board[0, 2];
            Mark d = Board[1, 0], e = Board[1, 1], f = Board[1, 2];
            Mark g = Board[2, 0], h = Board[2, 1], i = Board[2, 2];

            //giant or clause tests for all types of winning games
            return Equal(a, b, c) || Equal(d, e, f) || Equal(g, h, i) ||
                Equal(a, d, g) || Equal(b, e, h) || Equal(c, f, i) ||
                Equal(a, e, i) || Equal(c, e, g);
        }

        public override string ToString()
        {
            string con = "";
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    con += Board[x, y].ToString();
                }
                con += "\n";
            }
            return con;
        }

        public void ClearBoard()
        {
            for (int x = 0; x < 0; x++)
            {
                for (int y = 0; y < 0; y++)
                {
                    Board[x, y] = Mark._;
                }
            }
        }
    }

    public class OutOfTurnException : Exception { }

    public class AlreadyMarkedException : Exception { }

    public class MarkedOffBoardException : Exception { }

    public class GameOverException : Exception { }

    public enum Mark
    {
        _,
        X,
        O
    };

    public enum Player
    {
        Player1,
        Player2
    }
}



