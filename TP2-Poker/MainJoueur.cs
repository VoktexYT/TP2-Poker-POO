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

        bool ValeurMainCouleur(ref int valeurCartes)
        {
            return Cartes.All(carte => carte.Sorte == Cartes[0].Sorte);
        }

        bool ValeurMainQuinte(ref int valeurCartes)
        {
            TrierCroissant();
            
            List<Carte> valeurChaqueCarte = new List<Carte>();

            IEnumerable<int> differenceCarte =
                Cartes.Zip(Cartes.Skip(1), (a, b) => b.Valeur - a.Valeur);

            valeurCartes += valeurChaqueCarte.Sum((c) => c.Valeur);
            
            // Vérifier s'il y a une séquence continue
            return Cartes.All(diff => diff.Valeur == 1);
        }
        
        bool ValeurMainRepetee(int rep, ref int valeurCartes, int rep2 = -1)
        {
            // Grouper les cartes par valeur et récupérer les fréquences
            var groupes = Cartes
                .GroupBy(c => c.Valeur)
                .Select(g => new { Valeur = g.Key, Count = g.Count() }) // Stocke la valeur et le nombre d'occurrences
                .OrderByDescending(g => g.Count) // Trier par fréquence (brelan > paire)
                .ThenByDescending(g => g.Valeur) // Ensuite trier par valeur la plus forte
                .ToList();

            // Vérifier si la première répétition existe
            var premierGroupe = groupes.FirstOrDefault(g => g.Count == rep);
            if (premierGroupe == null)
            {
                return false;
            }

            // Additionner la valeur des cartes qui forment la répétition
            valeurCartes = premierGroupe.Valeur * rep;

            // Si on ne cherche qu'une seule répétition, on s'arrête là
            if (rep2 == -1)
            {
                return true;
            }

            // Vérifier si la deuxième répétition existe
            var deuxiemeGroupe = groupes.FirstOrDefault(g => g.Count == rep2 && g.Valeur != premierGroupe.Valeur);
            if (deuxiemeGroupe == null)
            {
                return false;
            }

            // Ajouter la valeur des cartes du deuxième groupe
            valeurCartes += deuxiemeGroupe.Valeur * rep2;

            return true;
        }




        // To do
        public (int, int) RecupererValeurMain()
        {
            (bool, int)[] resultats =
            {
                (false, 0), (false, 0), (false, 0),
                (false, 0), (false, 0), (false, 0),
                (false, 0), (false, 0), (true, Cartes.Max(carte => carte.Valeur))
            };

            resultats[0] = (
                ValeurMainQuinte(ref resultats[0].Item2) && ValeurMainCouleur(ref resultats[0].Item2),
                resultats[0].Item2
            );

            resultats[1] = (ValeurMainRepetee(4, ref resultats[1].Item2), resultats[1].Item2);
            resultats[2] = (ValeurMainRepetee(3, ref resultats[2].Item2, 2), resultats[2].Item2);
            resultats[3] = (ValeurMainCouleur(ref resultats[3].Item2), resultats[3].Item2);
            resultats[4] = (ValeurMainQuinte(ref resultats[4].Item2), resultats[4].Item2);
            resultats[5] = (ValeurMainRepetee(3, ref resultats[5].Item2), resultats[5].Item2);
            resultats[6] = (ValeurMainRepetee(2, ref resultats[6].Item2, 2), resultats[6].Item2);
            resultats[7] = (ValeurMainRepetee(2, ref resultats[7].Item2), resultats[7].Item2);

            int indexCombinaisonPlusGagnante = resultats.ToList().FindIndex(v => v.Item1);

            return indexCombinaisonPlusGagnante >= 0 
                ? (indexCombinaisonPlusGagnante, resultats[indexCombinaisonPlusGagnante].Item2) 
                : (resultats.Length-1, resultats[resultats.Length-1].Item2);
        }



        // To do
        public string RecupererValeurFrancais()
        {
            TrierCroissant();
            return valeurMainFrancais[RecupererValeurMain().Item1];
        }
    }
}
