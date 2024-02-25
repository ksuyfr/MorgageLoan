using System.ComponentModel.DataAnnotations;

namespace MorgageLoan.Dtos.Credit
{
    public class TestFirstFeeDto
    {
        //public int Id { get; set; } //ID кредита
        [Required]
        [Range(1, 100000000)]
        public decimal FullCoast { get; set; } = decimal.Zero; //полная стоимость цели кредита

        [Required]
        [Range(0, 100)]
        public double FirstPercent { get; set; } //процент первоначального взноса
        [Required]
        [Range(1, 100000000)]
        public decimal FirstFloor { get; set; } //сумма первоначального взноса в единице валюты

    }
}
