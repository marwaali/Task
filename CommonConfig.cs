using System.Collections.Generic;
using System.Xml.Serialization;

namespace Task
{
    [XmlRoot(ElementName = "Websites")]
    public class Websites
    {
        [XmlElement(ElementName = "Website")]
        public List<Website> Website { get; set; } = new List<Website>();
    }

    [XmlRoot(ElementName = "Website")]
    public class Website
    {
        [XmlElement(ElementName = "URL")]
        public string URL { get; set; }

        [XmlElement(ElementName = "Tile")]
        public string Tile { get; set; }

        [XmlElement(ElementName = "Description")]
        public string Description { get; set; }

        [XmlElement(ElementName = "CreatedDate")]
        public string CreatedDate { get; set; }

    }
}

