namespace ThreadAndDaringStore.Models
{
    public class ProductDto
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public double Price { get; set; }

        public string Currency { get; set; } = "ZAR";

        public bool VatIncluded { get; set; } = true;

        public int VatRate { get; set; } = 15;

        public bool Available { get; set; }

        public string Category { get; set; } = string.Empty;

        public List<string> Tags { get; set; } = new();

        public string ImageUrl { get; set; } = string.Empty;

        public int Stock { get; set; }
    }
}
