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
        static void Main(string[] args)
        {
            Parser p = new Parser("..//..//sudoku1.txt");
            p.Lecture();
            Grille g = p.GetGrille();
            g.Affiche();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
