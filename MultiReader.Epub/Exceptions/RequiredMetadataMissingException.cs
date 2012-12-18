using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epub.Exceptions
{
    public class RequiredMetadataMissingException : Exception
    {
        public RequiredMetadataMissingException(string metadataName, string message)
            : base(message)
        {
            MetadataName = metadataName;
        }

        public string MetadataName { get; set; }
    }
}
