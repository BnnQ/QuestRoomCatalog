using System.Linq.Expressions;

namespace Homework.Services
{
    public class DatabaseDataFilter<TItem>
    {
        public IQueryable<TItem> Items { get; set; }
        public IList<Expression<Func<TItem, bool>>> Rules { get; set; }
        
        public DatabaseDataFilter()
        {
            Items = new List<TItem>().AsQueryable();
            Rules = new List<Expression<Func<TItem, bool>>>();
        }
        public DatabaseDataFilter(IQueryable<TItem> items) : this()
        {
            Items = items;
        }
        public DatabaseDataFilter(IQueryable<TItem> items, IList<Expression<Func<TItem, bool>>> rules) : this(items)
        {
            Rules = rules;
        }

        public IQueryable<TItem> FilterItems()
        {
            foreach (var rule in Rules)
                Items = Items.Where(rule);

            return Items;
        }
    }
}