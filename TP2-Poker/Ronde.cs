namespace Poker102;

/// <summary>
/// Représente une ronde de poker où les cartes sont distribuées et évaluées.
/// </summary>
public class Ronde
{
    /// <summary>
    /// Le paquet de cartes utilisé pour la ronde.
    /// </summary>
    private Paquet _paquet { get; set; }

    /// <summary>
    /// Tableau contenant les mains des _joueurs.
    /// </summary>
    private readonly MainJoueur[] _joueurs = new MainJoueur[4];

    /// <summary>
    /// Initialise une nouvelle instance de la classe <see cref="Ronde"/> avec un paquet de cartes donné.
    /// </summary>
    /// <param name="p">Le paquet de cartes utilisé pour la ronde.</param>
    public Ronde(Paquet p)
    {
        _paquet = p;
    }

    /// <summary>
    /// Distribue 5 cartes à chaque joueur après avoir mélangé le paquet.
    /// </summary>
    public void DistribuerCartes()
    {
        _paquet.Brasser();

        for (int i = 0; i < 4; i++)
        {
            _joueurs[i] = new MainJoueur(i,
                _paquet.Distribuer(),
                _paquet.Distribuer(),
                _paquet.Distribuer(),
                _paquet.Distribuer(),
                _paquet.Distribuer()
            );
        }
    }

    /// <summary>
    /// Modifie les mains des _joueurs pour leur attribuer des cartes spécifiques (triche).
    /// </summary>
    public void TricherMainsDesJoueurs()
    {
        _joueurs[0].cartes[0] = new Carte(1, 12);
        _joueurs[0].cartes[1] = new Carte(0, 1);
        _joueurs[0].cartes[2] = new Carte(2, 9);
        _joueurs[0].cartes[3] = new Carte(0, 2);
        _joueurs[0].cartes[4] = new Carte(0, 11);
        
        _joueurs[1].cartes[0] = new Carte(1, 1);
        _joueurs[1].cartes[1] = new Carte(0, 2);
        _joueurs[1].cartes[2] = new Carte(2, 6);
        _joueurs[1].cartes[3] = new Carte(0, 10);
        _joueurs[1].cartes[4] = new Carte(3, 3);

        _joueurs[2].cartes[0] = new Carte(1, 2);
        _joueurs[2].cartes[1] = new Carte(2, 10);
        _joueurs[2].cartes[2] = new Carte(1, 7);
        _joueurs[2].cartes[3] = new Carte(3, 1);
        _joueurs[2].cartes[4] = new Carte(1, 12);
        
        _joueurs[3].cartes[0] = new Carte(1, 2);
        _joueurs[3].cartes[1] = new Carte(2, 10);
        _joueurs[3].cartes[2] = new Carte(1, 7);
        _joueurs[3].cartes[3] = new Carte(3, 1);
        _joueurs[3].cartes[4] = new Carte(1, 12);
    }
    
    /// <summary>
    /// Affiche les mains des joueurs et met en évidence le joueur gagnant.
    /// </summary>
    public void AfficherMainsJoueurs()
    {
        CalculerJoueurPlusFort(out int indexMainPlusForte);
        
        int i = 0;

        foreach (MainJoueur joueur in _joueurs)
        {
            joueur.Afficher();
            Console.SetCursorPosition(40, 5 * i + 6);
            MettreCouleurPerdant();
            
            if (_joueurs[indexMainPlusForte].RecupererValeurFrancais() == joueur.RecupererValeurFrancais())
            {
                MettreCouleurGagnant();
            }
            
            Console.Write(joueur.RecupererValeurFrancais());
            i++;
        }

        Console.SetCursorPosition(35, 5 * i + 5);
    }

    /// <summary>
    /// Calcule l'index de la main la plus forte
    /// </summary>
    /// <param name="indexCartePlusForte">L'index de la main la plus forte</param>
    private void CalculerJoueurPlusFort(out int indexMainPlusForte)
    {
        int indexCombinaisonGagnante = 0;
        indexMainPlusForte = 0;
        
        for (int j = 0; j < _joueurs.Length; j++)
        {
            if (_joueurs[indexCombinaisonGagnante].RecupererValeurMain().Item1 > _joueurs[j].RecupererValeurMain().Item1)
            {
                indexCombinaisonGagnante = j;
                indexMainPlusForte = indexCombinaisonGagnante;
            }

            if (_joueurs[indexCombinaisonGagnante].RecupererValeurMain().Item1 ==
                _joueurs[j].RecupererValeurMain().Item1)
            {
                if (_joueurs[indexMainPlusForte].RecupererValeurMain().Item2 <
                    _joueurs[j].RecupererValeurMain().Item2)
                {
                    indexMainPlusForte = j;
                }
            }
        }
    }

    /// <summary>
    ///  Change la couleur de la console pour mettre en evidence la main gagnante
    /// </summary>
    private void MettreCouleurGagnant()
    {
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
    }

    /// <summary>
    /// Change la couleur de la console pour mettre en evidence la main perdante
    /// </summary>
    private void MettreCouleurPerdant()
    {
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.White;
    }
}
