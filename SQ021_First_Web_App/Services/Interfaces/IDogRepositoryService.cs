using SQ021_First_Web_App.Models.Entity;

namespace SQ021_First_Web_App.Services.Interfaces
{
    public interface IDogRepositoryService
    {
        Task<bool> AddDogAsync(Dog entity);
        Task<bool> DeleteDogAsync(Dog entity);
        Task<bool> UpdateDogAsync(Dog entity);
        Task<Dog> GetAsync(string id);
        IQueryable<Dog> GetAsync();
    }
}
