namespace Poker102;

/// <summary>
/// Représente une ronde de poker où les cartes sont distribuées et évaluées.
/// </summary>
public class Ronde
{
    /// <summary>
    /// Le paquet de cartes utilisé pour la ronde.
    /// </summary>
    private Paquet lePaquet { get; set; }

    /// <summary>
    /// Tableau contenant les mains des joueurs.
    /// </summary>
    private MainJoueur[] joueurs = new MainJoueur[4];

    /// <summary>
    /// Initialise une nouvelle instance de la classe <see cref="Ronde"/> avec un paquet de cartes donné.
    /// </summary>
    /// <param name="p">Le paquet de cartes utilisé pour la ronde.</param>
    public Ronde(Paquet p)
    {
        lePaquet = p;
    }

    /// <summary>
    /// Distribue 5 cartes à chaque joueur après avoir mélangé le paquet.
    /// </summary>
    public void DistribuerCartes()
    {
        lePaquet.Brasser();

        for (int i = 0; i < 4; i++)
        {
            joueurs[i] = new MainJoueur(i,
                lePaquet.Distribuer(),
                lePaquet.Distribuer(),
                lePaquet.Distribuer(),
                lePaquet.Distribuer(),
                lePaquet.Distribuer()
            );
        }
    }

    /// <summary>
    /// Modifie les mains des joueurs pour leur attribuer des cartes spécifiques (triche).
    /// </summary>
    public void TricherMainsDesJoueurs()
    {
        joueurs[0].Cartes[0] = new Carte(0, 12);
        joueurs[0].Cartes[1] = new Carte(1, 8);
        joueurs[0].Cartes[2] = new Carte(2, 9);
        joueurs[0].Cartes[3] = new Carte(3, 10);
        joueurs[0].Cartes[4] = new Carte(2, 11);

        joueurs[1].Cartes[0] = new Carte(1, 10);
        joueurs[1].Cartes[1] = new Carte(1, 10);
        joueurs[1].Cartes[2] = new Carte(2, 10);
        joueurs[1].Cartes[3] = new Carte(2, 10);
        joueurs[1].Cartes[4] = new Carte(0, 3);

        joueurs[2].Cartes[0] = new Carte(2, 12);
        joueurs[2].Cartes[1] = new Carte(3, 0);
        joueurs[2].Cartes[2] = new Carte(3, 1);
        joueurs[2].Cartes[3] = new Carte(3, 2);
        joueurs[2].Cartes[4] = new Carte(3, 3);

        joueurs[3].Cartes[0] = new Carte(0, 1);
        joueurs[3].Cartes[1] = new Carte(2, 1);
        joueurs[3].Cartes[2] = new Carte(3, 1);
        joueurs[3].Cartes[3] = new Carte(3, 8);
        joueurs[3].Cartes[4] = new Carte(1, 8);
    }

    /// <summary>
    /// Évalue la main d'un joueur donné en utilisant un évaluateur de combinaisons.
    /// </summary>
    /// <param name="joueur">Le joueur dont la main doit être évaluée.</param>
    private void EvaluerMains(MainJoueur joueur)
    {
        Evaluateur evaluateur = new Evaluateur(
            joueur.Cartes[0],
            joueur.Cartes[1],
            joueur.Cartes[2],
            joueur.Cartes[3],
            joueur.Cartes[4]
        );

        joueur.RecupererValeurMain();
    }

    /// <summary>
    /// Détermine le joueur gagnant en comparant les mains des joueurs (à implémenter).
    /// </summary>
    private void DeterminerGagnant()
    {
        // TODO: Implémenter la logique pour déterminer le gagnant de la ronde.
    }

    /// <summary>
    /// Affiche les mains des joueurs et met en évidence le joueur gagnant.
    /// </summary>
    public void AfficherMainsJoueurs()
    {
        int indexCombinaisonGagnante = 0;
        int indexCartePlusForte = 0;
        
        for (int j = 0; j < joueurs.Length; j++)
        {
            if (joueurs[indexCombinaisonGagnante].RecupererValeurMain().Item1 > joueurs[j].RecupererValeurMain().Item1)
            {
                indexCombinaisonGagnante = j;
                indexCartePlusForte = indexCombinaisonGagnante;
            }

            if (joueurs[indexCombinaisonGagnante].RecupererValeurMain().Item1 ==
                joueurs[j].RecupererValeurMain().Item1)
            {
                if (joueurs[indexCartePlusForte].RecupererValeurMain().Item2 <
                    joueurs[j].RecupererValeurMain().Item2)
                {
                    indexCartePlusForte = j;
                }
            }
        }

        int i = 0;

        foreach (MainJoueur joueur in joueurs)
        {
            joueur.Afficher();
            Console.SetCursorPosition(40, 5 * i + 6);
            
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            
            if (joueurs[indexCartePlusForte] == joueur)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            
            Console.Write(joueur.RecupererValeurFrancais());
            
            i++;
        }

        Console.SetCursorPosition(35, 5 * i + 5);
    }
}
