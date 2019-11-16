using BusTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics;

namespace BusTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMongoCollection<Location> collection;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            var client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase db = client.GetDatabase("Location");
            _ = collection = db.GetCollection<Location>("Location");
        }
     
        public IActionResult Index()
        {
            return View();
        }




        public IActionResult Alerts()
        {
            return View();
        }

        public IActionResult Locations()
        {
            var model = collection.Find
        (FilterDefinition<Location>.Empty).ToList();

            return View(model);
        }

        public IActionResult Insert()
        {
            return View();
        }

        public IActionResult Index2(string id, Location l)
        {
            var model = collection.Find
        (FilterDefinition<Location>.Empty).ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult Insert(Location location)
        {
            collection.InsertOne(location);
            ViewBag.Message = "Location added successfully!";
            return View();
        }




        public IActionResult Update(string id)
        {
            ObjectId oId = new ObjectId(id);
            Location Lc = collection.Find(location => location.Id
            == oId).FirstOrDefault();

            return View(Lc);
        }

        [HttpPost]
        public IActionResult Update(string id, Location l)
        {
            l.Id = new ObjectId(id);
            var filter = Builders<Location>.
        Filter.Eq("Id", l.Id);
            var updateDef = Builders<Location>.Update.Set("OriginAddress", l.OriginAddress);
            updateDef = updateDef.Set("DestinationAddress", l.DestinationAddress);
            var result = collection.UpdateOne(filter, updateDef);
            if (result.IsAcknowledged)
            {
                ViewBag.Message = "Location updated successfully!";
            }
            else
            {
                ViewBag.Message = "Error while updating Location!";
            }
            return View(l);
        }

        public IActionResult ConfirmDelete(string id)
        {
            ObjectId oId = new ObjectId(id);
            Location Lc = collection.Find(location =>
        location.Id == oId).FirstOrDefault();
            return View(Lc);
        }


        [HttpPost]
        public IActionResult Delete(string id)
        {
            ObjectId oId = new ObjectId(id);
            DeleteResult result = collection.DeleteOne
        (l => l.Id == oId);

            if (result.IsAcknowledged)
            {
                TempData["Message"] = "Location deleted successfully!";
            }
            else
            {
                TempData["Message"] = "Error while deleting Location!";
            }
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
