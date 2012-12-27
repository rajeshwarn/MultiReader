using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MultiReader.Application.Files;
using MultiReader.Application.Parsers;
using eBdb.EpubReader;
using MultiReader.Application.Models;
using SharpEpub;

namespace MultiReader.Application.Parsers
{
    public class EpubParser : AbstractParser
    {
        private eBdb.EpubReader.Epub epub;
        private EpubFile parsedEpub;

        public EpubParser(string fileName)        
        {
            epub = new eBdb.EpubReader.Epub(fileName);

            parsedFile = new ContentFile
            {
                contentRaw = epub.GetContentAsHtml(),
                contentText = epub.GetContentAsPlainText()
            };

            EnsureAllCached();
        }

        private string _contentRaw;
        public string ContentRaw
        {
            get
            {
                if (_contentRaw == null)
                    _contentRaw = epub.GetContentAsHtml();
                return _contentRaw;
            }
            set { _contentRaw = value; }
        }

        private string _contentText;
        public string ContentText
        {
            get
            {
                if (_contentText == null)
                    _contentText = epub.GetContentAsPlainText();
                return _contentText;
            }
            set { _contentText = value; }
        }

        public override string GetFileContent() 
        {
            return ContentText;
        }

        public override IEnumerable<string> GetMetadata(MetadataType type)
        {
            EnsureCached(type);
            return parsedFile.Metadata[type];
        }

        private void EnsureCached(MetadataType type)
        {
            if (!parsedFile.Metadata.ContainsKey(type))
                parsedFile.Metadata[type] = GetMetadataFromFile(type).ToList();
        }

        private void EnsureAllCached()
        {
            foreach (MetadataType type in Enum.GetValues(typeof(MetadataType)))
            {
                EnsureCached(type);
            }
        }

        private IEnumerable<string> GetMetadataFromFile(MetadataType type)
        {
            switch (type)
            {
                case MetadataType.Author: return epub.Creator;
                case MetadataType.BookID: return epub.ID.Concat(new[] { epub.UUID }).Where(s => !String.IsNullOrEmpty(s));
                case MetadataType.Description: return epub.Description;
                case MetadataType.Format: return epub.Format;
                case MetadataType.Language: return epub.Language;
                case MetadataType.PublishDate: return epub.Date.Where(d => d.Type == "publication").Select(d => d.Date);
                case MetadataType.Publisher: return epub.Publisher;
                case MetadataType.Relations: return epub.Relation;
                case MetadataType.Rights: return epub.Rights;
                case MetadataType.Subject: return epub.Subject;
                case MetadataType.Title: return epub.Title;
                case MetadataType.Translator: return new List<string>(); // TODO
                case MetadataType.Type: return epub.Type;
            }

            return new List<string>();
        }

        //Ustawia metadane
        public override void SetMetadata(MetadataType type, IEnumerable<String> value)
        {
            parsedFile.Metadata[type] = value.ToList();
        }

        public override void SaveFileAs(string fileName, FileType type)
        {
            throw new NotImplementedException("...");
        }
    }
}

