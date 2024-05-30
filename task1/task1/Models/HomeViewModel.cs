using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace task1.Models
{
    public class HomeViewModel
    {
        public string SelectedSort { get; set; }
        [ViewData]
        public string Title { get; set; }
        public string Input { get; set; }
        public string Res { get; set; }
        public string Error { get; set; }
        public int Rows { get; set; }
        public string Count { get; set; }
        public string LongestSubstring { get; set; }
        public string Sorted { get; set; }
        public string TrimmedString { get;set; }

        public HomeViewModel(string selectedSort,
            string title, string input, string res, string error, int rows, string count,
            string longestSubstring, string sorted)
        {
            SelectedSort = selectedSort;
            Title = title;
            Input = input;
            Res = res;
            Error = error;
            Rows = rows;
            Count = count;
            LongestSubstring = longestSubstring;
            Sorted = sorted;
        }

        public HomeViewModel()
        {
        }

        public void OnGet()
        {
            Title = "Index";
        }
    }
}
