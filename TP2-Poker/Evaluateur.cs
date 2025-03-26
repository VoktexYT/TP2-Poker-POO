namespace Poker102;

public class Evaluateur
{
    private readonly Carte[] Cartes; // Tableau de cartes en lecture seule

    public Evaluateur(Carte c1, Carte c2, Carte c3, Carte c4, Carte c5)
    {
        // Initialisation directe du tableau
        Cartes = new Carte[] { c1, c2, c3, c4, c5 };
    }
    
    // Trie les cartes par valeur en ordre croissant
    public static void TrierCroissant(Carte[] cartes)
    {
        Array.Sort(cartes, (cA, cB) => cA.Valeur.CompareTo(cB.Valeur));
    }
    
    // Vérifie si toutes les cartes ont la même sorte (Flush)
    public bool EstCouleur(out int valeurCartes)
    {
        if (Cartes.All(carte => carte.Sorte == Cartes[0].Sorte))
        {
            // Valeur = somme des cartes (permet de départager deux flushs)
            valeurCartes = Cartes.Sum(c => c.Valeur);
            return true;
        }

        valeurCartes = 0;
        return false;
    }

    // Vérifie si les cartes forment une suite (Straight)
    public bool EstQuinte(out int valeurCartes)
    {
        Carte[] triees = (Carte[])Cartes.Clone();
        TrierCroissant(triees);

        // Vérifie si les cartes sont consécutives
        if (triees.Zip(triees.Skip(1), (a, b) => b.Valeur - a.Valeur).All(d => d == 1))
        {
            valeurCartes = triees.Last().Valeur; // La valeur la plus haute de la quinte
            return true;
        }

        valeurCartes = 0;
        return false;
    }

    // Vérifie si la main contient des répétitions de cartes (paire, brelan, carré, full house, etc.)
    public bool ADesCartesRepetees(int rep, out int valeurCartes, int rep2 = -1)
    {
        // Regroupe les cartes par valeur et compte combien il y en a de chaque type
        var groupes = Cartes
            .GroupBy(c => c.Valeur)  // Groupe les cartes ayant la même valeur
            .Select(g => new { Valeur = g.Key, Count = g.Count() }) // Crée une liste avec la valeur et le nombre d'occurrences
            .OrderByDescending(g => g.Count) // Trie d'abord par fréquence (ex: brelan avant paire)
            .ThenByDescending(g => g.Valeur) // Ensuite, trie par valeur (ex: brelan de roi avant brelan de dame)
            .ToList();

        // Recherche le premier groupe ayant exactement 'rep' occurrences (ex: un brelan = 3 répétitions)
        var premierGroupe = groupes.FirstOrDefault(g => g.Count == rep);
        
        // Si on ne trouve pas le premier groupe, alors la main ne contient pas la combinaison recherchée
        if (premierGroupe == null)
        {
            valeurCartes = 0;
            return false;
        }

        // Initialise la valeur des cartes en multipliant la valeur par le nombre de répétitions
        // Ex: si on a trois 10 (brelan), alors valeurCartes = 10 * 3 = 30
        valeurCartes = premierGroupe.Valeur * rep;

        // Si on ne cherche qu'une seule répétition (ex: juste une paire ou un brelan), on retourne vrai
        if (rep2 == -1)
        {
            return true;
        }

        // Recherche un deuxième groupe de cartes ayant 'rep2' occurrences
        // (utile pour détecter un full : ex. brelan + paire)
        var deuxiemeGroupe = groupes.FirstOrDefault(g => g.Count == rep2 && g.Valeur != premierGroupe.Valeur);

        // Si on ne trouve pas la deuxième répétition demandée, la main n'est pas valide
        if (deuxiemeGroupe == null)
        {
            valeurCartes = 0;
            return false;
        }

        // Ajoute la valeur du deuxième groupe à la valeur des cartes
        // Ex: Full avec 3 valets (3 * 11 = 33) et 2 six (2 * 6 = 12) → valeurCartes = 33 + 12 = 45
        valeurCartes += deuxiemeGroupe.Valeur * rep2;
        
        return true; // La main correspond bien au motif demandé
    }
}
