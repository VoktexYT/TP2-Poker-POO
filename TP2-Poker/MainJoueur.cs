namespace Poker102;
public class MainJoueur
{
    public Carte[] cartes = new Carte[5];
    
    private int _numeroJoueur;

    private List<string> _valeurMainFrancais = new List<string>([
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
        cartes = new[] { c0, c1, c2, c3, c4 };
        Evaluateur.TrierCroissant(cartes);
    }
    
    /// <summary>
    /// Convertir les résultats en texte en francais
    /// </summary>
    /// <returns>La chaine de caractere qui représente le résultat</returns>
    public string RecupererValeurFrancais()
    {
        Evaluateur.TrierCroissant(cartes);
        var valeurMain = RecupererValeurMain();
        return _valeurMainFrancais[valeurMain.Item1] + " => " + valeurMain.Item3;
    }

    /// <summary>
    /// Affiche en ordre croissant les cinq cartes
    /// </summary>
    public void Afficher()
    {
        Evaluateur.TrierCroissant(cartes);

        for(int i = 0; i < 5; i++)
        {
            cartes[i].Afficher(i, _numeroJoueur);
        }
    }
    
    /// <summary>
    /// Recuperer la valeur de la main en fonction des combinaisons possibles (quinte, couleur, etc)
    /// </summary>
    /// <returns>
    ///     Retourne l'index de la combinaison et la puissance de celle-ci
    ///     (INDEX RESULTAT (Ex. Max = 8), PUISSANCE = 3 (Si la carte la plus forte est 3))
    /// </returns>
    public (int, int, string) RecupererValeurMain()
    {
        (bool, int, string)[] resultats = CalculerMainJoueur();
        
        // Retourne l'index du premier TRUE dans le tableau "resultats"
        int indexCombinaisonPlusGagnante = resultats.ToList().FindIndex(v => v.Item1);

        return (indexCombinaisonPlusGagnante, resultats[indexCombinaisonPlusGagnante].Item2, resultats[indexCombinaisonPlusGagnante].Item3);
    }
    
    /// <summary>
    ///  Calcule toutes les combinaisons de cartes possibles et 
    /// </summary>
    /// <returns>
    ///     Retourne une liste de résultats composés
    ///         * d'un état (C'est une paire?)
    ///         * de la puissance de l'état (paire de 2 moins fort que paire de 4)
    ///         * des noms des cartes
    /// </returns>
    private (bool, int, string)[] CalculerMainJoueur()
    {
        Evaluateur evaluateur = new Evaluateur
        (
            cartes[0], 
            cartes[1],
            cartes[2],
            cartes[3],
            cartes[4]
        );
        
        int valeurQuinteCouleur, valeurCouleurQuinte = 0;
        string nomCartesQuinteCouleur, nomCarteCouleurQuinte = "";

        return
        [
            (evaluateur.EstQuinte(out valeurQuinteCouleur, out nomCartesQuinteCouleur) && evaluateur.EstCouleur(out valeurCouleurQuinte, out nomCarteCouleurQuinte), 
                valeurQuinteCouleur + valeurCouleurQuinte, $"{nomCartesQuinteCouleur}avec {nomCarteCouleurQuinte}"),
            (evaluateur.ADesCartesRepetees(4, out int valeurCarree, out string nomCartesCarrees, out string nomCarte1_2),
                valeurCarree, nomCartesCarrees),
            (evaluateur.ADesCartesRepetees(3, out int valeurFull, out string nomTripleCarte, out string nomDoubleCartes, 2),
                valeurFull, $"{nomTripleCarte} et {nomDoubleCartes}"),
            (evaluateur.EstCouleur(out int valeurCouleur, out string nomSorteCouleur),
                valeurCouleur, nomSorteCouleur),
            (evaluateur.EstQuinte(out int valeurQuinte, out string nomCartesQuintes),
                valeurQuinte, nomCartesQuintes),
            (evaluateur.ADesCartesRepetees(3, out int valeurBrelan, out string nomCartesBrelan, out string nomCarte1_6),
                valeurBrelan, nomCartesBrelan),
            (evaluateur.ADesCartesRepetees(2, out int valeurDoublePaire, out string nomCartesPremierePaire, out string nomCarteDeuxiemePaire, 2), 
                valeurDoublePaire, $"{nomCartesPremierePaire} et {nomCarteDeuxiemePaire}"),
            (evaluateur.ADesCartesRepetees(2, out int valeurPaire, out string nomDesCartesPaires, out string nomCarte1_8),
                valeurPaire, nomDesCartesPaires),
            (evaluateur.EstPlusGrandeCarte(out int valeurPlusGrandeCarte, out string nomPlusGrandeCarte), 
                valeurPlusGrandeCarte, nomPlusGrandeCarte) // Valeur de la carte la plus haute
        ];
    }
}
