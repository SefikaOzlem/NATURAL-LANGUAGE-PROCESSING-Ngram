using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class Program
    {
        static readonly Regex trimmer = new Regex(@"\s\s+"); //used to remove extra spaces in text 
        static string result;   //variable holding the final version of n grams 

        public static string append_words(string[] words, int start_index, int n) //function combining words to ask for n gram form 
        {
            string n_words = "";   // string phrase containing n words when combining words
            for (int i = start_index; i < (start_index + n); i++)
            {
                n_words = n_words + " " + words[i];
            }
            return n_words;
        }
        public static void n_gram(string file_text, int n) //The function where the text file file is split into words and string parts that take the form of n grams are created  
        {                           //and these parts are assigned to the dictionary and 50 of the most repetitive string parts are printed according to their values.     
            Dictionary<string, int> ngram_dic = new Dictionary<string, int>(); //The dictonary where the string part and its count value are kept 
            int count = 1;                         // The variable used to print only 50 of the most repeated string parts (n grams)
            string[] words = file_text.Split(' '); // Keeping words by splitting the text file according to the space 

            for (int i = 0; i < (words.Length - n) + 1; i++)
            {
                result = append_words(words, i, n);
                if (ngram_dic.ContainsKey(result.Trim()))
                {
                    ngram_dic[result.Trim()]++;
                }
                else
                {
                    ngram_dic.Add(result.Trim(), 1);
                }
            }

            // Sorting the string parts (n grams) in the dictionary according to the decreasing value and printing the first 50 of them
            foreach (KeyValuePair<string, int> datas in ngram_dic.OrderByDescending(datas => datas.Value))
            {
                if (count <= 50)
                {
                    Console.Write(count + ": ");
                    Console.WriteLine("{0} - {1}", datas.Key, datas.Value);
                }
                count++;
            }
        }
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();

            // "C:/Users/Lenovo/Desktop/4.sınıf/4408 INTRODUCTION TO NATURAL LANGUAGE PROCESSING/Novel-Samples/"+(input file name).txt; The file path where the txts are located is like this on my computer 
            try
            {
                string input_file; // asking  filename from user
                int n_val;         // asking n value from user 
                Console.Write("Enter a file name(xxx.txt) : ");
                input_file = Console.ReadLine();
                string fileName = "Novel-Samples/" + input_file; //The file path was created by combining the text files in the Novel-Samples folder with the file name entered by the user. 

                Console.Write("Enter n value : ");
                n_val = Convert.ToInt32(Console.ReadLine());
                string text = "";  // variable used to append each line of the file and keep it as a single string

                stopwatch.Start();
                using (StreamReader reader = new StreamReader(fileName, Encoding.Default))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        text += line + " ";
                    }
                }
                text = text.Trim().ToLower();
                text = Regex.Replace(text, @"[^\w\d\s]", ""); //removing punctuations from the file and replacing them with a single-character space
                text = trimmer.Replace(text, " ");            //The excess spaces formed after the previous operation are removed.
                n_gram(text, n_val);
                stopwatch.Stop();

                Console.WriteLine("Elapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("File Not Found or Mistyped");
            }
            Console.ReadLine();
        }
    }
}
