using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace HeadIconDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            var wc = new WebClient();
            var data = wc.DownloadData(@"https://wiki.52poke.com/wiki/%E5%AE%9D%E5%8F%AF%E6%A2%A6%E5%88%97%E8%A1%A8%EF%BC%88%E6%8C%89%E5%85%A8%E5%9B%BD%E5%9B%BE%E9%89%B4%E7%BC%96%E5%8F%B7%EF%BC%89");

            var memory = new MemoryStream(data);
            var gzip = new GZipStream(memory, CompressionMode.Decompress);
            var decompress = new MemoryStream();
            gzip.CopyTo(decompress);

            var content = Encoding.UTF8.GetString(decompress.ToArray());
            var regex = new Regex(@"href=""(\S*)"" title=""(?<name>\S*)"">(\k<name>)</a>", RegexOptions.Singleline);
            var regex1 = new Regex(@"<a href=""/wiki/File:(\d*)(\S*).png""");
            var matches = regex.Matches(content);

            var list = new List<DownloadData>();
            foreach (Match match in matches)
            {
                var d = new DownloadData
                {
                    Url = $"https://wiki.52poke.com{match.Groups["1"]}",
                    Name = match.Groups["name"].ToString()
                };

                data = wc.DownloadData(d.Url);
                try
                {
                    var c = Decompress(data);
                    var m = regex1.Match(c);
                    if (m.Success)
                    {
                        d.Code = m.Groups["1"].ToString();
                        d.EnglishName = m.Groups["2"].ToString();
                        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "image", $"{d.Code}.png");
                        if (File.Exists(filePath))
                        {
                            continue;
                        }

                        var r = new Regex($@"href=""(\S*{d.Code}{d.EnglishName}.png/\d*px-{d.Code}{d.EnglishName}.png)""");
                        var fileUrl = $@"https://wiki.52poke.com/wiki/File:{d.Code}{d.EnglishName}.png";
                        var www = new WebClient();
                        var downloadData = www.DownloadData(fileUrl);
                        var con = Decompress(downloadData);

                        var px = 0;
                        var url1 = string.Empty;
                        var reg1 = new Regex(@"\S*.png/(\d*)px");
                        foreach (Match m1 in r.Matches(con))
                        {
                            var m2 = reg1.Match(m1.Groups["1"].ToString());
                            var px1 = int.Parse(m2.Groups["1"].ToString());
                            if (px1 > px)
                            {
                                px = px1;
                                url1 = m1.Groups["1"].ToString();
                            }
                        }

                        if (px > 0)
                        {
                            var imageUrl = $"https:{url1}";
                            Console.WriteLine(imageUrl);
                            www.DownloadFile(imageUrl, filePath);
                        }
                        else
                        {
                            var rr = new Regex($@"href=""(//media.52poke.com\S*{d.Code}{d.EnglishName}.png)");
                            var mr1 = rr.Match(con);
                            if (mr1.Success)
                            {
                                var imageUrl = $"https:{mr1.Groups["1"]}";
                                Console.WriteLine(imageUrl);
                                www.DownloadFile(imageUrl, filePath);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
        }

        private static string Decompress(byte[] source)
        {
            try
            {
                var sourceStream = new MemoryStream(source);
                var gzipStream = new GZipStream(sourceStream, CompressionMode.Decompress);
                var resultStream = new MemoryStream();
                gzipStream.CopyTo(resultStream);
                return Encoding.UTF8.GetString(resultStream.ToArray());
            }
            catch
            {
                return Encoding.UTF8.GetString(source);
            }
        }
    }
}
