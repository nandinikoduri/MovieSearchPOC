using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using MovieSearchPOC.Models;
using Newtonsoft.Json;

namespace MovieSearchPOC.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Method to return Index View
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Method to search for the movies with the entered text
        /// </summary>
        /// <param name="searchText">Entered search text</param>
        /// <returns>Partial view with the list of top 20 movies</returns>
        public async Task<ActionResult> SearchMovie(string searchText)
        {
            List<Movie> movieInfo = new List<Movie>();
            //Hosted web API REST Service base url  
            string baseurl = Convert.ToString(ConfigurationManager.AppSettings["BASEURL"]);
            string apiKey= Convert.ToString(ConfigurationManager.AppSettings["TMDBAPIKEY"]);
            string posterBaseUrl = Convert.ToString(ConfigurationManager.AppSettings["POSTERBASEURL"]);
            try
            {
                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource using HttpClient  
                    //per page it returns max 20 rows by default, so taking only the first page 
                    //so that we get top 20 as a result.
                    HttpResponseMessage res = await client.GetAsync("search/movie?api_key="+apiKey+"&page=1&query=" + searchText);

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var movieResponse = res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Movie list  
                        MovieSearchResult movieSearchResult = JsonConvert.DeserializeObject<MovieSearchResult>(movieResponse);
                        movieInfo = (from mv in movieSearchResult.results.ToList()
                                     select new Movie
                                     {
                                         Id = mv.id,
                                         Title = mv.title,
                                         AverageVote = mv.vote_average,
                                         Overview = mv.overview,
                                         PosterPath = posterBaseUrl + mv.poster_path,
                                         ReleaseDate = mv.release_date
                                     }).ToList();

                    }
                    //returning the partial view with movie list
                    return PartialView("_Movie", movieInfo);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}