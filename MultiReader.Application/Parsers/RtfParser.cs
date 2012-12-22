using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MultiReader.Application.Files;
using System.IO;

namespace MultiReader.Application.Parsers
{
    class RtfParser : IParser
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
                 content = fileContent
             };
         }
         
        public Dictionary<MetadataType, string> metadata = new Dictionary<MetadataType, string>();

         public string GetMetadata(MetadataType type)
         {
             if (metadata.ContainsKey(type))
                 return metadata[type];
             return null;
         }

         public void SetMetadata(MetadataType type, string value)
         {
             metadata[type] = value;
         }

         public string GetFileContent()
         {
             return file.content;
         }

         public void SetFile(string fileName, string text)
         { }
    }
}
