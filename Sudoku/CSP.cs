using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class CSP
    {
        List<Variable> variables;
        List<ContrainteBinaire> contraintes;
        List<int> domaine;


        public CSP(List<Variable> variables, List<ContrainteBinaire> contraintes, List<int> domaine)
        {
            this.variables = variables;
            this.contraintes = contraintes;
            this.domaine = domaine;
        }

        public void SetVariables(List<Variable> variables)
        {
            this.variables = variables;
        }

        public void SetContraintes(List<ContrainteBinaire> contraintes)
        {
            this.contraintes = contraintes;
        }

        public void SetDomaine(List<int> domaine)
        {
            this.domaine = domaine;
        }

        public void AfficherVariableNonAssigne()
        {
            String message = "Variables non assignées : ";
            foreach(Variable v in this.variables)
            {
                if (!v.EstAssigne())
                {
                    message += v.GetNom() + " ";
                }
            }
            Console.WriteLine(message);
        }

        public void AfficherVariableAssigne()
        {
            String message = "Variables assignées : ";
            foreach (Variable v in this.variables)
            {
                if (v.EstAssigne())
                {
                    message += v.GetNom() + " ";
                }
            }
            Console.WriteLine(message);
        }

        public void AfficherContraintes()
        {
            foreach (ContrainteBinaire cb in contraintes)
            {
                cb.Affiche();
            }
            Console.WriteLine(String.Format("Il y a {0} contraintes.", contraintes.Count));
        }

        public bool estAssigneCompletConsistant()
        {
            foreach(Variable v in this.variables)
            {
                if (!v.EstAssigne())
                {
                    return false;
                }
            }

            return true;
        }

        public List<Variable> GetVariableNonAssigne()
        {
            List<Variable> variablesNonAssignees = new List<Variable>();


            foreach(Variable v in this.variables)
            {
                if (!v.EstAssigne())
                {
                    variablesNonAssignees.Add(v);
                }
            }

            return variablesNonAssignees;
        }

        public Variable GetFirstVariableNonAssigne()
        {
            return this.GetVariableNonAssigne()[0];
        }

        public List<Variable> GetVariables()
        {
            return this.variables;
        }

        public List<ContrainteBinaire> GetContraintes()
        {
            return this.contraintes;
        }

        public List<ContrainteBinaire> GetContraintesVariable(Variable v)
        {
            List<ContrainteBinaire> cb = new List<ContrainteBinaire>();

            foreach(ContrainteBinaire c in contraintes)
            {
                if (c.Contient(v))
                {
                    cb.Add(c);
                }
            }

            return cb;
        }

        public List<ContrainteBinaire> GetContraintesVariablesNonAssignees(Variable v)
        {
            List<ContrainteBinaire> cb = new List<ContrainteBinaire>();

            foreach (ContrainteBinaire c in contraintes)
            {
                if (c.Contient(v))
                {
                    if (c.GetV1().Equals(v))
                    {
                        if (!c.GetV2().EstAssigne())
                        {
                            cb.Add(c);
                        }
                    }
                    else if (c.GetV2().Equals(v))
                    {
                        if (!c.GetV1().EstAssigne())
                        {
                            cb.Add(c);
                        }
                    }
                }
            }

            return cb;
        }

        public ContrainteBinaire GetContrainteVariables(Variable v1, Variable v2)
        {
            ContrainteBinaire cb = null;

            foreach (ContrainteBinaire c in contraintes)
            {
                if (c.Contient(v1) && c.Contient(v2))
                {
                    cb = c;
                }
            }

            return cb;
        }

        public bool RespecteContraintes(Variable v)
        {
            if (!v.EstAssigne())
            {
                return true;
            }
            List<ContrainteBinaire> cb = GetContraintesVariable(v);

            foreach(ContrainteBinaire c in cb)
            {
                if (c.GetV1().Equals(v))
                {
                    if (c.GetV2().EstAssigne())
                    {
                        if (c.GetO().Equals(ContrainteBinaire.Operateur.NOT_EQUAL))
                        {
                            if(!(v.GetValeur() != c.GetV2().GetValeur()))
                            {
                                return false;
                            }

                        }
                    }
                }
                else if (c.GetV2().Equals(v))
                {
                    if (c.GetV1().EstAssigne())
                    {
                        if (c.GetO().Equals(ContrainteBinaire.Operateur.NOT_EQUAL))
                        {
                            if(!(v.GetValeur() != c.GetV1().GetValeur()))
                            {
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public List<Variable> GetVoisins(Variable v)
        {
            List<Variable> voisins = new List<Variable>();
            List<ContrainteBinaire> contraintes = this.GetContraintesVariable(v);

            foreach(ContrainteBinaire cb in contraintes)
            {
                if (cb.Contient(v))
                {
                    if (cb.GetV1().Equals(v))
                    {
                        voisins.Add(cb.GetV2());
                    }
                    else if (cb.GetV2().Equals(v))
                    {
                        voisins.Add(cb.GetV1());
                    }
                }
            }

            return voisins;
        }

    }
}
