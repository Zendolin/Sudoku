using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class Grille
    {
        public static readonly int NB_CASE_PAR_LIGNE = 9;
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
                    cases[j, i] = new Case();
                }
            }
        }

        public void Affiche()
        {
            for(int i = 0; i < NB_CASE_PAR_LIGNE; i++)
            {
                String line = "";
                for(int j = 0; j < NB_CASE_PAR_LIGNE; j++)
                {
                    if(j%3 == 0 && j != 0)
                    {
                        line += " ";
                    }
                    line += "[" + cases[j, i].GetChiffre() + "]";
                }
                if((i+1)%3 == 0)
                {
                    line += "\n";
                }
                Console.WriteLine(line);
            }
        }

        public void SetCases(Case[,] cases)
        {
            this.cases = cases;
        }
    }
}
