using System.Diagnostics;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
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
    public IActionResult SaveFileAndSendEmail(FileAndEmailModel faem)
    {
        if(faem.File == null || faem.File.FileName == null) return View("Index");

        _fileServices.UploadFileToAzure(faem.File);

        if (ModelState.IsValid)
        {
            // Process the file and email
            // ...
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
