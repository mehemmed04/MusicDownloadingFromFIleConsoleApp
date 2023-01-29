using MediaToolkit;
using MediaToolkit.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VideoLibrary;

namespace musics
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var links = File.ReadAllText("C:/Users/Bayr_kf60/Desktop/musics.txt").Split('\n');
            int counter = 1;
            foreach (var item in links)
            {

                var url = item.Remove(item.Length - 1, 1);

                try
                {
                    string source = "C:/Users/Bayr_kf60/Desktop/Musics/";
                    var youtube = YouTube.Default;
                    var vid = youtube.GetVideo(url);
                    string videopath = Path.Combine(source, vid.FullName);

                    byte[] bytes = vid.GetBytes();
                    File.WriteAllBytes(videopath, bytes);
                    var inputFile = new MediaFile { Filename = Path.Combine(source, vid.FullName) };
                    var outputFile = new MediaFile { Filename = Path.Combine(source, $"{counter++}.mp3") };
                    using (var engine = new Engine())
                    {
                        engine.GetMetadata(inputFile);
                        engine.Convert(inputFile, outputFile);
                    }
                    Console.WriteLine($"{counter}.mp3 downloaded");
                    File.Delete(Path.Combine(source, vid.FullName));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }


        }
    }
}
