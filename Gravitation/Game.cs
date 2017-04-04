using System;
using System.IO;
using System.Threading;

namespace Gravitation
{
    class Game
    {
        string[] level;
        int s_level = 1;

        char player = '☺';
        char block = '█';
        char spike = '▒';
        char point = '░';
        char empty = ' ';

        char[,] map = new char[15, 35];
        int maph = 15;
        int mapw = 35;

        int x, y;

        bool up = false;
        bool color = true;

        string tmp = null;

        bool playerM(char z, int a = 0, int b = 0) { if (map[y + a, x + b] == z) return true; else return false; }

        public void menu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Gravitation by PolskiSwir345");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White; 
            Console.WriteLine("1 - Play");
            Console.WriteLine("2 - Quit");

            var c = new ConsoleKeyInfo();
            c = Console.ReadKey();
            if (c.Key == ConsoleKey.D1) load();
            else if (c.Key == ConsoleKey.D2) Environment.Exit(0);
            else menu();
        }

        void load(string t = "/", bool files = true)
        {
            if (files)
            {
                Console.CursorVisible = false; 
                level = Directory.GetFiles(@"maps\", "*.txt"); 
                files = false;
            }
            if (t == "next")
            {
                s_level++;
                if (s_level > level.Length - 1)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Good job! :D");
                    Thread.Sleep(3000);
                    Environment.Exit(0);
                }
            }

            up = false;
            string[] text = File.ReadAllLines("maps/level" + s_level + ".txt");
            x = 2;
            y = 5;
            for (int i = 0; i <= text.Length - 1; i++)
            {
                for (int j = 0; j <= text[i].Length - 1; j++)
                {
                    map[i, j] = Convert.ToChar(text[i][j]);
                } 
            }
             
            Console.Clear();
            Player();
        }

        void Player()
        { 
             while (true)
             { 
                 show(); 
                 Thread.Sleep(30);  
                 if (Console.KeyAvailable)
                 {
                     collision();
                     var c = new ConsoleKeyInfo();
                     c = Console.ReadKey();
                     if (c.Key == ConsoleKey.Escape) Environment.Exit(0);
                     else if (c.KeyChar == 'a' || c.KeyChar == 'A')
                     {
                         if (playerM(empty, 0, -1))
                         {
                             map[y, x] = empty;
                             x--;
                             map[y, x] = player;
                         }
                         else if (playerM(spike, 0, -1)) load("restart");
                         else if (playerM(point, 0, -1)) load("next");
                     }
                     else if (c.KeyChar == 'd' || c.KeyChar == 'D')
                     {
                         if (playerM(empty, 0, 1))
                         {
                             map[y, x] = empty;
                             x++;
                             map[y, x] = player; 
                         }
                         else if (playerM(spike, 0, 1)) load("restart");
                         else if (playerM(point, 0, 1)) load("next");
                     }
                     else if (c.Key == ConsoleKey.Spacebar) up = !up;
                     else if (c.KeyChar == 'c' || c.KeyChar == 'C') color = !color; 
                 }
                 else
                 {
                     collision();
                 }
             } 
        }

        void collision()
        {
            if (up)
            {
                if (playerM(empty, -1))
                {
                    map[y, x] = empty;
                    y--;
                    map[y, x] = player;
                }
                else if (playerM(spike, -1)) load("restart");
                else if (playerM(point, -1)) load("next");
            }
            else
            {
                if (playerM(empty, 1))
                {
                    map[y, x] = empty;
                    y++;
                    map[y, x] = player;
                }
                else if (playerM(spike, 1)) load("restart");
                else if (playerM(point, 1)) load("next");
            }
        }

        void show()
        {
            if (color)
            {
                Console.SetCursorPosition(0, 0);
                for (int i = 0; i <= maph - 1; i++)
                {
                    for (int j = 0; j <= mapw - 1; j++)
                    {
                        if (map[i, j] == block) Console.ForegroundColor = ConsoleColor.White;
                        if (map[i, j] == spike) Console.ForegroundColor = ConsoleColor.Red;
                        if (map[i, j] == player) Console.ForegroundColor = ConsoleColor.Yellow;
                        if (map[i, j] == point) Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(map[i, j]);
                    }
                    Console.WriteLine();
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("maps/level" + s_level + ".txt | Up = " + Convert.ToInt32(up)); 

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(0, 0);
                tmp = null;
                for (int i = 0; i <= maph - 1; i++)
                {
                    for (int j = 0; j <= mapw - 2; j++)
                    {
                        tmp += map[i, j];
                    }
                    tmp += map[i, mapw - 1] + Environment.NewLine;
                }
                Console.Write(tmp);
                Console.WriteLine("maps/level" + s_level + ".txt | Up = " + Convert.ToInt32(up));
            }
        }
    }
}
