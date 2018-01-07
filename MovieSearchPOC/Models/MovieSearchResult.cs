﻿
using System.Collections.Generic;

namespace MovieSearchPOC.Models
{
    public class MovieSearchResult
    {
        public int page { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
        public List<MovieInfo> results { get; set; }
    }
    public class MovieInfo
    {
        public int vote_count { get; set; }
        public int id { get; set; }
        public bool video { get; set; }
        public double vote_average { get; set; }
        public string title { get; set; }
        public double popularity { get; set; }
        public string poster_path { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public List<object> genre_ids { get; set; }
        public string backdrop_path { get; set; }
        public bool adult { get; set; }
        public string overview { get; set; }
        public string release_date { get; set; }
    }
}