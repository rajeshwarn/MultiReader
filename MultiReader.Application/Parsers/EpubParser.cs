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
        private EpubMetadata metadata;

        public EpubParser(string fileName)        
        {
            epub = new eBdb.EpubReader.Epub(fileName);
            LoadAllMetadata();
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

        public override IEnumerable<Metadata> GetAllMetadata()
        {
            return metadata.GetAllMetadata();
        }

        private void LoadAllMetadata()
        {
            metadata = new EpubMetadata();
            metadata.Contributor = new Metadata { Name = "Contributor", Value = epub.Contributer };
            metadata.Coverage = new Metadata { Name = "Coverage", Value = epub.Coverage};
            metadata.Author = new Metadata { Name = "Author", Value = epub.Creator };
            metadata.PublishDate = new Metadata { Name = "PublishDate", Value = epub.Date.Where(d => d.Type == "publication").Select(d => d.Date) };
            metadata.Description = new Metadata { Name = "Description", Value = epub.Description };
            metadata.Format = new Metadata { Name = "Format", Value = epub.Format };
            metadata.ID = new Metadata { Name = "ID", Value = epub.ID };
            metadata.Language = new Metadata { Name = "Language", Value = epub.Language };
            metadata.Publisher = new Metadata { Name = "Publisher", Value = epub.Publisher };
            metadata.Relation = new Metadata { Name = "Relation", Value = epub.Relation };
            metadata.Rights = new Metadata { Name = "Rights", Value = epub.Rights };
            metadata.Source = new Metadata { Name = "Source", Value = epub.Source };
            metadata.Subject = new Metadata { Name = "Subject", Value = epub.Subject };
            metadata.Title = new Metadata { Name = "Title", Value = epub.Title };
            metadata.Type = new Metadata { Name = "Type", Value = epub.Type };
            metadata.UUID = new Metadata { Name = "UUID", Value = new[] { epub.UUID } };
        }

        public override void SetMetadata(Metadata data)
        {
            switch (data.Name)
            {
                case "Contributor": metadata.Contributor.Value = data.Value; break;
                case "Coverage": metadata.Coverage.Value = data.Value; break;
                case "Author": metadata.Author.Value = data.Value; break;
                case "PublishDate": metadata.PublishDate.Value = data.Value; break;
                case "Description": metadata.Description.Value = data.Value; break;
                case "Format": metadata.Format.Value = data.Value; break;
                case "ID": metadata.ID.Value = data.Value; break;
                case "Language": metadata.Language.Value = data.Value; break;
                case "Publisher": metadata.Publisher.Value = data.Value; break;
                case "Relation": metadata.Relation.Value = data.Value; break;
                case "Rights": metadata.Rights.Value = data.Value; break;
                case "Source": metadata.Source.Value = data.Value; break;
                case "Subject": metadata.Subject.Value = data.Value; break;
                case "Title": metadata.Title.Value = data.Value; break;
                case "Type": metadata.Type.Value = data.Value; break;
                case "UUID": metadata.UUID.Value = data.Value; break;
            }
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

    public class EpubMetadata
    {            
        public Metadata Contributor;
        public Metadata Coverage;
        public Metadata Author;
        public Metadata PublishDate;
        public Metadata Description;
        public Metadata Format;
        public Metadata ID;
        public Metadata Language;
        public Metadata Publisher;
        public Metadata Relation;
        public Metadata Rights;
        public Metadata Source;
        public Metadata Subject;
        public Metadata Title;
        public Metadata Type;
        public Metadata UUID;

        public IEnumerable<Metadata> GetAllMetadata()
        {
            yield return Contributor;
            yield return Coverage;
            yield return Author;
            yield return PublishDate;
            yield return Description;
            yield return Format;
            yield return ID;
            yield return Language;
            yield return Publisher;
            yield return Relation;
            yield return Rights;
            yield return Source;
            yield return Subject;
            yield return Title;
            yield return Type;
            yield return UUID;
        }
    }
}

