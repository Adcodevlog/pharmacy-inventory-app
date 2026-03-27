using Microsoft.AspNetCore.Mvc;
using MedicineApp.Models;
using System.Text.Json;

[ApiController]
[Route("api/[controller]")]
public class MedicineController : ControllerBase
{
    private readonly string path = "data.json";

    private List<Medicine> Read()
    {
        if (!System.IO.File.Exists(path))
            return new List<Medicine>();

        var json = System.IO.File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<Medicine>>(json);
    }

    private void Write(List<Medicine> data)
    {
        System.IO.File.WriteAllText(path,
            JsonSerializer.Serialize(data));
    }

    [HttpGet]
    public IActionResult Get() => Ok(Read());

    [HttpPost]
    public IActionResult Post([FromBody] Medicine med)
    {
        var data = Read();
        data.Add(med);
        Write(data);
        return Ok();
    }

    [HttpPut("{index}")]
    public IActionResult Put(int index, [FromBody] Medicine med)
    {
        var data = Read();
        if (index < 0 || index >= data.Count) return NotFound();

        data[index] = med;
        Write(data);
        return Ok();
    }

    [HttpDelete("{index}")]
    public IActionResult Delete(int index)
    {
        var data = Read();
        if (index < 0 || index >= data.Count) return NotFound();

        data.RemoveAt(index);
        Write(data);
        return Ok();
    }
}