using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicTacToe;
using System.Diagnostics;
namespace GameTest
{
    [TestClass]
    public class TicTacToeTest
    {

        [TestMethod]
        public void GameCreated()
        {
            Game game = new Game();
            Assert.IsNull(game.Winner);
        }

        [TestMethod]
        public void PrintBoard()
        {
            Game game = new Game();
            string output = game.ToString();
            Assert.AreEqual(output,"___\n___\n___\n");
            Debug.WriteLine(output);
        }

        [TestMethod]
        public void GameInProgress()
        {
            Game game = new Game();
            Assert.IsFalse(game.IsGameInProgress);
            game.MakeMark(Player.Player1, 0, 0);
            Assert.IsTrue(game.IsGameInProgress);
        }

        [TestMethod]
        [ExpectedException(typeof(MarkedOffBoardException))]
        public void MarkOnlyOnBoard()
        {
            Game game = new Game();
            game.MakeMark(Player.Player1, 0, 9);
        }

        [TestMethod]
        [ExpectedException(typeof(OutOfTurnException))]
        public void TakeTurns()
        {
            Game game = new Game();
            game.MakeMark(Player.Player1, 0, 0);
            game.MakeMark(Player.Player1, 1, 0);
        }


        [TestMethod]
        [ExpectedException(typeof(AlreadyMarkedException))]
        public void OnlyMarkOnce()
        {
            Game game = new Game();
            game.MakeMark(Player.Player1, 0, 0);
            game.MakeMark(Player.Player2, 0, 0);
        }

        [TestMethod]
        public void WinnerExists()
        {
            Game game = new Game();
            game.MakeMark(Player.Player1, 0, 0);
            game.MakeMark(Player.Player2, 1, 0);
            game.MakeMark(Player.Player1, 0, 1);
            game.MakeMark(Player.Player2, 1, 1);
            game.MakeMark(Player.Player1, 0, 2);
            Assert.AreEqual(game.Winner, Player.Player1);
        }

        [TestMethod]
        [ExpectedException(typeof(GameOverException))]
        public void NoMarksAfterGameOver()
        {
            Game game = new Game();
            game.MakeMark(Player.Player1, 0, 0);
            game.MakeMark(Player.Player2, 1, 0);
            game.MakeMark(Player.Player1, 0, 1);
            game.MakeMark(Player.Player2, 1, 1);
            game.MakeMark(Player.Player1, 0, 2);
            game.MakeMark(Player.Player2, 1, 2);
        }
    }
}
