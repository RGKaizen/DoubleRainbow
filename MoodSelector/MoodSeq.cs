using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoubleRainbow
{
    [JsonObject(MemberSerialization.OptIn)]
    public class MoodSeq
    {
        [JsonProperty]
        public String Name = " ";

        [JsonProperty]
        public List<ColorTypes.RGB> Color_List = new List<ColorTypes.RGB>();

        public MoodSeq()
        {
        }

        public MoodSeq(ColorTypes.RGB rgb)
        {
            Color_List.Add(rgb);

        }

        public override string ToString()
        {
            return Name;
        }
    }
}
