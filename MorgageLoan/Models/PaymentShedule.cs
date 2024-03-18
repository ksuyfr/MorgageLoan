namespace MorgageLoan.Models
{
    public class PaymentShedule
    {
        public decimal BalanceOwed {  get; set; } //ОСТАТОК_ДОЛГА
        public decimal PerCentPath { get; set; } //ПРОЦЕНТНАЯ_ЧАСТЬ
        public decimal BasicPath { get; set; }//ОСНОВНАЯ_ЧАСТЬ
    }
}
