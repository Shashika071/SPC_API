using System.Text.Json;
using System.IO;
using SPC_API.Models;
using System.Linq;

namespace SPC_API.Services
{
    public class JsonFileService
    {
        private static string baseDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Data");

        private static string suppliersFilePath = Path.Combine(baseDirectory, "suppliers.json");
        private static string plantsFilePath = Path.Combine(baseDirectory, "plants.json");
        private static string drugsFilePath = Path.Combine(baseDirectory, "drugs.json");
        private static string ordersFilePath = Path.Combine(baseDirectory, "orders.json");

        // Helper Method to Read Data from JSON
        private static List<T> ReadData<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<T>(); // Return empty list if file does not exist
            }

            var jsonData = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<T>>(jsonData) ?? new List<T>(); // In case deserialization fails
        }

        // Helper Method to Write Data to JSON
        private static void WriteData<T>(string filePath, List<T> data)
        {
            // Ensure the directory exists
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            var jsonData = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, jsonData);
        }

        // CRUD Operations for Suppliers
        public List<Supplier> GetSuppliers() => ReadData<Supplier>(suppliersFilePath);

        public void AddSupplier(Supplier supplier)
        {
            var suppliers = ReadData<Supplier>(suppliersFilePath);
            supplier.SupplierID = suppliers.Count == 0 ? 1 : suppliers.Max(s => s.SupplierID) + 1;
            suppliers.Add(supplier);
            WriteData(suppliersFilePath, suppliers);
        }

        // CRUD Operations for Manufacturing Plants
        public List<ManufacturingPlant> GetPlants() => ReadData<ManufacturingPlant>(plantsFilePath);

        public void AddPlant(ManufacturingPlant plant)
        {
            var plants = ReadData<ManufacturingPlant>(plantsFilePath);
            plant.PlantID = plants.Count == 0 ? 1 : plants.Max(p => p.PlantID) + 1;
            plants.Add(plant);
            WriteData(plantsFilePath, plants);
        }

        // CRUD Operations for Drugs
        public List<Drug> GetDrugs() => ReadData<Drug>(drugsFilePath);

        public void AddDrug(Drug drug)
        {
            var drugs = ReadData<Drug>(drugsFilePath);
            drug.DrugID = drugs.Count == 0 ? 1 : drugs.Max(d => d.DrugID) + 1;
            drugs.Add(drug);
            WriteData(drugsFilePath, drugs);
        }

        public Drug GetDrugById(int id)
        {
            return ReadData<Drug>(drugsFilePath).FirstOrDefault(d => d.DrugID == id);
        }

        // Update Drug (Edit Drug)
        public void UpdateDrug(Drug updatedDrug)
        {
            var drugs = ReadData<Drug>(drugsFilePath);
            var existingDrug = drugs.FirstOrDefault(d => d.DrugID == updatedDrug.DrugID);

            if (existingDrug != null)
            {
                // Update the drug's properties
                existingDrug.Name = updatedDrug.Name;
                existingDrug.Type = updatedDrug.Type;
                existingDrug.Price = updatedDrug.Price;
                existingDrug.Stock = updatedDrug.Stock;

                WriteData(drugsFilePath, drugs);
            }
        }

        // Delete Drug
        public void DeleteDrug(int id)
        {
            var drugs = ReadData<Drug>(drugsFilePath);
            var drugToRemove = drugs.FirstOrDefault(d => d.DrugID == id);

            if (drugToRemove != null)
            {
                drugs.Remove(drugToRemove);
                WriteData(drugsFilePath, drugs);
            }
        }

        // CRUD Operations for Orders
        public List<Order> GetOrders() => ReadData<Order>(ordersFilePath);

        public void AddOrder(Order order)
        {
            var orders = ReadData<Order>(ordersFilePath);
            order.OrderID = orders.Count == 0 ? 1 : orders.Max(o => o.OrderID) + 1;
            orders.Add(order);
            WriteData(ordersFilePath, orders);
        }
    }
}
