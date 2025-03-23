using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker102
{
    internal class Util
    {
        public static Random rdm = new Random();

        //---------------------------------------------
        //
        //---------------------------------------------
        public static void Titre(string leTitre)
        {
            ViderEcran();

            for (int i = 0; i < leTitre.Length; i++)
            {
                Console.Write("_");
            }

            Console.WriteLine("\n" + leTitre);

            for (int i = 0; i < leTitre.Length; i++)
            {
                Console.Write("_");
            }

            Console.WriteLine("\n\n");
        }

        //---------------------------------------------
        //
        //---------------------------------------------
        public static void ViderEcran()
        {
            Console.Clear();
            Console.WriteLine("\x1b[3J");
        }

        //---------------------------------------------
        //
        //---------------------------------------------
        public char SaisirChar()
        {
            ConsoleKeyInfo cle = Console.ReadKey();
            return cle.KeyChar;
        }


        //---------------------------------------------
        //
        //---------------------------------------------
        public int SaisirEntier()
        {
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int res))
            {
                return res;
            }

            return 0;
        }

        //---------------------------------------------
        //
        //---------------------------------------------
        public double SaisirReel()
        {
            string? input = Console.ReadLine();

            if (double.TryParse(input, out double res))
            {
                return res;
            }

            return 0.0;
        }
        //---------------------------------------------
        //
        //---------------------------------------------
        public static void Pause()
        {
            Console.WriteLine("\n\tAppuyez sur une touche...");
            Console.ReadKey(true);
        }

        //---------------------------------------------
        //
        //---------------------------------------------
        public void Sep(string msg = "")
        {
            Console.WriteLine($"----------{msg}----------");
        }

        public static void SetNoirEttBlanc()
        {
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            ViderEcran();
        }

        public static void InitTapis()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Black;
            ViderEcran();
        }
    }
}
