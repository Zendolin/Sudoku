using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class Arc
    {
        private Variable v1;
        private Variable v2;

        public Arc(Variable v1, Variable v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }

        public Variable GetV1()
        {
            return this.v1;
        }

        public Variable GetV2()
        {
            return this.v2;
        }

        public override string ToString()
        {
            return String.Format("Arc : {0} - {1}", this.v1, this.v2);
        }

    }
}
