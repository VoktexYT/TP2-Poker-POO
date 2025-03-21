using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker102
{
    internal class MainJoueur
    {
        public Carte[] Cartes = new Carte[5];
        int _numeroJoueur;


        public MainJoueur(int nj, Carte c0, Carte c1, Carte c2, Carte c3, Carte c4)
        {
            _numeroJoueur = nj;
            Cartes[0] = c0;
            Cartes[1] = c1;
            Cartes[2] = c2;
            Cartes[3] = c3;
            Cartes[4] = c4;

            Trier();
        }

        public void Afficher()
        {
            Trier();

            for(int i = 0; i < 5; i++)
            {
                Cartes[i].Afficher(i, _numeroJoueur);
            }
        }

        void Trier()
        {
            Array.Sort(Cartes, ComparerCarte);
        }

        int ComparerCarte(Carte cA, Carte cB)
        {
            if (cA.Valeur < cB.Valeur)
            {
                return 1;
            }

            if (cA.Valeur > cB.Valeur)
            {
                return -1;
            }

            return 0;
        }

        // To do
        public int RecupererValeurMain()
        {
            return 10;
        }

        // To do
        public string RecupererValeurFrancais()
        {
            return "allo";
        }
    }
}
