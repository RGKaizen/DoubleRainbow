using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubleRainbow.Interfaces
{
    public interface IHueGenerator
    {
        DRColor.HSV getNextColor(float hue_delta, float sat_delta, float value_delta);
    }
}
