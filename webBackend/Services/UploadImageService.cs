using AutoMapper;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using webBackend.Models;
using webBackend.Models.Answer;
using webBackend.Models.Group;
using webBackend.Models.User;

namespace webBackend.Services
{
    public interface IUploadImageServiceService
    {

        Image Upload(Imagemodel imagemodel);
    }
    public class UploadImageService : IUploadImageServiceService
    {
        private readonly IMongoCollection<Image> _image;
        private GridFSBucket bucket;
        private readonly IMongoCollection<webBackend.Models.User.User> _users;
        private readonly IMongoDatabase database;
        private readonly IMapper _mapper;
        public UploadImageService(AppSettings settings, IMapper mapper)
        {
            var client = new MongoClient(settings.ConnectionString);
            database = client.GetDatabase(settings.DatabaseName);

            _image = database.GetCollection<Image>(settings.ImageCollectionName);
            _users = database.GetCollection<webBackend.Models.User.User>(settings.UsersCollectionName);
            _mapper = mapper;
        }
        public Image Upload(Imagemodel imagemodel)
        {
            Image image = new Image();
            if (imagemodel.Files != null)
            {
                var index = 1;
                foreach (var file in imagemodel.Files)
                {
                    string idImage = UploadedFile(index, file);
                    image.Pictures.Add(idImage);
                    index++;
                }
            }
            _image.InsertOne(image);
            return image;
        }
        public string UploadedFile(int index, IFormFile file)
        {
            var path = file.OpenReadStream();
            var id = bucket.UploadFromStream(index.ToString(), path);

            return id.ToString();
        }
    }
}