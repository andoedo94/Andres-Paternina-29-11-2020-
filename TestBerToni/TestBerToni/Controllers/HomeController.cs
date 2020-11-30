using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TestBerToni.Models;
using TestBerToni.WebServices;

namespace TestBerToni.Controllers
{
    public class HomeController : Controller
    {
        private AlbumWebServices ws = new AlbumWebServices();
        private JavaScriptSerializer serializer = new JavaScriptSerializer();
        public ActionResult Index()
        {
            ViewData["Albums"] = ws.GetAlbums();
            return View();
        }
        
        public string Photos(int id)
        {
            IEnumerable<Photo> photos = ws.GetPhotos(id);
            return serializer.Serialize(photos);
        }

        public string Comments(int id)
        {
            IEnumerable<Comment> comments = ws.GetComments(id);
            return serializer.Serialize(comments);
        }

    }
}