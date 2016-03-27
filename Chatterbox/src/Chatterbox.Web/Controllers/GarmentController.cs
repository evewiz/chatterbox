using Chatterbox.Model.Models;
using Chatterbox.Model.Repositories;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Net.Http.Headers;
using System.Collections.Generic;
using System.IO;

namespace Chatterbox.Web.Controllers
{
    public class GarmentController : Controller
    {
        private readonly IGarmentRepository _garmentRepository;

        public GarmentController(IGarmentRepository garmentRepository)
        {
            _garmentRepository = garmentRepository;
        }

        public IActionResult Upload(ICollection<IFormFile> files)
        {
            foreach (var file in files)
            {
                using (var binaryReader = new BinaryReader(file.OpenReadStream()))
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                    var garmentImage = new GarmentImage
                    {
                        Garment = new Garment(),
                        ConentType = file.ContentType,
                        FileName = fileName,
                        FileSize = file.Length,
                        Image = binaryReader.ReadBytes((int)file.Length),
                    };

                    _garmentRepository.Create(garmentImage);
                }
            }
            return View();
        }

        public FileContentResult Download(int id)
        {
            var garmentImage = _garmentRepository.GetGarmentImage(id);
            return File(garmentImage.Image, garmentImage.ConentType, garmentImage.FileName);
        }
    }
}
