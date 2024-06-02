using System.Reflection;
using task1.Models;
using task1.Models.Errors;
using task1.Services;

namespace PMTTasksTests
{
    public class Tests
    {

        [Test]
        public void Task1Test()
        {
            IHomeService homeService = new HomeServiceImpl();
            FormModel model = new FormModel("quicksort", "abcdef");
            model = homeService.ParseModel(model);
            Assert.AreEqual("cbafed", model.Res);
        }
        [Test]
        public void Task2Test()
        {
            IHomeService homeService = new HomeServiceImpl();
            FormModel model = new FormModel("quicksort", "123");
            model = homeService.ParseModel(model);
            Assert.AreEqual("Неверные символы: 1 2 3 ", model.Error);
        }
        [Test]
        public void Task3Test()
        {
            IHomeService homeService = new HomeServiceImpl();
            FormModel model = new FormModel("quicksort", "abcde");
            model = homeService.ParseModel(model);
            Dictionary<char, int> testDict = new Dictionary<char, int>();
            testDict.Add(
                'a', 2
                );
            testDict.Add(
                'b', 2
                );
            testDict.Add(
                'c', 2
                );
            testDict.Add(
                'd', 2
                );
            testDict.Add(
                'e', 2
                );
            Assert.AreEqual(testDict, model.Count);
        }
        [Test]
        public void Task4Test()
        {
            IHomeService homeService = new HomeServiceImpl();
            FormModel model = new FormModel("quicksort", "abcdef");
            model = homeService.ParseModel(model);
            Assert.AreEqual("afe", model.LongestSubstring);
        }
        [Test]
        public void Task5Test()
        {
            IHomeService homeService = new HomeServiceImpl();
            FormModel model = new FormModel("quicksort", "abcde");
            model = homeService.ParseModel(model);
            Assert.AreEqual("aabbccddee", model.Sorted);
        }
    }
}