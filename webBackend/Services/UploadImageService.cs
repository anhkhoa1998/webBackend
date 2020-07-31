using Microsoft.AspNetCore.Http;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webBackend.Services
{
    public class UploadImageService
    {
        private GridFSBucket bucket;
        public Image image { get; set; }
        public UploadImageService(Imagemodel imagemodel)
        {
            if(imagemodel.Files!=null)
            {
                var index = 1;
                foreach (var file in imagemodel.Files)
                {
                    string idImage = UploadedFile(index, file);
                    image.Pictures.Add(idImage);
                    index++;
                }
            }
            var ThumbnailId = UploadedFile(imagemodel.ThumbnailImg.FileName, imagemodel.ThumbnailImg);
            image.ThumbnailId = ThumbnailId;
        }
        public  string UploadedFile(int index, IFormFile file)
        {
            var path = file.OpenReadStream();
            var id = bucket.UploadFromStream(index.ToString(), path);

            return id.ToString();
        }
        public string UploadedFile(string fileName, IFormFile file)
        {
            var path = file.OpenReadStream();
            var id = bucket.UploadFromStream(fileName, path);

            return id.ToString();
        }

    }
    public class Imagemodel
    {
        public IFormFile[] Files { get; set; }
        public IFormFile ThumbnailImg { get; set; }
    }
    public class Image
    {
        public List<string> Pictures { get; set; }
        public string ThumbnailId { get; set; }
    }
   
}
