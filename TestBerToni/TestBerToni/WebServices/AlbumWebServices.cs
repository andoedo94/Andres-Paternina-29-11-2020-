using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Script.Serialization;
using TestBerToni.Models;

namespace TestBerToni.WebServices
{
    public class AlbumWebServices
    {
        private string baseUrl = System.Configuration.ConfigurationManager.AppSettings["Url"];
        private JavaScriptSerializer serializer = new JavaScriptSerializer();
        public IEnumerable<Album> GetAlbums() 
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = null;
            response = (client.GetAsync(string.Format("{0}/albums", baseUrl))).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            IEnumerable<Album> resultado = serializer.Deserialize<IEnumerable<Album>>(result);
            return resultado;
        }

        public IEnumerable<Photo> GetPhotos(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = null;
            response = (client.GetAsync(string.Format("{0}/photos", baseUrl))).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            IEnumerable<Photo> resultado = serializer.Deserialize<IEnumerable<Photo>>(result);
            return resultado.Where(photo => photo.albumId == id);
        }

        public IEnumerable<Comment> GetComments(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = null;
            response = (client.GetAsync(string.Format("{0}/comments", baseUrl))).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            IEnumerable<Comment> resultado = serializer.Deserialize<IEnumerable<Comment>>(result);
            return resultado.Where(comment => comment.postId == id);
        }
    }
}