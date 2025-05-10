using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
//DOTNET RUN POUR DEMARER SUR VSCODE
namespace CMD_DEMO_BDD
{
    class C_PERSONNE
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public override string ToString()
        {
            return $"{Id,4}, {Nom,-32}, {Prenom,-32}";
        }
    }

    class Program
    {
        const string CHAINE_CONNECTION = "Data Source=192.168.62.170;Initial Catalog=csharp;Integrated Security=False;User ID=Azmog;Password=Marmar2005; TrustServerCertificate=True;";

        static List<C_PERSONNE> Donne_Liste_Personne(SqlConnection P_Bdd)
        {
            List<C_PERSONNE> Liste_Personnes = new List<C_PERSONNE>();

            SqlCommand La_Requete = new SqlCommand("select * from Personnes", P_Bdd);
            SqlDataReader La_Reponse = La_Requete.ExecuteReader();
            while (La_Reponse.Read() == true)
            {
                C_PERSONNE Une_Personne = new C_PERSONNE()
                {
                    Id = (int)La_Reponse[0],
                    Nom = (string)La_Reponse[1],
                    Prenom = (string)La_Reponse[2],
                };
                Liste_Personnes.Add(Une_Personne);
            }

            return Liste_Personnes;
        }

        static void Ajoute_Personne(string P_Nom, string P_Prenom, SqlConnection P_Bdd)
        {
            SqlCommand La_Requete_Ajout = new SqlCommand(
                $"insert into Personnes(Nom, Prenom) values('{P_Nom}' '{P_Prenom}')", P_Bdd);
            La_Requete_Ajout.ExecuteNonQuery();
        }

        static void Main(string[] args)
        {
            using SqlConnection Bdd = new SqlConnection(CHAINE_CONNECTION);
            Bdd.Open();

            // Ajoute_Personne("Adam", "Marzuk", Bdd);

            List<C_PERSONNE> Les_Personnes = Donne_Liste_Personne(Bdd);
            foreach (var Un_Element in Les_Personnes)
            {
                Console.WriteLine(Un_Element);
            }

            Bdd.Close();
        }
    }
}
