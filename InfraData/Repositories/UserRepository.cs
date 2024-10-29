using Domain.Entities;
using Domain.Repositories;
using InfraData.Context;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace InfraData.Repositories
{
    public class UserRepository : IUserRepository
    {

        public readonly ContextDb _db;

        public UserRepository(ContextDb db)
        {
            _db = db;
        }

        public async Task<ICollection<User>> FindAll() => await _db.Users.Include(x => x.Character).ThenInclude(x=> x.Equipment).ToListAsync();

        public async Task<User> FindById(int id) => await _db.Users.Include(x=> x.Character).ThenInclude(x => x.Equipment).SingleOrDefaultAsync(u => u.Id == id);

        public async Task<User> FindByDocument(string document) => await _db.Users.Include(x => x.Character).ThenInclude(x => x.Equipment).SingleOrDefaultAsync(u => u.Document == document);
        public async Task<User> FindByEmail(string email) => await _db.Users.Include(x => x.Character).ThenInclude(x => x.Equipment).SingleOrDefaultAsync(u => u.Email == email);
        public async Task<User> FindByIdForUpdate(int id) => await _db.Users.Include(x => x.Character).ThenInclude(x => x.Equipment).SingleOrDefaultAsync(u => u.Id == id);
        public async Task<User> Update(User body)
        {

            _db.Users.Update(body);
            await _db.SaveChangesAsync();
            return body;

        }

        public async Task<User> UpdatePassword(User body)
        {
            body.Password = ComputeHash(body.Password);
            _db.Users.Update(body);
            await _db.SaveChangesAsync();
            return body;

        }

        public async Task<User> Create(User body)
        {
            body.Password = ComputeHash(body.Password);
            _db.Users.Add(body);
            await _db.SaveChangesAsync();
            return body;
        }
        public async Task<bool> Delete(User body)
        {
            _db.Users.Remove(body);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<User> Login(string email, string password)
        {
            string hashedPassword = ComputeHash(password);

            return await _db.Users.Include(x => x.Character).ThenInclude(x => x.Equipment).FirstOrDefaultAsync(x => x.Email == email && x.Password == hashedPassword);
        }


        public async Task<bool> ValidationFields(string email, string document)
        {
            var result = await _db.Users.ToListAsync();
            var existingUser = result.Any(u => u.Email == email || u.Document == document);
            return existingUser;

        }




        private string ComputeHash(string input)
        {
            using (var algorithm = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);

                byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }


     /*   public async Task<User> FindByCode(string code)
        {
            var user = await _db.Users
                                     .Include(u => u.VerificationCode)
                                     .FirstOrDefaultAsync(u => u.VerificationCode != null && u.VerificationCode.Code == code);

            return user;

        }*/
    }
}
