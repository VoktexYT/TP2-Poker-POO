namespace Poker102;

/// <summary>
/// Classe principale du programme Poker102.
/// </summary>
internal static class Program
{
    private static readonly string[] _titres = 
    [
        "Valeurs des mains au Poker",
        "  Codé par Ubert Guertin  "
    ];
    
    private static readonly string _espacement = new string(' ', 15);

    /// <summary>
    /// Affiche le titre du programme dans la console avec un fond rouge et du texte blanc.
    /// </summary>
    private static void AfficherTitre()
    {
        Console.BackgroundColor = ConsoleColor.DarkRed;
        Console.ForegroundColor = ConsoleColor.White;
        
        Console.WriteLine(_espacement + _titres[0] + _espacement);
        Console.WriteLine(_espacement + _titres[1] + _espacement);
    }

    /// <summary>
    /// Demande à l'utilisateur s'il souhaite relancer une nouvelle ronde de jeu.
    /// </summary>
    /// <param name="relancerJeu">Référence à une variable booléenne qui sera mise à jour selon la réponse de l'utilisateur.</param>
    private static void DemanderNouvelleRonde(ref bool relancerJeu)
    {
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.White;
        
        Console.Write("\n Voulez-vous relancer une ronde ? (o/n): ");
        ConsoleKeyInfo key = Console.ReadKey();
        relancerJeu = key.KeyChar == 'o';
    }

    /// <summary>
    /// Jouer une ronde de Poker
    /// </summary>
    private static void JouerRonde()
    {
        Paquet paquet = new Paquet();
        Ronde ronde = new Ronde(paquet);

        ronde.DistribuerCartes();
            
        ////////////////////////////////////
        ronde.TricherMainsDesJoueurs();
        ////////////////////////////////////
            
        ronde.AfficherMainsJoueurs();
    }

    /// <summary>
    /// Initialise l'affichage du titre et de l'arriere plan du Poker
    /// </summary>
    private static void InitialiserAffichage()
    {
        Util.ViderEcran();
        Util.InitTapis();
        AfficherTitre();
    }

    /// <summary>
    /// Point d'entrée du programme. Gère le déroulement du jeu en boucle tant que l'utilisateur veut continuer.
    /// </summary>
    /// <param name="args">Arguments de la ligne de commande (non utilisés).</param>
    private static void Main(string[] args)
    {
        EcranIntroduction.Afficher();
        bool relancerJeu = true;

        while (relancerJeu)
        {
            InitialiserAffichage();
            JouerRonde();
            DemanderNouvelleRonde(ref relancerJeu);
        }
    }
}
