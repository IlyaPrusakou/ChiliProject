using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net;
using System.Threading;
using System.Xml;
using System.Xml.Linq;

namespace ChiliProject
{
    class Program
    {
        public static void Render(string path, List<PanelWrapper> list, Parser prs)
        {
            Rectangle rect = new Rectangle(0, 0, prs.ViewWidth, prs.ViewHeight);
            Image viewport = new Bitmap(prs.ViewWidth, prs.ViewHeight);
            Graphics grp = Graphics.FromImage(viewport);
            grp.PageUnit = GraphicsUnit.Pixel;
            Pen pen = new Pen(Brushes.DeepSkyBlue, 5);
            grp.FillRectangle(new SolidBrush(Color.FromName("White")), rect);
            using (Stream fs = File.Create(path))
            {
                for (int i = 0; i < list.Count; i++)
                {
                    float Width = list[i].CurrentPanel.PanelWidth;
                    float Height = list[i].CurrentPanel.PanelHeight;
                    grp.DrawRectangle(pen, list[i].Location.X, list[i].Location.Y, Width, Height);
                }
                viewport.Save(fs, ImageFormat.Jpeg);
            }
            viewport.Dispose();
            grp.Dispose();
            pen.Dispose();
        }
        

        static void Main(string[] args)
        {
            Parser prs = new Parser();
            Console.WriteLine("Input data path for extract");
            //string Data = Console.ReadLine(@"D:\ДЗ\chiliproject\docs\3.xml");  якобы ввожу
            //prs.Extract(Data);
            prs.Extract(@"D:\ДЗ\chiliproject\docs\3.xml");
            WrapperManager manager = new WrapperManager();
            List<PanelWrapper> list = manager.WrapPanels(prs.Panels, prs.Xroot, prs.Yroot);
            Render(@"D:\ДЗ\chiliproject\docs\result.jpg", list, prs);
            Console.ReadLine();
        }
    }
}
