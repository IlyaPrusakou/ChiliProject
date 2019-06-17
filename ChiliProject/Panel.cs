using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChiliProject
{
    public class Panel
    {
        public string PanelId { get; set; }
        public string PanelName { get; set; }
        public string MinRot { get; set; }
        public string MaxRot { get; set; }
        public string InitialRot { get; set; }
        public string StartRot { get; set; }
        public string EndRot { get; set; }
        public string HingeOffset { get; set; }
        public float PanelWidth { get; set; }
        public float PanelHeight { get; set; }
        public string AttachedToSide { get; set; }
        public string CreaseBottom { get; set; }
        public string CreaseTop { get; set; }
        public string CreaseLeft { get; set; }
        public string CreaseRight { get; set; }
        public string IgnoreCollisions { get; set; }
        public string MouseEnabled { get; set; }
        public List<string> AttachedPanels { get; set; }

        public Panel()
        {

        }
    }
}
