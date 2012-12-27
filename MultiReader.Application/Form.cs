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
using MultiReader.Application.Parsers;
using MultiReader.Application.Files;
using MultiReader.Application.Helpers;
using MultiReader.Application.Models;

namespace MultiReader.Application
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
            tbFileContent.Clear();
            label1.Text = "";
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filePath = "";

            //openFD.InitialDirectory = "C:";
            ofd.Title = "Open a File";
            ofd.FileName = "";
            ofd.Filter = "Text Document|*.txt|Rich Text Document|*.rtf|Microsoft Word Document|*.docx|PDF Document|*.pdf|EPUB Files|*.epub";
            string ext;

            if (ofd.ShowDialog() != DialogResult.Cancel)
            {
                filePath = ofd.FileName;
                ext = Path.GetExtension(filePath);
                FileInfo fInfo = new FileInfo(filePath); 

                switch(ext)
                {
                    case ".txt":
                        HandleOpenTxt(filePath);
                        break;
                    case ".rtf":
                        HandleOpenRtf(filePath);
                        break;
                    case ".docx":
                        HandleOpenDoc(filePath);
                        break;
                    case ".pdf":
                        HandleOpenPdf(filePath);
                        break;
                    case ".epub":
                        HandleOpenEpub(filePath);
                        break;
                    default:
                        break;

                }

                label1.Text = ofd.FileName;
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
            saveFD.Filter = "Text Document|*.txt|Rich Text Document|*.rtf|Microsoft Word Document|*.docx|PDF Document|*.pdf|EPUB File|*.epub";
            string ext;

            if (saveFD.ShowDialog() != DialogResult.Cancel)
            {
                saved_File = saveFD.FileName;
                ext = Path.GetExtension(saved_File);
                string file = tbFileContent.Text;

                switch (ext)
                {
                    case ".txt":
                        tbFileContent.SaveFile(saved_File, RichTextBoxStreamType.PlainText);
                        break;
                    case ".rtf":
                        tbFileContent.SaveFile(saved_File, RichTextBoxStreamType.RichText);
                        break;
                    case ".docx":
                        parser.SaveFileAs(saved_File, FileType.Docx);
                        break;
                    case ".pdf":                      
                        parser.SaveFileAs(saved_File, FileType.Pdf);
                        break;
                    case ".epub":
                        parser.SaveFileAs(saved_File, FileType.Epub);
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
            tbFileContent.Copy();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbFileContent.Cut();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbFileContent.Paste();
        }
    }
}