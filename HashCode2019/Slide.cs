using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HashCode2019
{
    public abstract class Slide
    {
        public virtual Orientation Orientation { get; set; }


    }

    public class HorizontalSlide : Slide
    {
        public override Orientation Orientation => Orientation.Horizontal;
        public Photo Photo { get; }
        public List<string> Tags => Photo.TagList;

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
        public List<string> Tags => Photos[0].TagSet.Union(Photos[1].TagSet).ToList();


        public VerticalSlide(Photo verticalOne, Photo verticalTwo)
        {
            Photos = new List<Photo>() { verticalOne, verticalTwo };
        }
        public override string ToString()
        {
            return String.Join(' ', Photos.Select(p => p.Id));
        }
    }
    
}
