using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Counters
    {
        [Key]
        public int Id { get; set; }
        
        public int Key { get; set; }

        public int Value { get; set; }
    }
}
