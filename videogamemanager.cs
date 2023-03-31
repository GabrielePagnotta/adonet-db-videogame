using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
            }finally { 
                conn.Close();
                Environment.Exit(0);
            }
            return gamelist;
        }

        public void addvideogame(string Name, string ows, DateTime release, long softwarehouse)
        {
            var connectionString = "Data Source = localhost;Initial Catalog=db-videogames;Integrated Security=True;Encrypt=False";
            string query = "INSERT INTO videogames (name,overview,release_date,software_house_id) VALUES(@Name,@ows,@release_date,@software_house_id)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.Add(new SqlParameter("@Name", $"{Name}"));
                cmd.Parameters.Add(new SqlParameter("@ows", $"{ows}"));
                cmd.Parameters.Add(new SqlParameter("@release_date", release));
                cmd.Parameters.Add(new SqlParameter("@software_house_id", softwarehouse));

                connection.Open();
                int affectedRows = cmd.ExecuteNonQuery();
                }catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally {
                    connection.Close();
                    Environment.Exit(0);
                }

            }
        }

        public void removegame(string Name)
        {
            var connectionString = "Data Source = localhost;Initial Catalog=db-videogames;Integrated Security=True;Encrypt=False";
            string query = "DELETE FROM videogames WHERE name = @name";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add(new SqlParameter("@name", Name));
                connection.Open();
                int affectedRows = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }finally {
                    connection.Close();
                    Environment.Exit(0);
                }

            }

            
        }
    }
}
