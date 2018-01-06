using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft;
using Newtonsoft.Json;
using BlogApp.Models;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Diagnostics.Contracts;
using Realms;
using Realms.Exceptions;

namespace BlogApp.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            var realm = Realm.GetInstance();
            var json = "";
            Contract.Ensures(Contract.Result<IActionResult>() != null);

#region Grabbing json from url

            using (HttpClient hc = new HttpClient())
            {
                var url = "https://no89n3nc7b.execute-api.ap-southeast-1.amazonaws.com/staging/exercise";


                using (HttpResponseMessage response = hc.GetAsync(url).Result)
                    {
                        using (HttpContent content = response.Content)
                        {
                            json = content.ReadAsStringAsync().Result;
                            

                        }
                    }
            }
#endregion

#region Parsing json and saving it in Realm database

            JObject jsonObject = JObject.Parse(json);
            JArray articlesArray = (JArray)jsonObject["articles"];

            List<Article> articlesList = articlesArray.ToObject<List<Article>>();


            var allArticles = realm.All<Article>();
            // Clearing realm, because the data is the same from the server, dont need to check for duplicates and append the rest

            try 
            {
                if (allArticles.Count() > 0){
                    using (var trans = realm.BeginWrite())
                    {
                        realm.RemoveAll();
                        trans.Commit();
                    }
                }



            }
            catch ( Realm​Invalid​Transaction​Exception ex)
            {
                Console.WriteLine("Error deleting...");
                Console.WriteLine(ex);
            }

            // Looping thru articles in the list and then saving it in Realm

            foreach (Article obj in articlesList)
            {
                // making sure there isn't an article with an empty title or content
                var objTitle = obj.title;
                var objContent = obj.content; 
                if (objTitle != "" || objContent != ""){
                    
                    Article article = new Article(obj.title, obj.content, obj.image_url);
                    try{
                        realm.Write(() => {
                          realm.Add(article);
                      });  
                    }
                    catch
                    {
                        Console.WriteLine("Error saving");
                    }

                }

            }
#endregion

            foreach (Article item in allArticles)
            {
                Console.WriteLine(item);
            }



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
    }
}
