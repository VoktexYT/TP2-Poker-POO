using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker102
{
    internal class Carte
    {
        readonly char[] _SYMBOLES = 
        {
            '\u2664',
            '\u2667',
            '\u2662',
            '\u2661'
        };

        readonly ConsoleColor[] _COULEURS =
        {
            ConsoleColor.Black,
            ConsoleColor.DarkBlue,
            ConsoleColor.DarkRed,
            ConsoleColor.DarkYellow
        };

        const string _VALEUR_TEXTE = "23456789XJQKA";


        public int Sorte { get; set; }
        public int Valeur { get; set; }

        char _valTexte = '2';

        public Carte(int s = 0, int v = 0)
        {
            Sorte = s;
            Valeur = v;
            convertirVal();
        }

        void convertirVal()
        {
            _valTexte = _VALEUR_TEXTE[Valeur];
        }

        public void Afficher(int posX, int posY)
        {
            AjusteCouleurSorte();

            int positionGaucheCurseur = 2 + posX * (5 + 1);

            string[] structureCarte =
            {
                _valTexte + "    ",
                $"  {SorteGraphique()}  ",
                "    " + _valTexte
            };

            for (int i = 0; i < structureCarte.Length; i++)
            {
                Console.CursorLeft = positionGaucheCurseur;
                Console.CursorTop = 5 + (posY * 5) + i;
                Console.WriteLine(structureCarte[i]);
            }
        }

        char SorteGraphique()
        {
            return _SYMBOLES[Sorte];
        }

        void AjusteCouleurSorte()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = _COULEURS[Sorte];
        }
    }
}
