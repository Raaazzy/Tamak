using Tamak.Data.Enum;

namespace Tamak.Data.Models
{
    public class Time
    {
        public long Id { get; set; }
        public string StringData { get; set; }
        public int NumData { get; set; }
        public bool Avaliable { get; set; }
        public long AssortimentId { set; get; }
        public virtual Assortiment Assortiment { set; get; }
    }
}
