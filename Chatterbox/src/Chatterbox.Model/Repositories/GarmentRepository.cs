using System.Linq;
using System.Collections.Generic;
using Chatterbox.Model.Models;
using System;

namespace Chatterbox.Model.Repositories
{
    public class GarmentRepository : IGarmentRepository
    {
        private readonly string _connectionString;

        public GarmentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public Garment GetGarment(int garmentId)
        {
            return GetGarments().Single(garment => garment.Id == garmentId);
        }

        public GarmentImage GetGarmentImage(int garmentId)
        {
            return GetGarment(garmentId).GarmentImages.Single(img => img.IsActive);
        }

        public List<Garment> GetGarments()
        {
            using (var db = new ChatterboxEntities(_connectionString))
            {
                return db.Garments.Include(nameof(Garment.GarmentImages)).AsNoTracking().ToList();
            }
        }

        public void Create(GarmentImage garmentImage)
        {
            using (var db = new ChatterboxEntities(_connectionString))
            {
                db.GarmentImages.Add(garmentImage);
                db.SaveChanges();
            }
        }
    }
}
