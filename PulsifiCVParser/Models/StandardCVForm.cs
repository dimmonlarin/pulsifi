using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PulsifiCVParser.Models
{
    public class StandardCVForm
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        [DefaultValue(default(List<string>))]
        public List<Experience> Experiences { get; set; }
    }
}

public class Experience
{
    public string Period { get; set; }
    public string Company { get; set; }
}
