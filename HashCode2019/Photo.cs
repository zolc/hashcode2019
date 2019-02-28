using System;
using System.Collections.Generic;
using System.Text;

namespace HashCode2019
{
    public class Photo
    {

        public Orientation Orientation { get ; set ; }
        public HashSet<string> TagSet { get; private set; }
        private List<string> _tagList;
        public List<string> TagList {
            get
            {
                return _tagList;
            }
            set
            {
                TagSet = new HashSet<string>(value);
                _tagList = value;
            }
        }
    }

    public enum Orientation
    {
        Horizontal,
        Vertical
    }
}
