namespace Poker102;

/// <summary>
/// Représente une carte de poker avec une valeur et une sorte.
/// </summary>
public class Carte
{
    // Symboles Unicode pour représenter les sortes (♠, ♣, ♥, ♦)
    private readonly char[] _SYMBOLES =
    {
        '\u2660', // Pique
        '\u2663', // Trèfle
        '\u2665', // Cœur
        '\u2666'  // Carreau
    };

    // Couleurs d'arrière-plan associées aux sortes
    private readonly ConsoleColor[] _COULEURS_ARRIERES_PLANS =
    {
        ConsoleColor.Black,      // Pique
        ConsoleColor.DarkBlue,   // Trèfle
        ConsoleColor.DarkRed,    // Cœur
        ConsoleColor.DarkYellow  // Carreau
    };

    // Représentation textuelle des valeurs des cartes
    private readonly string[] _VALEURS_TEXTES = [
        "2", "3", "4", "5", "6", "7", "8", "9",
        "10", "J", "Q", "K", "A"
    ];

    private const int _LARGEUR = 5; // Largeur visuelle d'une carte
    private const int _DECALAGE_GAUCHE = 2; // Décalage horizontal pour l'affichage
    private const int _ESPACEMENT_X = 2; // Espacement entre les cartes
    private const ConsoleColor _COULEUR_TEXTE = ConsoleColor.White; // Couleur du texte des cartes

    public int Sorte { get; set; }
    public int Valeur { get; set; }

    private readonly string _texte;
    private readonly char _symbole;
    private readonly ConsoleColor _couleurArrierePlan;

    /// <summary>
    /// Initialise une nouvelle carte avec une sorte et une valeur spécifiques.
    /// </summary>
    /// <param name="s">La sorte de la carte (0 = Pique, 1 = Trèfle, 2 = Cœur, 3 = Carreau).</param>
    /// <param name="v">La valeur de la carte (0 = 2, ..., 12 = As).</param>
    public Carte(int s = 0, int v = 0)
    {
        Sorte = s;
        Valeur = v;
        
        _texte = _VALEURS_TEXTES[v];
        _symbole = _SYMBOLES[s];
        _couleurArrierePlan = _COULEURS_ARRIERES_PLANS[s];
    }

    /// <summary>
    /// Affiche la carte à une position spécifique dans la console.
    /// </summary>
    /// <param name="posX">Position X où afficher la carte.</param>
    /// <param name="posY">Position Y où afficher la carte.</param>
    public void Afficher(int posX, int posY)
    {
        AjusteCouleurSorte();
        Dessiner((posX, posY));
    }

    /// <summary>
    /// Ajuste la couleur du texte et de l'arrière-plan en fonction de la sorte de la carte.
    /// </summary>
    private void AjusteCouleurSorte()
    {
        Console.ForegroundColor = _COULEUR_TEXTE;
        Console.BackgroundColor = _couleurArrierePlan;
    }

    /// <summary>
    /// Dessine la carte à une position donnée dans la console.
    /// </summary>
    /// <param name="position2D">Tuple contenant la position X et Y.</param>
    private void Dessiner((int x, int y) position2D)
    {
        string[] structureCarte =
        {
            _texte.PadRight(_LARGEUR),
            $"  {_symbole}  ", 
            _texte.PadLeft(_LARGEUR),
        };
        
        int positionGaucheCurseur = _DECALAGE_GAUCHE + position2D.x * (_LARGEUR + _ESPACEMENT_X);
        
        for (int i = 0; i < structureCarte.Length; i++)
        {
            Console.CursorLeft = positionGaucheCurseur;
            Console.CursorTop = _LARGEUR + (position2D.y * _LARGEUR) + i;
            Console.WriteLine(structureCarte[i]);
        }
    }
}