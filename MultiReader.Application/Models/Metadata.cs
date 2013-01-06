using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MultiReader.Application.Helpers;

namespace MultiReader.Application.Models
{
    public class Metadata
    {
        public string Name { get; set; }
        public IEnumerable<string> Value { get; set; }

        public bool IsEmpty { get { return Value == null || Value.Count() == 0; } }

        public override string ToString()
        {
            return IsEmpty ? String.Empty : Value.JoinUsing(", ");
        }

        public static Metadata FromString(string name, string content, bool multiValued = false)
        {
            if (content == null) content = String.Empty;

            return new Metadata
            {
                Name = name,
                Value = multiValued 
                        ? new List<string> { content }
                        : content.Split(new [] { ", " }, StringSplitOptions.RemoveEmptyEntries).ToList()
            };
        }
    }
}
