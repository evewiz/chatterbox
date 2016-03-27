using Chatterbox.Model.Repositories;
using Chatterbox.Web.ViewModels;
using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Chatterbox.Web.Controllers
{
    public class WardrobeController : Controller
    {
        private readonly IGarmentRepository _garmentRepository;

        public WardrobeController(IGarmentRepository garmentRepository)
        {
            _garmentRepository = garmentRepository;
        }

        public IActionResult Garments()
        {
            List<GarmentViewModel> garments = new List<GarmentViewModel>();

            foreach(var dbGarment in _garmentRepository.GetGarments())
            {
                var dbGarmentImage = dbGarment.GarmentImages.Single(img => img.IsActive);

                garments.Add(new GarmentViewModel
                {
                   GarmentId = dbGarment.Id,
                   GarmentImageId = dbGarmentImage.Id,
                   Notes = dbGarmentImage.Notes,
                   ContentType = dbGarmentImage.ConentType,
                   FileName = dbGarmentImage.FileName,
                   FileSize = dbGarmentImage.FileSize,
                   CreationDate = dbGarmentImage.CreationDate,
                });
            }
            return View(garments);
        }
    }
}
