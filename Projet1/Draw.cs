using System.Collections.Generic;
using System.Threading;

namespace Projet1
{
    class Draws
    {
        static public List<DrawItem> toDraw { get; set; } = new List<DrawItem>();
        static private readonly Thread _thread;

        static Draws()
        {
            _thread = new Thread(ReDraw);
        }
        static public void Run()
        {
            _thread.Start();
        }

        static public void Stop()
        {
            _thread.Abort();
        }

        static private void ReDraw()
        {
            while (true)
            {
                if (toDraw.Count > 0)
                {
                    for (int i = 0; i < toDraw.Count; i++)
                    {
                        toDraw[i].Draw();
                    }
                }

                //Thread.Sleep(1);
            }
        }
    }
}