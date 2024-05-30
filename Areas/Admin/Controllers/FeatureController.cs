using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PustokCrBusiness.Services.Abstracts;
using PustokCrCore.Models;

namespace PustokCr.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class FeatureController : Controller
    {
        private readonly IFeatureService _featureService;

        public FeatureController(IFeatureService featureService)
        {
            _featureService = featureService;
        }

        public IActionResult Index()
        {
            var features = _featureService.GetAllFeatures();
            return View(features);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Feature feature)
        {
            if (!ModelState.IsValid)
                return View();


            _featureService.AddFeatureAsync(feature);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var existFeature = _featureService.GetFeature(x => x.Id == id);

            if (existFeature == null)
            {
                return NotFound();
            }
            return View(existFeature);
        }

        [HttpPost]
        public IActionResult DeletePost(int id)
        {
            _featureService.DeleteFeature(id);
            return RedirectToAction("Index");
        }


        public IActionResult Update(int id)
        {
            var existFeature = _featureService.GetFeature(x => x.Id == id);
            if (existFeature == null) throw new NullReferenceException("bele bir sey yoxdur!");

            return View(existFeature);
        }

        [HttpPost]
        public IActionResult Update(Feature newFeature)
        {
            _featureService.UpdateFeature(newFeature.Id, newFeature);
            return RedirectToAction("Index");
        }
    }
}
