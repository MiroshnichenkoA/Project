using System;
using System.IO;
using System.Threading.Tasks;

namespace Logger
{
    public static class Logger
    {
        const int Length = 30;

        private static void Main(string[] args)
        {
        }

        public static void Loging(string textToLogg)
        {
            int counter = 1;
            string name = String.Concat(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, "_", counter);
            string path = @$"C:\Logger\{name}.txt";
            FileInfo fileInfo = new FileInfo(path);

            if (fileInfo.Exists)
            {
                if (fileInfo.Length >= Length)
                {
                    counter += 1;
                    name = String.Concat(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, "_", counter);
                    path = @$"C:\Logger\{name}.txt";
                    fileInfo = new FileInfo(path);
                    textToLogg = String.Join(" ", DateTime.Now, textToLogg);
                    LogThis(textToLogg, path);
                }
                else
                {
                    textToLogg = String.Join(" ", DateTime.Now, textToLogg);
                    LogThis(textToLogg, path);
                }
            }
            else
            {
                textToLogg = String.Join(" ", DateTime.Now, textToLogg);
                LogThis(textToLogg, path);
            }
        }
        
        static async Task LogThis(string textToLogg, string path)
        {
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                await sw.WriteLineAsync(textToLogg);
            }
        }
    }
}
