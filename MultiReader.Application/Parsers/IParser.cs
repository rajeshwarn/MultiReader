using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MultiReader.Application.Files;
using MultiReader.Application.Models;

namespace MultiReader.Application.Parsers
{
    public interface IParser
    {
        string GetFileContent();
        void SaveFileAs(string fileName, FileType type);
        IEnumerable<string> GetMetadata(MetadataType type);
        //IEnumerable<Metadata> GetAllMetadata();
        void SetMetadata(MetadataType type, string value);
        void SetMetadata(MetadataType type, IEnumerable<string> value);
    }
}
