using Microsoft.AspNetCore.Mvc;
using DevExtreme.NETCore.Demos;
using Newtonsoft.Json;

namespace DevExtreme.NETCore.Demos.Controllers
{
    public class VectorMapController : Controller
    {
        public ActionResult Palette()
        {
            return View();
        }
    }
}