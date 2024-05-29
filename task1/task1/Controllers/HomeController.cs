using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using task1.Models;
using task1.Views.Home;

namespace task1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string input)
        {
            string str = input;
            int size = str.Length;
            string res = "";
            string error = "";
            string abc = "abcdefghijklmnopqrstuvwxyz";
            Dictionary<char, int> count = new Dictionary<char, int>();
            foreach (char ch in str)
            {
                if (!abc.Contains(ch) || ch.ToString().ToLower() != ch.ToString())
                {
                    if (error == "")
                    {
                        error = "Ошибка. Неверные символы: ";
                    }
                    error += ch;
                }
            }
            if (error != "")
            {
                ViewData["error"] = error;
                return View();
            }
            if (size % 2 == 0)
            {
                string str1 = str.Substring(0, size / 2);
                string str2 = str.Substring(size / 2);
                for (int i = size / 2 - 1; i >= 0; i--)
                {
                    char ch = str1[i];
                    res += ch;
                    if (!count.ContainsKey(ch))
                    {
                        count.Add(ch, 1);
                    }
                    else
                    {
                        count[ch]++;
                    }
                }
                for (int i = size / 2 - 1; i >= 0; i--)
                {
                    char ch = str2[i];
                    res += ch;
                    if (!count.ContainsKey(ch))
                    {
                        count.Add(ch, 1);
                    }
                    else
                    {
                        count[ch]++;
                    }
                }
            }
            else
            {
                for (int i = size - 1; i >= 0; i--)
                {
                    char ch = str[i];
                    res += ch;
                    if (!count.ContainsKey(ch))
                    {
                        count.Add(ch, 2);
                    }
                    else
                    {
                        count[ch] += 2;
                    }
                }
                res += str;
            }
            string countstr = "Подсчёт символов:\n";
            foreach (char key in count.Keys)
            {
                countstr += key + ": " + count[key].ToString() + '\n';
            }
            ViewData["res"] = res;
            ViewData["count"] = countstr;
            ViewData["rows"] = count.Count() + 1;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult GetString() => View();

        [HttpPost]
        public IActionResult GetString(string input)
        {
            Console.WriteLine(input);
            return View();
        }
    }
}
