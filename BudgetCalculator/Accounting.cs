using System;
using System.Collections.Generic;
using System.Linq;

namespace BudgetCalculator
{
    internal class Period
    {
        public Period(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        public bool SameYearMonth()
        {
            return Start.YearMonth() == End.YearMonth();
        }

        public int GetDays()
        {
            return ((End - Start).Days + 1);
        }
    }

    internal class Accounting
    {
        private readonly IRepository<Budget> _repository;

        public Accounting(IRepository<Budget> repository)
        {
            _repository = repository;
        }

        public decimal TotalAmount(Period period)
        {
            var budgets = _repository.GetAll();

            return period.SameYearMonth()
                ? GetOneMonthBudget(period, budgets)
                : GetAllMonthBudget(period, budgets);
        }

        private int GetAllMonthBudget(Period period, List<Budget> budgets)
        {
            int allTotalAmount = 0;
            for (var currentTime = period.Start;
                IsEndYearMonth(period, currentTime);
                currentTime = currentTime.AddMonths(1))
            {
                DateTime startDateTime;
                DateTime endDateTime;
                if (currentTime.YearMonth() == period.Start.YearMonth())
                {
                    startDateTime = period.Start;
                    endDateTime = currentTime.LastDateTime();
                }
                else if (currentTime.YearMonth() == period.End.YearMonth())
                {
                    startDateTime = currentTime.FirstDateTime();
                    endDateTime = period.End;
                }
                else
                {
                    startDateTime = currentTime.FirstDateTime();
                    endDateTime = currentTime.LastDateTime();
                }

                allTotalAmount += GetOneMonthBudget(new Period(startDateTime, endDateTime), budgets);
            }

            return allTotalAmount;
        }

        private static bool IsEndYearMonth(Period period, DateTime dateTime)
        {
            return dateTime.YearMonth() != period.End.AddMonths(1).YearMonth();
        }

        private int GetOneMonthBudget(Period period, List<Budget> budgets)
        {
            var yearMonth = period.Start.YearMonth();
            var monthBudget = budgets.FirstOrDefault(p => p.YearMonth == yearMonth);

            var days = period.Start.Days();
            return monthBudget.DailyAmount(days) * period.GetDays();
        }
    }
}