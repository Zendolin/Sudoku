using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class Variable
    {
        private String nom;
        private Boolean assigne;
        private int valeur;
        private List<int> domaine = new List<int>(Grille.POSSIBILITE);

        public Variable(String nom)
        {
            this.nom = nom;
            this.assigne = false;
            valeur = 0;
        }
        public Variable(String nom, Boolean assigne, int valeur)
        {
            this.nom = nom;
            this.assigne = assigne;
            this.valeur = valeur;

            if (this.assigne)
            {
                List<int> tDomaine = new List<int>();
                tDomaine.Add(this.valeur);
                this.domaine = tDomaine;
            }
        }

        public Boolean EstAssigne()
        {
            return this.assigne;
        }

        public void SetAssigne(bool assigne)
        {
            this.assigne = assigne;
        }

        public String GetNom()
        {
            return this.nom;
        }

        public int GetValeur()
        {
            return this.valeur;
        }

        public void SetValeur(int valeur)
        {
            this.valeur = valeur;
        }

        public override string ToString()
        {
            String sDomaine = "{";

            foreach(int x in this.domaine)
            {
                sDomaine += x + ",";
            }
            sDomaine += "}";

            return String.Format("Variable {0}, assigné : {1}, valeur = {2}, domaine = {3}", this.nom, this.assigne, this.valeur, sDomaine);
        }

        public List<int> GetDomaine()
        {
            return this.domaine;
        }

        public void SetDomaine(List<int> domaine)
        {
            this.domaine = domaine;
        }

        public void SupprimerValeurDomaine(int valeur)
        {
            this.domaine.Remove(valeur);
        }
    }
}
