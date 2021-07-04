using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FalconDownloader.Helpers
{
    public class ColorAttribute : Attribute
    {
        public ColorAttribute(string name)
        {
            var color = Color.FromName(name);
            ForeColor = color;
        }

        public Color ForeColor { get; private set; }
    }
}
