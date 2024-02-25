using MorgageLoan.Dtos.Credit;
using MorgageLoan.Models;

namespace MorgageLoan.Mappers
{
    public static class CreditMappers
    {
        public static CreditDto ToCreditDto(this Credit creditModel)
        {
            return new CreditDto
            {
                Id = creditModel.Id,
                CreditName = creditModel.CreditName,
                FullCoast = creditModel.FullCoast,
                InterestRate = creditModel.InterestRate,
                FirstFloor = creditModel.FirstFloor,
                MonthlyPayment = creditModel.MonthlyPayment,
                CreditTerm = creditModel.CreditTerm,
            };
        }

        public static Credit ToCreditFromCreateDto(this CreateCreditRequestDto creditDto)
        {
            return new Credit
            {
                CreditName = creditDto.CreditName,
                FullCoast = creditDto.FullCoast,
                InterestRate = creditDto.InterestRate,
                FirstPercent = creditDto.FirstPercent,
                FirstFloor = creditDto.FirstFloor,
                MonthlyPayment = creditDto.MonthlyPayment,
                CreditTerm = creditDto.CreditTerm,
                CreateOn = creditDto.CreateOn,
            };
        }
    }
}
