using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PulsifiApp.Models
{
    public class Job
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [ForeignKey("Location")]
        public int? LocationID { get; set; }
        public Location Location { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
       
        [Column(TypeName = "integer")]
        public JobStatus Status { get; set; }
    }

    public enum JobStatus
    {
        Active = 0,
        Inactive = 1
    }
}
