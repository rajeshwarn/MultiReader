using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiReader.Application.Files
{
    public class ContentFile
    {
        public string contentText { get; set; }
        public string contentRaw { get; set; }

        public Dictionary<MetadataType, IEnumerable<string>> Metadata { get; set; }
    }
}
