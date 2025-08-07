using System.ComponentModel.DataAnnotations;

namespace ThreadAndDaringStore.Models
{
    public class Product
    {
        [Key]
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public double Price { get; set; }

        public string Currency { get; set; } = "ZAR";

        public bool VatIncluded { get; set; } = true;

        public int VatRate { get; set; } = 15;

        public bool Available { get; set; }

        public string Category { get; set; } = string.Empty;

         public string Tags { get; set; } = string.Empty; // store as comma-separated
         public string ImageUrl { get; set; } = string.Empty;
        public int Stock { get; set; }
    }
}

