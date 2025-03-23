namespace Poker102;

/// <summary>
/// Représente un paquet de cartes utilisé dans le jeu de poker.
/// </summary>
public class Paquet
{
    /// <summary>
    /// Nombre de sortes de cartes (ex : trèfle, cœur, carreau, pique).
    /// </summary>
    private const int _NOMBRE_SORTE_CARTE = 4;
    
    /// <summary>
    /// Nombre de valeurs possibles pour une carte (2, 3, ..., Roi, As).
    /// </summary>
    private const int _NOMBRE_VALEUR_CARTE = 13;
    
    /// <summary>
    /// Tableau contenant toutes les cartes du paquet.
    /// </summary>
    private readonly Carte[] cartes = new Carte[_NOMBRE_SORTE_CARTE * _NOMBRE_VALEUR_CARTE];

    /// <summary>
    /// Nombre de fois que le paquet doit être mélangé.
    /// </summary>
    private const int NOMBRE_DE_FOIS_BRASSER = 100;
    
    /// <summary>
    /// Indique la position actuelle dans le paquet lors de la distribution.
    /// </summary>
    private int _curseur = 0;

    /// <summary>
    /// Constructeur qui initialise un paquet de cartes complet.
    /// </summary>
    public Paquet()
    {
        InitialiserCartes();
    }

    /// <summary>
    /// Remplit le paquet avec les 52 cartes uniques.
    /// </summary>
    private void InitialiserCartes()
    {
        int i = 0;
        for (int s = 0; s < _NOMBRE_SORTE_CARTE; s++)
        {
            for (int v = 0; v < _NOMBRE_VALEUR_CARTE; v++)
            {
                cartes[i] = new Carte(s, v);
                i++;
            }
        }
    }

    /// <summary>
    /// Distribue la prochaine carte du paquet.
    /// </summary>
    /// <returns>Une carte du paquet.</returns>
    public Carte Distribuer()
    {
        return cartes[_curseur++];   
    }

    /// <summary>
    /// Affiche toutes les cartes du paquet en grille (4 x 13).
    /// </summary>
    public void Afficher()
    {
        int i = 0;
        for (int y = 0; y < 4; y++)
        { 
            for (int x = 0; x < 13; x++)
            {
                cartes[i].Afficher(x, y);
                i++;
            }
        }
    }

    /// <summary>
    /// Mélange le paquet en effectuant des échanges aléatoires entre les cartes.
    /// </summary>
    public void Brasser()
    {
        for(int i = 0; i < NOMBRE_DE_FOIS_BRASSER; i++)
        {
            int indA = Util.rdm.Next(0, 52);
            Carte carteA = cartes[indA];

            int indB = Util.rdm.Next(0, 52);
            Carte carteB = cartes[indB];
           
            cartes[indB] = carteA;
            cartes[indA] = carteB;
        } 
    }
}
