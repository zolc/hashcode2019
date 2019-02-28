using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HashCode2019
{
    public abstract class Slide
    {
        public virtual Orientation Orientation { get; set; }

        public abstract List<string> Tags { get; }
    }

    public class HorizontalSlide : Slide
    {
        public override Orientation Orientation => Orientation.Horizontal;
        public Photo Photo { get; }
        public override List<string> Tags => Photo.TagList;

        public HorizontalSlide(Photo horizontalPhoto)
        {
            this.Photo = horizontalPhoto;
        }

        public override string ToString()
        {
            return Photo.Id.ToString();
        }
    }
    public class VerticalSlide : Slide
    {
        public override Orientation Orientation => Orientation.Vertical;
        public List<Photo> Photos { get; }
        public override List<string> Tags { get; }


        public VerticalSlide(Photo verticalOne, Photo verticalTwo)
        {
            Photos = new List<Photo>() { verticalOne, verticalTwo };
            Tags = Photos[0].TagSet.Union(Photos[1].TagSet).ToList();
        }
        public override string ToString()
        {
            return String.Join(' ', Photos.Select(p => p.Id));
        }
    }
    
}
