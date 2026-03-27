using Microsoft.AspNetCore.Mvc;
using MedicineApp.Models;
using System.Text.Json;

[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly string salesPath = "sales.json";
    private readonly string medPath = "data.json";

    [HttpPost]
    public IActionResult Sell([FromBody] Sale sale)
    {
        var sales = System.IO.File.Exists(salesPath)
            ? JsonSerializer.Deserialize<List<Sale>>(System.IO.File.ReadAllText(salesPath))
            : new List<Sale>();

        var meds = JsonSerializer.Deserialize<List<Medicine>>(
            System.IO.File.ReadAllText(medPath));

        var med = meds.FirstOrDefault(m => m.FullName == sale.MedicineName);

        if (med == null) return NotFound("Medicine not found");

        if (med.Quantity < sale.QuantitySold)
            return BadRequest("Not enough stock");

        med.Quantity -= sale.QuantitySold;

        sales.Add(sale);

        System.IO.File.WriteAllText(salesPath, JsonSerializer.Serialize(sales));
        System.IO.File.WriteAllText(medPath, JsonSerializer.Serialize(meds));

        return Ok();
    }
}