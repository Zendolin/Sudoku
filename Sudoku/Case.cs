using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class Case
    {
        private int chiffre;
        private int nbLigne;
        private int nbColonne;
        private Variable variable;
        private bool dejaDefini;

        public Case(int nbLigne, int nbColonne, Variable variable)
        {
            this.chiffre = 0;
            this.dejaDefini = false;
            this.variable = variable;
        }

        public Case(int nbLigne, int nbColonne, int chiffre, Variable variable, bool dejaDefini)
        {
            this.chiffre = chiffre;
            this.dejaDefini = dejaDefini;
            this.variable = variable;
        }

        public int GetChiffre()
        {
            return this.chiffre;
        }

        public void SetChiffre(int chiffre)
        {
            this.chiffre = chiffre;
        }

        public Variable GetVariable()
        {
            return this.variable;
        }
        
        public void SetVariable(Variable variable)
        {
            this.variable = variable;
        }

        public bool GetDejaDefini()
        {
            return this.dejaDefini;
        }

        public char GetCarac()
        {
            char c = ' ';

            if(this.chiffre > 0)
            {
                c = (char)('0' + this.chiffre);
            }

            return c;
        }
    }
}
