namespace SPC_API.Models
{
    public class Supplier
    {
        public int SupplierID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactInfo { get; set; }
    }

    public class ManufacturingPlant
    {
        public int PlantID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string ContactInfo { get; set; }
    }

    public class Drug
    {
        public int DrugID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }

    public class Order
    {
        public int OrderID { get; set; }
        public int DrugID { get; set; }
        public int Quantity { get; set; }
        public string OrderDate { get; set; }
    }
}
