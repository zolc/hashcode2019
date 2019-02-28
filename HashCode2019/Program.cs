using System;
using System.Collections.Generic;
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

            var vertical = photoList.Where(p => p.Orientation == Orientation.Vertical);
            var horizontal = photoList.Where(p => p.Orientation == Orientation.Horizontal);

            var slides = new List<Slide>();
            foreach (var h in horizontal)
            {
                slides.Add(new Slide(h));
            }

            foreach(var s in slides)
            {
                Console.WriteLine(s);
            }
        }
    }
}
