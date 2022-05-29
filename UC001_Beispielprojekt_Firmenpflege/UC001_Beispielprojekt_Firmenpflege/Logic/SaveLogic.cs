using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UC001_Beispielprojekt_Firmenpflege.Models;

namespace UC001_Beispielprojekt_Firmenpflege.Logic
{
    public static class SaveLogic
    {
        private static SearchModel Search { get; set; } = new SearchModel();

        public static void SaveSearch(SearchModel search)
        {
            Search = search;
        }

        public static SearchModel GetSearch()
        {
            return Search;
        }
    }
}