using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MultiReader.Application.Files;
using System.IO;
using MultiReader.Application.Models;

namespace MultiReader.Application.Parsers
{
    class RtfParser : AbstractParser
    {
        ContentFile file;

        public RtfParser(string filename)
        {
            string fileContent = "";
            using (StreamReader sr = new StreamReader(new FileStream(filename, FileMode.Open)))
            {
                fileContent = sr.ReadToEnd();
                sr.Close();
            }

            file = new ContentFile()
            {
                contentText = fileContent
            };
        }

        public override IEnumerable<string> GetMetadata(MetadataType type)
        {
            if (parsedFile.Metadata.ContainsKey(type))
                return parsedFile.Metadata[type];

            return null;
        }

        public override void SetMetadata(MetadataType type, IEnumerable<string> value)
        {
            parsedFile.Metadata[type] = value.ToList();
        }

        public override string GetFileContent()
        {
            return file.contentText;
        }

        public override void SaveFileAs(string fileName, FileType type)
        {
            throw new NotImplementedException("...");
        }
    }
}
