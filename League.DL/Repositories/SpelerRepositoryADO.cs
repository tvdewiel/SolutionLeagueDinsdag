using League.BL.DTO;
using League.BL.Interfaces;
using League.BL.Model;
using League.DL.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace League.DL.Repositories
{
    public class SpelerRepositoryADO : ISpelerRepository
    {
        private string connectionString;

        public SpelerRepositoryADO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool BestaatSpeler(Speler s)
        {
            string query = "SELECT count(*) FROM dbo.Speler WHERE naam=@naam";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                try
                {
                    command.Parameters.Add(new SqlParameter("@naam", SqlDbType.NVarChar));
                    command.CommandText = query;
                    command.Parameters["@naam"].Value = s.Naam;
                    int n = (int)command.ExecuteScalar();
                    if (n > 0) return true;
                    else return false;
                }
                catch (Exception ex)
                {
                    throw new ADORepositoryException("BestaatSpeler", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public Speler SchrijfSpelerInDB(Speler s)
        {
            string query = "INSERT INTO Speler(naam,lengte,gewicht) output INSERTED.ID VALUES(@naam,@lengte,@gewicht)";
            using(SqlConnection connection = new SqlConnection(connectionString))
            using(SqlCommand command = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@naam", s.Naam);
                    if (s.Lengte.HasValue) command.Parameters.AddWithValue("@lengte", s.Lengte);
                    else command.Parameters.AddWithValue("@lengte", DBNull.Value);
                    if (s.Gewicht.HasValue) command.Parameters.AddWithValue("@gewicht", s.Gewicht);
                    else command.Parameters.AddWithValue("@gewicht", DBNull.Value);
                    command.CommandText = query;
                    int newID = (int)command.ExecuteScalar();
                    s.ZetId(newID); return s;
                }
                catch(Exception ex) { throw new ADORepositoryException("SchrijfSpelerinDB",ex); }
            }
        }
        public IReadOnlyList<SpelerInfo> SelecteerSpelers(int? id, string naam)
        {
            string query = "SELECT t1.*,\r\n       case when t2.Stamnummer is null then null \r\n       else concat(t2.naam,' (',t2.Bijnaam,') - ',t2.Stamnummer)\r\n\t   end teamnaam\r\nFROM [Speler] t1 left join team t2 on t1.TeamId=t2.Stamnummer ";
            if (id.HasValue) query += "WHERE id=@id";
            else query += "WHERE t1.naam like @naam";
            List<SpelerInfo> spelers= new List<SpelerInfo>();
            using(SqlConnection connection = new SqlConnection(connectionString))
            using(SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                if (id.HasValue) command.Parameters.AddWithValue("@id", id);
                else command.Parameters.AddWithValue("@naam","%"+naam+"%");
                connection.Open();
                IDataReader reader=command.ExecuteReader();
                while (reader.Read())
                {
                    string teamnaam = null;
                    if (!reader.IsDBNull(reader.GetOrdinal("teamnaam"))) teamnaam = (string)reader["teamnaam"];
                    int? lengte = null;
                    if (!reader.IsDBNull(reader.GetOrdinal("lengte"))) lengte = (int?)reader["lengte"];
                    int? gewicht = null;
                    if (!reader.IsDBNull(reader.GetOrdinal("gewicht"))) gewicht = (int?)reader["gewicht"];
                    int? rugnummer = null;
                    if (!reader.IsDBNull(reader.GetOrdinal("rugnummer"))) rugnummer = (int?)reader["rugnummer"];
                    SpelerInfo speler = new SpelerInfo((int)reader["id"], (string)reader["naam"],  rugnummer, lengte, gewicht, teamnaam);
                    spelers.Add(speler);
                }
                reader.Close();
                return spelers;
            }
        }
        public Speler SelecteerSpeler(int id)
        {
            string sql = "SELECT t1.id spelerid,t1.naam spelernaam,t1.Rugnummer spelerrugnummer,\r\n       t1.Lengte spelerlengte,t1.Gewicht spelergewicht,tt.*\r\nFROM speler t1 left join \r\n(\r\nselect t1.Stamnummer,t1.Naam ploegnaam,t1.Bijnaam,t2.*\r\nfrom team t1 left join speler t2 on t1.Stamnummer=t2.TeamId\r\n) tt\r\non tt.Stamnummer=t1.TeamId\r\n\r\nwhere t1.id=@id";
            using (SqlConnection con = new SqlConnection(connectionString))
            using(SqlCommand cmd=con.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                IDataReader reader = cmd.ExecuteReader();
                Speler speler = null;
                Team team = null;
                while(reader.Read())
                {
                    if (speler==null) //eerste keer
                    {
                        int? lengte = null;
                        if (!reader.IsDBNull(reader.GetOrdinal("spelerlengte"))) lengte = (int?)reader["spelerlengte"];
                        int? gewicht = null;
                        if (!reader.IsDBNull(reader.GetOrdinal("spelergewicht"))) gewicht = (int?)reader["spelergewicht"];
                        speler = new Speler(id, (string)reader["spelernaam"], lengte, gewicht);
                        int? rugnummer = null;
                        if (!reader.IsDBNull(reader.GetOrdinal("spelerrugnummer")))
                        {
                            rugnummer = (int)reader["spelerrugnummer"];
                            speler.ZetRugnummer((int)rugnummer);
                        }
                        if (reader.IsDBNull(reader.GetOrdinal("stamnummer"))) return speler;
                    }
                    if (team == null)
                    {                        
                        team = new Team((int)reader["stamnummer"], (string)reader["ploegnaam"]);
                        if (!reader.IsDBNull(reader.GetOrdinal("bijnaam")))
                            team.ZetBijnaam((string)reader["bijnaam"]);
                    }
                    //alle spelers
                    if (!reader.IsDBNull(reader.GetOrdinal("id")))
                    {
                        int? lengte = null;
                        if (!reader.IsDBNull(reader.GetOrdinal("lengte"))) lengte = (int?)reader["lengte"];
                        int? gewicht = null;
                        if (!reader.IsDBNull(reader.GetOrdinal("gewicht"))) gewicht = (int?)reader["gewicht"];
                        int sid = (int)reader["id"];
                        Speler s = new Speler(sid, (string)reader["naam"], lengte, gewicht);
                        s.ZetTeam(team);
                        int? rugnummer = null;
                        if (!reader.IsDBNull(reader.GetOrdinal("rugnummer")))
                        {
                            rugnummer = (int)reader["rugnummer"];
                            speler.ZetRugnummer((int)rugnummer);
                        }
                        if (sid == id) speler = s;
                    }
                }
                return speler;
            }
        }
    }
}
