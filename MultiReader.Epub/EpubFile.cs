using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ionic.Zip;
using Epub.Exceptions;
using System.IO;

namespace Epub
{
    public class EpubFile
    {
        private static string ncxFileName = @"ops/toc.ncx";
        private static string opfFileName = @"ops/content.opf";

        private ZipFile _file;

        private OpfMetadataFile _ncxMetadata;
        private OpfMetadataFile _opfMetadata;
        private List<ContentSection> _sections;


        #region Constructors
        public EpubFile()
            :this(@"Data\template.epub")
        {
        }

        public EpubFile(string filePath)
            : this(filePath, Encoding.Default)
        {
        }

        public EpubFile(string filePath, Encoding encoding)
        {
            // Załaduj archiwum 
            _file = new ZipFile(filePath, encoding);

            // Zapamiętaj referencje do plików, które mogą ulec zmianie
            ZipEntry _ncxEntry = _file.Entries.FirstOrDefault(f => Normalize(f.FileName) == ncxFileName);
            ZipEntry _opfEntry = _file.Entries.FirstOrDefault(f => Normalize(f.FileName) == opfFileName);
            IEnumerable<ZipEntry> sections = _file.Entries.Where(f => Path.GetExtension(f.FileName) == ".html");

            // Rozpakuj i rozczytaj pliki metadanych (content.opf) i spisu treści (toc.ncx)
            _ncxMetadata = new OpfMetadataFile(_ncxEntry);
            _opfMetadata = new OpfMetadataFile(_opfEntry);

            // Znajdź i wczytaj wszystkie rozdziały dokumentu
            _sections = sections.Select(e => new ContentSection(e)).ToList();

            // Usuń te pliki z archiwum - zostaną ponownie zapakowane przy zapisie pliku
            _file.RemoveEntries(sections.Union(new ZipEntry[] { _ncxEntry }).Union(new ZipEntry[] { _opfEntry }).ToArray());
        }
        #endregion

        #region Helpers
        // Normalizacja nazwy pliku (Zamiana '\' na '/', małe litery)
        private static string Normalize(string filename)
        {
            return filename.Replace(@"\", "/").ToLower();
        }

        // Lista wszystkich tagów metadanych
        public static readonly string[] ValidMetadataNames = 
        {
            "title", "language", "identifier", "creator", "contributor", "publisher", "subject", 
            "description", "date", "type", "format", "source", "relation", "coverage", "rights",
        };

        // Lista tagów metadanych, wymaganych w pliku
        public static readonly string[] RequiredMetadataNames = 
        {
            "title", "language", "identifier",
        };

        // Lista tagów metadanych, które mogą występować wielokrotnie
        public static readonly string[] MultipleOccurencesMetadataNames = 
        {
            "creator", "contributor",
        };
        private static bool IsMetadataName(string name)
        {
            return ValidMetadataNames.Contains(name);
        }
        #endregion

        #region Properties
        // Indekser, pozwalający indeksować (get, set) metadane
        public string this[string key]
        {
            // String tytul = plik["title"]
            get
            { 
                key = key.ToLower();

                if (!IsMetadataName(key))
                    throw new MetadataException(key, "Niewłaściwa nazwa metadanej.");

                return _opfMetadata.GetMetadata(key);
            }
            
            //plik["title"] = "Tytuł";
            set 
            {
                key = key.ToLower();

                if (!IsMetadataName(key))
                    throw new MetadataException(key, "Niewłaściwa nazwa metadanej.");

                _opfMetadata.SetMetadata(key, value);
            }
        }

        // Pobierz listę nazw rozdziałów w dokumencie
        public string[] GetSectionNames()
        {
            return _opfMetadata.GetSectionNames();
        }

        // Pobierz zawartość (raw html) rozdziału o danej nazwie - może być null
        public ContentSection GetSection(string id)
        {
            return _sections.FirstOrDefault(s => s.Id == id);
        }
        #endregion

        #region FileOperations
        // Pakuje i zapisuje plik do danej lokalizacji
        public void SaveTo(string fileName)
        {
            SaveTo(fileName, Encoding.Default);
        }

        public void SaveTo(string fileName, Encoding encoding)
        {
            using (Stream s = File.OpenWrite(fileName))
            {
                _file.AddEntry(opfFileName, _opfMetadata.Content, encoding);
                _file.AddEntry(ncxFileName, _ncxMetadata.Content, encoding);

                foreach (ContentSection cs in _sections)
                {
                    _file.AddEntry(cs.ArchivePath, encoding.GetBytes(cs.Content));
                }

                _file.AlternateEncoding = encoding;
                _file.AlternateEncodingUsage = ZipOption.Always;
                _file.Save(s);
            }
        }
        #endregion
    }
}
