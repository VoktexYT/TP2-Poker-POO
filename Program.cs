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
        static void Main(string[] args)
        {
            Util.InitTapis();

            Util.Titre("Poker 102!");

            Paquet paquet = new Paquet();

            paquet.Brasser();

            for(int i = 0; i < 4; i++)
            {
                joueurs[i] = new MainJoueur(
                    i, 
                    paquet.Distribuer(), 
                    paquet.Distribuer(), 
                    paquet.Distribuer(), 
                    paquet.Distribuer(), 
                    paquet.Distribuer()
                );
            }

            TricharMainsDesJoueurs();

            foreach(MainJoueur joueur in joueurs)
            { 
                joueur.Afficher();  
            }
            Util.Pause();

            Util.SetNoirEttBlanc();
        }

        static void TricharMainsDesJoueurs()
        {
            joueurs[0].Cartes[0] = new Carte(0, 12);
            joueurs[0].Cartes[1] = new Carte(0, 10);
            joueurs[0].Cartes[2] = new Carte(0, 11);
            joueurs[0].Cartes[3] = new Carte(0, 9);
            joueurs[0].Cartes[4] = new Carte(0, 8);

            joueurs[1].Cartes[0] = new Carte(0, 12);
            joueurs[1].Cartes[1] = new Carte(1, 12);
            joueurs[1].Cartes[2] = new Carte(2, 12);
            joueurs[1].Cartes[3] = new Carte(3, 12);
            joueurs[1].Cartes[4] = new Carte(0, 0);

            joueurs[2].Cartes[0] = new Carte(2, 12);
            joueurs[2].Cartes[1] = new Carte(3, 0);
            joueurs[2].Cartes[2] = new Carte(3, 1);
            joueurs[2].Cartes[3] = new Carte(3, 2);
            joueurs[2].Cartes[4] = new Carte(3, 3);

            joueurs[3].Cartes[0] = new Carte(0, 1);
            joueurs[3].Cartes[1] = new Carte(2, 1);
            joueurs[3].Cartes[2] = new Carte(3, 1);
            joueurs[3].Cartes[3] = new Carte(3, 8);
            joueurs[3].Cartes[4] = new Carte(1, 8);
        }
    }
}
