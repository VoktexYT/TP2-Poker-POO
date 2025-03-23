namespace Poker102;

/// <summary>
/// Classe utilitaire contenant des méthodes pour gérer l'affichage et d'autres fonctionnalités globales du programme.
/// </summary>
public class Util
{
	/// <summary>
	/// Générateur de nombres aléatoires utilisé dans tout le programme.
	/// </summary>
	public static Random rdm = new Random();

	/// <summary>
	/// Vide l'écran de la console.
	/// </summary>
	public static void ViderEcran()
	{
		Console.Clear(); // Efface le contenu de la console.
		Console.WriteLine("\x1b[3J"); // Supprime l'historique du terminal (utile sur certains systèmes).
	}
    
	/// <summary>
	/// Met le programme en pause jusqu'à ce que l'utilisateur appuie sur une touche.
	/// </summary>
	public static void Pause()
	{
		Console.WriteLine("\n\tAppuyez sur une touche...");
		Console.ReadKey(true); // Attend une entrée utilisateur sans afficher la touche pressée.
	}

	/// <summary>
	/// Réinitialise la couleur du terminal en noir et blanc, puis vide l'écran.
	/// </summary>
	public static void SetNoirEttBlanc()
	{
		Console.ResetColor(); // Réinitialise les couleurs par défaut de la console.
		Console.BackgroundColor = ConsoleColor.Black;
		Console.ForegroundColor = ConsoleColor.White;
		ViderEcran(); // Vide l'écran après la modification des couleurs.
	}

	/// <summary>
	/// Initialise l'affichage du tapis vert du jeu de poker en changeant la couleur de fond de la console.
	/// </summary>
	public static void InitTapis()
	{
		Console.OutputEncoding = System.Text.Encoding.UTF8; // Active l'encodage UTF-8 pour l'affichage des caractères spéciaux.
		Console.BackgroundColor = ConsoleColor.DarkGreen; // Définit le fond en vert foncé.
		Console.ForegroundColor = ConsoleColor.Black; // Définit le texte en noir.
		ViderEcran(); // Vide l'écran après la modification des couleurs.
	}
}