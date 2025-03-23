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
        TrierCroissant();
    }

    public void Afficher()
    {
        TrierCroissant();

        for(int i = 0; i < 5; i++)
        {
            Cartes[i].Afficher(i, _numeroJoueur);
        }
    }

    void TrierCroissant()
    {
        Array.Sort(Cartes, (Carte cA, Carte cB) =>
        {
            return (cA.Valeur < cB.Valeur) ? -1 : (cA.Valeur > cB.Valeur) ? 1 : 0; 
        });
    }

    bool ValeurMainCouleur(ref int valeurCartes)
    {
        return Cartes.All(carte => carte.Sorte == Cartes[0].Sorte);
    }

    bool ValeurMainQuinte(ref int valeurCartes)
    {
        TrierCroissant();
        
        List<Carte> valeurChaqueCarte = new List<Carte>();

        IEnumerable<int> differenceCarte =
            Cartes.Zip(Cartes.Skip(1), (a, b) => b.Valeur - a.Valeur);

        valeurCartes += valeurChaqueCarte.Sum((c) => c.Valeur);
        
        return Cartes.All(diff => diff.Valeur == 1);
    }
    
    bool ValeurMainRepetee(int rep, ref int valeurCartes, int rep2 = -1)
    {
        var groupes = Cartes
            .GroupBy(c => c.Valeur)
            .Select(g => new { Valeur = g.Key, Count = g.Count() })
            .OrderByDescending(g => g.Count)
            .ThenByDescending(g => g.Valeur)
            .ToList();

        var premierGroupe = groupes.FirstOrDefault(g => g.Count == rep);
        
        if (premierGroupe == null)
        {
            return false;
        }

        valeurCartes = premierGroupe.Valeur * rep;

        if (rep2 == -1)
        {
            return true;
        }

        var deuxiemeGroupe = 
            groupes.FirstOrDefault(
                g => g.Count == rep2 && g.Valeur != premierGroupe.Valeur);
        
        if (deuxiemeGroupe == null)
        {
            return false;
        }

        valeurCartes += deuxiemeGroupe.Valeur * rep2;

        return true;
    }
    
    // To do
    public (int, int) RecupererValeurMain()
    {
        (bool, int)[] resultats =
        {
            (false, 0), (false, 0), (false, 0),
            (false, 0), (false, 0), (false, 0),
            (false, 0), (false, 0), (true, Cartes.Max(carte => carte.Valeur))
        };

        resultats[0] = (
            ValeurMainQuinte(ref resultats[0].Item2) && ValeurMainCouleur(ref resultats[0].Item2),
            resultats[0].Item2
        );

        resultats[1] = (ValeurMainRepetee(4, ref resultats[1].Item2), resultats[1].Item2);
        resultats[2] = (ValeurMainRepetee(3, ref resultats[2].Item2, 2), resultats[2].Item2);
        resultats[3] = (ValeurMainCouleur(ref resultats[3].Item2), resultats[3].Item2);
        resultats[4] = (ValeurMainQuinte(ref resultats[4].Item2), resultats[4].Item2);
        resultats[5] = (ValeurMainRepetee(3, ref resultats[5].Item2), resultats[5].Item2);
        resultats[6] = (ValeurMainRepetee(2, ref resultats[6].Item2, 2), resultats[6].Item2);
        resultats[7] = (ValeurMainRepetee(2, ref resultats[7].Item2), resultats[7].Item2);

        int indexCombinaisonPlusGagnante = resultats.ToList().FindIndex(v => v.Item1);

        return indexCombinaisonPlusGagnante >= 0 
            ? (indexCombinaisonPlusGagnante, resultats[indexCombinaisonPlusGagnante].Item2) 
            : (resultats.Length-1, resultats[resultats.Length-1].Item2);
    }
    
    // To do
    public string RecupererValeurFrancais()
    {
        TrierCroissant();
        return valeurMainFrancais[RecupererValeurMain().Item1];
    }
}
