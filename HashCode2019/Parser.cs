using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HashCode2019
{
    public static class Parser
    {
        public static List<Photo> Parse(string filename)
        {
            var photoList = new List<Photo>();

            using (StreamReader file = File.OpenText(filename))
            {
                var numberOfPhotos = int.Parse(file.ReadLine());

                for (int i = 0; i < numberOfPhotos; i++)
                {
                    var line = file.ReadLine();
                    var split = line.Split(' ');
                    photoList.Add(new Photo()
                    {
                        Orientation = split[0] == "H" ? Orientation.Horizontal : Orientation.Vertical,
                        TagList = new List<string>(split.Skip(2))
                    });
                }

            }

            return photoList;
        }
    }
}
