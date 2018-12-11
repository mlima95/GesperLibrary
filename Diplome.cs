using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GesperLibrary
{
    public class Diplome
    {
        int id;
        string libelle;
        List<Employe> lesEmployes;

        public Diplome(int id, string libelle)
        {
            this.id = id;
            this.libelle = libelle;
            lesEmployes = new List<Employe>();
            
        }
        public string ToString()
        {
            return string.Format("id:{0},libelle:{1}", id, libelle);
        }

        public int Id
        {
            get
            {
                return this.id;
            }
        }

        public string AfficherEmploye()
        {
            string s = "";
            foreach (Employe e in lesEmployes)
            {
                s += string.Format("id:{0},nom:{1},prenom:{2},sexe:{3},cadre:{4},salaire:{5},service:{6}", id, e.Nom, e.Prenom, e.Sexe, e.Cadre, e.Salaire, e.LeService.Id);
            }
            return s += "\n";
        }

        public List<Employe> LesEmployes
        {
            get { return this.lesEmployes; }
        }

        public string Libelle
        {
            get { return this.libelle; }

        }

        public void AddEmployeDiplome(Employe e)
        {
            lesEmployes.Add(e);
        }

    }
}
