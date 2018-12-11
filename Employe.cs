using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GesperLibrary
{
    public class Employe
    {
        byte cadre;
        int id;
        string nom, prenom;
        char sexe;
        double salaire;
        List<Diplome> lesDiplomes;
        Service leService;

        public Employe(int id, string nom, string prenom, char sexe, byte cadre, double salaire, Service service)
        {
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
            this.sexe = sexe;
            this.cadre = cadre;
            this.salaire = salaire;
            this.leService = service;
            lesDiplomes = new List<Diplome>();
        }
        public string ToString()
        {
            return string.Format("id:{0},nom:{1},prenom:{2},sexe:{3},cadre:{4},salaire:{5},service:{6}", id, nom, prenom, sexe, cadre, salaire, this.leService.Id);
        }

        public string AfficherDiplome()
        {
            string s = "" + this.Nom + " " + this.Prenom + " :\n";
            foreach (Diplome d in lesDiplomes)
            {
                s += string.Format("id :{0} libelle : {1}\n", d.Id, d.Libelle);
            }
            return s += "\n";
        }
        public int CountDiplome()
        {
            return lesDiplomes.Count();
        }

        public Service Service
        {
            get { return this.LeService; }
        }
        public byte Cadre
        {
            get { return this.cadre; }
        }

        public int Id
        {
            get { return this.id; }
        }

        public List<Diplome> LesDiplomes
        {
            get
            {
                return this.lesDiplomes;
            }
        }

        public Service LeService
        {
            get { return this.leService; }
        }

        public string Nom
        {
            get { return this.nom; }
        }

        public string Prenom
        {
            get { return this.prenom; }
        }

        public double Salaire
        {
            get { return this.salaire; }
        }

        public char Sexe
        {
            get { return this.sexe; }
        }

        public void AddDiplomeEmploye(Diplome d)
        {
            lesDiplomes.Add(d);
        }
    }
}
