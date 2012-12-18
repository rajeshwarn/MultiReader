using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
//using Microsoft.Win32;
//using Microsoft.Office.Interop.Word;
//using Word = Microsoft.Office.Interop.Word;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using iTextSharp.text.pdf.parser;
//using eBdb.EpubReader;
using MultiReader.Parsers;
using MultiReader.Files;

namespace MultiReader
{
    public partial class Reader : Form
    {
        public IParser parser = null;

        public Reader()
        {
            InitializeComponent();
        }

        private void Reader_Load(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            label1.Text = "";
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string chosen_File = "";

            //openFD.InitialDirectory = "C:";
            openFD.Title = "Open a File";
            openFD.FileName = "";
            openFD.Filter = "Text Document (.txt)|*.txt|Rich Text Document (.rtf)|*.rtf|Microsoft Word Document (.doc)|*.doc|PDF Document (.pdf)|*.pdf|EPUB Files (.epub)|*.epub";
            string ext;

            if (openFD.ShowDialog() != DialogResult.Cancel)
            {
                chosen_File = openFD.FileName;
                ext = Path.GetExtension(chosen_File);
                FileInfo fInfo = new FileInfo(chosen_File); 

                switch(ext)
                {
                    case ".txt":
                        parser = new PlainTextParser(chosen_File);
                        richTextBox1.Text = parser.GetFileContent();
                        parser.SetMetadata(MetadataType.Author, "unknown");
                        parser.SetMetadata(MetadataType.Title, "unknown");
                        parser.SetMetadata(MetadataType.Translator, "unknown");
                        parser.SetMetadata(MetadataType.Publisher, "unknown");
                        parser.SetMetadata(MetadataType.PublishDate, "unknown");
                        parser.SetMetadata(MetadataType.BookID, "unknown");
                        authorTextBox.Text = parser.GetMetadata(MetadataType.Author);
                        titleTextBox.Text = parser.GetMetadata(MetadataType.Title);
                        translatorTextBox.Text = parser.GetMetadata(MetadataType.Translator);
                        publisherTextBox.Text = parser.GetMetadata(MetadataType.Publisher);

                        break;
                    case ".rtf":
                        // Parser = new RtfParser(Chosen_File);
                        // richTextBox1.Text = Parser.GetFileContent();
                        richTextBox1.LoadFile(chosen_File, RichTextBoxStreamType.RichText);
                        break;
                    case ".doc":
                        parser = new DocParser(chosen_File);
                        string contentDOC = parser.GetFileContent();
                        authorTextBox.Text = parser.GetMetadata(MetadataType.Author);
                        titleTextBox.Text = parser.GetMetadata(MetadataType.Title);
                        //translatorTextBox.Text = parser.GetMetadata(MetadataType.Translator);
                        //publisherTextBox.Text = parser.GetMetadata(MetadataType.Publisher);
                        richTextBox1.Text = contentDOC;
                        break;
                    case ".pdf":
                        parser = new PdfParser(chosen_File);
                        string contentPDF = parser.GetFileContent();
                        richTextBox1.Text = contentPDF;
                        break;
                    case ".epub":
                        parser = new EpubParser(chosen_File);
                        titleTextBox.Text = parser.GetMetadata(MetadataType.Title);
                        string contentEpub = parser.GetFileContent();
                        richTextBox1.Text = contentEpub;
                        break;
                    default:
                        break;

                }

                label1.Text = openFD.FileName;
                fileNameLabel2.Text = fInfo.Name.ToString();
                fileFormatLabel2.Text = fInfo.Extension.ToString();
                fileSizeLabel2.Text = fInfo.Length.ToString();
                creationTimeLabel2.Text = fInfo.CreationTime.ToString();
                lastAccessTimeLabel2.Text = fInfo.LastAccessTime.ToString();
                lastTimeWriteLabel2.Text = fInfo.LastWriteTime.ToString();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string saved_File = "";
            //saveFD.InitialDirectory = "C:";
            saveFD.Title = "Save a Text File";
            saveFD.FileName = "";
            saveFD.Filter = "Text Document (.txt)|*.txt|Rich Text Document (.rtf)|*.rtf|Microsoft Word Document (.doc)|*.doc|PDF Document (.pdf)|*.pdf|EPUB Files (.epub)|*.epub";
            string ext;

            if (saveFD.ShowDialog() != DialogResult.Cancel)
            {
                saved_File = saveFD.FileName;
                ext = Path.GetExtension(saved_File);
                string file = richTextBox1.Text;

                switch (ext)
                {
                    case ".txt":
                        richTextBox1.SaveFile(saved_File, RichTextBoxStreamType.PlainText);
                        break;
                    case ".rtf":
                        richTextBox1.SaveFile(saved_File, RichTextBoxStreamType.RichText);
                        break;
                    case ".doc":
                        parser = new DocParser();
                        parser.SetFile(saved_File, file);
                        parser.SetMetadata(MetadataType.Author, authorTextBox.Text);
                        break;
                    case ".pdf":                        
                        parser = new PdfParser();
                        parser.SetFile(saved_File, file);
                        break;
                    case ".epub":
                        parser = new EpubParser(saved_File, "title");
                        parser.SetFile(saved_File, file);
                        break;
                    default:
                        break;
                }
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void Reader_Load_1(object sender, EventArgs e)
        {

        }

        private void fileFormatLabel2_Click(object sender, EventArgs e)
        {

        }

        private void fileSizeLabel2_Click(object sender, EventArgs e)
        {

        }

        private void creationTimeLabel2_Click(object sender, EventArgs e)
        {

        }

        private void lastAccessTimeLabel2_Click(object sender, EventArgs e)
        {

        }

        private void fileNameLabel2_Click(object sender, EventArgs e)
        {

        }

        private void lastTimeWriteLabel2_Click(object sender, EventArgs e)
        {

        }
    }
}

        /*

        private void rtfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Chosen_File = "";

            openFD.InitialDirectory = "C:";
            openFD.Title = "Open a Text File";
            openFD.FileName = "";
            openFD.Filter = "Rich Text Document (.rtf)|*.rtf";

            if (openFD.ShowDialog() != DialogResult.Cancel)
            {
                Chosen_File = openFD.FileName;
                Parser = new PlainTextParser(Chosen_File);
                richTextBox1.Text = Parser.GetFileContent();
                //richTextBox1.LoadFile(Chosen_File, RichTextBoxStreamType.RichText); //opening
                label1.Text = openFD.FileName;
            }
        }

        private void docToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.openFD.ShowDialog() == DialogResult.OK)
            {
                object fileName = openFD.FileName;
                object readOnly = false;
                object isVisible = false;
                object missing = System.Reflection.Missing.Value;
                WordApp = new Word.Application();
                Word.Document doc = WordApp.Documents.Open(ref fileName, ref missing, ref readOnly,
                                                            ref missing, ref missing, ref missing,
                                                            ref missing, ref missing, ref missing,
                                                            ref missing, ref missing, ref isVisible,
                                                            ref missing, ref missing, ref missing,
                                                            ref missing);
                StringBuilder text = new StringBuilder();
                using (XmlReader reader = XmlReader.Create(new StringReader(doc.Content.get_XML())))
                {
                    while (reader.Read())
                    {
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Element:
                                if (reader.Name.Equals("w:t"))
                                {
                                    text.Append(reader.ReadInnerXml());
                                }
                                else if (reader.Name.Equals("w:rPr"))
                                {
                                    text.AppendLine();
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
                richTextBox1.Text = text.ToString();
                label1.Text = openFD.FileName;
            }
        }

        private void pdfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFD.InitialDirectory = "C:";
            openFD.Title = "Open a PDF File";
            openFD.FileName = "";
            openFD.Filter = "PDF Files|*.pdf";

            StringBuilder text = new StringBuilder();
            
            if (this.openFD.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(openFD.FileName))
                {
                    PdfReader pdfReader = new PdfReader(openFD.FileName);

                    for (int page = 1; page <= pdfReader.NumberOfPages; page++)
                    {
                        ITextExtractionStrategy strategy = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();
                        string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);

                        currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));
                        text.Append(currentText);
                        pdfReader.Close();
                    }
               }

                richTextBox1.Text = text.ToString();
                label1.Text = openFD.FileName;
            }
        }

        private void epubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFD.InitialDirectory = "C:";
            openFD.Title = "Open a EPUB File";
            openFD.FileName = "";
            openFD.Filter = "EPUB Files|*.epub";

            if (this.openFD.ShowDialog() == DialogResult.OK)
            {
                Epub epub = new Epub(openFD.FileName);
                string plainText = epub.GetContentAsPlainText();
                richTextBox1.Text = plainText;
                label1.Text = openFD.FileName;
            }
        }

        private void pdfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFD.InitialDirectory = "C:";
            openFD.Title = "Open a PDF File";
            openFD.FileName = "";
            openFD.Filter = "PDF Files|*.pdf";

            StringBuilder text = new StringBuilder();

            if (this.openFD.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(openFD.FileName))
                {
                    PdfReader pdfReader = new PdfReader(openFD.FileName);

                    for (int page = 1; page <= pdfReader.NumberOfPages; page++)
                    {
                        ITextExtractionStrategy strategy = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();
                        string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);

                        currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));
                        text.Append(currentText);
                        pdfReader.Close();
                    }
                }

                richTextBox1.Text = text.ToString();
                label1.Text = openFD.FileName;
            }*/

/*
            // step 1: creation of a document-object
            iTextSharp.text.Document myDocument = new iTextSharp.text.Document(PageSize.A4.Rotate());
            try
            {

                // step 2:
                // Now create a writer that listens to this doucment and writes the document to desired Stream.

                PdfWriter.GetInstance(myDocument, new FileStream("Salman.pdf", FileMode.Create));

                // step 3:  Open the document now using
                myDocument.Open();

                // step 4: Now add some contents to the document
                myDocument.Add(new iTextSharp.text.Paragraph("First Pdf File made by Salman using iText"));

            }
            catch (DocumentException de)
            {
                Console.Error.WriteLine(de.Message);
            }
            catch (IOException ioe)
            {
                Console.Error.WriteLine(ioe.Message);
            }

            // step 5: Remember to close the documnet

            myDocument.Close();*/