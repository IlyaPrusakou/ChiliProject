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
    public class WrapperManager
    {
        public WrapperManager()
        {

        }

        public  string GetRotationType(string side, string rotationtypeparent) 
        {
            string pattern = side + ", " + rotationtypeparent;
            string result = "";
            switch (pattern)
            {
                case "0, 0":
                    result = "180";
                    break;
                case "0, 90":
                    result = "270";
                    break;
                case "0, 180":
                    result = "0";
                    break;
                case "0, 270":
                    result = "90";
                    break;
                case "1, 0":
                    result = "90";
                    break;
                case "1, 90":
                    result = "180";
                    break;
                case "1, 180":
                    result = "270";
                    break;
                case "1, 270":
                    result = "0";
                    break;
                case "2, 0":
                    result = "0";
                    break;
                case "2, 90":
                    result = "90";
                    break;
                case "2, 180":
                    result = "180";
                    break;
                case "2, 270":
                    result = "270";
                    break;
                case "3, 0":
                    result = "270";
                    break;
                case "3, 90":
                    result = "0";
                    break;
                case "3, 180":
                    result = "90";
                    break;
                case "3, 270":
                    result = "180";
                    break;
            }
            return result;
        }

        public  PointF GetLocation(string RotationType, string HingeOffset, PointF origin, float newWidth, float newHeigth)
        {
            PointF location = new PointF();
            if (HingeOffset != "0")
            {
                float hinge = Convert.ToSingle(HingeOffset.Replace(".", ","));
                if (RotationType == "0" || RotationType == "180")
                {
                    origin.X = origin.X + hinge; 
                }
                if (RotationType == "90" || RotationType == "270")
                {
                    origin.Y = origin.Y + hinge; 
                }
            }

            if (RotationType == "270")
            {
                location.X = origin.X - newWidth;
                location.Y = origin.Y - newHeigth / 2;
            }
            else if (RotationType == "0")
            {
                location.X = origin.X - newWidth / 2;
                location.Y = origin.Y - newHeigth;
            }
            else if (RotationType == "90")
            {
                location.X = origin.X;
                location.Y = origin.Y - newHeigth / 2;
            }
            else if (RotationType == "180")
            {
                location.X = origin.X - newWidth / 2;
                location.Y = origin.Y;
            }

            return location;
        }

        public  List<PanelWrapper> WrapPanels(List<Panel> panels, float xroot, float yroot)
        {
            List<PanelWrapper> newlist = new List<PanelWrapper>();
            foreach (var panel in panels)
            {
                PanelWrapper wrappedPanel = new PanelWrapper(panel);
                newlist.Add(wrappedPanel);
            }
            newlist[0].Location = new PointF(xroot, yroot); //root locations стоят не те значения, их можно передать в этот метод
            newlist[0].ParentPanel = null;
            newlist[0].RotationType = "0";
            newlist[0].GetVertex();

            foreach (var item in newlist)
            {
                if (item.CurrentPanel.AttachedPanels.Count != 0)
                {
                    foreach (var childsname in item.CurrentPanel.AttachedPanels)
                    {
                        foreach (var wrapped in newlist)
                        {
                            if (wrapped.CurrentPanel.PanelName == childsname)
                            {
                                wrapped.ParentPanel = item.CurrentPanel;
                                wrapped.RotationType = GetRotationType(wrapped.CurrentPanel.AttachedToSide, item.RotationType);
                                int attachedside = Convert.ToInt32(wrapped.CurrentPanel.AttachedToSide);
                                //float newWidth = Convert.ToSingle(wrapped.CurrentPanel.PanelWidth.Replace(".", ","));
                                float newWidth = wrapped.CurrentPanel.PanelWidth;
                                //float newHeight = Convert.ToSingle(wrapped.CurrentPanel.PanelHeight.Replace(".", ","));
                                float newHeight = wrapped.CurrentPanel.PanelHeight;
                                wrapped.Location = GetLocation(wrapped.RotationType, wrapped.CurrentPanel.HingeOffset, item.IndexedPositions[attachedside], newWidth, newHeight);
                                wrapped.GetVertex();
                            }
                        }
                    }
                }
            }
            return newlist;
        }
    }
}
