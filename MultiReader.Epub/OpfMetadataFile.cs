using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ionic.Zip;
using System.IO;
using System.Xml.Linq;
using MultiReader.Epub.Xml;
using MultiReader.Epub.Exceptions;

namespace MultiReader.Epub
{
    class OpfMetadataFile
    {
        private XDocument content;

        private XElement MetadataSection
        {
            get
            {
                return content.ElementByLocalName("package").ElementByLocalName("metadata");
            }
        }

        public OpfMetadataFile(ZipEntry file)
            : this(file, null)
        {
        }

        public OpfMetadataFile(ZipEntry file, string password)
        {
            try
            {
                using (Stream s = (String.IsNullOrEmpty(password) ? file.OpenReader() : file.OpenReader(password)))
                {
                    using (StreamReader sr = new StreamReader(s))
                    {
                        try
                        {
                            content = XDocument.Parse(sr.ReadToEnd());
                        }
                        catch (Exception ex)
                        {
                            throw new MetadataParseException(ex, "Nie można było otworzyć pliku z metadanymi. Może to być spowodowane uszkodzeniem jego struktury XMLowej, niewłaściwym kodowaniem, albo brakiem dostępu.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new EpubFileException(ex, "Błąd w rozpakowywaniu pliku ePub. Może to być spowodowane uszkodzeniem pliku, brakiem plików z metadanymi bądź brakiem dostępu.");
            }
        }

        public string GetMetadata(string key)
        {
            List<XElement> el = MetadataSection.ElementsByLocalName(key).ToList();

            if (!el.Any())
            {
                if (EpubFile.RequiredMetadataNames.Contains(key))
                    throw new RequiredMetadataMissingException(key, "Brakuje jednej z wymaganych metadanych.");

                return null;
            }

            if (el.Count() == 1)
            {

                return el.First().Value;
            }

            if (!EpubFile.MultipleOccurencesMetadataNames.Contains(key))
                throw new MultipleMetadataOccurencesException(key, "Metadana występuje więcej niż raz, mimo że nie powinna.");

            return String.Join(";", el.Select(e => e.Value));
        }

        public void SetMetadata(string key, string value)
        {
            XElement el = MetadataSection.ElementByLocalName(key);

            el.Value = value;
        }

        public string[] GetSectionNames()
        {

            return null;
        }

        public string Content
        {
            get
            {
                return content.ToString();
            }
        }
    }
}
