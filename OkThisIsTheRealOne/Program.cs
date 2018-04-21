using System;
using System.Collections;
using System.Collections.Generic;

namespace CursorMovement
{
    class Binds
    {
        public static int xmin = 0, ymin = 0, xmax = 30, ymax = 15;
    }


    class Drawing
    {
        static bool[,] Marks = new bool[Binds.xmax + 1, Binds.ymax + 1];

        static int lineStartx, lineStarty;

        public static bool lineVert, lineFroth;

        // Main
        public static void DrawAll()
        {
            InputDraw();
            DrawScreen();
        }

        // Draws Screen
        static void DrawScreen()
        {
            Console.Clear();

            for (int xscan = 0; xscan < Binds.xmax + 1; xscan++)
            {
                for (int yscan = 0; yscan < Binds.ymax + 1; yscan++)
                {
                    if (Marks[xscan, yscan] == true)
                    {
                        WriteAt(xscan, yscan, "O");
                    }
                }
            }

            WriteAt(Cursor.x, Cursor.y, "X");
        }

        // Write a String
        static void WriteAt(int x, int y, string text)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(text);
        }

        // Draw If Input
        static void InputDraw()
        {
            if (MainClass.input == "Draw")
            {
                Marks[Cursor.x, Cursor.y] = true;
            }
            else if (MainClass.input == "Line")
            {
                // Start Line
                if (!Cursor.DrawingLine)
                {
                    Cursor.DrawingLine = true;
                    lineStartx = Cursor.x;
                    lineStarty = Cursor.y;
                }
                // End Line
                else
                {
                    Cursor.DrawingLine = false;

                    if (lineVert)
                    {
                        for (int count = 0; count <= Math.Abs(lineStarty - Cursor.y); count++)
                        {
                            if (lineFroth)
                            {

                                ssMarks[Cursor.x, lineStarty + count] = true;
                            }
                            else
                            {
                                Marks[Cursor.x, lineStarty - count] = true;
                            }
                        }
                    }
                    else
                    {
                        for (int count = 0; count <= Math.Abs(lineStartx - Cursor.x); count++)
                        {
                            if (lineFroth)
                            {
                                Marks[lineStartx + count, Cursor.y] = true;
                            }
                            else
                            {
                                Marks[lineStartx - count, Cursor.y] = true;
                            }
                        }
                    }
                }
            }
        }
    }


    class Cursor
    {
        public static int x, y;
        public static bool DrawingLine = false;

        // Update the cordenates
        public static void UpdateCords()
        {
            if (MainClass.input == "a")
            {
                x = x - 1;
            }
            if (MainClass.input == "d")
            {
                x = x + 1;
            }
            if (MainClass.input == "w")
            {
                y = y - 1;
            }
            if (MainClass.input == "s")
            {
                y = y + 1;
            }

            if (x < Binds.xmin)
            {
                x = Binds.xmin;
            }
            else if (x > Binds.xmax)
            {
                x = Binds.xmax;
            }
            if (y < Binds.ymin)
            {
                y = Binds.ymin;
            }
            else if (y > Binds.ymax)
            {
                y = Binds.ymax;
            }

            if (DrawingLine) { SetLineMode(); }
        }
        // Sets the direction of the line
        public static void SetLineMode()
        {
            if (MainClass.input == "a")
            {
                Drawing.lineVert = false;
                Drawing.lineFroth = false;
            }
            else if (MainClass.input == "d")
            {
                Drawing.lineVert = false;
                Drawing.lineFroth = true;
            }
            else if (MainClass.input == "w")
            {
                Drawing.lineVert = true;
                Drawing.lineFroth = false;
            }
            else if (MainClass.input == "s")
            {
                Drawing.lineVert = true;
                Drawing.lineFroth = true;
            }
        }
    }


    class MainClass
    {
        public static string input;

        // Main Loop
        public static void Main(string[] args)
        {
            while (true)
            {
                input = GetInput();
                Cursor.UpdateCords();
                Drawing.DrawAll();
            }
        }

        // Get Input
        public static string GetInput()
        {
            ConsoleKey textinput;

            textinput = Console.ReadKey(true).Key;
            if (textinput == ConsoleKey.A)
            {
                return ("a");
            }
            else if (textinput == ConsoleKey.D)
            {
                return ("d");
            }
            else if (textinput == ConsoleKey.W)
            {
                return ("w");
            }
            else if (textinput == ConsoleKey.S)
            {
                return ("s");
            }
            else if (textinput == ConsoleKey.Spacebar)
            {
                return ("Draw");
            }
            else if (textinput == ConsoleKey.Enter)
            {
                return ("Line");
            }
            else
            {
                return ("none");
            }
        }
    }
}