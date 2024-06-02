using Microsoft.AspNetCore.Identity;
using System.Net.Mail;
using System.Text.RegularExpressions;
using task1.Controllers;
using task1.Models;
using task1.Utilities;

namespace task1.Services
{
    public class HomeServiceImpl : IHomeService
    {
        private char[] Quicksort(char[] array, int leftIndex, int rightIndex)
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
        public FormModel ParseModel(FormModel model)
        {
            JSONHelper json = new JSONHelper();
            json.Deserialize();
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
            var method = model.SelectedSort.ToLower();

            if (JSONHelper.IsBlackListed(input))
            {
                model.Error = "Введённая строка находится в чёрном списке: " + input;
                return model;
            }

            foreach (char ch in str)
            {
                if (!abc.Contains(ch) || ch.ToString().ToLower() != ch.ToString())
                {
                    error += ch + " ";
                }
            }

            if (error != "")
            {
                model.Error = "Неверные символы: " + error;
                return model;
            }

            if (method != "quicksort" && method != "treesort")
            {
                model.Error = "Неверный метод сортировки: " + method;
                return model;
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

            if (method == "quicksort")
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
                string url = json.RandomApi + "min=0&max=" + (sizeNew - 1) + "&count=1";
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

            string trimmedString = res.Substring(0, randIndex) + res.Substring(randIndex + 1, sizeNew - randIndex - 1);

            model.TrimIndex = randIndex;
            model.Res = res;
            model.Count = count;
            model.Rows = count.Count() + 2;
            model.LongestSubstring = res.Substring(firstIndex, lastIndex - firstIndex + 1);
            model.firstIndex = firstIndex;
            model.Sorted = sortedstr;
            model.TrimmedString = trimmedString;
            return model;
        }
    }
}
