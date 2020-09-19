using ConnectToFriendLinkedinLibrary;
using ConsumeGoogleSheet;
using CoreApp;
using System;
using System.Text;

namespace ConnectToFriendLinkedinConsole
{
    public class Program
    {
        private static void Main(string[] args)
        {
            IOutput outputConsole = new ConsoleOutput();
            var linkedins = GetUrlsLinkedin(outputConsole);
            outputConsole.WriteText(Environment.NewLine);
            ConnectToFriendLinkedin(outputConsole, linkedins);
        }

        private static string[] GetUrlsLinkedin(IOutput outputConsole)
        {
            GoogleSheet googleSheet = new GoogleSheet(outputConsole);
            outputConsole.WriteText("Endereços de amigos que serão connectados!");
            outputConsole.WriteText(Environment.NewLine);
            return googleSheet.GetAddresses();
        }

        private static void ConnectToFriendLinkedin(IOutput outputConsole, string[] linkedinsList)
        {
            Console.WriteLine("Entre com seu email do linkedin: ");
            var email = Console.ReadLine();
            Console.WriteLine("Entre com seu password do linkedin: ");
            var password = LerSenha();

            string message = "Ola! Gostaria de fazer parte de sua rede de relacionamentos! Adquiri seu contato pelo bootcamp IGTI.";
            using (ConnectToFriendLinkedin conFriendLinkedin = new ConnectToFriendLinkedin(outputConsole))
            {
                if (conFriendLinkedin.Login(email, password))
                {
                    foreach (var addressLinkedin in linkedinsList)
                    {
                        conFriendLinkedin.RunConnect(addressLinkedin, message);
                    }
                }
                else
                    outputConsole.WriteText("Não foi possível realizar login!");
            }
        }

        public static string LerSenha()
        {
            StringBuilder pw = new StringBuilder();
            bool caracterApagado = false;

            while (true)
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);

                if (cki.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }

                if (deletarTexto(cki))
                {
                    if (pw.Length != 0)
                    {
                        Console.Write("\b \b");
                        pw.Length--;

                        caracterApagado = true;
                    }
                }
                else
                {
                    caracterApagado = false;
                }

                if (!caracterApagado && verificarCaracterValido(cki))
                {
                    Console.Write('•');
                    pw.Append(cki.KeyChar);
                }
            }

            return pw.ToString();
        }

        private static bool verificarCaracterValido(ConsoleKeyInfo tecla)
        {
            if (char.IsLetterOrDigit(tecla.KeyChar) || char.IsPunctuation(tecla.KeyChar) ||
                char.IsSymbol(tecla.KeyChar))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool deletarTexto(ConsoleKeyInfo tecla)
        {
            if (tecla.Key == ConsoleKey.Backspace || tecla.Key == ConsoleKey.Delete)
                return true;
            else
                return false;
        }
    }
}