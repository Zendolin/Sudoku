using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class GenerateurCB
    {
        List<ContrainteBinaire> contraintes;
        Case[,] cases;

        public GenerateurCB(Case[,] cases)
        {
            this.cases = cases;
            this.contraintes = new List<ContrainteBinaire>();
        }

        public void Generer()
        {
            for(int i = 0; i < Grille.NB_CASE_PAR_LIGNE; i++)
            {
                for(int j = 0; j < Grille.NB_CASE_PAR_LIGNE; j++)
                {
                    if(!cases[i, j].GetVariable().EstAssigne())
                    {
                        ContrainteBinaire cb;

                        //Contrainte sur la ligne
                        for(int k = 0; k < Grille.NB_CASE_PAR_LIGNE; k++)
                        {
                            if(k != i)
                            {
                                cb = new ContrainteBinaire(cases[i, j].GetVariable(), cases[k, j].GetVariable(), ContrainteBinaire.Operateur.NOT_EQUAL);
                                if (!contraintes.Contains(cb))
                                {
                                    contraintes.Add(cb);
                                }
                                    
                            }
                        }

                        //Contrainte sur la colonne
                        for(int k = 0; k < Grille.NB_CASE_PAR_LIGNE; k++)
                        {
                            if(k != j)
                            {
                                cb = new ContrainteBinaire(cases[i, j].GetVariable(), cases[i, k].GetVariable(), ContrainteBinaire.Operateur.NOT_EQUAL);
                                if (!contraintes.Contains(cb))
                                {
                                    contraintes.Add(cb);
                                }
                                
                            }
                        }

                        //Contrainte sur la région
                        int tmpI = i - (i % 3);
                        int tmpJ = j - (j % 3);
                        for(int k = tmpI; k < tmpI + Grille.NB_CASE_PAR_REGION ; k++)
                        {
                            for(int l = tmpJ; l < tmpJ + Grille.NB_CASE_PAR_REGION ; l++)
                            {
                                if(i != k && j != l)
                                {
                                    cb = new ContrainteBinaire(cases[i, j].GetVariable(), cases[k, l].GetVariable(), ContrainteBinaire.Operateur.NOT_EQUAL);
                                    if (!contraintes.Contains(cb))
                                    {
                                        contraintes.Add(cb);
                                    }
                                }
                            }
                        } 


                    }
                }
            }
        }

        public List<ContrainteBinaire> GetContraintes()
        {
            return this.contraintes;
        }

    }
}
