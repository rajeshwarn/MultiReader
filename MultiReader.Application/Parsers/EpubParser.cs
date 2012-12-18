using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MultiReader.Files;
using MultiReader.Parsers;
using eBdb.EpubReader;

namespace MultiReader
{
    class EpubParser : IParser
    {
        ContentFile parsedFile;
        eBdb.EpubReader.Epub epub; //odczytanie treści
        Epub.EpubFile epubFile; //odczytanie meta danych, zapis

        public EpubParser()
        { 
            // Nie powinno być bezparametrowego konstruktora PLE PLE PLE
        }

        public EpubParser(string fileName, string title)
        {
            epubFile = new Epub.EpubFile(fileName);
            title = epubFile["title"];
           // epubFile["title"] =  //"Oj tam oj tam";
            epubFile.SaveTo(fileName + ".cpy");
        }

        public EpubParser(string title, string author, string id)
        {
        }

        public EpubParser(string fileName)        
        {
            epub = new eBdb.EpubReader.Epub(fileName);

            parsedFile = new ContentFile()
            {
                content = epub.GetContentAsPlainText(),
                //contentFull = epub.GetContentAsHtml()
            };

        }

        //Zwraca tekst z pliku epub
        public string GetFileContent() 
        {
            return parsedFile.content;
            //return parsedFile.contentFull;
        }

        //Zapisuje plik jako epub
        public void SetFile(string fileName, string text) 
        {
            epubFile.SaveTo(fileName);
        }

        public Dictionary<MetadataType, string> metadata = new Dictionary<MetadataType, string>();

        //Pobiera metadane
        public string GetMetadata(MetadataType type)
        {
            
            return type.ToString();
        }

        //Ustawia metadane
        public void SetMetadata(MetadataType type, string value)
        {
            metadata[type] = value;
        }
    }
}

