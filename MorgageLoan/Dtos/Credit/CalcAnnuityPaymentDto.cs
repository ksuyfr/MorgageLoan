using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MorgageLoan.Dtos.Credit
{
    public class CalcAnnuityPaymentDto
    {
        [Required]
        [Range(0, 100)]
        [DisplayName ("Процентная ставка")]
        public decimal InterestRate { get; set; } = 0.01M; //процентная ставка

        [Required]
        [Range(1, 50)]
        [Display(Name = "Срок кредита в годах ")]
        public int MorgageTerm { get; set; } = 1;//срок кредита в годах 

        [Required]
        [Range(1, 100000000)]
        [Display(Name = "Полная стоимость цели кредита")]
        public decimal FullCoast { get; set; } = decimal.Zero; //полная стоимость цели кредита

        [Required]
        [Range(1, 100000000)]
        [Display(Name = "Первоначальный взнос в валюте")]
        public decimal FirstFloor { get; set; }
    }
}
