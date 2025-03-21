using Microsoft.VisualBasic;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Claims;
using System.Text;
using System;

namespace Poker102
{
    internal class Program
    {

        static MainJoueur[] joueurs = new MainJoueur[4];
        static void AfficherTitre()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Valeurs des mains au Poker");
            Console.WriteLine("    Codé par Ubert Guertin");
        }

        static void Main(string[] args)
        {
            Util.InitTapis();
           
            AfficherTitre();

            Paquet paquet = new Paquet();
            Ronde ronde = new Ronde(paquet);

            ronde.DistribuerCartes();
            ronde.TricherMainsDesJoueurs();
            ronde.AfficherMainsJoueurs();

            Util.Pause();
            Util.SetNoirEttBlanc();
        }

    }
}
