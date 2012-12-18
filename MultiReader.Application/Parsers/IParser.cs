using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MultiReader.Files;

namespace MultiReader.Parsers
{
    public interface IParser
    {
        string GetFileContent();
        void SetFile(string fileName, string text);
        string GetMetadata(MetadataType type);
        void SetMetadata(MetadataType type, string value);
    }
}
