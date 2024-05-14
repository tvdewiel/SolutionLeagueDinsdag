using League.DL.Repositories;

namespace ConsoleAppTestLike
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            string c = @"Data Source=NB21-6CDPYD3\SQLEXPRESS;Initial Catalog=LeagueDinsdag;Integrated Security=True;";
            SpelerRepositoryADO repo = new SpelerRepositoryADO(c);
            var x = repo.SelecteerSpelers(null, "ob");
            Console.WriteLine(x);
        }
    }
}
