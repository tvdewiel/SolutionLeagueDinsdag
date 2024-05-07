﻿using League.BL.Interfaces;
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
    }
}