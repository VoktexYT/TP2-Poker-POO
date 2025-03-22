using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker102
{
    internal class Paquet
    {
        public Carte[] Cartes = new Carte[52];
        int _curseur = 0;

        public Paquet()
        {
            int i = 0;
            for (int s = 0; s < 4; s++)
            {
                for (int v = 0; v < 13; v++)
                {
                    Cartes[i] = new Carte(s, v);
                    i++;
                }
            }
        }

        public Carte Distribuer()
        {
            return Cartes[_curseur++];   
        }

        public void Afficher()
        {
            int i = 0;
            for (int y = 0; y < 4; y++)
            { 
                for (int x = 0; x < 13; x++)
                {
                    Cartes[i].Afficher(x, y);
                    i++;
                }
            }
        }

        public void Brasser()
        {
            for(int i = 0; i < 100; i++)
            {
                int indA = Util.rdm.Next(0, 52);
                Carte carteA = Cartes[indA];

                int indB = Util.rdm.Next(0, 52);
                Carte carteB = Cartes[indB];
               
                Cartes[indB] = carteA;
                Cartes[indA] = carteB;
            } 
        }
    }
}
