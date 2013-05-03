using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MultiReader.Application.Parsers;
using MultiReader.Application.Files;
using MultiReader.Application.Helpers;
using System.Windows.Forms;
using MultiReader.Application.Models;

namespace MultiReader.Application
{
    public partial class Reader
    {
        public void HandleOpenEpub(string filePath)
        {
            parser = new EpubParser(filePath);
            tbAuthor.Text = parser.GetMetadata(MetadataType.Author).JoinUsing(", ");
            tbTitle.Text = parser.GetMetadata(MetadataType.Title).FirstOrDefault();
            tbPublisher.Text = parser.GetMetadata(MetadataType.Publisher).JoinUsing(", ");
            //tbTranslator.Text = parser.GetMetadata(MetadataType.Translator).JoinUsing(", ");
            tbBookID.Text = parser.GetMetadata(MetadataType.BookID).JoinUsing(", ");
            tbDateOfPublication.Text = parser.GetMetadata(MetadataType.PublishDate).FirstOrDefault();
            tbFileContent.Text = parser.GetFileContent();
        }

        public void HandleOpenPdf(string filePath)
        {
            parser = new PdfParser(filePath);
            string contentPDF = parser.GetFileContent();
            tbFileContent.Text = contentPDF;
        }

        public void HandleOpenRtf(string filePath)
        {
            // TODO: Parser
            tbFileContent.LoadFile(filePath, RichTextBoxStreamType.RichText);
        }

        public void HandleOpenDoc(string filePath)
        {
            parser = new DocxParser(filePath);
            tbFileContent.Text = parser.GetFileContent();
            tbAuthor.Text = String.Join(", ", parser.GetMetadata(MetadataType.Author));
            tbTitle.Text = String.Join(", ", parser.GetMetadata(MetadataType.Title));
            tbDateOfPublication.Text = parser.GetMetadata(MetadataType.PublishDate).FirstOrDefault();
        }

        public void HandleOpenTxt(string filePath)
        {
            parser = new PlainTextParser(filePath);
            tbFileContent.Text = parser.GetFileContent();
            parser.SetMetadata(MetadataType.Author, "unknown");
            parser.SetMetadata(MetadataType.Title, "unknown");
            parser.SetMetadata(MetadataType.Translator, "unknown");
            parser.SetMetadata(MetadataType.Publisher, "unknown");
            parser.SetMetadata(MetadataType.PublishDate, "unknown");
            parser.SetMetadata(MetadataType.BookID, "unknown");
            tbAuthor.Text = String.Join(", ", parser.GetMetadata(MetadataType.Author));
            tbTitle.Text = String.Join(", ", parser.GetMetadata(MetadataType.Title));
            //tbTranslator.Text = String.Join(", ", parser.GetMetadata(MetadataType.Translator));
            tbPublisher.Text = String.Join(", ", parser.GetMetadata(MetadataType.Publisher));
        }

        public void CellEdited(object sender, DataGridViewCellEventArgs e)
        {
            var data = new Metadata 
            { 
                Name = (string)dataGridView1.Rows[e.RowIndex].Cells[0].Value,
                Value = ((string)dataGridView1.Rows[e.RowIndex].Cells[1].Value).Split(new [] { ", " }, StringSplitOptions.None)
            };

            parser.SetMetadata(data);
        }
    }
}
