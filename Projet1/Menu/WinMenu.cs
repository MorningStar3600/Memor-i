using System;
using System.Collections;

namespace Projet1.Menu
{
    public static class WinMenu
    {
        private static int _width;
        private static int _height;
        public static void Start(int width, int height, Game game)
        {
            _width = width;
            _height = height;
            
            string[] cards = {"default","default","default","default","default","default","default","default","default","default","default","default","default","default","default","default","default","default","default","Menu/main"};
            string[] back = {"default","default","default","default","default","default","default","default","default","default","default","default","default","default","default","default","default","default","default","default"};
            Program.LoadCardManager(cards, back, 5, EventHandler, width, height, 0, true);
            
            game.SavePlayersToFile();
            
            ArrayList list = new ArrayList();
            list.Add(new UpdatedText("Scores partie :", width / 3, height / 3 - game.GetNumbPlayers() / 2 - 2));
            for (int i = 0; i < game.GetNumbPlayers(); i++)
            {
                string value = game._players[i].GetName() + ":" + game._players[i].GetScore();
                list.Add(new UpdatedText(value, width/3, height/2 - game.GetNumbPlayers()/2+i*2));
            }

            string[] scores = game.GetScoresFromGame(game.IdGame);
            list.Add(new UpdatedText("Scores globaux :", 2*width / 3, height / 3 - scores.Length / 2 - 2));
            for (int i = 0; i < scores.Length; i++)
            {
                Draws.toDraw.Add(new UpdatedText("Test?", 0, i));
                list.Add(new UpdatedText(scores[i], 2*width/3, height/2 - game.GetNumbPlayers()/2+i*2));
            }

            for (int i = 0; i < list.Count; i++)
            {
                Draws.toDraw.Add(list[i] as UpdatedText);
            }
            
            game.EndGame();

        }

        private static void EventHandler(CardManager cm, int cardId, int eventId, char key, int keyCode)
        {
            if (eventId == 1)
            {
                if (cardId == 19)
                {
                    MainMenu.Start(_width, _height);
                }
            }
        }
    }
}