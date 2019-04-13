using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class Grille
    {
        public static readonly int NB_CASE_PAR_LIGNE = 9; //9 : taille normale, 3 : test
        public static readonly int NB_CASE_PAR_REGION = 3;
        public static readonly List<int> POSSIBILITE = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        private Case[,] cases;

        public Grille()
        {
            cases = new Case[NB_CASE_PAR_LIGNE, NB_CASE_PAR_LIGNE];
            GrilleVide();
        }

        public void GrilleVide()
        {
            for (int i = 0; i < NB_CASE_PAR_LIGNE; i++)
            {
                for (int j = 0; j < NB_CASE_PAR_LIGNE; j++)
                {
                    cases[i, j] = new Case(i, j, new Variable("X" + i.ToString() + j.ToString()));
                }
            }
        }

        public void MAJCases()
        {
            foreach(Case c in this.cases)
            {
                c.SetChiffre(c.GetVariable().GetValeur());
            }
        }

        public void Affiche()
        {
            MAJCases();

            for (int i = 0; i < NB_CASE_PAR_LIGNE; i++)
            {
                String line = "";
                if (i%3 == 0 && i != 0)
                {
                    line += "\n";
                }
                for (int j = 0; j < NB_CASE_PAR_LIGNE; j++)
                {
                    if(j%3 == 0 && j != 0)
                    {
                        line += " ";
                    }
                    line += "[" + cases[i, j].GetCarac() + "]";
                }

                Console.WriteLine(line);
            }
            Console.WriteLine();
        }

        public void AfficheEnCouleur()
        {
            MAJCases();

            for (int i = 0; i < NB_CASE_PAR_LIGNE; i++)
            {
                Console.Write("");
                if (i % 3 == 0 && i != 0)
                {
                    Console.Write("\n");
                }
                for (int j = 0; j < NB_CASE_PAR_LIGNE; j++)
                {
                    if (j % 3 == 0 && j != 0)
                    {
                        Console.Write(" ");
                    }
                    Console.Write("[");
                    if (!cases[i, j].GetDejaDefini())
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    Console.Write(cases[i, j].GetCarac());
                    Console.ResetColor();
                    Console.Write("]");
                }

                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void SetCases(Case[,] cases)
        {
            this.cases = cases;
        }

        public Case[,] GetCases()
        {
            MAJCases();
            return this.cases;
        }
    }
}
