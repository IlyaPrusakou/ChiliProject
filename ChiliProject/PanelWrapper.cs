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
    public class PanelWrapper
    {
        public Panel CurrentPanel { get; set; }
        public Panel ParentPanel { get; set; }
        public string RotationType { get; set; }
        public List<PointF> IndexedPositions { get; set; }
        public PointF Location { get; set; }

        public PanelWrapper(Panel panel)
        {
            CurrentPanel = panel;
            IndexedPositions = new List<PointF>();
        }
        public void GetVertex()
        {
            //string Widthstr = CurrentPanel.PanelWidth.Replace(".", ",");
            //string Heightstr = CurrentPanel.PanelHeight.Replace(".", ",");
            //float CurrentPanelWidth = Convert.ToSingle (Widthstr);
            float CurrentPanelWidth = CurrentPanel.PanelWidth;
            //float CurrentPanelHeight = Convert.ToSingle (Heightstr);
            float CurrentPanelHeight = CurrentPanel.PanelHeight;
            switch (RotationType)
            {
                case "0":
                    IndexedPositions.Add(new PointF(Location.X + CurrentPanelWidth / 2, Location.Y + CurrentPanelHeight));
                    IndexedPositions.Add(new PointF(Location.X + CurrentPanelWidth, Location.Y + CurrentPanelHeight / 2));
                    IndexedPositions.Add(new PointF(Location.X + CurrentPanelWidth / 2, Location.Y));
                    IndexedPositions.Add(new PointF(Location.X, Location.Y + CurrentPanelHeight / 2));
                    break;
                case "90":
                    IndexedPositions.Add(new PointF(Location.X, Location.Y + CurrentPanelHeight / 2));
                    IndexedPositions.Add(new PointF(Location.X + CurrentPanelWidth / 2, Location.Y + CurrentPanelHeight));
                    IndexedPositions.Add(new PointF(Location.X + CurrentPanelWidth, Location.Y + CurrentPanelHeight / 2));
                    IndexedPositions.Add(new PointF(Location.X + CurrentPanelWidth / 2, Location.Y));
                    break;
                case "180":
                    IndexedPositions.Add(new PointF(Location.X + CurrentPanelWidth / 2, Location.Y));
                    IndexedPositions.Add(new PointF(Location.X, Location.Y + CurrentPanelHeight / 2));
                    IndexedPositions.Add(new PointF(Location.X + CurrentPanelWidth / 2, Location.Y + CurrentPanelHeight));
                    IndexedPositions.Add(new PointF(Location.X + CurrentPanelWidth, Location.Y + CurrentPanelHeight / 2));
                    break;
                case "270":
                    IndexedPositions.Add(new PointF(Location.X + CurrentPanelWidth, Location.Y + CurrentPanelHeight / 2));
                    IndexedPositions.Add(new PointF(Location.X + CurrentPanelWidth / 2, Location.Y));
                    IndexedPositions.Add(new PointF(Location.X, Location.Y + CurrentPanelHeight / 2));
                    IndexedPositions.Add(new PointF(Location.X + CurrentPanelWidth / 2, Location.Y + CurrentPanelHeight));
                    break;
            }
        }
        public void Swapdimension()
        {
            float futureHeight = CurrentPanel.PanelWidth;
            CurrentPanel.PanelWidth = CurrentPanel.PanelHeight;
            CurrentPanel.PanelHeight = futureHeight;
        }
    }
}
