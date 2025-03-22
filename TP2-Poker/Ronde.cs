using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker102
{
    internal class Ronde
    {
        Paquet lePaquet { get; set; }
        MainJoueur[] joueurs = new MainJoueur[4];

        // To do
        public Ronde(Paquet p)
        {
            lePaquet = p;
        }

        // To do
        public void DistribuerCartes()
        {
            lePaquet.Brasser();

            for (int i = 0; i < 4; i++)
            {
                joueurs[i] = new MainJoueur(
                    i,
                    lePaquet.Distribuer(),
                    lePaquet.Distribuer(),
                    lePaquet.Distribuer(),
                    lePaquet.Distribuer(),
                    lePaquet.Distribuer()
                );
            }
        }

        // To do
        public void TricherMainsDesJoueurs()
        {
            joueurs[0].Cartes[0] = new Carte(0, 12);
            joueurs[0].Cartes[1] = new Carte(1, 8);
            joueurs[0].Cartes[2] = new Carte(2, 9);
            joueurs[0].Cartes[3] = new Carte(3, 10);
            joueurs[0].Cartes[4] = new Carte(2, 11);

            joueurs[1].Cartes[0] = new Carte(1, 10);
            joueurs[1].Cartes[1] = new Carte(1, 10);
            joueurs[1].Cartes[2] = new Carte(2, 10);
            joueurs[1].Cartes[3] = new Carte(2, 10);
            joueurs[1].Cartes[4] = new Carte(0, 3);

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

        // To do
        void EvaluerMains(MainJoueur joueur)
        {
            Evaluateur evaluateur = new Evaluateur(
                joueur.Cartes[0],
                joueur.Cartes[1],
                joueur.Cartes[2],
                joueur.Cartes[3],
                joueur.Cartes[4]
            );

            joueur.RecupererValeurMain();
        }

        // To do
        void DeterminerGagnant()
        {

        }

        public void AfficherMainsJoueurs()
        {
            int i = 0;
            int indexJoueurPlusFort = 0;
            
            for (int j = 0; j < joueurs.Length; j++)
            {
                if (joueurs[indexJoueurPlusFort].RecupererValeurMain() > joueurs[j].RecupererValeurMain())
                {
                    indexJoueurPlusFort = j;
                }
            }

            List<int> listeIndexJoueursPlusForts = new List<int>();
            
            for (int j = 0; j < joueurs.Length; j++)
            {
                if (joueurs[j].RecupererValeurMain() == joueurs[indexJoueurPlusFort].RecupererValeurMain())
                {
                    listeIndexJoueursPlusForts.Add(j);        
                }
            }

            foreach (MainJoueur joueur in joueurs)
            {
                joueur.Afficher();
                Console.SetCursorPosition(35, 5 * i + 6);
                
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                
                if (listeIndexJoueursPlusForts.Contains(i))
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                }
                
                Console.Write(joueur.RecupererValeurFrancais());
                i++;
            }

            Console.SetCursorPosition(35, 5 * i + 5);

        }
    }
}
