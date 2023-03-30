using adonet_db_videogame;
using System.Data.SqlClient;


var connStr = "Data Source = localhost;Initial Catalog=db-videogames;Integrated Security=True;Encrypt=False;";
var vid = new videogamemanager(connStr);

Console.WriteLine("inserisci un nome");
var name =Console.ReadLine();
Console.WriteLine("inserisci una descrizione");
var ow = Console.ReadLine();
var getadd = vid.addvideogame(name,ow);


Console.WriteLine("inserisci l'id da ricercare");
var dato1=Convert.ToInt64(Console.ReadLine());
Console.WriteLine("inserisci il nome da ricercare");
var dato2 = Console.ReadLine();
var getinput = vid.getvideogame(dato1,dato2);
foreach (var game in getinput) Console.WriteLine(game);



