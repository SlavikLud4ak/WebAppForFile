using System.Diagnostics;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAppForFile.Models;
using WebAppForFile.Services.Abstract;

namespace WebAppForFile.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IFileServices _fileServices;

    public HomeController(ILogger<HomeController> logger, IFileServices fileServices)
    {
        _logger = logger;
        _fileServices = fileServices;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SaveFileAndSendEmailAsync(FileAndEmailModel faem)
    {
        if(faem.File == null || faem.File.FileName == null) return View("Index");

        _fileServices.UploadFileToAzure(faem.File);

        if (ModelState.IsValid)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(new { toAddress = faem.Email }), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://mwebappforfile.azurewebsites.net/Home/SaveFileAndSendEmail/Function1", content);

                if (response.IsSuccessStatusCode)
                {
                    //
                }
                else
                {
                    // 
                }
            }
            return RedirectToAction("Success");
        }        

        return View("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


}
