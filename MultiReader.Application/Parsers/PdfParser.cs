using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MultiReader.Application.Parsers;
using MultiReader.Application.Files;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using iTextSharp.text;
using MultiReader.Application.Models;

namespace MultiReader.Application.Parsers
{
    public class PdfParser : AbstractParser
    {
        PdfReader pdfReader;
        //PdfWriter pdfWriter;

        public PdfParser(string fileName)
        {
            pdfReader = new PdfReader(fileName);

            parsedFile = new ContentFile()
            {
                contentText = GetFileContent()
            };
        }

       /* public void InitializeWriter(string fileName)
        {
            iTextSharp.text.Document myDocument = new iTextSharp.text.Document();
            FileStream fileStream = new FileStream();
            pdfWriter = new PdfWriter();
        }*/

        public override IEnumerable<string> GetMetadata(MetadataType type)
        {
            if (parsedFile.Metadata.ContainsKey(type))
                return parsedFile.Metadata[type];
            return null;
        }

        public override void SetMetadata(MetadataType type, IEnumerable<string> value)
        {
            parsedFile.Metadata[type] = value.ToList();
        }

        public override string GetFileContent()
        {
            StringBuilder text = new StringBuilder();

            for (int page = 1; page <= pdfReader.NumberOfPages; page++)
            {
                ITextExtractionStrategy strategy = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();
                string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);

                currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));
                text.Append(currentText);
                pdfReader.Close();
            }
            return text.ToString();
        }

        public override void SaveFileAs(string fileName, FileType type)
        {
            // step 1: creation of a document-object
            iTextSharp.text.Document myDocument = new iTextSharp.text.Document(PageSize.A4.Rotate());
            try
            {
                // step 2:
                // Now create a writer that listens to this doucment and writes the document to desired Stream.
                PdfWriter.GetInstance(myDocument, new FileStream(fileName, FileMode.CreateNew));

                // step 3:  Open the document now using
                myDocument.Open();

                // step 4: Now add some contents to the document
                myDocument.Add(new iTextSharp.text.Paragraph(parsedFile.contentRaw));

            }
            catch (iTextSharp.text.DocumentException de)
            {
                Console.Error.WriteLine(de.Message);
            }
            catch (IOException ioe)
            {
                Console.Error.WriteLine(ioe.Message);
            }
            myDocument.Close();
        }
    }
}
