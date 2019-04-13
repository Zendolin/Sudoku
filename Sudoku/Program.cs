using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Sudoku
{
    class Program
    {   
        public static void AfficheListeVariables(List<Variable> variables)
        {
            foreach(Variable v in variables)
            {
                Console.WriteLine(v);
            }
        }

        public static void AfficheListeContraintes(List<ContrainteBinaire> contraintes)
        {
            foreach (ContrainteBinaire cb in contraintes)
            {
                Console.WriteLine(cb);
            }
        }

        public static void AfficheDomaine(List<int> domaine)
        {
            foreach(int i in domaine)
            {
                Console.Write(String.Format("{0},", i));
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            int choix = 0;
            String fichier = "";
            Grille g = new Grille();
            Parser p = new Parser("");

            Console.WriteLine("Bienvenue.\nVous êtes dans le Résolveur de Sudoku par CSP.");
            Console.WriteLine("Tout d'abord, veuillez sélectionner un sudoku.\n");

            do
            {
                Console.WriteLine("Vous avez le choix entre choisir un fichier qui contient le Sudoku (1) ou bien le rentrer dans la console (2).");
                try
                {
                    choix = int.Parse(Console.ReadLine());
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    Console.WriteLine("Appuyez sur une touche pour quitter.");
                    Console.ReadKey();
                    return;
                }
                
            } while (choix != 1 && choix != 2);


            if (choix == 1)
            {
                Console.WriteLine("Veuillez-vous assurez que le fichier est situé dans le dossier du projet.\nVeuillez entrer le nom du fichier texte contenant le Sudoku (sans l'extension):");
                fichier = Console.ReadLine();
                p = new Parser(String.Format("..//..//{0}.txt", fichier));
                p.Lecture();
            }
            else if(choix == 2)
            {
                p.Saisie();
            }

            g = p.GetGrille();


            Console.WriteLine("Voici la grille.");
            g.Affiche();

            GenerateurCB gcb = new GenerateurCB(g.GetCases());
            gcb.Generer();

            CSP csp = new CSP(p.GetVariables(), gcb.GetContraintes(), Grille.POSSIBILITE);

            Search s = new Search(csp, g);

            Func<Variable> firstUnassigned = new Func<Variable>(csp.GetFirstVariableNonAssigne);
            Func<Variable> mrv = new Func<Variable>(s.MRV);
            Func<Variable> mrvdh = new Func<Variable>(s.MRVSAndDH);

            Func<Variable, List<int>> lcv = new Func<Variable, List<int>>(s.LCV);

            Console.WriteLine("Nous allons maintenant choisir les différentes heuristiques.");

            do
            {
                Console.WriteLine("Voulez-vous utilisez AC-3 ? (Oui - 1, Non - 2)");
                try
                {
                    choix = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    Console.WriteLine("Appuyez sur une touche pour quitter.");
                    Console.ReadKey();
                    return;
                }
            } while(choix != 1 && choix != 2);

            if(choix == 1)
            {
                s.AC3();
            }

            do
            {
                Console.WriteLine("Choissisez l'heuristique de variable que vous voulez utiliser :\n" +
                    " - Premère variable non assignée (1)\n - Minimum Remaining Value (2)\n - Minimum Remaining Value + Degree Heuristique (3)");
                try
                {
                    choix = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    Console.WriteLine("Appuyez sur une touche pour quitter.");
                    Console.ReadKey();
                    return;
                }
            } while (choix != 1 && choix != 2 && choix != 3);


            Func<Variable> hvb;
            switch (choix)
            {
                case (1):
                    hvb = firstUnassigned;
                    break;
                case (2):
                    hvb = mrv;
                    break;
                case (3):
                    hvb = mrvdh;
                    break;
                default:
                    hvb = firstUnassigned;
                    break;
            }

            do
            {
                Console.WriteLine("Voulez-vous utiliser Least Constraining Value ? (Oui - 1, Non - 2)");

                try
                {
                    choix = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    Console.WriteLine("Appuyez sur une touche pour quitter.");
                    Console.ReadKey();
                    return;
                }
            } while (choix != 1 && choix != 2);


            Func<Variable, List<int>> hva = null;
            if (choix == 1)
            {
                hva = lcv;
            }

            Console.WriteLine("Tout est prêt.\nAppuyez sur une touche pour lancer la recherche.");
            Console.ReadKey();

            s.BacktrackingSearch(hvb, hva);

            Console.WriteLine("Appuyez sur une touche pour quitter.");
            Console.ReadKey();
        }
    }
}
