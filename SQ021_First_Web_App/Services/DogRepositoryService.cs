using Microsoft.EntityFrameworkCore;
using SQ021_First_Web_App.Data;
using SQ021_First_Web_App.Models.Entity;
using SQ021_First_Web_App.Services.Interfaces;

namespace SQ021_First_Web_App.Services
{
    public class DogRepositoryService : IDogRepositoryService
    {
        private readonly MyDbContext _context;
        public DogRepositoryService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddDogAsync(Dog entity)
        {
            await _context.AddAsync(entity);
            var result = await _context.SaveChangesAsync();
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> DeleteDogAsync(Dog entity)
        {
            _context.Remove(entity);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
                return true;
            else
                return false;
        }

        public async Task<Dog?> GetAsync(string id)
        {
            return await _context.Dogs.FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<Dog> GetAsync()
        {
            return _context.Dogs;
        }

        public async Task<bool> UpdateDogAsync(Dog entity)
        {
            _context.Update(entity);
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }
    }
}
