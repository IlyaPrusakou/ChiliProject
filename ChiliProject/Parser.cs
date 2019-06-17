using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ChiliProject
{
    public class Parser
    {
        public List<Panel> Panels { get; set; }
        public float Xroot { get; set; }
        public float Yroot { get; set; }
        public int ViewWidth { get; set; }
        public int ViewHeight { get; set; }
        public float ScaleFactor { get; set; }

        public Parser()
        {
            ScaleFactor = 1.5F;
            Panels = new List<Panel>();
        }

        public void Extract(string filepath)
        {
            using (FileStream fs = File.Open(filepath, FileMode.Open, FileAccess.ReadWrite))
            {
                using (XmlReader rdr = XmlReader.Create(fs))
                {
                    XDocument doc = XDocument.Load(rdr);
                    XElement rootElement = doc.Root;
                    IEnumerable<XElement>  panelstag = rootElement.Descendants("panels");
                    XElement rootpanel = panelstag.Elements().ToList()[0] ;
                    var result = Getelement(rootpanel);
                    Panels.Add(result);
                    IEnumerable<XElement> gggg = rootpanel.Descendants("item");
                    foreach (var item in gggg)
                    {
                        result = Getelement(item);
                        Panels.Add(result);
                    }
                    AdditionalExtract(rootElement);
                }
            }
        }
        private void AdditionalExtract(XElement rootElement)
        {
            string Xrootstr = rootElement.Attribute("rootX").Value.Replace(".", ",");
            string Yrootstr = rootElement.Attribute("rootY").Value.Replace(".", ",");
            Xroot = Convert.ToSingle(Xrootstr) / ScaleFactor;
            Yroot = Convert.ToSingle(Yrootstr) / ScaleFactor;
            string ViewWidthstr = rootElement.Attribute("originalDocumentWidth").Value.Replace(".", ",");
            string ViewHeightstr = rootElement.Attribute("originalDocumentHeight").Value.Replace(".", ",");
            ViewWidth = Convert.ToInt32(ViewWidthstr);
            ViewHeight = Convert.ToInt32(ViewHeightstr);
        }
        private Panel Getelement(XElement rootpanel)
        {
            Panel localpanel = new Panel();
            localpanel.PanelId = rootpanel.Attribute("panelId").Value;
            localpanel.PanelName = rootpanel.Attribute("panelName").Value;
            localpanel.MinRot = rootpanel.Attribute("minRot").Value;
            localpanel.MaxRot = rootpanel.Attribute("maxRot").Value;
            localpanel.InitialRot = rootpanel.Attribute("initialRot").Value;
            localpanel.StartRot = rootpanel.Attribute("startRot").Value;
            localpanel.EndRot = rootpanel.Attribute("endRot").Value;
            localpanel.HingeOffset = rootpanel.Attribute("hingeOffset").Value;
            localpanel.PanelWidth =Convert.ToSingle(rootpanel.Attribute("panelWidth").Value.Replace(".",",")) / ScaleFactor;
            localpanel.PanelHeight = Convert.ToSingle(rootpanel.Attribute("panelHeight").Value.Replace(".", ",")) / ScaleFactor;
            localpanel.AttachedToSide = rootpanel.Attribute("attachedToSide").Value;
            localpanel.CreaseBottom = rootpanel.Attribute("creaseBottom").Value;
            localpanel.CreaseTop = rootpanel.Attribute("creaseTop").Value;
            localpanel.CreaseLeft = rootpanel.Attribute("creaseLeft").Value;
            localpanel.CreaseRight = rootpanel.Attribute("creaseRight").Value;
            localpanel.IgnoreCollisions = rootpanel.Attribute("ignoreCollisions").Value;
            localpanel.MouseEnabled = rootpanel.Attribute("mouseEnabled").Value;
            localpanel.AttachedPanels = new List<string>();
            IEnumerable<XElement> attachedpanels = rootpanel.Element("attachedPanels").Elements();
            foreach (var g in attachedpanels)
            {
                string guidpanel = g.Attribute("panelName").Value; //panelId
                localpanel.AttachedPanels.Add(guidpanel);
            }

            return localpanel;
        }

    }
}
