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
    }
}
