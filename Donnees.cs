using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace GesperLibrary
{
    public class Donnees
    {
        List<Diplome> LesDiplomes;
        List<Employe> LesEmployes;
        List<Service> LesServices;

        public Donnees()
        {
            LesDiplomes = new List<Diplome>();
            LesEmployes = new List<Employe>();
            LesServices = new List<Service>();
        }



        public string AfficherServices()
        {
            string sService = "";

            foreach (Service service in LesServices)
            {
                sService += service.ToString() + "\n";
            }
            return sService += "\n";
        }

        public string AfficherDiplomes()
        {
            string sDiplome = "";

            foreach (Diplome diplome in LesDiplomes)
            {
                sDiplome += diplome.ToString() + "\n";
            }

            return sDiplome += "\n";
        }

        public string AfficherEmployes()
        {
            string sEmploye = "";

            foreach (Employe employe in LesEmployes)
            {
                sEmploye += employe.ToString() + "\n";
            }
            return sEmploye += "\n";
        }

        public void ChargerDiplomes()
        {
            MySqlConnection cnx = ConnexionBDD();
            cnx.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cnx;
            cmd.CommandText = "diplome";
            cmd.CommandType = CommandType.TableDirect;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Diplome dm = new Diplome((int)reader[0], (string)reader[1]);
                LesDiplomes.Add(dm);
            }
            cnx.Close();

        }

        public void ChargerEmployes()
        {
            MySqlConnection cnx = ConnexionBDD();
            cnx.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cnx;
            cmd.CommandText = "employe";
            cmd.CommandType = CommandType.TableDirect;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                Employe ep = new Employe(Convert.ToInt32(reader[0]), Convert.ToString(reader[1]), Convert.ToString(reader[2]), Convert.ToChar(reader[3]), Convert.ToByte(reader[4]), Convert.ToDouble(reader[5]), LesServices[(int)reader[6] - 1]);
                LesEmployes.Add(ep);

            }
            cnx.Close();
        }

        public void ChargerServices()
        {
            MySqlConnection cnx = ConnexionBDD();
            cnx.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cnx;
            cmd.CommandText = "service";
            cmd.CommandType = CommandType.TableDirect;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (reader.GetString(2) == "P")
                {
                    Service sv = new Service(Int32.Parse(reader.GetString(0)), reader.GetString(1), Char.Parse(reader.GetString(2)), reader.GetString(3), Int32.Parse(reader.GetString(4)));
                    LesServices.Add(sv);
                }
                else if (reader.GetString(2) == "A")

                {
                    Service sv = new Service(Int32.Parse(reader.GetString(0)),
                        reader.GetString(1), Char.Parse(reader.GetString(2)),
                         Double.Parse(reader.GetString(5)));

                    LesServices.Add(sv);
                }
            }
        }

        public string Afficher()
        {
            string s = "";
            s += "Services :" + "\n";
            s+=this.AfficherServices();
            s += "Employe :" + "\n";
            s+=this.AfficherEmployes();
            s += "Diplomes :" + "\n";
            s+= this.AfficherDiplomes();

            s += "LesDiplomes des Employes :"+ "\n";
            foreach (Employe e in LesEmployes)
            {

                s+=e.AfficherDiplome()+ "\n";

            }

            s += "Les Employes des Diplomes :" + "\n";
            foreach (Diplome d in LesDiplomes)
            {
                s += d.AfficherEmploye() + "\n";
            }

            s += "Les Employes des Services :" + "\n";
            foreach(Service sS in LesServices)
            {
                s += sS.AfficherEmploye() + "\n";
            }
            return s;
        }
        public void ToutCharger()
        {
            this.ChargerDiplomes();
            this.ChargerServices();
            this.ChargerEmployes();
            this.ChargerLesDiplomesDesEmployes();
            this.ChargerLesEmployesDesServices();
            this.ChargerLesEmployeTitulaireDesDiplomes();
        }

        public void ChargerLesEmployesDesServices()
        {
            foreach (Service service in LesServices)
            {
                MySqlConnection cnx = ConnexionBDD();
                cnx.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cnx;
                cmd.CommandText = "select * from employe where emp_service=@id";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@id", MySqlDbType.Int32);
                cmd.Parameters["@id"].Direction = ParameterDirection.Input;
                cmd.Parameters["@id"].Value = service.Id;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    foreach (Employe employe in LesEmployes)
                    {

                        if (employe.Id == (int)reader["emp_id"])
                        {
                            service.AddEmployeService(employe);
                        }
                    }
                }
            }
        }


        public void ChargerLesDiplomesDesEmployes()
        {
            foreach (Employe employe in LesEmployes)
            {
                MySqlConnection cnx = ConnexionBDD();
                cnx.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cnx;
                cmd.CommandText = "select * from posseder where pos_employe=@id";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@id", MySqlDbType.Int32);
                cmd.Parameters["@id"].Direction = ParameterDirection.Input;
                cmd.Parameters["@id"].Value = employe.Id;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    foreach (Diplome diplome in LesDiplomes)
                    {

                        if (diplome.Id == (int)reader["pos_diplome"])
                        {
                            employe.AddDiplomeEmploye(diplome);
                        }
                    }
                }
            }
        }


        public void ChargerLesEmployeTitulaireDesDiplomes()
        {
            foreach (Diplome d in LesDiplomes)
            {
                MySqlConnection cnx = ConnexionBDD();
                cnx.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cnx;
                cmd.CommandText = "select * from posseder where pos_diplome=@id";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@id", MySqlDbType.Int32);
                cmd.Parameters["@id"].Direction = ParameterDirection.Input;
                cmd.Parameters["@id"].Value = d.Id;
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    foreach (Employe employe in LesEmployes)
                    {
                        if (employe.Id == (int)reader["pos_employe"])
                        {
                            d.AddEmployeDiplome(employe);
                        }
                    }
                }
            }
        }

        public void Sauvegarder()
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection cnx = ConnexionBDD();
            cmd.Connection = cnx;
            cnx.Open();
            cmd.CommandText = "RemiseAZero";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            cmd.CommandText = "insert into service (ser_id,ser_designation,ser_type,ser_produit,ser_capacite,ser_budget) values(@id,@designation,@type,@produit,@capacite,@budget)";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@id", MySqlDbType.Int32);
            cmd.Parameters.Add("@designation", MySqlDbType.String);
            cmd.Parameters.Add("@type", MySqlDbType.VarChar);
            cmd.Parameters.Add("@produit", MySqlDbType.String);
            cmd.Parameters.Add("@capacite", MySqlDbType.Int32);
            cmd.Parameters.Add("@budget", MySqlDbType.Int32);
            foreach (Service s in LesServices)
            {
                if (s.Type == 'P')
                {
                    cmd.Parameters["@id"].Value = s.Id;
                    cmd.Parameters["@designation"].Value = s.Designation;
                    cmd.Parameters["@type"].Value = s.Type;
                    cmd.Parameters["@produit"].Value = s.Produit;
                    cmd.Parameters["@capacite"].Value = s.Id;
                    cmd.Parameters["@budget"].Value = 0;
                }
                else if (s.Type == 'A')
                {
                    cmd.Parameters["@id"].Value = s.Id;
                    cmd.Parameters["@designation"].Value = s.Designation;
                    cmd.Parameters["@type"].Value = s.Type;
                    cmd.Parameters["@budget"].Value = s.Budget;
                    cmd.Parameters["@produit"].Value = "";
                    cmd.Parameters["@capacite"].Value = 0;
                }
                cmd.ExecuteNonQuery();
            }
            cnx.Close();
            MySqlCommand cmd1 = new MySqlCommand();
            cmd1.Connection = cnx;
            cmd1.Connection.Open();
            cmd1.CommandText = "insert into employe (emp_id,emp_nom,emp_prenom,emp_sexe,emp_cadre,emp_salaire,emp_service) values(@id,@nom,@prenom,@sexe,@cadre,@salaire,@service)";
            cmd1.CommandType = CommandType.Text;
            cmd1.Parameters.Add("@id", MySqlDbType.Int32);
            cmd1.Parameters.Add("@nom", MySqlDbType.String);
            cmd1.Parameters.Add("@prenom", MySqlDbType.String);
            cmd1.Parameters.Add("@sexe", MySqlDbType.VarChar);
            cmd1.Parameters.Add("@cadre", MySqlDbType.Bit);
            cmd1.Parameters.Add("@salaire", MySqlDbType.Int32);
            cmd1.Parameters.Add("@service", MySqlDbType.Int32);
            foreach (Employe e in LesEmployes)
            {
                cmd1.Parameters["@id"].Value = e.Id;
                cmd1.Parameters["@nom"].Value = e.Nom;
                cmd1.Parameters["@prenom"].Value = e.Prenom;
                cmd1.Parameters["@sexe"].Value = e.Sexe;
                cmd1.Parameters["@cadre"].Value = e.Cadre;
                cmd1.Parameters["@salaire"].Value = e.Salaire;
                cmd1.Parameters["@service"].Value = e.Service.Id;
                cmd1.ExecuteNonQuery();
            }
            cnx.Close();
            MySqlCommand cmd2 = new MySqlCommand();
            cmd2.Connection = cnx;
            cmd2.Connection.Open();
            cmd2.CommandText = "insert into diplome (dip_id,dip_libelle) values(@id,@libelle)";
            cmd2.CommandType = CommandType.Text;
            cmd2.Parameters.Add("@id", MySqlDbType.Int32);
            cmd2.Parameters.Add("@libelle", MySqlDbType.String);
            foreach (Diplome d in LesDiplomes)
            {
                cmd2.Parameters["@id"].Value = d.Id;
                cmd2.Parameters["@libelle"].Value = d.Libelle;
                cmd2.ExecuteNonQuery();
            }
            cnx.Close();
            MySqlCommand cmd3 = new MySqlCommand();
            cmd3.Connection = cnx;
            cmd3.Connection.Open();
            cmd3.CommandText = "insert into posseder (pos_diplome,pos_employe) values(@diplome,@employe)";
            cmd3.CommandType = CommandType.Text;
            cmd3.Parameters.Add("@diplome", MySqlDbType.Int32);
            cmd3.Parameters.Add("@employe", MySqlDbType.Int32);
            foreach (Employe e in LesEmployes)
            {
                for (int i = 0; i < e.CountDiplome(); i++)
                {
                    cmd3.Parameters["@diplome"].Value = e.LesDiplomes[i].Id;
                    cmd3.Parameters["@employe"].Value = e.Id;
                    cmd3.ExecuteNonQuery();
                }
            }
            cnx.Close();
        }


        public MySqlConnection ConnexionBDD()
        {
            MySqlConnection cnx;
            string sCnx;
            sCnx = string.Format("user=root;password=;host=localhost;database=gesper");
            cnx = new MySqlConnection(sCnx);
            return cnx;
        }
    }
}
