using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MultiReader.Application.Files;
using MultiReader.Application.Parsers;
using eBdb.EpubReader;
using MultiReader.Application.Models;

namespace MultiReader.Application.Parsers
{
    public class EpubParser : AbstractParser
    {
        eBdb.EpubReader.Epub epub; //odczytanie treści
        //Epub.EpubFile epubFile; //odczytanie meta danych, zapis

        #region Test
        //public EpubParser()
        //{
        //    // Nie powinno być bezparametrowego konstruktora PLE PLE PLE
        //}

        //public EpubParser(string fileName, string title)
        //{
        //    epubFile = new Epub.EpubFile(fileName);
        //    title = epubFile["title"];
        //   // epubFile["title"] =  //"Oj tam oj tam";
        //    epubFile.SaveTo(fileName + ".cpy");
        //}
        #endregion

        public EpubParser(string title, string author, string id)
        {
        }

        public EpubParser(string fileName)        
        {
            epub = new eBdb.EpubReader.Epub(fileName);
        }

        //Zwraca tekst z pliku epub
        public override string GetFileContent() 
        {
            return epub.GetContentAsPlainText();
        }

        public override IEnumerable<string> GetMetadata(MetadataType type)
        {
            if (!parsedFile.Metadata.ContainsKey(type))
                parsedFile.Metadata[type] = GetMetadataFromFile(type).ToList();

            return parsedFile.Metadata[type];
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

