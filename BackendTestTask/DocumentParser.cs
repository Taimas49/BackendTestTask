using AngleSharp.Dom;
using System;
namespace BackendTestTask
{
    public class DocumentParser
    {
        private readonly string[] parsingAttributes = new string[] { "p", "h1", "a" };
        public string Parse(IDocument htmlDocument)
        {
            string text = "";
            foreach (string attribute in parsingAttributes)
            {
                var elements = htmlDocument.QuerySelectorAll(attribute);
                foreach (IElement element in elements)
                {
                    if (!String.IsNullOrWhiteSpace(element.TextContent))
                    {
                        text += element.TextContent.Trim();
                    }
                }
            }
            return text;
        }
    }
}
