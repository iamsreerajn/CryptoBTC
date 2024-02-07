using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoBTC.Models
{
    public class CandlesBTC
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public long Timestamp { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public double Volume { get; set; }
        public string Resolution { get; set; }
        public bool IsClosed { get; set; }
        public DateTime TimestampISO { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate{ get; set; }
        public DateTime? LastUpdated { get; set; }
    }
    public class CandleAverage
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool IsSingleDay { get; set; }
        public double? Average { get; set; }
    }
}
