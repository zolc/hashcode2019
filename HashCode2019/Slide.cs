﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HashCode2019
{
    public abstract class Slide
    {
        public Orientation Orientation { get; set; }
        public List<Photo> Photos { get; set; }

        public Slide(Photo horizontalPhoto)
        {
            Photos = new List<Photo>() { horizontalPhoto };
            Orientation = Orientation.Horizontal;
        }

        public Slide(Photo verticalOne, Photo verticalTwo)
        {
            Photos = new List<Photo>() { verticalOne, verticalTwo };
            Orientation = Orientation.Vertical;
        }

        public override string ToString()
        {
            return String.Join(' ', Photos.Select(p => p.Id));
        }
    }

    public class HorizontalSlide : Slide
    {
        public Orientation Orientation => Orientation.Horizontal;
        public List<string> Tags =>

    }
    public class VerticalSlide : Slide
    {
        public Orientation Orientation => Orientation.Vertical;

        public List<string> Tags =>

    }
}
