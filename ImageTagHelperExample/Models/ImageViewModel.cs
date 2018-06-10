using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageTagHelperExample.Models
{
    public class ImageViewModel
    {
        public Uri ImageUrl { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
