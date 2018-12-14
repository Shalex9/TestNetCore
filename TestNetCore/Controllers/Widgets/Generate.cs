using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
//using NAudio.Wave;

namespace TestNetCore.Controllers
{
    public class Generate
    {
        public void tts(string text, string lang, string mp3Path, string nameFile)
        {
            if (text.Length > 0)
            {
                string fileName = nameFile;

                try
                {
                    bool exists;
                    exists = Directory.Exists(mp3Path);
                    if (!exists)
                        Directory.CreateDirectory(mp3Path);

                    string mp3FileName = mp3Path + fileName + ".mp3";
                    if (!File.Exists(mp3FileName))
                        dowloadVoice(text, lang, mp3FileName);
                    else
                        Console.WriteLine("MP3 file is exist...");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
            {
                Console.WriteLine("You need to enter a longer text.");
            }
        }

        private void dowloadVoice(string text, string lang, string mp3FileName)
        {
            WebClient wb = new WebClient();
            Console.WriteLine("Webclient created");
            Generate TokenMaker = new Generate();
            Console.WriteLine("Token generator created");
            string token = TokenMaker.MakeToken(text);
            Console.WriteLine("Token: " + token);

            string URL = "https://translate.google.com/translate_tts?ie=UTF-8&q=" + text + "&tl=" + lang + "&total=" + text + "&idx=0&textlen=" + text.Length + "&client=tw-ob&tk=" + token + "&prev=input";
            Console.WriteLine(URL);
            Console.WriteLine("Download started");
            wb.DownloadFile(URL, mp3FileName);

            wb.Dispose();

            Console.WriteLine("MP3 Lame File Created");
        }

        public string MakeToken(string text)
        {
            int time = ((int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds) / 3600;
            char[] chars = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(text)).ToCharArray();
            long stamp = time;

            foreach (int ch in chars)
            {
                stamp = (long)MakeRl((stamp + ch), "+-a^+6");
            }

            stamp = (long)MakeRl(stamp, "+-3^+b+-f");

            if (stamp < 0)
            {
                stamp = (long)((stamp & 2147483647) + 2147483648);
            }

            stamp = stamp % long.Parse((Math.Pow(10.00, 6.00)).ToString());

            return stamp + "." + (stamp ^ time);
        }

        private long MakeRl(long num, string str)
        {
            for (int i = 0; i < str.Length - 2; i += 3)
            {
                string d = str.Substring(i + 2, 1);

                if (Encoding.ASCII.GetBytes(d.ToString())[0] >= Encoding.ASCII.GetBytes("a")[0])
                {
                    d = (Encoding.ASCII.GetBytes(d.ToString())[0] - 87).ToString();
                }
                else
                {
                    d = long.Parse(d).ToString();
                }

                if (str.Substring(i + 1, 1) == "+")
                {
                    d = (num >> Int32.Parse(d)).ToString();
                }
                else
                {
                    d = (num << Int32.Parse(d)).ToString();
                }

                if (str.Substring(i, 1) == "+")
                {
                    num = num + long.Parse(d) & 4294967295;
                }
                else
                {
                    num = num ^ long.Parse(d);
                }
            }
            return num;
        }
    }
}