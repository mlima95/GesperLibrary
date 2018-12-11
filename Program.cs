using GesperLibrary;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GesperConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Donnees donnee = new Donnees();
            string s = "";
            donnee.ToutCharger();
            s=donnee.Afficher();
            donnee.Sauvegarder();
            //donnee.ChargerServices();
            //donnee.ChargerDiplomes();
            //donnee.ChargerEmployes();
            //s = donnee.AfficherServices();     
            //s += donnee.AfficherDiplomes();
            //s += donnee.AfficherEmployes();

            Console.WriteLine(s);
            Console.ReadLine();
        }
    }
}

