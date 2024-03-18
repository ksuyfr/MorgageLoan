using System.ComponentModel.DataAnnotations.Schema;

namespace MorgageLoan.Models
{
    public class Credit //можно создать абстрактный класс кредита, с процентом, суммой и тд. но тогда придется отделить общую сумму покупки от суммы кредита, и пересчитывать внутри класса. сложно. оставлю как идею для улучшения
    {
        public int Id { get; set; } //ID кредита
        public string? CreditName { get; set; } = string.Empty; //Название кредита. Можно записать цель
        [Column(TypeName ="decimal(18, 2)")]
        public decimal FullCoast { get; set; } = decimal.Zero; //полная стоимость цели кредита
        public decimal InterestRate { get; set; } = 0.01M; //процентная ставка
        public decimal? FirstPercent { get; set; } //процент первоначального взноса
        [Column(TypeName = "decimal(18, 2)")]
        public decimal FirstFloor {  get; set; } //сумма первоначального взноса в единице валюты
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? MonthlyPayment { get; set; }  //ежемесячный платеж
        public int MorgageTerm { get; set; } = 1;//срок кредита в годах
        public DateTime? CreateOn { get; set; }= DateTime.Now;//дата создания кредита. Потом будет разбавляться другими временами

    }
}
