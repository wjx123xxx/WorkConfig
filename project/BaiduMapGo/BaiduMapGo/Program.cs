using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

using Wx.App.BaiduMapGo.Common;
using Wx.App.BaiduMapGo.Entity;
using Wx.App.BaiduMapGo.Struct;

namespace BaiduMapGo
{
    internal class Program
    {
        #region Private Methods

        private static void Main(string[] args)
        {
            var regionList = File.ReadAllText("city.txt").Split(
                new[]
                {
                    "$$"
                }, StringSplitOptions.RemoveEmptyEntries);

            var task = new RegionTask(1, 14);
            foreach (var region in regionList)
            {
                var points = region.Split(';');
                IList<MercatorPoint> ml = new List<MercatorPoint>();
                foreach (var p in points)
                {
                    var location = p.Split(',');
                    var lng = Convert.ToDouble(location[0]);
                    var lat = Convert.ToDouble(location[1]);
                    var m = BaiduProjection.Instance.CoordinateToMercator(lat, lng);
                    ml.Add(m);
                }
                task.AddRegion(ml);
            }
            task.CalculateQuest();

            foreach (var zoom in task)
            {
                File.WriteAllText($"Zoom{zoom.Zoom}.txt", zoom.ToString());
            }
            var downloader = new MapDownloader(task);
            while (!downloader.IsComplete)
            {
                Console.WriteLine(downloader.GetDescription());
                Thread.Sleep(5000);
            }

            Console.ReadLine();
        }

        #endregion
    }
}