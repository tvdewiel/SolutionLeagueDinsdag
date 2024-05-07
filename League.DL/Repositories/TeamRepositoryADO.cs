using League.BL.Interfaces;
using League.BL.Model;
using League.DL.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace League.DL.Repositories
{
    public class TeamRepositoryADO : ITeamRepository
    {
        private string connectionString;

        public TeamRepositoryADO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool BestaatTeam(Team t)
        {
            string query = "SELECT count(*) FROM dbo.Team WHERE stamnummer=@stamnummer";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                try
                {
                    command.Parameters.AddWithValue("@stamnummer",t.Stamnummer);
                    int n = (int)command.ExecuteScalar();
                    if (n > 0) return true;
                    else return false;
                }
                catch (Exception ex)
                {
                    throw new ADORepositoryException("BestaatTeam", ex);
                }                
            }
        }
        public void SchrijfTeamInDB(Team t)
        {
            string query = "INSERT INTO team(stamnummer,naam,bijnaam) VALUES(@stamnummer,@naam,@bijnaam)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@stamnummer", t.Stamnummer);
                    command.Parameters.AddWithValue("@naam", t.Naam);                   
                    if (t.Bijnaam==null) command.Parameters.AddWithValue("@bijnaam", t.Bijnaam);
                    else command.Parameters.AddWithValue("@bijnaam", DBNull.Value);
                    command.CommandText = query;
                    command.ExecuteNonQuery();                   
                }
                catch (Exception ex) { throw new ADORepositoryException("SchrijfSpelerinDB", ex); }
            }
        }

        public Team SelecteerTeam(int stamnummer)
        {
            Team team=null;
            string sql = "select t1.Stamnummer,t1.naam as ploegnaam,t1.bijnaam,t2.* from team t1"
                +" left join speler t2 on t1.Stamnummer=t2.TeamId where stamnummer=@stamnummer";
            using(SqlConnection connection = new SqlConnection(connectionString))
            using(SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.Parameters.AddWithValue("@stamnummer", stamnummer);
                connection.Open();
                IDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    if (team == null)
                    {
                        string naam = (string)reader["ploegnaam"];
                        string bijnaam = null;
                        if (!reader.IsDBNull(reader.GetOrdinal("bijnaam"))) bijnaam=(string)reader["bijnaam"];
                        team = new Team(stamnummer, naam);
                        if (bijnaam!=null) team.ZetBijnaam(bijnaam);
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("id")))
                    {
                        //speler toevoegen
                        int? lengte = null;
                        if (!reader.IsDBNull(reader.GetOrdinal("lengte"))) lengte = (int)reader["lengte"];
                        int? gewicht = null;
                        if (!reader.IsDBNull(reader.GetOrdinal("gewicht"))) gewicht = (int)reader["gewicht"];
                        Speler s = new Speler((int)reader["id"], (string)reader["naam"], lengte, gewicht);
                        s.ZetTeam(team);
                        if (!reader.IsDBNull(reader.GetOrdinal("rugnummer")))
                            s.ZetRugnummer((int)reader["rugnummer"]);
                    }
                }
                reader.Close();
                return team;
            }
        }
    }
}
