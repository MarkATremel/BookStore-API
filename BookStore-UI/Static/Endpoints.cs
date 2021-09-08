using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore_UI.Static
{
    public static class Endpoints
    {
        public static string BaseURL = "http://localhost:64706/";
        public static string AuthorsEndpoint = $"{BaseURL}api/authors/";
        public static string BooksEndpoint = $"{BaseURL}api/books/";
        public static string RegisterEndpoint = $"{BaseURL}api/users/register/";
        public static string LoginEndpoint = $"{BaseURL}api/users/login/";
    }
}
