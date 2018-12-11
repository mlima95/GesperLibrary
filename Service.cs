using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GesperLibrary
{
    public class Service
    {
        double budget;
        int capacite, id;
        string designation, produit;
        char type;
        List<Employe> lesEmployesDuService = new List<Employe>();

        public Service(int id, string designation, char type, string produit, int capacite)
        {

            this.id = id;
            this.produit = produit;
            this.designation = designation;
            this.type = type;
            this.capacite = capacite;
        }

        public Service(int id, string designation, char type, double budget)
        {

            this.id = id;
            this.designation = designation;
            this.budget = budget;
            this.type = type;

        }

        public string ToString()
        {
            return string.Format("id:{0},designation:{1},type:{2},produit:{3},capacite:{4},budget:{5}", id, designation, type, produit, capacite, budget);
        }

        public string AfficherEmploye()
        {
            string s = "";
            foreach (Employe e in LesEmployesDuService)
            {
                s += string.Format("id:{0},nom:{1},prenom:{2},sexe:{3},cadre:{4},salaire:{5},service:{6}", id, e.Nom, e.Prenom, e.Sexe, e.Cadre, e.Salaire, e.LeService.Id);
            }
            return s += "\n";
        }

        public double Budget
        {
            get { return this.budget; }
        }

        public int Capacite
        {
            get { return this.capacite; }
        }
        public string Designation
        {
            get { return this.designation; }
        }

        public int Id
        {
            get { return this.id; }
        }

        public List<Employe> LesEmployesDuService
        {
            get { return this.lesEmployesDuService; }
        }

        public string Produit
        {
            get { return this.produit; }
        }


        public char Type
        {
            get { return this.type; }
        }

        public void AddEmployeService(Employe e)
        {
            LesEmployesDuService.Add(e);
        }
    }
}
