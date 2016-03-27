using System;

namespace Chatterbox.Web.ViewModels
{
    public class GarmentViewModel
    {
        public int GarmentId { get; set; }

        public Guid GarmentImageId { get; set; }

        public string Notes { get; set; }

        public string ContentType { get; set; }

        public string FileName { get; set; }

        public long FileSize { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
