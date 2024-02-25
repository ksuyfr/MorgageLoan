using MorgageLoan.Dtos.Credit;
using MorgageLoan.Models;

namespace MorgageLoan.Interfaces
{
    public interface ICreditRepository
    {
        Task<List<Credit>> GetAllAsync();
        Task<Credit?> GetByIdAsync(int id);
        Task<Credit> CreateAsync(Credit creditModel);
        Task<Credit?> UpdateAsync(int id, UpdateCreditRequestDto creditDto);
        Task<Credit?> DeleteAsync(int id); 
    }
}
