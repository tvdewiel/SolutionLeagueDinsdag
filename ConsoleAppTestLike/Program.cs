using League.DL.Repositories;
using System.Configuration;

namespace ConsoleAppTestLike
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            //string c = @"Data Source=NB21-6CDPYD3\SQLEXPRESS;Initial Catalog=LeagueDinsdag;Integrated Security=True;";
            SpelerRepositoryADO repo = new SpelerRepositoryADO(ConfigurationManager.ConnectionStrings["LeagueDBConnection"].ToString());
            var x = repo.SelecteerSpelers(null, "ob");
            Console.WriteLine(x.First().Naam);
        }
    }
}
