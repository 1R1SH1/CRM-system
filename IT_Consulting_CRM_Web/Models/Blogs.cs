using Microsoft.VisualBasic;
using System;
using System.Xml.Linq;

namespace IT_Consulting_CRM_Web.Models
{
    public class Blogs
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Image { get; set; }
        public string BlogInformation { get; set; }
        public DateTime DateTime { get; set; }
    }    
}
