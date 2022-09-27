using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gas.Models
{
    public class ServiceOrderModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }       
        public string Text { get; set; }
        public DateTimeOffset DateProccess { get; set; }
        public int IdClient { get; set; }
        public bool isVisible { get; set; }
        public bool Delivered { get; set; }

        
    }
}
