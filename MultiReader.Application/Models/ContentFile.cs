using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MultiReader.Application.Models;
using MultiReader.Application.Parsers;

namespace MultiReader.Application.Files
{
    public class ContentFile
    {
        public string contentText { get; set; }
        public string contentRaw { get; set; }

        public Dictionary<MetadataType, IEnumerable<string>> Metadata = new Dictionary<MetadataType, IEnumerable<string>>();
    }
}
