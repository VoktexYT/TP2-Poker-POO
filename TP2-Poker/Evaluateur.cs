namespace Poker102;

public class Evaluateur
{
    private readonly Carte[] _cartes;
    
    
    private List<string> _valeurCarteFrancais = new List<string>([
        "deux", "trois", "quatre",
        "cinq", "six", "sept",
        "huit", "neuf", "dix",
        "Valet", "Dame", "Roi", "As",
    ]);
    
    private List<string> _valeurCarteSorteFrancais = new List<string>([
        "Pique", "Trèfle", "Cœur", "Carreau"
    ]);
    
    public Evaluateur(Carte c1, Carte c2, Carte c3, Carte c4, Carte c5)
    {
        _cartes = new Carte[] { c1, c2, c3, c4, c5 };
    }
    
    /// <summary>
    /// Trier un tableau en ordre croissant
    /// </summary>
    /// <param name="cartes">Un tableau qui contient des cartes</param>
    public static void TrierCroissant(Carte[] cartes)
    {
        Array.Sort(cartes, (cA, cB) => cA.Valeur.CompareTo(cB.Valeur));
    }
    
    /// <summary>
    ///  Retourne la carte avec la plus grande valeur
    /// </summary>
    /// <param name="valeurCartes">La valeur de la plus grand carte</param>
    /// <param name="nomCarte">Le nom de la plus grande carte (0 = deux)</param>
    /// <returns>Toujours TRUE</returns>
    public bool EstPlusGrandeCarte(out int valeurCartes, out string nomCarte)
    {
        valeurCartes =_cartes.Max(c => c.Valeur);
        nomCarte = _valeurCarteFrancais[valeurCartes];
        return true;
    }
    
    /// <summary>
    ///  Vérifie si toutes les cartes sont les mêmes sortes (Couleur)
    /// </summary>
    /// <param name="valeurCartes">La valeur des cartes combinées</param>
    /// <param name="nomCartes">Enssemble de nom de carte</param>
    /// <returns>TRUE s'il y a une combinaison de type 'couleur'</returns>
    public bool EstCouleur(out int valeurCartes, out string nomCartes)
    {
        nomCartes = "";

        if (_cartes.All(carte => carte.Sorte ==_cartes[0].Sorte))
        {
            // Valeur = somme des cartes (permet de départager deux flushs)
            valeurCartes =_cartes.Sum(c => c.Valeur);

            nomCartes = _valeurCarteSorteFrancais[_cartes[0].Sorte];
            
            return true;
        }

        valeurCartes = 0;
        return false;
    }

    /// <summary>
    ///  Vérifie si les cartes forment une suite
    /// </summary>
    /// <param name="valeurCartes">Valeur de l'enssemble des cartes de la quinte</param>
    /// <param name="nomCarte">Nom de tous les cartes de la quinte</param>
    /// <returns>TRUE s'il y a une quinte dans la main</returns>
    public bool EstQuinte(out int valeurCartes, out string nomCarte)
    {
        Carte[] triees = (Carte[])_cartes.Clone();
        TrierCroissant(triees);
        nomCarte = "";

        // Vérifie si les cartes sont consécutives
        if (triees.Zip(triees.Skip(1), (a, b) => b.Valeur - a.Valeur).All(d => d == 1))
        {
            valeurCartes = triees.Last().Valeur; // La valeur la plus haute de la quinte

            foreach (Carte c in triees)
            {
                nomCarte += _valeurCarteFrancais[c.Valeur] + ", ";
            }
            
            return true;
        }

        valeurCartes = 0;
        return false;
    }

    /// <summary>
    ///  Vérifie si la main contient des répétitions de cartes (paire, brelan, carré, full, etc.)
    /// </summary>
    /// <param name="rep">Le nombre de fois que le premier groupe de cartes doit se répéter</param>
    /// <param name="valeurCartes">Valeur de la somme des deux groupes de cartes</param>
    /// <param name="nomCarte1">Nom du premier groupe de cartes</param>
    /// <param name="nomCarte2">Nom du deuxieme groupe de cartes</param>
    /// <param name="rep2">Le nombre de fois que le deuxieme groupe de cartes doit se répéter</param>
    /// <returns></returns>
    public bool ADesCartesRepetees(int rep, out int valeurCartes, out string nomCarte1, out string nomCarte2, int rep2 = -1)
    {
        nomCarte1 = "";
        nomCarte2 = "";
        
        // Regroupe les cartes par valeur et compte combien il y en a de chaque type
        var groupes =_cartes
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
        nomCarte1 = _valeurCarteFrancais[premierGroupe.Valeur];

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
        nomCarte2 = _valeurCarteFrancais[deuxiemeGroupe.Valeur];
        return true; // La main correspond bien au motif demandé
    }
}
