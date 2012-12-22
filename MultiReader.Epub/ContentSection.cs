using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ionic.Zip;
using System.IO;

namespace MultiReader.Epub
{
    public class ContentSection
    {
        public ContentSection(ZipEntry entry)
        {
            Id = Path.GetFileNameWithoutExtension(entry.FileName);
            ArchivePath = entry.FileName;
            using (Stream s = entry.OpenReader())
            {
                using (StreamReader sr = new StreamReader(s))
                {
                    Content = sr.ReadToEnd();
                }
            }
        }

        public string Content { get; set; }
        public string Id { get; set; }
        public string ArchivePath { get; set; }
    }
}
