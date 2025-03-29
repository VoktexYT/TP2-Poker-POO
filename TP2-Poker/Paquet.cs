namespace Poker102;

/// <summary>
/// Représente un paquet de cartes utilisé dans le jeu de poker.
/// </summary>
public class Paquet
{
    private const int _NOMBRE_SORTE_CARTE = 4;
    private const int _NOMBRE_VALEUR_CARTE = 13;
    private const int _NOMBRE_DE_FOIS_BRASSER = 100;
   
    private readonly Carte[] _cartes = new Carte[_NOMBRE_SORTE_CARTE * _NOMBRE_VALEUR_CARTE];
    
    /// <summary>
    /// Indique la position actuelle dans le paquet lors de la distribution.
    /// </summary>
    private int _curseur = 0;

    public Paquet()
    {
        InitialiserCartes();
    }

    /// <summary>
    /// Distribue la prochaine carte du paquet.
    /// </summary>
    /// <returns>Une carte du paquet.</returns>
    public Carte Distribuer()
    {
        return _cartes[_curseur++];   
    }

    /// <summary>
    /// Mélange le paquet en effectuant des échanges aléatoires entre les cartes.
    /// </summary>
    public void Brasser()
    {
        for(int i = 0; i < _NOMBRE_DE_FOIS_BRASSER; i++)
        {
            int indA = Util.rdm.Next(0, 52);
            Carte carteA = _cartes[indA];

            int indB = Util.rdm.Next(0, 52);
            Carte carteB = _cartes[indB];
           
            _cartes[indB] = carteA;
            _cartes[indA] = carteB;
        } 
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
                _cartes[i] = new Carte(s, v);
                i++;
            }
        }
    }
}
