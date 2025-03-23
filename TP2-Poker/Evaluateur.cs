namespace Poker102;

public class Evaluateur
{
    Carte[] cartes = new Carte[5];

    public Evaluateur(Carte c1, Carte c2, Carte c3, Carte c4, Carte c5)
    {
        cartes[0] = c1;
        cartes[1] = c2;
        cartes[2] = c3;
        cartes[3] = c4;
        cartes[4] = c5;
    }

    // To do
    public void TrierMain()
    {

    }

    // To do
    public int GetValeur()
    {
        return 0;
    }

    // To do
    string ConvertirValeurEnFrancais(int valeurEntier)
    {
        return "allo";
    }
}

