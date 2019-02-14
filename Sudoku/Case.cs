using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class Case
    {
        private char chiffre;

        public Case()
        {
            chiffre = ' ';
        }

        public Case(char chiffre)
        {
            this.chiffre = chiffre;
        }

        public char GetChiffre()
        {
            return chiffre;
        }
    }
}
