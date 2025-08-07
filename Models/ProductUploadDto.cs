public class ProductUploadDto
{
    public string ProductCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Price { get; set; }
    public string Category { get; set; } = string.Empty;
    public int Stock { get; set; }

    public IFormFile? Image { get; set; } 
    public List<string>? Tags { get; set; }
}
