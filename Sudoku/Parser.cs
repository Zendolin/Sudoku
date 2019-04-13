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
        private List<Variable> variables;

        public Parser(String file)
        {
            this.file = file;
            this.grille = new Grille();
            this.variables = new List<Variable>();
        }


        public void Lecture()
        {
            System.IO.StreamReader file = new System.IO.StreamReader(this.file);

            String line;

            Case[,] cases = new Case[Grille.NB_CASE_PAR_LIGNE, Grille.NB_CASE_PAR_LIGNE];

            for (int i = 0; i < Grille.NB_CASE_PAR_LIGNE; i++)
            {
                line = file.ReadLine();
                line = line.Replace(",", "");
                for (int j = 0; j < Grille.NB_CASE_PAR_LIGNE; j++)
                {
                    char c = line[j];
                    if(c == '0')
                    {
                        cases[i, j] = new Case(i, j, new Variable("X" + i.ToString() + j.ToString()));
                    }
                    else
                    {
                        cases[i, j] = new Case((int)Char.GetNumericValue(c), i, j, new Variable("X" + i.ToString() + j.ToString(), true, (int)Char.GetNumericValue(c)), true);
                    }
                    
                }
            }

            file.Close();

            this.grille.SetCases(cases);
        }

        public void Saisie()
        {
            List<String> lines = new List<string>();

            for (int i = 0; i < Grille.NB_CASE_PAR_LIGNE; i++)
            {
                Console.WriteLine(String.Format("Veuillez entrer la ligne n°{0} du Sudoku (séparez chaque chiffre par une virgule, pour une case vide entrez 0) :", i+1));
                lines.Add(Console.ReadLine());
            }

            Case[,] cases = new Case[Grille.NB_CASE_PAR_LIGNE, Grille.NB_CASE_PAR_LIGNE];

            for (int i = 0; i < Grille.NB_CASE_PAR_LIGNE; i++)
            {
                String line = lines[i];
                line = line.Replace(",", "");
                for (int j = 0; j < Grille.NB_CASE_PAR_LIGNE; j++)
                {
                    char c = line[j];
                    if (c == '0')
                    {
                        cases[i, j] = new Case(i, j, new Variable("X" + i.ToString() + j.ToString()));
                    }
                    else
                    {
                        cases[i, j] = new Case((int)Char.GetNumericValue(c), i, j, new Variable("X" + i.ToString() + j.ToString(), true, (int)Char.GetNumericValue(c)), true);
                    }

                }
            }

            this.grille.SetCases(cases);
        }

        public Grille GetGrille()
        {
            return this.grille;
        }

        public List<Variable> GetVariables()
        {

            Case[,] tmpCases = this.grille.GetCases();

            for(int i = 0; i < Grille.NB_CASE_PAR_LIGNE; i++)
            {
                for(int j = 0; j < Grille.NB_CASE_PAR_LIGNE; j++){
                    this.variables.Add(tmpCases[i, j].GetVariable());
                }
            }

            return this.variables;
        }
    }
}
