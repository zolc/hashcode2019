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


            var buckets = new List<Slide>[100];
            var buckets2 = new List<Slide>[100];
            for (int i = 0; i < 100; ++i)
            {
                buckets[i] = new List<Slide>();
                //buckets2[i] = new List<Slide>();
            }

            foreach (var h in horizontal)
                buckets[h.TagList.Count].Add(new HorizontalSlide(h));

            var maxNonEmptyBucket = 0;
            for (int i = 1; i < 100; ++i)
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

                buckets[slide.Tags.Count].Add(slide);
                verticalClone.RemoveAt(secondPhotoIndex);
                verticalClone.RemoveAt(0);
            }

            maxNonEmptyBucket = 0;
            for (int i = 1; i < 100; ++i)
                if (buckets[i].Count != 0)
                    maxNonEmptyBucket = i;

            var presentation = new List<Slide>() { buckets[maxNonEmptyBucket][0] };
            buckets[maxNonEmptyBucket].RemoveAt(0);

            for (int i = maxNonEmptyBucket; i > 0; --i)
            {
                while (buckets[i].Count > 0)
                {
                    var prevDistance = int.MaxValue;
                    var chosenSecondSlide = buckets[i][0];
                    var chosenSecondSlideIndex = 0;
                    for (int secondSlideIndex = 0; secondSlideIndex < buckets[i].Count; ++secondSlideIndex)
                    {
                        var secondSlide = buckets[i][secondSlideIndex];
                        var intersectionCount = presentation[presentation.Count - 1].Tags.Intersect(secondSlide.Tags).Count();
                        var target = secondSlide.Tags.Count / 2;
                        var currDist = Math.Abs(intersectionCount - target);
                        if (currDist < prevDistance)
                        {
                            prevDistance = currDist;
                            chosenSecondSlide = secondSlide;
                            chosenSecondSlideIndex = secondSlideIndex;

                            // TODO: CHANGE CONDITION TO  == 0, 2, 3...
                            if (currDist == 0)
                            {
                                break;
                            }
                        }
                    }

                    presentation.Add(chosenSecondSlide);
                    buckets[i].RemoveAt(chosenSecondSlideIndex);
                }
            }


            //Console.WriteLine("Horizontal");
            //for (int i = 1; i < 100; ++i)
            //{
            //    Console.WriteLine($"{i}: {buckets[i].Count}");
            //}

            //Console.WriteLine("Vertical");
            //for (int i = 1; i < 100; ++i)
            //{
            //    Console.WriteLine($"{i}: {buckets2[i].Count}");
            //}


            Writer.WriteToFile(args[1], presentation);
        }
    }
}
