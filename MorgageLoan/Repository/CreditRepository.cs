using Microsoft.EntityFrameworkCore;
using MorgageLoan.Data;
using MorgageLoan.Dtos.Credit;
using MorgageLoan.Interfaces;
using MorgageLoan.Models;

namespace MorgageLoan.Repository
{
    public class CreditRepository : ICreditRepository
    {
        private readonly ApplicationDBContext _context;
        public CreditRepository(ApplicationDBContext context) 
        {
            _context = context;
        }

        public async Task<Credit> CreateAsync(Credit creditModel)
        {
            await _context.Credit.AddAsync(creditModel);
            await _context.SaveChangesAsync();
            return creditModel;
        }

        public async Task<Credit?> DeleteAsync(int id)
        {
            var creditModel = await _context.Credit.FirstOrDefaultAsync(x => x.Id == id);
            if (creditModel == null)
            {
                return null;
            }
            _context.Credit.Remove(creditModel);
            await _context.SaveChangesAsync();
            return creditModel;
        }

        public async Task<List<Credit>> GetAllAsync()
        {
            return await _context.Credit.ToListAsync();
        }

        public async Task<Credit?> GetByIdAsync(int id)
        {
            return await _context.Credit.FindAsync(id);
        }

        public async Task<Credit?> UpdateAsync(int id, UpdateCreditRequestDto creditDto)
        {
            var existingCredit = await _context.Credit.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCredit == null)
            {
                return null;
            }

            existingCredit.CreditName = creditDto.CreditName;
            existingCredit.FullCoast = creditDto.FullCoast;
            existingCredit.InterestRate = creditDto.InterestRate;
            existingCredit.FirstPercent = creditDto.FirstPercent;
            existingCredit.FirstFloor = creditDto.FirstFloor;
            existingCredit.MonthlyPayment = creditDto.MonthlyPayment;
            existingCredit.CreditTerm = creditDto.CreditTerm;
            existingCredit.CreateOn = creditDto.CreateOn;

            await _context.SaveChangesAsync();

            return existingCredit;
        }
    }
}
