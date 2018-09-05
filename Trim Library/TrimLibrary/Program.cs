using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrimLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            string line;
            List<string> words = new List<string>();
            List<string> toRemove = new List<string>();
            StreamReader file = new StreamReader(@"C:\Users\beanifer\Desktop\WordLibrary.txt");
            for (int count = 0; (line = file.ReadLine()) != null; count++)
            {
                words.Add(line);
            }
            Console.WriteLine("{0} words found.", words.Count);
            foreach (string item in words)
            {
                if (item.Count() > 8 || item.Count() < 4)
                {
                    toRemove.Add(item);
                }
            }
            foreach (string item in toRemove)
            {
                try
                {
                    words.Remove(item);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            words.Sort();
            Console.WriteLine("{0} words after deletions.", words.Count);
            using (StreamWriter outputFile = new StreamWriter(@"C:\Users\beanifer\Desktop\words_new.txt"))
            {
                foreach (string word in words)
                {
                    outputFile.WriteLine(word);
                }
            }
            Console.WriteLine("Done!");
            file.Close();
        }
    }
}
