using AngleSharp.Html.Dom;
using System.Linq;



namespace BackendTestTask
{
    public class DocumentCleaner
    {
        public string Clean(IHtmlDocument document)
        {
            var body = CleanTextFromDocument(document.Body);

            var text = body.TextContent;

            return text;
        }

        private IHtmlElement CleanTextFromDocument(IHtmlElement body)
        {
            var scripts = body.QuerySelectorAll("script");
            var style = body.QuerySelectorAll("style");
            var itemsToRemove = scripts.Concat(style);
            foreach (var item in itemsToRemove)
            {
                item.Remove();
            }
            return body;
        }
    }
}
