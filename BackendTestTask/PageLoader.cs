using System;
using System.Net.Http;
using System.IO;
using AngleSharp.Html.Parser;
using BackendTestTask.Utility;
using AngleSharp.Html.Dom;

namespace BackendTestTask

{
    public class PageLoader
    {
        static readonly HttpClient client = new HttpClient();
        private void UrlChecker(string url)
        {
            Uri uriResult;
            bool tryCreateResult = Uri.TryCreate(url, UriKind.Absolute, out uriResult);
            if (tryCreateResult == true && uriResult != null)
            {
                AppLogger.GetInstance().Info("Uri has been created successefully");
                Console.WriteLine("Строка содержит корректный адрес");
                System.Threading.Thread.Sleep(1000);
            }
            else
            {
                AppLogger.GetInstance().Error("Couldn't create an uri.");
                Console.WriteLine("Некорректный url адрес");
                throw new UriFormatException();
            }
        }
        public async void LoadPage(string url)
        {
            UrlChecker(url);

            DBManager.SaveWebsiteToDB(url);


            var docCleaner = new DocumentCleaner();
            var parser = new HtmlParser();
            var separator = new Separator();

            HttpResponseMessage response;
            HttpContent content;

            AppLogger.GetInstance().Info("Trying to load page.");
            try
            {
                response = await client.GetAsync(url);
                content = response.Content;
            }
            catch (Exception)
            {
                AppLogger.GetInstance().Error("Can not load page");
                throw new Exception("Can not load page");
               
            }
            AppLogger.GetInstance().Info("Page loaded successfully.");


            Console.WriteLine("Страница успешно получена!");

            string html = await content.ReadAsStringAsync();

            IHtmlDocument HtmlDocumentContent = await parser.ParseDocumentAsync(html);

            string text = docCleaner.Clean(HtmlDocumentContent);

            using (StreamWriter sw = new StreamWriter("DownloadedHTML.html"))
            {
                sw.Write(text);
            }
            separator.CreateWords(text);
        }
    }
}






