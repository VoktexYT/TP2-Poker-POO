namespace Poker102;

/// <summary>
/// Cette classe permet d'afficher le menu d'introduction du programme
/// </summary>
public static class EcranIntroduction
{
	// Affichage du mot Poker en 3D
	private static readonly string banniere =
		"   ______   ______     __  __     ______     ______   \n  /\\  == \\ /\\  __ \\   /\\ \\/ /    /\\  ___\\   /\\  == \\  \n  \\ \\  _-/ \\ \\ \\/\\ \\  \\ \\  _\"-.  \\ \\  __\\   \\ \\  __<  \n   \\ \\_\\    \\ \\_____\\  \\ \\_\\ \\_\\  \\ \\_____\\  \\ \\_\\ \\_\\\n    \\/_/     \\/_____/   \\/_/\\/_/   \\/_____/   \\/_/ /_/";
	
	// Affiche le menu d'introduction
	public static void Afficher()
	{
		Console.Clear();
		
		Console.WriteLine(banniere + "\n\n");
		Console.WriteLine("  Ubert Guertin");
		Console.WriteLine("  Programmation Orientée Objet\n");
		Console.WriteLine("  TP2 : Valeurs des mains au Poker\n");
		Console.WriteLine("  2025/03/28\n\n");
		
		Util.Pause();
		
		Console.Clear();
	}
}
