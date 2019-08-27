namespace BudgetCalculator
{
    public class Budget
    {
        public string YearMonth { get; set; }

        public int Amount { get; set; }

        public int DailyAmount(int days)
        {
            return Amount / days;
        }
    }
}