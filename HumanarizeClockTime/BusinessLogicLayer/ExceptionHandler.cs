using System;
using System.IO;

namespace ClockTimeToWord.BusinessLogicLayer
{
    public static class ExceptionHandler
    {
        public static void LogException(Exception ex)
        {
            try
            {
                string writePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData);
                string date;
                string path = Path.Combine(writePath, "Exception_");
                date = DateTime.Now.ToShortDateString().Replace('/', '_');

                if (!(System.IO.Directory.Exists(path)))
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                using (FileStream fs1 = new FileStream(path + date + ".txt", FileMode.Append, FileAccess.Write))
                {
                    StreamWriter s1 = new StreamWriter(fs1);
                    s1.Write(ex);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
