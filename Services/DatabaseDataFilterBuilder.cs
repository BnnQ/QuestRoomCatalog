using System.Linq.Expressions;

namespace Homework.Services
{
    public class DatabaseDataFilterBuilder<TItem>
    {
        private readonly DatabaseDataFilter<TItem> filter;

        public DatabaseDataFilterBuilder()
        {
            filter = new();
        }

        public DatabaseDataFilterBuilder<TItem> SetItems(IQueryable<TItem> items)
        {
            filter.Items = items;
            return this;
        }

        public DatabaseDataFilterBuilder<TItem> AddRule(Expression<Func<TItem, bool>> rule)
        {
            filter.Rules.Add(rule);
            return this;
        }

        public DatabaseDataFilterBuilder<TItem> AddRuleIf(Expression<Func<TItem, bool>> rule, bool condition)
        {
            if (condition)
                filter.Rules.Add(rule);

            return this;
        }

        public DatabaseDataFilter<TItem> Build()
        {
            return filter;
        }
    }
}