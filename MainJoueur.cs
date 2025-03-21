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

        bool ValeurMainCouleur()
        {
            return (
                Cartes[0].Sorte == Cartes[1].Sorte
                && Cartes[1].Sorte == Cartes[2].Sorte
                && Cartes[2].Sorte == Cartes[3].Sorte
                && Cartes[3].Sorte == Cartes[4].Sorte
            );
        }

        bool ValeurMainQuinte()
        {
            int valeurPremiereCarte = Cartes[0].Valeur;
            int i = 0;

            foreach (Carte carte in Cartes)
            {
                if (carte.Valeur - i != valeurPremiereCarte)
                {
                    return false;
                }
            }

            return true;
        }

        bool ValeurMainRepetee(int rep)
        {
            Carte carteAvant = Cartes[0];
            int repetition = 0;

            for (int i = 0; i < 5; i++)
            {
                if (Cartes[i] == carteAvant)
                {
                    repetition++;
                    carteAvant = Cartes[i];

                    if (repetition >= rep)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        // To do
        public int RecupererValeurMain()
        {
   

            return 1;
        }

        // To do
        public string RecupererValeurFrancais()
        {
            return "allo";
        }
    }
}
