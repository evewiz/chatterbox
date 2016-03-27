using Chatterbox.Model.Models;
using System.Collections.Generic;

namespace Chatterbox.Model.Repositories
{
    public interface IGarmentRepository
    {
        Garment GetGarment(int garmentId);

        GarmentImage GetGarmentImage(int garmentId);

        List<Garment> GetGarments();

        void Create(GarmentImage garmentImage);
    }
}
