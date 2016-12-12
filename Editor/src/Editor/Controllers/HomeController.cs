using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Editor.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Editor.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult SelectUser()
        {
            EditorModel model = new EditorModel();
            string editorString = HttpContext.Session.GetString("currenteditor");
            if (!String.IsNullOrEmpty(editorString))
            {
                model = JsonConvert.DeserializeObject<EditorModel>(editorString);
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult SelectUser(EditorModel model)
        {
            EditorModel newModel = new EditorModel();
            newModel.UserName = model.UserName;

            string editorString = HttpContext.Session.GetString("currenteditor");
            if (!String.IsNullOrEmpty(editorString))
            {
                model = JsonConvert.DeserializeObject<EditorModel>(editorString);
                newModel.Text = model.Text;
            }

            HttpContext.Session.SetString("currenteditor", JsonConvert.SerializeObject(newModel));
            return RedirectToAction("Editor");
        }

        public IActionResult Editor()
        {
            string editorString = HttpContext.Session.GetString("currenteditor");
            EditorModel model = JsonConvert.DeserializeObject<EditorModel>(editorString);
            return View(model);
        }

        [HttpPost]
        public IActionResult Editor(string editor1)
        {
            string editorString = HttpContext.Session.GetString("currenteditor");
            EditorModel model = JsonConvert.DeserializeObject<EditorModel>(editorString);
            model.Text = editor1;
            HttpContext.Session.SetString("currenteditor", JsonConvert.SerializeObject(model));
            return RedirectToAction("SelectUser");
        }
    }
}
