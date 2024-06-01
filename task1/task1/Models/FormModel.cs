using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace task1.Models
{
    [Serializable]
    public class FormModel
    {
        public string Res { get; set; }
        public int firstIndex { get;set; }
        public int lastIndex { get;set; }
        public string SelectedSort { get; set; }
        [ViewData]
        public string Input { get; set; }
        public int Rows { get; set; }
        public Dictionary<char, int> Count { get; set; }
        public string LongestSubstring { get; set; }
        public string Sorted { get; set; }
        public string TrimmedString { get; set; }
        public int TrimIndex { get; set; }
        [NonSerialized]
        public string Error;

        public FormModel(string selectedSort, string input)
        {
            SelectedSort = selectedSort;
            Input = input;
        }

        public FormModel()
        {
        }

    }
}
