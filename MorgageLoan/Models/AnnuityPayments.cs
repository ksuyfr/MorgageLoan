#undef IEnumerable_STATE //в зависимости от желаемого возврата

using DecimalMath;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace MorgageLoan.Models
{
    public class AnnuityPayments
#if (IEnumerable_STATE)
        : IEnumerable 
#endif
    {
        [SetsRequiredMembers]
        public AnnuityPayments(decimal interestRateAnnum, int morgageTerm, decimal sumMorgage)
        {
            InterestRateAnnum = interestRateAnnum;
            MorgageTermMonths = morgageTerm * 12;
            SumMorgage = sumMorgage;
            MonthlyRate = InterestRateAnnum / 12 / 100;
            //ЕЖЕМЕСЯЧНАЯ_СТАВКА = ПРОЦЕНТНАЯ_СТАВКА_ГОДОВЫХ / 12 / 100

            CommonRate = DecimalEx.Pow((Decimal.Add(MonthlyRate, 1)), MorgageTermMonths);
            //ОБЩАЯ_СТАВКА = (1 + ЕЖЕМЕСЯЧНАЯ_СТАВКА) ^ СРОК_ИПОТЕКИ_МЕСЯЦЕВ

            MonthlyPayment = Decimal.Divide(
                                 Decimal.Multiply(Decimal.Multiply(SumMorgage, MonthlyRate), CommonRate),
                                 Decimal.Subtract(CommonRate, 1));
            //ЕЖЕМЕСЯЧНЫЙ_ПЛАТЕЖ = СУММА_КРЕДИТА * ЕЖЕМЕСЯЧНАЯ_СТАВКА * ОБЩАЯ_СТАВКА / (ОБЩАЯ_СТАВКА - 1)

            OverPayment = Decimal.Subtract(Decimal.Multiply(MonthlyPayment, MorgageTermMonths), SumMorgage);
            //ПЕРЕПЛАТА = ЕЖЕМЕСЯЧНЫЙ_ПЛАТЕЖ * СРОК_ИПОТЕКИ_МЕСЯЦЕВ - СУММА_КРЕДИТА

            paymentShedules = new();
        }
        public required decimal InterestRateAnnum { get; init; } //ПРОЦЕНТНАЯ_СТАВКА_ГОДОВЫХ
        public required int MorgageTermMonths { get; init; } //СРОК_ИПОТЕКИ_МЕСЯЦЕВ
        public required decimal SumMorgage { get; init; }
        public decimal MonthlyRate { get; init; } // ЕЖЕМЕСЯЧНАЯ_СТАВКА
        public decimal CommonRate { get; init; } //ОБЩАЯ_СТАВКА
        public decimal MonthlyPayment { get; init; } //ЕЖЕМЕСЯЧНЫЙ_ПЛАТЕЖ
        public decimal OverPayment { get; init; } //ПЕРЕПЛАТА 

        public List<PaymentShedule>? paymentShedules { get; set; }

        public void PaymentShedulesGenerate()
        {
            //ОСТАТОК_ДОЛГА
            //ПРОЦЕНТНАЯ_ЧАСТЬ = ОСТАТОК_ДОЛГА* ЕЖЕМЕСЯЧНАЯ_СТАВКА
            //ОСНОВНАЯ_ЧАСТЬ = ЕЖЕМЕСЯЧНЫЙ_ПЛАТЕЖ - ПРОЦЕНТНАЯ_ЧАСТЬ
            paymentShedules.Add(new PaymentShedule());
            paymentShedules[0].BalanceOwed = SumMorgage;

            for (int i = 0; i < MorgageTermMonths; i++)
            {
                paymentShedules[i].PerCentPath = Decimal.Multiply(paymentShedules[i].BalanceOwed, MonthlyRate);
                if (i + 1 == MorgageTermMonths)
                {
                    paymentShedules[i].BasicPath = Decimal.Subtract(paymentShedules[i].BalanceOwed, paymentShedules[i].PerCentPath);
                }
                else
                {
                    paymentShedules[i].BasicPath = Decimal.Subtract(MonthlyPayment,paymentShedules[i].PerCentPath);
                    paymentShedules.Add(new PaymentShedule());
                    paymentShedules[i + 1].BalanceOwed = Decimal.Subtract(paymentShedules[i].BalanceOwed, paymentShedules[i].BasicPath);
                }
                
            }
        }
#if (IEnumerable_STATE)
        public IEnumerator GetEnumerator()=> paymentShedules.GetEnumerator();
#else
        public IEnumerable<PaymentShedule> GetPaymentSchedule()
        {
            for (int i = 0; i < paymentShedules?.Count(); i++)
            {
                yield return paymentShedules[i];
            }
        }
#endif

    }
}
