using System;
namespace BackendTestTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var loader = new PageLoader();
            Console.WriteLine("Вставьте url");
            string url = Console.ReadLine();
            loader.LoadPage(url);
            Console.ReadLine();
        }
    }
}
