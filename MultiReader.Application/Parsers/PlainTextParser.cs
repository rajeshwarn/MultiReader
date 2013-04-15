using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MultiReader.Application.Files;
using MultiReader.Application.Models;

namespace MultiReader.Application.Parsers
{
    public class PlainTextParser : AbstractParser
    {

        public PlainTextParser(string filename)
        {
            string fileContent = "";
            using(StreamReader sr = new StreamReader(new FileStream(filename, FileMode.Open)))
            {
                fileContent = sr.ReadToEnd();
            }

            parsedFile = new ContentFile()
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
            return parsedFile.contentText;
        }

        public override void SaveFileAs(string fileName, FileType type)
        {
            File.WriteAllText(fileName, parsedFile.contentText);
        }

        public override void SetMetadata(Metadata data)
        {
        }

        public override IEnumerable<Metadata> GetAllMetadata()
        {
            return new Metadata[0];
        }
    }
}
