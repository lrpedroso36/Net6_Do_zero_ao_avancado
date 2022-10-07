public static class ProductsRepository
{
    public static List<Product> Products { get; set; }

    public static void AddProduct(Product product)
    {
        if (product == null)
        {
            Products = new List<Product>();
        }
        Products.Add(product);
    }

    public static Product GetBy(string code)
    {
        return Products.First(x => x.Code == code);
    }
}
