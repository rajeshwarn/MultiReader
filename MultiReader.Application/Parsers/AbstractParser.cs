using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MultiReader.Application.Files;
using MultiReader.Application.Models;

namespace MultiReader.Application.Parsers
{
    public abstract class AbstractParser : IParser
    {
        public ContentFile parsedFile;

        public void SetMetadata(MetadataType type, string value)
        {
            SetMetadata(type, new List<string> { value });
        }

        public abstract string GetFileContent();
        public abstract void SaveFileAs(string fileName, FileType type);
        public abstract IEnumerable<string> GetMetadata(MetadataType type);
        //public abstract IEnumerable<Metadata> GetAllMetadata();
        public abstract void SetMetadata(MetadataType type, IEnumerable<string> value);

    }
}
