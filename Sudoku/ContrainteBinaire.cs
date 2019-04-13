using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class ContrainteBinaire : IEquatable<ContrainteBinaire>
    {
        private Variable v1;
        private Variable v2;
        private Operateur o;

        public enum Operateur{
            EQUAL,
            NOT_EQUAL
        }

        public ContrainteBinaire(Variable v1, Variable v2, Operateur o)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.o = o;
        }

        public void Affiche()
        {
            String ope = "";

            if (o == ContrainteBinaire.Operateur.EQUAL)
            {
                ope = " = ";
            }
            else if(o == ContrainteBinaire.Operateur.NOT_EQUAL)
            {
                ope = " =/= ";
            }

            Console.WriteLine(v1.GetNom() + ope + v2.GetNom());
        }

        public override String ToString()
        {
            String ope = "";

            if (o == ContrainteBinaire.Operateur.EQUAL)
            {
                ope = " = ";
            }
            else if (o == ContrainteBinaire.Operateur.NOT_EQUAL)
            {
                ope = " =/= ";
            }

            return String.Format("{0} {1} {2}", v1.GetNom(), ope, v2.GetNom());
        }

        public Variable GetV1()
        {
            return this.v1;
        }

        public Variable GetV2()
        {
            return this.v2;
        }

        public Operateur GetO()
        {
            return this.o;
        }

        public override bool Equals(Object obj)
        {
            return Equals(obj as ContrainteBinaire);
        }

        public bool Equals(ContrainteBinaire cb)
        {
            if(cb == null)
            {
                return false;
            }
            else if ((cb.GetV1().Equals(this.v1) || cb.GetV2().Equals(this.v1)) && (cb.GetV2().Equals(this.v2) || cb.GetV1().Equals(this.v2)) && cb.GetO().Equals(this.o))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool Contient(Variable v)
        {
            if(v.Equals(this.v1) || v.Equals(this.v2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool estRespecte()
        {
            if (this.o == ContrainteBinaire.Operateur.EQUAL)
            {
                if(this.v1.GetValeur() == this.v2.GetValeur())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (this.o == ContrainteBinaire.Operateur.NOT_EQUAL)
            {
                if (this.v1.GetValeur() != this.v2.GetValeur())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }
    }
}
