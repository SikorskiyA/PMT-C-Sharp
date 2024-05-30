using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using task1.Models;
using task1.Views.Home;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace task1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            var model = new HomeViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(HomeViewModel model)
        {
            string input = model.Input;
            if (input == null)
            {
                input = "";
            }
            string str = input;
            int size = str.Length;
            string res = "";
            string error = "";
            string abc = "abcdefghijklmnopqrstuvwxyz";
            Dictionary<char, int> count = new Dictionary<char, int>();
            var method = model.SelectedSort;

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
                model.Error = error;
                return View(model);
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

            int sizeNew = res.Length;
            int firstIndex = -1;
            int lastIndex = -1;
            string vowels = "aeiouy";

            for (int i = 0; i < sizeNew && firstIndex == -1; i++)
            {
                firstIndex = vowels.Contains(res[i]) ? i : -1;
            }
            for (int i = sizeNew - 1; i >= 0 && lastIndex == -1; i--)
            {

                lastIndex = vowels.Contains(res[i]) ? i : -1;
            }

            char[] sorted;
            string sortedstr = "";

            if (method == "Quicksort")
            {
                sorted = Quicksort(res.ToArray(), 0, sizeNew - 1);
            }
            else
            {
                sorted = TreeSort(res.ToArray());
            }

            for (int i = 0; i < sizeNew; i++)
            {
                sortedstr += sorted[i];
            }

            int randIndex = -1;

            try
            {
                var client = new HttpClient();
                string url = "https://www.randomnumberapi.com/api/v1.0/random?min=0&max=" + (sizeNew - 1) + "&count=1";
                var responce = client.GetAsync(url);
                var apiResponce = responce.Result.Content.ReadAsStringAsync().Result;
                string num = "";

                for (int i = 1; i < apiResponce.Length && apiResponce[i] != ']'; i++)
                {
                    num += apiResponce[i];
                }

                randIndex = Convert.ToInt32(num);
            }
            catch
            {
                Random rand = new Random();
                randIndex = rand.Next(0, sizeNew - 1);
            }

            string trimmedString = "Индекс удаляемого символа - " + randIndex.ToString() + "\nСтрока после обработки:\n" + res.Substring(0, randIndex) +
                res.Substring(randIndex + 1, sizeNew - randIndex - 1);

            model.Title = "task1";
            model.Res = res;
            model.Count = countstr;
            model.Rows = count.Count() + 2;
            model.LongestSubstring = firstIndex >= 0 ?
                "Самая длинная подстрока, начинающаяся и заканчивающаяся гласной: \n" +
                res.Substring(firstIndex, lastIndex - firstIndex + 1) :
                "В строке нет гласных";
            model.Sorted = "Отсортированная строка: \n" + sortedstr;
            model.TrimmedString = trimmedString;

            return View(model);
        }

        [HttpGet]
        public IActionResult GetString() => View();

        [HttpPost]
        public IActionResult GetString(string input)
        {
            Console.WriteLine(input);
            return View();
        }

        public char[] Quicksort(char[] array, int leftIndex, int rightIndex)
        {
            var i = leftIndex;
            var j = rightIndex;
            var pivot = array[leftIndex];
            while (i <= j)
            {
                while (array[i] < pivot)
                {
                    i++;
                }

                while (array[j] > pivot)
                {
                    j--;
                }
                if (i <= j)
                {
                    char temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                    i++;
                    j--;
                }
            }

            if (leftIndex < j)
                Quicksort(array, leftIndex, j);
            if (i < rightIndex)
                Quicksort(array, i, rightIndex);
            return array;
        }
        private static char[] TreeSort(char[] array)
        {
            var treeNode = new TreeNode(array[0]);
            for (int i = 1; i < array.Length; i++)
            {
                treeNode.Insert(new TreeNode(array[i]));
            }

            return treeNode.Transform();
        }
    }
    public class TreeNode
    {
        public TreeNode(char data)
        {
            Data = data;
        }

        public char Data { get; set; }
        public TreeNode Left { get; set; }
        public TreeNode Right { get; set; }
        public void Insert(TreeNode node)
        {
            if (node.Data < Data)
            {
                if (Left == null)
                {
                    Left = node;
                }
                else
                {
                    Left.Insert(node);
                }
            }
            else
            {
                if (Right == null)
                {
                    Right = node;
                }
                else
                {
                    Right.Insert(node);
                }
            }
        }
        public char[] Transform(List<char> elements = null)
        {
            if (elements == null)
            {
                elements = new List<char>();
            }

            if (Left != null)
            {
                Left.Transform(elements);
            }

            elements.Add(Data);

            if (Right != null)
            {
                Right.Transform(elements);
            }

            return elements.ToArray();
        }
    }
}
