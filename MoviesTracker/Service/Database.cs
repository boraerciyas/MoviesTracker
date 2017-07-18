using MoviesTracker.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MoviesTracker.Service
{
    public static class Database
    {
        private static string DB_TABLE_NAME = "Movie.dbo.Films";

        public static string GetConnectionString()
        {
            return @"Server=CASPERNIRVANA\SQLEXPRESS;Database=Movie;Trusted_Connection=Yes;";
        }

        public static IEnumerable<Film> GetFilms(string order, string orderDirection)
        {
            List<Film> films = new List<Film>();


            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand comm = conn.CreateCommand())
                {
                    string commandText = @" SELECT * 
                                            FROM " + DB_TABLE_NAME;

                    if(!String.IsNullOrEmpty(order) && !String.IsNullOrEmpty(orderDirection))
                    {
                        if (order.Equals("ReleaseTime"))
                        {
                            commandText += " ORDER BY CONVERT(DATETIME, " + order + ", 103) " + orderDirection;

                        }
                        else commandText += " ORDER BY " + order + " " + orderDirection;
                    }

                    comm.CommandText = commandText;

                    conn.Open();

                    using (SqlDataReader reader = comm.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            Film film = new Film()
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                Rate = Convert.ToDecimal(reader["Rate"]),
                                ReleaseTime = reader["ReleaseTime"] == DBNull.Value ? new DateTime(1995, 1, 1) : Convert.ToDateTime(reader["ReleaseTime"]),
                                Title = reader["Title"] == DBNull.Value ? "" : Convert.ToString(reader["Title"]),
                                Genre = reader["Genre"] == DBNull.Value ? "" : Convert.ToString(reader["Genre"]),
                                //Director = reader["Director"] == DBNull.Value ? "" : Convert.ToString(reader["Director"])

                            };
                            films.Add(film);
                        }
                    }
                }
            }

            return films;
        }
        public static void SetFilms(Film film)
        {
            using(SqlConnection conn = new SqlConnection(GetConnectionString())) { 
                using(SqlCommand SQLCREATEFILMCOMMAND = conn.CreateCommand()) { 

                    SQLCREATEFILMCOMMAND.CommandText = @"INSERT INTO Movie.dbo.Films(Title, ReleaseTime, Genre, Rate) VALUES('" + film.Title + "', '" + film.ReleaseTime + "', '" + film.Genre + "', "  + film.Rate +")";
                    conn.Open();
                    SQLCREATEFILMCOMMAND.ExecuteNonQuery();
                }
            }
        }
        public static Film GetFilms(int id)
        {
            Film film = null;
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand comm = conn.CreateCommand())
                {
                    comm.CommandText = @" SELECT * 
                                            FROM Films 
                                            WHERE ID=" + id;

                    conn.Open();

                    using (SqlDataReader reader = comm.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Film filmq = new Film()
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                Rate = Convert.ToDecimal(reader["Rate"]),

                                Title = reader["Title"] == DBNull.Value ? "" : Convert.ToString(reader["Title"]),
                                Genre = reader["Genre"] == DBNull.Value ? "" : Convert.ToString(reader["Genre"]),
                                ReleaseTime = reader["ReleaseTime"] == DBNull.Value ? new DateTime(1995,1,1) : Convert.ToDateTime(reader["ReleaseTime"])
                            };
                            film = filmq;
                        }
                    }
                }
            }

            return film;
        }
        public static void UpdateFilm(Film film)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand SQLUPDATEFILMCOMMAND = conn.CreateCommand())
                {
                    Console.WriteLine("This will be the ReleaseTime of the film that was choosen for Edit Action: " + film.ReleaseTime.Date);

                    //SQLUPDATEFILMCOMMAND.CommandText = @"INSERT INTO Movie.dbo.Films(Title, ReleaseTime, Genre, Rate) VALUES('" + film.Title + "', '" + film.ReleaseTime + "', '" + film.Genre + "', " + film.Rate + ")";
                    SQLUPDATEFILMCOMMAND.CommandText = @"UPDATE " + DB_TABLE_NAME + " " +
                                                        "SET " +
                                                             "Title='" + film.Title + "', " +
                                                             "ReleaseTime='" + film.ReleaseTime.ToString("yyyyMMdd")  + "', " +
                                                             "Genre='" + film.Genre + "', " +
                                                             "Rate=" + film.Rate + " " +
                                                        "WHERE ID = " + film.ID;
                    conn.Open();
                   SQLUPDATEFILMCOMMAND.ExecuteNonQuery();
                }
            }
        }
        public static void DeleteFilm(int id)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand SQLUPDATEFILMCOMMAND = conn.CreateCommand())
                {
                    SQLUPDATEFILMCOMMAND.CommandText = @"DELETE FROM " + DB_TABLE_NAME + " " +
                                                         "WHERE ID=" + id;
                    conn.Open();
                    SQLUPDATEFILMCOMMAND.ExecuteNonQuery();
                }
            }
        }
    }
}