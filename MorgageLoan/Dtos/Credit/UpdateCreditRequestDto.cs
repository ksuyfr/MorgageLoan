using System.ComponentModel.DataAnnotations;

namespace MorgageLoan.Dtos.Credit
{
    public class UpdateCreditRequestDto
    {
        [MinLength(5, ErrorMessage = "Наименование кредита должно быть не менее 5 символов")]
        [MaxLength(280, ErrorMessage = "Ну-ну, крокодильи слёзы тоже лить не надо. Это делу не поможет. Сокращай до 280 символов.")]
        public string? CreditName { get; set; } = string.Empty; //Название кредита. Можно записать цель
        [Required]
        [Range(1, 100000000)]
        public decimal FullCoast { get; set; } = decimal.Zero; //полная стоимость цели кредита
        [Required]
        [Range(0, 100)]
        public double InterestRate { get; set; } = 0.01; //процентная ставка
        [Required]
        [Range(0, 100)]
        public double? FirstPercent { get; set; } //процент первоначального взноса
        [Required]
        [Range(1, 100000000)]
        public decimal FirstFloor { get; set; } //сумма первоначального взноса в единице валюты
        public decimal? MonthlyPayment { get; set; }  //ежемесячный платеж
        [Required]
        [Range(1, 50)]
        public int MorgageTerm { get; set; } = 1;//срок кредита в годах
        public DateTime? CreateOn { get; set; } = DateTime.Now;//дата создания кредита. Потом будет разбавляться другими временами

    }
}
