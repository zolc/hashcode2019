using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HashCode2019
{
    public static class Writer
    {

        public static void WriteToFile(string filename, List<Slide> slides)
        {
            var numberOfSlides = slides.Count.ToString();
            File.WriteAllLines(filename, new string[] { numberOfSlides });
            File.AppendAllLines(filename, slides.Select(s => s.ToString()).ToArray());
        }
    }
}
