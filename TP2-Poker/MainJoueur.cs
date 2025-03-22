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

        private List<string> valeurMainFrancais = new List<string>([
            "QUINTE COULEUR",
            "CARRE",
            "FULL",
            "COULEUR",
            "QUINTE",
            "BRELAN",
            "DOUBLE PAIRE",
            "PAIRE",
            "CARTE LA PLUS FORTE"
        ]);

        public MainJoueur(int nj, Carte c0, Carte c1, Carte c2, Carte c3, Carte c4)
        {
            _numeroJoueur = nj;
            Cartes = new[] { c0, c1, c2, c3, c4 };
            TrierCroissant();
        }

        public void Afficher()
        {
            TrierCroissant();

            for(int i = 0; i < 5; i++)
            {
                Cartes[i].Afficher(i, _numeroJoueur);
            }
        }

        void TrierCroissant()
        {
            Array.Sort(Cartes, (Carte cA, Carte cB) =>
            {
                return (cA.Valeur < cB.Valeur) ? -1 : (cA.Valeur > cB.Valeur) ? 1 : 0; 
            });
        }

        bool ValeurMainCouleur()
        {
            return Cartes.All(carte => carte.Sorte == Cartes[0].Sorte);
        }

        bool ValeurMainQuinte()
        {
            TrierCroissant();
            
            // Vérifier s'il y a une séquence continue
            return Cartes.
                Zip(Cartes.Skip(1), (a, b) => b.Valeur - a.Valeur)
                .All(diff => diff == 1);
        }
        
        bool ValeurMainRepetee(int rep, int rep2 = -1)
        {
            // Récupérer les fréquences des valeurs
            var groupes = Cartes
                .GroupBy(c => c.Valeur)
                .Select(g => g.Count())
                .OrderByDescending(x => x)
                .ToList();

            // Si le premier groupe n'a pas la fréquence 'rep', on retourne false
            if (groupes.Count == 0 || groupes[0] != rep)
            {
                return false;
            }

            if (rep2 == -1)
            {
                return true;
            }

            // Vérifier si le deuxième groupe existe et correspond à rep2
            return groupes.Count > 1 && groupes[1] == rep2;
        }

        // To do
        public int RecupererValeurMain()
        {
            bool estQuinte = ValeurMainQuinte();
            bool estCouleur = ValeurMainCouleur();

            bool[] valeurs = new bool[]
            {
                estQuinte && estCouleur,           // Quinte couleur
                ValeurMainRepetee(4),          // Carré
                ValeurMainRepetee(3, 2),  // Full
                estCouleur,                        // Couleur
                estQuinte,                         // Quinte
                ValeurMainRepetee(3),          // Brelan
                ValeurMainRepetee(2, 2),  // Double Paire
                ValeurMainRepetee(2),          // Paire
                true                               // Carte la plus forte
            };

            return Array.FindIndex(valeurs, v => v) 
                    is int index and >= 0 ? index : valeurs.Length;
        }


        // To do
        public string RecupererValeurFrancais()
        {
            TrierCroissant();
            return valeurMainFrancais[RecupererValeurMain()];
        }
    }
}
