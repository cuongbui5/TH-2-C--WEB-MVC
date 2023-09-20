using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

public class StudentController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}