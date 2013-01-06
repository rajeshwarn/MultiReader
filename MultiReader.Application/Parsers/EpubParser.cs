using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MultiReader.Application.Files;
using MultiReader.Application.Parsers;
using MultiReader.Application.Helpers;
using MultiReader.Application.Models;
using SharpEpub;
using Epub;
using System.Collections;

namespace MultiReader.Application.Parsers
{
    public class EpubParser : AbstractParser
    {
        private eBdb.EpubReader.Epub epub;
        private Document parsedEpub;

        public EpubParser(string fileName)        
        {
            epub = new eBdb.EpubReader.Epub(fileName);
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
            return GetMetadataFromFile(type);
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
            parsedEpub = new Document();

            foreach (DictionaryEntry chapter in epub.Content)
            {
                var chapterContent = (eBdb.EpubReader.ContentData)chapter.Value;
                parsedEpub.AddXhtmlData(chapterContent.FileName, chapterContent.Content);
            }

            foreach (DictionaryEntry extendedData in epub.ExtendedData)
            {
                var dataContent = (eBdb.EpubReader.ExtendedData)extendedData.Value;

                if (dataContent.IsText && dataContent.FileName.EndsWith(".css"))
                    parsedEpub.AddStylesheetData(dataContent.FileName, dataContent.Content);
                else if (!dataContent.IsText && dataContent.FileName.EndsWith(".jpg") ||
                                                dataContent.FileName.EndsWith(".png") ||
                                                dataContent.FileName.EndsWith(".bmp") ||
                                                dataContent.FileName.EndsWith(".jpeg"))
                {
                    parsedEpub.AddImageData(dataContent.FileName, Encoding.UTF8.GetBytes(dataContent.Content));
                }
            }

            foreach (eBdb.EpubReader.NavPoint tocEntry in epub.TOC)
            {
                FillTocRecursively(tocEntry, null);
            }

            parsedEpub.AddAuthor(epub.Creator.JoinUsing(", "));
            parsedEpub.AddBookIdentifier(epub.ID.JoinUsing(", "));
            parsedEpub.AddDescription(epub.Description.JoinUsing(", "));
            parsedEpub.AddFormat(epub.Format.JoinUsing(", "));
            parsedEpub.AddLanguage(epub.Language.JoinUsing(", "));
            parsedEpub.AddRelation(epub.Relation.JoinUsing(", "));
            parsedEpub.AddRights(epub.Rights.JoinUsing(", "));
            parsedEpub.AddSubject(epub.Subject.JoinUsing(", "));
            parsedEpub.AddTitle(epub.Title.JoinUsing(", "));
            parsedEpub.AddType(epub.Type.JoinUsing(", "));

            parsedEpub.Generate(fileName);
        }

        private void FillTocRecursively(eBdb.EpubReader.NavPoint tocEntry, NavPoint parent)
        {
            NavPoint navPoint;
            
            if (parent != null)
                navPoint = parent.AddNavPoint(tocEntry.Title, tocEntry.Source, tocEntry.Order);
            else
                navPoint = parsedEpub.AddNavPoint(tocEntry.Title, tocEntry.Source, tocEntry.Order);

            if (tocEntry.Children == null)
                return;

            foreach (eBdb.EpubReader.NavPoint nestedEntry in tocEntry.Children)
            {
                FillTocRecursively(nestedEntry, navPoint);
            }
        }
    }
}

