using CoreApp;
using System;

namespace ConnectToFriendLinkedinConsole
{
    public class ConsoleOutput : IOutput
    {
        public void WriteText(string text)
        {
            Console.WriteLine(text);
        }
    }
}