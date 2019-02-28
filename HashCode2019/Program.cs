using System;
using System.Linq;

namespace HashCode2019
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Plik dostarcz, sieroto.");
                return;
            }

            var photoList = Parser.Parse(args[0]);

            foreach (var photo in photoList)
            {
                Console.WriteLine($"{photo.Orientation} [{String.Join(',', photo.TagList)}]"); 
            }

        }
    }
}
