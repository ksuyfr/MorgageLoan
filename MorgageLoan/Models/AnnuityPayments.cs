using DecimalMath;
using System.ComponentModel.DataAnnotations;

namespace MorgageLoan.Models
{
    public class AnnuityPayments
    {
        AnnuityPayments(decimal interestRateAnnum, int morgageTerm, decimal sumMorgage)
        {
            InterestRateAnnum = interestRateAnnum;
            MorgageTermMonths = morgageTerm*12;
            SumMorgage = sumMorgage;
            MonthlyRate = InterestRateAnnum / 12 / 100;
            //ЕЖЕМЕСЯЧНАЯ_СТАВКА = ПРОЦЕНТНАЯ_СТАВКА_ГОДОВЫХ / 12 / 100

            CommonRate = DecimalEx.Pow((Decimal.Add(MonthlyRate,1)), MorgageTermMonths);
            //ОБЩАЯ_СТАВКА = (1 + ЕЖЕМЕСЯЧНАЯ_СТАВКА) ^ СРОК_ИПОТЕКИ_МЕСЯЦЕВ

            MonthlyPayment = Decimal.Divide(
                                 Decimal.Multiply(Decimal.Multiply(SumMorgage, MonthlyRate), CommonRate),
                                 Decimal.Subtract(CommonRate, 1));
            //ЕЖЕМЕСЯЧНЫЙ_ПЛАТЕЖ = СУММА_КРЕДИТА * ЕЖЕМЕСЯЧНАЯ_СТАВКА * ОБЩАЯ_СТАВКА / (ОБЩАЯ_СТАВКА - 1)

            OverPayment = Decimal.Subtract(Decimal.Multiply(MonthlyPayment, MorgageTermMonths), SumMorgage);
            //ПЕРЕПЛАТА = ЕЖЕМЕСЯЧНЫЙ_ПЛАТЕЖ * СРОК_ИПОТЕКИ_МЕСЯЦЕВ - СУММА_КРЕДИТА

            //ОСТАТОК_ДОЛГА
            balanceOwed = new decimal[MorgageTermMonths];
            //ПРОЦЕНТНАЯ_ЧАСТЬ = ОСТАТОК_ДОЛГА* ЕЖЕМЕСЯЧНАЯ_СТАВКА
            perCentPath = new decimal[MorgageTermMonths];
            //ОСНОВНАЯ_ЧАСТЬ = ЕЖЕМЕСЯЧНЫЙ_ПЛАТЕЖ - ПРОЦЕНТНАЯ_ЧАСТЬ
            basicPath = new decimal[MorgageTermMonths];

            balanceOwed[0] = sumMorgage;

            for (int i = 0; i < MorgageTermMonths; i++)
            {
                perCentPath[i] = Decimal.Multiply(balanceOwed[i], MonthlyRate);
                basicPath[i] = MonthlyPayment - perCentPath[i];
                if (i + 1 < MorgageTermMonths)
                    balanceOwed[i + 1] = balanceOwed[i] - basicPath[i];
            }

        }
        public required decimal InterestRateAnnum { get; init; } //ПРОЦЕНТНАЯ_СТАВКА_ГОДОВЫХ
        public required int MorgageTermMonths { get; init; } //СРОК_ИПОТЕКИ_МЕСЯЦЕВ
        public required decimal SumMorgage { get; init; }
        public decimal MonthlyRate { get; init; } // ЕЖЕМЕСЯЧНАЯ_СТАВКА
        public decimal CommonRate { get; init; } //ОБЩАЯ_СТАВКА
        public decimal MonthlyPayment { get; init; } //ЕЖЕМЕСЯЧНЫЙ_ПЛАТЕЖ
        public decimal OverPayment { get; init; } //ПЕРЕПЛАТА 

        decimal[] balanceOwed; //ОСТАТОК_ДОЛГА
        decimal[] perCentPath; //ПРОЦЕНТНАЯ_ЧАСТЬ
        decimal[] basicPath; //ОСНОВНАЯ_ЧАСТЬ
        

    }
}
