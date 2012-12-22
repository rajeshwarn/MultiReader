using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epub;

namespace MultiReader.Application.Parsers
{
    class Class1
    {

        public Class1()
        {
            Cosnowego();
        }
        public void Cosnowego()
        {
        
            var epub = new Document();

            // set metadata
            epub.AddAuthor("Jerome K. Jerome");
            epub.AddTitle("Three Men in a Boat (To Say Nothing of the Dog)");
            epub.AddLanguage("en");

            // embed fonts
            epub.AddFile("C:\\Fonts\\LiberationSerif-Regular.ttf",
              "fonts/LiberationSerif-Regular.ttf", "application/octet-stream");
            epub.AddFile("C:\\Fonts\\LiberationSerif-Bold.ttf",
              "fonts/LiberationSerif-Bold.ttf", "application/octet-stream");
            epub.AddFile("C:\\Fonts\\LiberationSerif-Italic.ttf",
              "LiberationSerif-Italic.ttf", "application/octet-stream");
            epub.AddFile("C:\\Fonts\\LiberationSerif-BoldItalic.ttf",
              "fonts/LiberationSerif-BoldItalic.ttf", "application/octet-stream");

            // Add stylesheet with @font-face
            epub.AddStylesheetFile("templates\\style.css", "style.css");

            // Add image files (figures)
            epub.AddImageFile("figures\\fig1.png", "fig1.png");
            epub.AddImageFile("figures\\drawing.svg", "drawing.svg");

            // add chapters' xhtml and setup TOC entries
            int navCounter = 1;
            for (int chapterCounter = 1; chapterCounter < 10; chapterCounter++)
            {
                String chapterFile = String.Format("page{0}.xhtml", chapterCounter);
                String chapterName = String.Format("Chapter {0}", chapterCounter);
                epub.AddXhtmlFile("tempdir\\" + chapterFile, chapterFile);
                var chapterTOCEntry =
                    epub.AddNavPoint(chapterName, chapterFile, navCounter++);
                // add nested TOC entries
                for (int part = 0; part < 3; part++)
                {
                    String partName = String.Format("Part {0}", part);
                    String partHref = chapterFile + String.Format("#{0}", part);
                    chapterTOCEntry.AddNavPoint(partName, partHref, navCounter++);
                }
            }

            // Generate resulting epub file
            epub.Generate("output\\mybook.epub");
        }
    }
}
