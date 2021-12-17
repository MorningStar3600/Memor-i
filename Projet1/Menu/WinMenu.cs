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
            
            Score.Save();
            
            ArrayList list = new ArrayList();
            list.Add(new UpdatedText("Scores partie :", width / 3, height / 3 - game.GetNumbPlayers() / 2 - 2));
            for (int i = 0; i < game.GetNumbPlayers(); i++)
            {
                string value = game._players[i].GetName() + ":" + Score.GetScore(game.IdGame, game._players[i]);
                list.Add(new UpdatedText(value, width/3, height/3 - game.GetNumbPlayers()/2+i*2));
            }

            
            list.Add(new UpdatedText("Meilleur joueur de ce jeu : ", 2*width / 3, height / 3 - 2));
            string bestPlayer = game.GetBestScoreFromGame(game.IdGame).Split('\\')[0] + " : " + game.GetBestScoreFromGame(game.IdGame).Split('\\')[1];
            list.Add(new UpdatedText(bestPlayer, 2 * width / 3, height / 3));

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