using Microsoft.VisualBasic;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Claims;
using System.Text;
using System;

namespace Poker102
{
    internal class Program
    {
        static void AfficherTitre()
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("     Valeurs des mains au Poker");
            Console.WriteLine("         Codé par Ubert Guertin");
        }

        static void Main(string[] args)
        {
            bool relancerJeu = true;

            while (relancerJeu)
            {
                Util.ViderEcran();
                Util.InitTapis();
           
                AfficherTitre();

                Paquet paquet = new Paquet();
                Ronde ronde = new Ronde(paquet);

                ronde.DistribuerCartes();
                // ronde.TricherMainsDesJoueurs();
                ronde.AfficherMainsJoueurs();

                Console.ResetColor();
                
                Console.Write("\nVoulez-vous relancer une ronde ? (o/n): ");
                ConsoleKeyInfo key = Console.ReadKey();
                relancerJeu = key.KeyChar == 'o';
            }
        }

    }
}
