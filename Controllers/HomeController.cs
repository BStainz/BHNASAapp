using BHNASAapp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;


namespace BHNASAapp.Controllers{
    public class HomeController : Controller{
        private readonly ILogger<HomeController> _logger;
        private readonly BHClient _client;

        public HomeController(ILogger<HomeController> logger, BHClient client){
            _logger = logger;
            _client = client;
        }
        public async Task<IActionResult> IndexAsync(){
            //this method is saving fetched images and description to the file system,update to your paths
            try{
                var response = await _client.Get();              
                 var localPath = @"C:\Users\brend\Desktop";
                var localFileName = localPath + "text" + DateTime.Now.ToString("yyyyMMddHHmmssffff") +
               ".txt";
                var localImgFileName = localPath + "img" + DateTime.Now.ToString("yyyyMMddHHmmssffff")
               + ".jpg";
                // Appending the info to the txt file
                using (StreamWriter sw = System.IO.File.AppendText(localFileName)){
                    sw.Write(response.Url + ", " + response.Explanation + ", " + localFileName + ", " +
                    localImgFileName);
                }
                using (StreamReader sr = System.IO.File.OpenText(localFileName)){
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                }
                var remoteFileUrl = response.Url;
                if (!Uri.TryCreate(remoteFileUrl, UriKind.Absolute, out Uri? uriResult))
                    throw new InvalidOperationException("URL is invalid.");
                //Saving the image
                byte[] fileBytes = await new HttpClient().GetByteArrayAsync(remoteFileUrl);
                System.IO.File.WriteAllBytes(localImgFileName, fileBytes);
                return View(response);
            }
            catch (Exception){
                throw;
            }
        }
        public IActionResult Privacy(){
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(){
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ??
           HttpContext.TraceIdentifier
            });
        }
    }
}
//Brendan Hannon CPS-3330-01 Spring2023 HW7