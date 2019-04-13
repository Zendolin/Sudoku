using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class Search
    {
        private CSP csp;
        private Grille grille;
        private int nbInitialVariableNonAssigne;

        public Search(CSP csp, Grille grille)
        {
            this.csp = csp;
            this.nbInitialVariableNonAssigne = this.csp.GetVariableNonAssigne().Count;
            this.grille = grille;
        }

        /* Backtracking Search */
        public List<Variable> BacktrackingSearch(Func<Variable> heuristique, Func<Variable,List<int>> domaine)
        {
            List<Variable> assignement = new List<Variable>();

            return RecursiveBacktracking(assignement, heuristique, domaine);
        }

        /* Recursive Backtracking */
        public List<Variable> RecursiveBacktracking(List<Variable> assignement, Func<Variable> heuristique, Func<Variable, List<int>> domaine)
        {
            if (estAssigneCompletConsistant(assignement))
            {
                Console.WriteLine("\nAssignement complet");
                AfficheAssignement(assignement);
                grille.AfficheEnCouleur();
                return assignement;
            }

            if (this.csp.GetVariableNonAssigne().Count > 0)
            {
                Variable v = heuristique();
                if(domaine != null)
                {
                    v.SetDomaine(domaine(v));
                }
                

                foreach (int valeur in v.GetDomaine())
                {
                    v.SetAssigne(true);
                    v.SetValeur(valeur);

                    if (this.csp.RespecteContraintes(v))
                    {
                        assignement.Add(v);
                        List<Variable> tmp = RecursiveBacktracking(assignement, heuristique, domaine);
                        if (tmp == null)
                        {
                            assignement.Remove(v);
                            v.SetAssigne(false);
                            v.SetValeur(0);
                        }
                        else
                        {
                            return tmp;
                        }
                    }
                    else
                    {
                        v.SetAssigne(false);
                        v.SetValeur(0);
                    }


                }
            }

            return null;
        }

        public bool estAssigneCompletConsistant(List<Variable> assignement)
        {
            if(assignement.Count != nbInitialVariableNonAssigne)
            {
                return false;
            }
            else
            {
                foreach(Variable v in assignement)
                {
                    if (!v.EstAssigne())
                    {
                        return false;
                    }
                }
            }

            return true;

        }

        public void AfficheAssignement(List<Variable> assignement)
        {
            String message = "[";

            foreach(Variable v in assignement)
            {
                message += String.Format("{0}={1},", v.GetNom(), v.GetValeur());
            }

            message += "]";
            Console.WriteLine(message + "\n");
        }

        /* Arc Consistency - AC3*/
        public void AC3()
        {
            List<Arc> queue = GenerationQueueArc();

            while(queue.Count > 0)
            {
                Arc arc = SupprimePremierArc(queue);

                if(arc != null)
                {
                    if(SupprimerValeursInconsistantes(arc.GetV1(), arc.GetV2()))
                    {
                        List<Variable> voisins = this.csp.GetVoisins(arc.GetV1());
                        foreach(Variable vo in voisins)
                        {
                            if (!vo.EstAssigne())
                            {
                                queue.Add(new Arc(vo, arc.GetV1()));
                            }
                        }
                    }
                }
            }

        }

        /* Génère tous les arcs et les placent dans une queue */
        public List<Arc> GenerationQueueArc()
        {
            List<Arc> arcs = new List<Arc>();
            List<Variable> variablesNonAssigne = this.csp.GetVariableNonAssigne();
            

            foreach(Variable v in variablesNonAssigne)
            {
                List<Variable> voisins = this.csp.GetVoisins(v);

                foreach(Variable vo in voisins)
                {
                    arcs.Add(new Arc(v, vo));
                }
            }
            

            return arcs;
        }

        /* Retourne et supprime le premier arc de la queue */
        public Arc SupprimePremierArc(List<Arc> queue)
        {
            if(queue.Count == 0)
            {
                return null;
            }

            Arc a = queue[0];

            queue.RemoveAt(0);

            return a;
        }

        /* Supprime les valeurs inconsistantes */
        public bool SupprimerValeursInconsistantes(Variable v1, Variable v2)
        {
            bool supprime = false;

            ContrainteBinaire cb = this.csp.GetContrainteVariables(v1, v2);

            if(cb != null)
            {
                List<int> tDomaine = new List<int>(v1.GetDomaine());
                foreach (int x in tDomaine)
                {
                    v1.SetAssigne(true);
                    v1.SetValeur(x);

                    bool satisfy = false;

                    foreach (int y in v2.GetDomaine())
                    {
                        if (!v2.EstAssigne())
                        {
                            v2.SetAssigne(true);
                            v2.SetValeur(y);

                            if (cb.estRespecte())
                            {
                                satisfy = true;
                            }

                            v2.SetAssigne(false);
                            v2.SetValeur(0);
                        }
                        else
                        {
                            if (cb.estRespecte())
                            {
                                satisfy = true;
                            }
                        }
     
                    }

                    if (!satisfy)
                    {
                        v1.SupprimerValeurDomaine(x);
                        supprime = true;
                    }

                    v1.SetAssigne(false);
                    v1.SetValeur(0);
                }
            }

            return supprime;
        }

        /* Minimum Remaining Value */
        public Variable MRV()
        {
            int min = 200;
            Variable vMRV = null;

            List<Variable> variables = this.csp.GetVariables();

            foreach (Variable v in variables)
            {
                if (!v.EstAssigne())
                {
                    int tDomaineCount = v.GetDomaine().Count;
                    if (tDomaineCount < min)
                    {
                        vMRV = v;

                        min = tDomaineCount;
                    }
                }
            }

            return vMRV;
        }

        /* Minimum Remaining ValueS And Degree Heuristic */
        public Variable MRVSAndDH()
        {
            return DH(MRVS());
        }

        /* Minimum Remaining ValueS */
        public List<Variable> MRVS()
        {
            int min = 200;
            List<Variable> vMRVS = new List<Variable>();
            
            List<Variable> variables = this.csp.GetVariables();

            foreach (Variable v in variables)
            {
                if (!v.EstAssigne())
                {
                    int tDomaineCount = v.GetDomaine().Count;
                    if (tDomaineCount <= min)
                    {
                        if (tDomaineCount < min)
                        {
                            vMRVS.Clear();
                            vMRVS.Add(v);
                        }

                        if (tDomaineCount == min)
                        {
                            vMRVS.Add(v);
                        }

                        min = tDomaineCount;

                    }
                }
            }


            return vMRVS;
        }

        /* Degree Heuristic */
        public Variable DH(List<Variable> variables)
        {
            Variable dh = null;

            int max = -1;

            foreach(Variable v in variables)
            {
                int tNbContraintes = this.csp.GetContraintesVariablesNonAssignees(v).Count;
                if(tNbContraintes > max)
                {
                    dh = v;
                    max = tNbContraintes;
                }

            }

            return dh;
        }


        /* Least Contraining Values */
        /* Renvoie un domaine trié par valeur le moins contraignante*/
        public List<int> LCV(Variable v)
        {
            List<Variable> voisins = this.csp.GetVoisins(v);
            List<int> tDomaine = new List<int>();

            int[,] nbIDomaine = new int[v.GetDomaine().Count, 2];
            int count = 0;

            int k = 0;
            foreach(int i in v.GetDomaine())
            {
                count = 0;
                foreach (Variable vo in voisins)
                {
                    if (vo.GetDomaine().Contains(i))
                    {
                        count++;
                    }
                }

                nbIDomaine[k, 0] = i;
                nbIDomaine[k, 1] = count;
                k++;
            }

            /* Tri le domaine de la variable */
            for(int i = 0; i < v.GetDomaine().Count - 1; i++)
            {
                for(int j = 0; j < v.GetDomaine().Count - 1 - i; j++)
                {
                    if(nbIDomaine[j, 1] > nbIDomaine[j+1, 1])
                    {
                        int tmpD = nbIDomaine[j, 0];
                        int tmpC = nbIDomaine[j, 1];

                        nbIDomaine[j, 0] = nbIDomaine[j + 1, 0];
                        nbIDomaine[j, 1] = nbIDomaine[j + 1, 1];

                        nbIDomaine[j + 1, 0] = tmpD;
                        nbIDomaine[j + 1, 1] = tmpC;
                    }
                }
            }

            for(int i = 0;  i < v.GetDomaine().Count; i++)
            {
                tDomaine.Add(nbIDomaine[i, 0]);
            }

            return tDomaine;
        }

    }
}
