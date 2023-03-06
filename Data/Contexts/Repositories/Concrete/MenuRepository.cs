using Data.Contexts.Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Extensions;
using Core.Helpers;
using System.Text.RegularExpressions;
using Core.Constants;
namespace Presentation.Services
{
    public class MenuRepository
    {
        //private string _prompt = "";
        private readonly string[] _options;
        private readonly List<string> _menuList;
        private int _currentSelection;
        private int _drawMenuColumnPos;
        private readonly int _drawMenuRowPos;
        private int _menuMaximumWidth;


        public MenuRepository(string[] options, int row, int col)
        {
            //_prompt = prompt;
            _menuList = options.ToList();
            _options = options;
            _currentSelection = 0;
            _drawMenuRowPos = row;
            _drawMenuColumnPos = col;

        }

        public int GetMaximumWidth()
        {
            return _menuMaximumWidth;
        }

        public void CenterMenuToConsole()
        {
            _drawMenuColumnPos = GetConsoleWindowWidth() / 2 - (_menuMaximumWidth / 2);
        }

        
        public void ModifyMenuLeftJustified()
        {
            int maximumWidth = 0;
            string space = "";

            foreach (var t in _menuList)
            {
                if (t.Length > maximumWidth)
                {
                    maximumWidth = t.Length;
                }
            }

            maximumWidth += 6;

            for (int i = 0; i < _menuList.Count; i++)
            {
                int spacesToAdd = maximumWidth - _menuList[i].Length;
                for (int j = 0; j < spacesToAdd; j++)
                {
                    space += " ";
                }
                _menuList[i] = _menuList[i] + space;
                space = "";
            }

            _menuMaximumWidth = maximumWidth;
        }

        
        public void ModifyMenuCentered()
        {
            int maximumWidth = 0;
            string space = " ";

            foreach (var t in _menuList)
            {
                if (t.Length > maximumWidth)
                {
                    maximumWidth = t.Length;
                }
            }

            maximumWidth += 1;
            CosnsoleHelper.PrintStringCentered("\n\n\n\n\n\n",ConsoleColor.DarkCyan);
            


            for (int i = 0; i < _menuList.Count; i++)
            {
                if (_menuList[i].Length % 2 != 0)
                {
                    _menuList[i] += " "; 
                }

                var minimumWidth = maximumWidth - _menuList[i].Length;
                minimumWidth /= 2;
                for (int j = 0; j < minimumWidth; j++)
                {
                    space += " ";
                }

                _menuList[i] = space + _menuList[i] + space;    
                space = " ";                            
            }

            for (int i = 0; i < _menuList.Count; i++)
            {
                if (_menuList[i].Length < maximumWidth)
                {
                    _menuList[i] += "";
                }
            }

            _menuMaximumWidth = maximumWidth;

        }

        // UTILITIES FOR THE CLASS
        public void SetConsoleWindowSize(int width, int height)
        {
            Console.WindowWidth = width;
            Console.WindowHeight = height;
        }

        public static int GetConsoleWindowWidth()
        {
            return Console.WindowWidth;
        }

        public void SetConsoleTextColor(ConsoleColor foreground, ConsoleColor background)
        {
            Console.ForegroundColor = foreground;
            Console.BackgroundColor = background;
        }

        public void ResetCursorVisible()
        {
            Console.CursorVisible = Console.CursorVisible != true;
        }

        public void SetCursorPosition(int row, int column)
        {
            if (row > 0 && row < Console.WindowHeight)
            {
                Console.CursorTop = row;
            }

            if (column > 0 && column < Console.WindowWidth)
            {
                Console.CursorLeft = column;
            }
        }


       
        public int RunMenu()
        {
            bool run = true;
            DrawMenu();
            while (run)
            {
                var keyPressedCode = CheckKeyPress();
                if (keyPressedCode == 10)   // up arrow
                {
                    _currentSelection--;
                    if (_currentSelection < 0)
                    {
                        _currentSelection = _menuList.Count - 1;
                    }

                }
                else if (keyPressedCode == 11)  // down arrow
                {
                    _currentSelection++;
                    if (_currentSelection > _menuList.Count-1)
                    {
                        _currentSelection = 0;
                    }

                }
                else if (keyPressedCode == 12)
                {
                    run = false;
                }
                else if (keyPressedCode == 13)
                {
                    return -1;

                }
              
                
                DrawMenu();
            }

            return _currentSelection;
        }

        private void DrawMenu()
        {
            
            
            for (int i = 0; i < (_menuList.Count); i++)
            {
                SetCursorPosition(_drawMenuRowPos + i, _drawMenuColumnPos);
                SetConsoleTextColor(ConsoleColor.Black, ConsoleColor.DarkGreen);
                if (i == _currentSelection)
                {
                    SetConsoleTextColor(ConsoleColor.White, ConsoleColor.Green);
                    
                    if (i == _menuList.Count - 1)
                    {
                        SetConsoleTextColor(ConsoleColor.White, ConsoleColor.DarkRed);
                    }
                   
                } 
               
                Console.WriteLine(_menuList[i]);
               
                Console.ResetColor();
            }
        }

        private int CheckKeyPress()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            do
            {
                ConsoleKey keyPressed = keyInfo.Key;
                if (keyPressed == ConsoleKey.UpArrow)
                {
                    Console.Beep(523, 300);

                    return 10;
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    Console.Beep(523, 300);
                    System.Threading.Thread.Sleep(1);
                    return 11;
                }
                else if (keyPressed == ConsoleKey.Enter)
                {

                    Console.Beep(600, 300); 
                    
                    return 12;
                }
                else if (keyPressed == ConsoleKey.Q)
                {
                    Console.Beep(600, 200);

                    return 13;
                }
                else
                {
                    Console.Beep(623, 200);
                    return 12;
                    Console.Beep();

                    return 0;
                }

            } while (true);
        }

    }

    public class CosnsoleHelper
    {
        public static void WriteWithColor(string text, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (text.Length / 2)) + "}", text));
            Console.ResetColor();
        }
        public static void PrintStringCentered(string text, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            int leftPadding = (Console.WindowWidth - text.Length) / 2;
            Console.WriteLine(String.Format("{0}{1}", new string(' ', leftPadding), text));
            Console.ResetColor();
        }
    }
}

