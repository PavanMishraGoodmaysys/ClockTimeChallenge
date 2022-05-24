using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace UnitTestProject
{
    public class CommonMethod
    {
        /// <summary>
        /// Generate all possible positive test
        /// </summary>
        /// <returns></returns>
        internal static List<TestDataModel> Get_PositiveTestData()
        {
            using (StreamReader r = new StreamReader("TestData/ClockTimeTestData_Postive.json"))
            {
                string json = r.ReadToEnd();
                List<TestDataModel> list = JsonConvert.DeserializeObject<List<TestDataModel>>(json);
                return list;
            }
        }
        internal static List<TestDataModel> Get_NegativeTestData()
        {
            using (StreamReader r = new StreamReader("TestData/ClockTimeTestData_Negative.json"))
            {
                string json = r.ReadToEnd();
                List<TestDataModel> list = JsonConvert.DeserializeObject<List<TestDataModel>>(json);
                return list;
            }
        }

        /// <summary>
        /// to write the test result
        /// </summary>
        /// <param name="result"></param>
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        internal static async void PrintTestResultInTextFile(string result)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            try
            {
                string writePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData);
                string date;
                string path = Path.Combine(writePath, "test_result");
                date = DateTime.Now.ToShortDateString().Replace('/', '_');

                if (!(System.IO.Directory.Exists(path)))
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                FileStream fs1 = new FileStream(path + date + ".txt", FileMode.Append, FileAccess.Write);
                StreamWriter s1 = new StreamWriter(fs1);

                s1.Write("Result: " + result + "\n");

                s1.Close();
                fs1.Close();
            }
            catch
            {
                throw;
            }
        }
    }

}
