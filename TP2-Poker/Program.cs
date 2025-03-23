namespace Poker102;

/// <summary>
/// Classe principale du programme Poker102.
/// </summary>
public class Program
{
    // Titres affichés en haut du programme
    private const string titre1 = "Valeurs des mains au Poker";
    private const string titre2 = "  Codé par Ubert Guertin  ";
    private static readonly string espacement = new string(' ', 15);

    /// <summary>
    /// Affiche le titre du programme dans la console avec un fond rouge et du texte blanc.
    /// </summary>
    static void AfficherTitre()
    {
        Console.BackgroundColor = ConsoleColor.DarkRed;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(espacement + titre1 + espacement);
        Console.WriteLine(espacement + titre2 + espacement);
    }

    /// <summary>
    /// Demande à l'utilisateur s'il souhaite relancer une nouvelle ronde de jeu.
    /// </summary>
    /// <param name="relancerJeu">Référence à une variable booléenne qui sera mise à jour selon la réponse de l'utilisateur.</param>
    static void DemanderNouvelleRonde(ref bool relancerJeu)
    {
        Console.Write("\nVoulez-vous relancer une ronde ? (o/n): ");
        ConsoleKeyInfo key = Console.ReadKey();
        relancerJeu = key.KeyChar == 'o'; // La partie est relancée si l'utilisateur appuie sur 'o'.
    }

    /// <summary>
    /// Point d'entrée du programme. Gère le déroulement du jeu en boucle tant que l'utilisateur veut continuer.
    /// </summary>
    /// <param name="args">Arguments de la ligne de commande (non utilisés).</param>
    static void Main(string[] args)
    {
        bool relancerJeu = true;

        while (relancerJeu)
        {
            Util.ViderEcran(); // Efface l'écran pour une meilleure lisibilité.
            Util.InitTapis(); // Initialise le tapis de jeu.
       
            AfficherTitre(); // Affiche le titre du programme.

            Paquet paquet = new Paquet(); // Crée un nouveau paquet de cartes.
            Ronde ronde = new Ronde(paquet); // Crée une nouvelle ronde de jeu.

            ronde.DistribuerCartes(); // Distribue les cartes aux joueurs.
            // ronde.TricherMainsDesJoueurs(); // Permet d'afficher des mains spécifiques (probablement pour test).
            ronde.AfficherMainsJoueurs(); // Affiche les cartes des joueurs.
            
            Console.ResetColor(); // Réinitialise les couleurs de la console.

            DemanderNouvelleRonde(ref relancerJeu); // Demande à l'utilisateur s'il veut recommencer.
        }
    }
}
