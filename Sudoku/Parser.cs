using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class Parser
    {
        private Grille grille;
        private String file;

        public Parser(String file)
        {
            this.file = file;
            this.grille = new Grille();
        }


        public void Lecture()
        {
            System.IO.StreamReader file = new System.IO.StreamReader(this.file);

            String line;
            Case[,] cases = new Case[Grille.NB_CASE_PAR_LIGNE, Grille.NB_CASE_PAR_LIGNE];

            for(int i = 0; i < Grille.NB_CASE_PAR_LIGNE; i++)
            {
                line = file.ReadLine();
                line = line.Replace(",", "");
                for (int j = 0; j < Grille.NB_CASE_PAR_LIGNE; j++)
                {
                    if(line[j] == '0')
                    {
                        cases[j, i] = new Case();
                    }
                    else
                    {
                        cases[j, i] = new Case(line[j]);
                    }
                    
                }
            }

            file.Close();

            this.grille.SetCases(cases);
        }

        public Grille GetGrille()
        {
            return grille;
        }
    }
}
