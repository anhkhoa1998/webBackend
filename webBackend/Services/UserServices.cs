﻿using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using webBackend.Models;
using webBackend.Models.Authen;
using webBackend.Models.User;
using webBackend.Ultils;

namespace webBackend.Services
{
    public interface IUserServices
    {
        Task<Users> GetById(string id);
        Task<Users> Create(UserModel userModel);
        Users Authenticate(AuthenModel authenModel);
        Task<UserUpdateModel> Update(string id, UserUpdateModel p);
        Task<Users> Delete(string id);
        Task<List<string>> GetListClass(string id);
        Task<UserInformation> GetUserInformation(string userId);

    }
    public class UserServices : IUserServices
    {
        private readonly IMongoCollection<Users> _users;
        private readonly IMongoDatabase database;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UserServices(AppSettings settings, IMapper mapper)
        {
            var client = new MongoClient(settings.ConnectionString);
            database = client.GetDatabase(settings.DatabaseName);

            _appSettings = settings;
            _users = database.GetCollection<Users>(settings.UsersCollectionName);
            _mapper = mapper;
        }

        public async Task<Users> Create(UserModel userModel)
        {
            var user = _mapper.Map<Users>(userModel);
            await _users.InsertOneAsync(user);
            return user;
        }

        public Users Authenticate(AuthenModel authenModel)
        {
            var user = _users.Find(b => b.Username == authenModel.Username && b.Password == authenModel.Password).FirstOrDefault();
            if (user == null) return null;
            // authentication successful so generate jwt token
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Type.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user;
        }
        public Task<Users> GetById(string id)
        {
            return _users.Find(p => p.Id == id).FirstOrDefaultAsync();
        }
        public async Task<UserUpdateModel> Update(string id, UserUpdateModel p)
        {
            var user = await GetById(id);
            _mapper.Map(p, user);
            await _users.ReplaceOneAsync(p => p.Id == id, user);
            return p;
        }
        public async Task<Users> Delete(string id)
        {
            var user = await GetById(id);
            await _users.DeleteOneAsync(p => p.Id == id);
            return user;
        }
        public async Task<List<string>> GetListClass(string id)
        {
            var user = await _users.Find(u => u.Id == id).FirstOrDefaultAsync();
            return user.ListClass;
        }

        public async Task<UserInformation> GetUserInformation(string userId)
        {
            var user = await _users.Find(u => u.Id == userId).FirstOrDefaultAsync();
            var result = new UserInformation { FirstName = user.FirstName, LastName = user.LastName, ListClass = user.ListClass };


            return result;
        }
    }
}
