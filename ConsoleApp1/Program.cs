namespace ConsoleApp1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using (var client = new HttpClient())
            {
                string fileName = "";
                client.BaseAddress = new Uri("");
                var httpContent = new MultipartFormDataContent();

                StreamContent fileStreamContent;
                using (var fileStream = new FileStream(fileName, FileMode.Open))
                {
                    fileStreamContent = new StreamContent(fileStream);
                    httpContent.Add(fileStreamContent, "testname", "testFileName");

                    var randomDateStreamContent = new StringContent(DateTime.Now.ToString());
                    httpContent.Add(randomDateStreamContent, "CurrentDateTime");

                    var result = await client.PostAsync("/api/LargeBlob/files", httpContent);
                }
            }
        }
    }
}