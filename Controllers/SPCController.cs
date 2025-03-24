using Microsoft.AspNetCore.Mvc;
using SPC_API.Models;
using SPC_API.Services;

namespace SPC_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SPCController : ControllerBase
    {
        private readonly JsonFileService _jsonFileService;

        // Constructor with Dependency Injection
        public SPCController(JsonFileService jsonFileService)
        {
            _jsonFileService = jsonFileService;
        }

        // Supplier CRUD APIs
        [HttpGet("suppliers")]
        public ActionResult<List<Supplier>> GetSuppliers() => Ok(_jsonFileService.GetSuppliers());

        [HttpPost("suppliers")]
        public ActionResult AddSupplier([FromBody] Supplier supplier)
        {
            _jsonFileService.AddSupplier(supplier);
            return Ok();
        }

        // Manufacturing Plant CRUD APIs
        [HttpGet("plants")]
        public ActionResult<List<ManufacturingPlant>> GetPlants() => Ok(_jsonFileService.GetPlants());

        [HttpPost("plants")]
        public ActionResult AddPlant([FromBody] ManufacturingPlant plant)
        {
            _jsonFileService.AddPlant(plant);
            return Ok();
        }

        // Drug CRUD APIs
        [HttpGet("drugs")]
        public ActionResult<List<Drug>> GetDrugs() => Ok(_jsonFileService.GetDrugs());

        [HttpPost("drugs")]
        public ActionResult AddDrug([FromBody] Drug drug)
        {
            _jsonFileService.AddDrug(drug);
            return Ok();
        }

        // Edit Drug API (PUT)
        [HttpPut("drugs/{id}")]
        public ActionResult EditDrug(int id, [FromBody] Drug updatedDrug)
        {
            var drug = _jsonFileService.GetDrugById(id);
            if (drug == null)
            {
                return NotFound($"Drug with ID {id} not found.");
            }

            // Update drug properties
            drug.Name = updatedDrug.Name;
            drug.Type = updatedDrug.Type;
            drug.Price = updatedDrug.Price;
            drug.Stock = updatedDrug.Stock;

            _jsonFileService.UpdateDrug(drug);
            return Ok();
        }

        // Delete Drug API (DELETE)
        [HttpDelete("drugs/{id}")]
        public ActionResult DeleteDrug(int id)
        {
            var drug = _jsonFileService.GetDrugById(id);
            if (drug == null)
            {
                return NotFound($"Drug with ID {id} not found.");
            }

            _jsonFileService.DeleteDrug(id);
            return Ok();
        }

        // Order CRUD APIs
        [HttpGet("orders")]
        public ActionResult<List<Order>> GetOrders() => Ok(_jsonFileService.GetOrders());

        [HttpPost("orders")]
        public ActionResult AddOrder([FromBody] Order order)
        {
            _jsonFileService.AddOrder(order);
            return Ok();
        }
    }
}
