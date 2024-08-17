using AutonomyApi.Models.Entities;

namespace AutonomyApi.Models.ViewModels.Budget
{
    public class BudgetItemUpdateViewList : List<BudgetItemUpdateView>
    {
        public List<BudgetItem> ToBudgetItemList()
        {
            var items = new List<BudgetItem>();

            for (var pos = 0; pos < Count; pos++)
            {
                var item = this[pos];

                items.Add(new BudgetItem
                {
                    Position = pos,
                    Name = item.Name,
                    Quantity = item.Quantity,
                    CurrencyId = item.CurrencyId,
                    UnitPrice = item.UnitPrice,
                    Duration = item.Duration,
                    DurationTimeUnit = item.DurationTimeUnit
                });
            }

            return items;
        }
    }
}
