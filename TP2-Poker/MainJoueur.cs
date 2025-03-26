namespace Poker102;
public class MainJoueur
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
        Evaluateur.TrierCroissant(Cartes);
    }

    /// <summary>
    /// Affiche en ordre croissant les cinq cartes
    /// </summary>
    public void Afficher()
    {
        Evaluateur.TrierCroissant(Cartes);

        for(int i = 0; i < 5; i++)
        {
            Cartes[i].Afficher(i, _numeroJoueur);
        }
    }
    
    /// <summary>
    /// Recuperer la valeur de la main en fonction des combinaisons possibles (quinte, couleur, etc)
    /// </summary>
    /// <returns>
    ///     Retourne l'index de la combinaison et la puissance de celle-ci
    ///     (INDEX RESULTAT (Ex. Max = 8), PUISSANCE = 3 (Si la carte la plus forte est 3))
    /// </returns>
    public (int, int) RecupererValeurMain()
    {
        Evaluateur evaluateur = new Evaluateur(Cartes[0],Cartes[1],Cartes[2],Cartes[3],Cartes[4]);

        int valeur0, valeur1 = 0;
        
        // (Chaque méthode retourne TRUE ou FALSE (Ex: S'il y a un full), La puissance de la main)
        var resultats = new (bool, int)[]
        {
            (evaluateur.EstQuinte(out valeur0) && evaluateur.EstCouleur(out valeur1), valeur0 + valeur1),
            (evaluateur.ADesCartesRepetees(4, out int valeur2), valeur2),
            (evaluateur.ADesCartesRepetees(3, out int valeur3, 2), valeur3),
            (evaluateur.EstCouleur(out int valeur4), valeur4),
            (evaluateur.EstQuinte(out int valeur5), valeur5),
            (evaluateur.ADesCartesRepetees(3, out int valeur6), valeur6),
            (evaluateur.ADesCartesRepetees(2, out int valeur7, 2), valeur7),
            (evaluateur.ADesCartesRepetees(2, out int valeur8), valeur8),
            (true, Cartes.Max(c => c.Valeur)) // Valeur de la carte la plus haute
        };
        
        // Retourne l'index du premier TRUE dans le tableau "resultats"
        int indexCombinaisonPlusGagnante = resultats.ToList().FindIndex(v => v.Item1);

        return (indexCombinaisonPlusGagnante, resultats[indexCombinaisonPlusGagnante].Item2);
    }
    
    // To do
    public string RecupererValeurFrancais()
    {
        Evaluateur.TrierCroissant(Cartes);
        return valeurMainFrancais[RecupererValeurMain().Item1];
    }
}
