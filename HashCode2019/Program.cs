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
            if (args.Length < 2)
            {
                Console.WriteLine("A output gdzie chcesz?");
                return;
            }

            var photoList = Parser.Parse(args[0]);

            var vertical = photoList.Where(p => p.Orientation == Orientation.Vertical);
            var horizontal = photoList.Where(p => p.Orientation == Orientation.Horizontal);


            var buckets = new List<Slide>[200];
            var buckets2 = new List<Slide>[200];
            for (int i = 0; i < 200; ++i)
            {
                buckets[i] = new List<Slide>();
                buckets2[i] = new List<Slide>();
            }

            foreach (var h in horizontal)
                buckets[h.TagList.Count].Add(new HorizontalSlide(h));

            var maxNonEmptyBucket = 0;
            for (int i = 1; i < 200; ++i)
                if (buckets[i].Count != 0)
                    maxNonEmptyBucket = i;




            var verticalClone = new List<Photo>(vertical);
            while (verticalClone.Count > 1)
            {
                var firstPhoto = verticalClone[0];
                var targetTagsCount = maxNonEmptyBucket - firstPhoto.TagList.Count;
                var secondPhotoTagsCount = verticalClone[1].TagList.Count;
                var secondPhotoIndex = 1;
                var distanceToSecond = Math.Abs(targetTagsCount - secondPhotoTagsCount);
                for (var i = 2; i < verticalClone.Count; i++)   
                {
                    var currentPhoto = verticalClone[i];
                    var currentPhotoTagsCount = currentPhoto.TagList.Count;
                    var distanceToCurrent = Math.Abs(targetTagsCount - currentPhotoTagsCount);
                    if (distanceToCurrent < distanceToSecond)
                    {
                        secondPhotoTagsCount = currentPhotoTagsCount;
                        secondPhotoIndex = i;
                        distanceToSecond = distanceToCurrent;
                    }
                }

                var slide = new VerticalSlide(firstPhoto, verticalClone[secondPhotoIndex]);
                buckets2[firstPhoto.TagList.Count + secondPhotoTagsCount].Add(slide);
                verticalClone.RemoveAt(secondPhotoIndex);
                verticalClone.RemoveAt(0);
            }

            Console.WriteLine("Horizontal");
            for (int i = 1; i < maxNonEmptyBucket; ++i)
            {
                Console.WriteLine($"{i}: {buckets[i].Count}");
            }

            Console.WriteLine("Vertical");
            for (int i = 1; i < 150; ++i)
            {
                Console.WriteLine($"{i}: {buckets2[i].Count}");
            }

            //var slides = new List<Slide>();
            //foreach (var h in horizontal)
            //    slides.Add(new Slide(h));

            //Writer.WriteToFile(args[1], slides);
        }
    }
}
