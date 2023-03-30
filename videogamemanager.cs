using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adonet_db_videogame
{
    public class videogamemanager
    {
        string connStr;

        public videogamemanager(string connStr)
        {
            this.connStr = connStr;
        }

        public List<Videogame> getvideogame(long ids, string likeString)
        {
            var conn = new SqlConnection(connStr);
            var gamelist = new List<Videogame>();

            try
            {
                conn.Open();
                var query = "SELECT *"
                    + " FROM videogames"
                    + $" WHERE name LIKE @NameLike AND id LIKE @idlike";


                using var command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@NameLike", $"{likeString}%");
                command.Parameters.AddWithValue("@idLike", $"{ids}%"); ;

                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var ididx = reader.GetOrdinal("id");
                    var id = reader.GetInt64(ididx);

                    var nameidx = reader.GetOrdinal("name");
                    var name = reader.GetString(nameidx);

                    var vvidx = reader.GetOrdinal("overview");
                    var overview = reader.GetString(vvidx);

                    var game = new Videogame(id, name, overview);
                    gamelist.Add(game);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return gamelist;
        }

        public List<Videogame> addvideogame(string names, string overviews)
        {
            var conn = new SqlConnection(connStr);
            var gamelist = new List<Videogame>();

            try
            {
                conn.Open();
                var query = "INSERT INTO videogames(name, overview)"
                    + "VALUES(@NameLike,@overlike)";


                using var command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@NameLike", $"{names}");
                command.Parameters.AddWithValue("@overlike", $"{overviews}");

                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var ididx = reader.GetOrdinal("id");
                    var id = reader.GetInt64(ididx);

                    var nameidx = reader.GetOrdinal("name");
                    var name = reader.GetString(nameidx);

                    var vvidx = reader.GetOrdinal("overview");
                    var overview = reader.GetString(vvidx);

                    var game = new Videogame(id, name, overview);
                    gamelist.Add(game);
                }

            }
            catch
            {
                throw new Exception("errore");
            }
            return gamelist;
        }
    }
}
