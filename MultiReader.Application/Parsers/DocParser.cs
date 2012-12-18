using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MultiReader.Parsers;
using MultiReader.Files;
using Microsoft.Win32;
using Microsoft.Office.Interop.Word;
using Word = Microsoft.Office.Interop.Word;
using System.Xml;
using System.Reflection;
using System.Runtime.InteropServices;

namespace MultiReader.Parsers
{
    class DocParser : IParser
    {
        object readOnly = false;
        object isVisible = false;
        object missing = System.Reflection.Missing.Value;
        private Word.Application WordApp;
        Word.Document doc;
        private ContentFile parsedFile;

        public DocParser()
        {
            
        }

        public DocParser(object fileName)
        {
            WordApp = new Word.Application();
            doc = WordApp.Documents.Open(ref fileName, ref missing, ref readOnly,
                                                            ref missing, ref missing, ref missing,
                                                            ref missing, ref missing, ref missing,
                                                            ref missing, ref missing, ref isVisible,
                                                            ref missing, ref missing, ref missing,
                                                            ref missing);
            parsedFile = new ContentFile()
            {
                content = "<unknown>",
                //contentFull = epub.GetContentAsHtml()
            };
            
        }

        public Dictionary<MetadataType, string> metadata = new Dictionary<MetadataType, string>();

        public string GetMetadata(MetadataType type)
        {
            string propertyName;
            string propertyValue;
            try
            {
                propertyName = type.ToString();
                propertyValue = GetWordDocumentPropertyValue(doc, propertyName).ToString();
                return propertyValue;
            }
            catch
            {
                return "";
            }
        }

        public void SetMetadata(MetadataType type, string value)
        {
            string propertyName;
            propertyName = type.ToString();
            SetWordDocumentPropertyValue(doc, propertyName, value);
            //metadata[type] = value;
        }

        public string GetFileContent()
        {
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
            return text.ToString();
        }

        public void SetFile(string fileName, string text)
        {
            object Visible = false;
            object start1 = 0;
            object end1 = 0;
 
            WordApp = new Word.Application();
            doc = WordApp.Documents.Add(ref missing, ref missing, ref missing, ref missing);
            Range rng = doc.Range(ref start1, ref missing);           
            rng.InsertAfter(text);
            object filename = fileName;

            doc.SaveAs(ref filename, ref missing, ref missing, 
                                ref missing, ref missing, ref missing,
                                ref missing, ref missing, ref missing,
                                ref missing, ref missing, ref missing, 
                                ref missing, ref missing, ref missing, ref missing);

            WordApp.Visible = true;
            //doc.RemoveDocumentInformation(WdRemoveDocInfoType.wdRDIAll);
            //doc.DocumentInspectors;
        }

        private object GetWordDocumentPropertyValue(Word.Document document, string propertyName)
        {
            object builtInProperties = document.BuiltInDocumentProperties;
            Type builtInPropertiesType = builtInProperties.GetType();
            object property = builtInPropertiesType.InvokeMember("Item", BindingFlags.GetProperty, null, builtInProperties, new object[] { propertyName });
            Type propertyType = property.GetType();
            object propertyValue = propertyType.InvokeMember("Value", BindingFlags.GetProperty, null, property, new object[] { });
            return propertyValue;

            /*object wordCustomProperties = wordDoc.CustomDocumentProperties;
            Type typeDocCustomProps = wordCustomProperties.GetType();
            foreach (string propName in customProperties.Keys)
            {
                string propValue = customProperties[propName];

                object[] oArg = { propName, false, Microsoft.Office.Core.MsoDocProperties.msoPropertyTypeString, propValue };

                typeDocCustomProps.InvokeMember("Add",
                            BindingFlags.Default | BindingFlags.InvokeMethod, null,
                            wordCustomProperties, oArg);

            }*/
        }

        private void SetWordDocumentPropertyValue(Word.Document document, string propertyName, string propertyValue)
        {
            try
            {
                object builtInProperties;
                builtInProperties = document.BuiltInDocumentProperties;
                Type builtInPropertiesType = builtInProperties.GetType();
                object property = builtInPropertiesType.InvokeMember("Item", System.Reflection.BindingFlags.GetProperty, null, builtInProperties, new object[] { propertyName });
                Type propertyType = property.GetType();
                propertyType.InvokeMember("Value", BindingFlags.SetProperty, null, property, new object[] { propertyValue });
                document.UpdateSummaryProperties();
                document.Save();
            }
            catch (COMException ex)
            {
                if (ex.ErrorCode == -2146824090)
                {
                    // No problem, user just cancelled out of the save as dialog
                    return;
                }
            }
        }
    }
}
