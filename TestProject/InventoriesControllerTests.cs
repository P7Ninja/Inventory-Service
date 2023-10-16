using InventoryService.Controllers;
using InventoryService.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace TestProject;

public class InventoriesControllerTests
{
    private InventoriesController controller;
    private Inventory inv;

    [SetUp]
    public void Setup()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var optionsBuilder = new DbContextOptionsBuilder<InventoryServiceContext>().UseSqlite(connection);
        var context = new InventoryServiceContext(optionsBuilder.Options);
        context.Database.EnsureCreated();
        controller = new InventoriesController(context);
        inv = new Inventory(0, "Test inventory");
    }

    [Test]
    public async Task Post()
    {
        var response = await controller.PostInventory(inv);

        Assert.IsInstanceOf<CreatedAtActionResult>(response.Result);
    }

    [Test]
    public async Task GetUsersInventories()
    {
        await controller.PostInventory(new Inventory(0, "Test inventory1"));
        await controller.PostInventory(new Inventory(0, "Test inventory2"));

        var response = await controller.GetInventories(0);

        Assert.IsInstanceOf<OkObjectResult>(response.Result);

        var result = response.Result as OkObjectResult;

        Assert.That((result.Value as IEnumerable<Inventory>).Count() == 2);
    }

    [Test]
    public async Task GetInventory404()
    {
        var response = await controller.GetInventory(0);

        Assert.IsInstanceOf<NotFoundResult>(response.Result);
    }

    [Test]
    public async Task PostToInventory()
    {
        await controller.PostInventory(inv);

        var itemToAdd = new InventoryItem(0, 5, DateTime.Now.AddDays(2));
        var response = await controller.AddToInventory(inv.Id, itemToAdd);

        Assert.IsInstanceOf<NoContentResult>(response);
    }


    [Test]
    public async Task PutInventory()
    {
        var postResponse = await controller.PostInventory(inv);
        Assert.IsInstanceOf<CreatedAtActionResult>(postResponse.Result);

        inv.Name = "New name";
        inv.UserId++;
        var putResponse = await controller.PutInventory(inv.Id, inv);

        Assert.IsInstanceOf<NoContentResult>(putResponse);

        var getResponse = await controller.GetInventory(inv.Id);

        Assert.IsInstanceOf<OkObjectResult>(getResponse.Result);

        var result = getResponse.Result as OkObjectResult;

        Assert.That(result.Value as Inventory == inv);
    }

    [Test]
    public async Task Delete()
    {
        await controller.PostInventory(inv);
        var deleteResponse = await controller.DeleteInventory(inv.Id);
        var getResponse = await controller.GetInventory(inv.Id);

        Assert.IsInstanceOf<NoContentResult>(deleteResponse);
        Assert.IsInstanceOf<NotFoundResult>(getResponse.Result);

    }
}
