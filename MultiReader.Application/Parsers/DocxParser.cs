using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MultiReader.Application.Parsers;
using MultiReader.Application.Files;
using MultiReader.Application.Helpers;
using MultiReader.Application.Models;
using DocumentFormat.OpenXml.Packaging;
using System.Xml.Linq;
using System.Xml;
using DocumentFormat.OpenXml.Wordprocessing;

namespace MultiReader.Application.Parsers
{
    class DocxParser : AbstractParser, IDisposable
    {
        private bool disposed = false;
        private WordprocessingDocument doc;
        private string path;
        private DocxMetadata metadata;

        public DocxParser(ContentFile file)
        {
            parsedFile = file;
            path = Path.GetTempPath() + Path.DirectorySeparatorChar + "tmp-" + DateTime.Now.Ticks.ToString();

            doc = WordprocessingDocument.Create(path, DocumentFormat.OpenXml.WordprocessingDocumentType.Document);

            var mainPart = doc.AddMainDocumentPart();
            var paragraph = new Paragraph(new Run(new Text(file.contentText)));

            SetMetadata(MetadataType.Author, file.Metadata[MetadataType.Author]);
            SetMetadata(MetadataType.Description, file.Metadata[MetadataType.Author]);
            SetMetadata(MetadataType.Language, file.Metadata[MetadataType.Author]);
            SetMetadata(MetadataType.Subject, file.Metadata[MetadataType.Author]);
            SetMetadata(MetadataType.Title, file.Metadata[MetadataType.Author]);
            SetMetadata(MetadataType.Type, file.Metadata[MetadataType.Author]);
        }

        public DocxParser(string fileName)
        {
            path = Path.GetTempPath() + Path.DirectorySeparatorChar + "tmp-" + DateTime.Now.Ticks.ToString();
            File.Copy(fileName, path);

            doc = WordprocessingDocument.Open(path, true);

            parsedFile = new ContentFile()
            {
                contentRaw = doc.MainDocumentPart.Document.InnerXml,
                contentText = ParseFileContent(XElement.Parse(doc.MainDocumentPart.Document.InnerXml))
            };

            SetMetadata(MetadataType.Author, doc.PackageProperties.Creator);
            SetMetadata(MetadataType.Description, doc.PackageProperties.Description);
            SetMetadata(MetadataType.Language, doc.PackageProperties.Language);
            SetMetadata(MetadataType.Subject, doc.PackageProperties.Subject);
            SetMetadata(MetadataType.Title, doc.PackageProperties.Title);
            SetMetadata(MetadataType.Type, doc.PackageProperties.ContentType);

            if (doc.PackageProperties.Created.HasValue)
                SetMetadata(MetadataType.PublishDate, doc.PackageProperties.Created.Value.ToString());

            LoadAllMetadata();
        }

        private MetadataType[] ignoredMetadata = new [] 
        {
            MetadataType.BookID,
            MetadataType.Format,
            MetadataType.Publisher,
            MetadataType.Relations,
            MetadataType.Rights,
            MetadataType.Translator
        };

        public override IEnumerable<string> GetMetadata(MetadataType type)
        {
            if (ignoredMetadata.Contains(type))
                return null;

            return parsedFile.Metadata[type];
        }

        public override void SetMetadata(MetadataType type, IEnumerable<string> value)
        {
            parsedFile.Metadata[type] = value;

            if (type == MetadataType.Author) doc.PackageProperties.Creator = value.JoinUsing(", ");
            if (type == MetadataType.Description) doc.PackageProperties.Description = value.JoinUsing(", ");
            if (type == MetadataType.Language) doc.PackageProperties.Language = value.JoinUsing(", ");
            if (type == MetadataType.Subject) doc.PackageProperties.Subject = value.JoinUsing(", ");
            if (type == MetadataType.Title) doc.PackageProperties.Title = value.JoinUsing(", ");
            if (type == MetadataType.Type) doc.PackageProperties.ContentType = value.JoinUsing(", ");
            if (type == MetadataType.PublishDate) doc.PackageProperties.Created = DateTime.Parse(value.FirstOrDefault());
        }

        public override string GetFileContent()
        {
            return parsedFile.contentText;
        }

        public override void SaveFileAs(string fileName, FileType type)
        {
            Save();

            if (File.Exists(fileName))
                File.Delete(fileName);

            File.Copy(path, fileName);

            ClearTempFile();
            disposed = true;
        }

        private void ClearTempFile()
        {
            if (!String.IsNullOrEmpty(path) && File.Exists(path))
            {
                try { File.Delete(path); }
                catch { }
            }
        }

        public string ParseFileContent(XElement xml)
        {
            StringBuilder sb = new StringBuilder();
            using (XmlReader reader = xml.CreateReader())
            {
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (reader.Name.Equals("w:t"))
                            {
                                sb.Append(reader.ReadInnerXml());
                            }
                            else if (reader.Name.Equals("w:rPr"))
                            {
                                sb.AppendLine();
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            return sb.ToString();
        }

        public void Save()
        {
            if (disposed)
                return;

            try { doc.Dispose(); }
            catch { }
        }

        public void Dispose()
        {
            Save();
            ClearTempFile();
        }

        public void LoadAllMetadata()
        {
            metadata = new DocxMetadata();

            metadata.Category = new Metadata { Name = "Category", Value = GetValue(doc.PackageProperties.Category) };
            metadata.ContentStatus = new Metadata { Name = "ContentStatus", Value = GetValue(doc.PackageProperties.ContentStatus) };
            metadata.ContentType = new Metadata { Name = "ContentType", Value = GetValue(doc.PackageProperties.ContentType) };
            metadata.Created = new Metadata { Name = "Created", Value = new[] { doc.PackageProperties.Created.HasValue ? doc.PackageProperties.Created.ToString() : "" } };
            metadata.Creator = new Metadata { Name = "Creator", Value = GetValue(doc.PackageProperties.Creator) };
            metadata.Description = new Metadata { Name = "Description", Value = GetValue(doc.PackageProperties.Description) };
            metadata.Identifier = new Metadata { Name = "Identifier", Value = GetValue(doc.PackageProperties.Identifier) };
            metadata.Keywords = new Metadata { Name = "Keywords", Value = GetValue(doc.PackageProperties.Keywords) };
            metadata.Language = new Metadata { Name = "Language", Value = GetValue(doc.PackageProperties.Language) };
            metadata.LastModifiedBy = new Metadata { Name = "LastModifiedBy", Value = GetValue(doc.PackageProperties.LastModifiedBy) };
            metadata.LastPrinted = new Metadata { Name = "LastPrinted", Value = new[] { doc.PackageProperties.LastPrinted.HasValue ? doc.PackageProperties.LastPrinted.ToString() : "" } };
            metadata.Modified = new Metadata { Name = "Modified", Value = new[] { doc.PackageProperties.Modified.HasValue ? doc.PackageProperties.Modified.ToString() : "" } };
            metadata.Revision = new Metadata { Name = "Revision", Value = GetValue(doc.PackageProperties.Revision) };
            metadata.Subject = new Metadata { Name = "Subject", Value = GetValue(doc.PackageProperties.Subject) };
            metadata.Title = new Metadata { Name = "Title", Value = GetValue(doc.PackageProperties.Title) };
            metadata.Version = new Metadata { Name = "Version", Value = GetValue(doc.PackageProperties.Version) };
        }

        private IEnumerable<string> GetValue(string v)
        {
            if (String.IsNullOrWhiteSpace(v))
                return new[] { String.Empty };

            return v.Split(new [] { ", " }, StringSplitOptions.None);
        }

        public override IEnumerable<Metadata> GetAllMetadata()
        {
            return metadata.GetAllMetadata();
        }

        public override void SetMetadata(Metadata data)
        {
            switch (data.Name)
            {
                case "Category": metadata.Category.Value = data.Value; break;
                case "ContentStatus": metadata.ContentStatus.Value = data.Value; break;
                case "ContentType": metadata.ContentType.Value = data.Value; break;
                case "Created": metadata.Created.Value = data.Value; break;
                case "Creator": metadata.Creator.Value = data.Value; break;
                case "Description": metadata.Description.Value = data.Value; break;
                case "Identifier": metadata.Identifier.Value = data.Value; break;
                case "Keywords": metadata.Keywords.Value = data.Value; break;
                case "Language": metadata.Language.Value = data.Value; break;
                case "LastModifiedBy": metadata.LastModifiedBy.Value = data.Value; break;
                case "LastPrinted": metadata.LastPrinted.Value = data.Value; break;
                case "Modified": metadata.Modified.Value = data.Value; break;
                case "Revision": metadata.Revision.Value = data.Value; break;
                case "Subject": metadata.Subject.Value = data.Value; break;
                case "Title": metadata.Title.Value = data.Value; break;
                case "Version": metadata.Version.Value = data.Value; break;
            }
        }

        public class DocxMetadata
        {
            public Metadata Category;
            public Metadata ContentStatus;
            public Metadata ContentType;
            public Metadata Created;
            public Metadata Creator;
            public Metadata Description;
            public Metadata Identifier;
            public Metadata Keywords;
            public Metadata Language;
            public Metadata LastModifiedBy;
            public Metadata LastPrinted;
            public Metadata Modified;
            public Metadata Revision;
            public Metadata Subject;
            public Metadata Title;
            public Metadata Version;

            public IEnumerable<Metadata> GetAllMetadata()
            {
                yield return Category;
                yield return ContentStatus;
                yield return ContentType;
                yield return Created;
                yield return Creator;
                yield return Description;
                yield return Identifier;
                yield return Keywords;
                yield return Language;
                yield return LastModifiedBy;
                yield return LastPrinted;
                yield return Modified;
                yield return Revision;
                yield return Subject;
                yield return Title;
                yield return Version;
            }
        }
    }
}
