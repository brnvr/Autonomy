using System.Data.SqlTypes;

namespace AutonomyApi.WebService.DynamicFilters
{
    public class ExpFilter<T, TProperty> : IDynamicFilter<T>
    {
        public TProperty Value { get; }
        public Func<T, IEnumerable<TProperty>> GetValue { get; }
        public Func<TProperty, TProperty, bool> Compare { get; }
        public bool AllowsNull { get; set; } = true;

        public ExpFilter(TProperty value, Func<TProperty, TProperty, bool> compare, Func<T, IEnumerable<TProperty>> getValue)
        {
            GetValue = getValue;
            Value = value;
            Compare = compare;
        }

        public static ExpFilter<T, TProperty> Equals(TProperty value, Func<T, IEnumerable<TProperty>> getValue)
        {
            return new ExpFilter<T, TProperty>(value, (a, b) => a is null || a.Equals(b), getValue);
        }

        public static ExpFilter<T, TProperty> GreaterThan(TProperty value, Func<T, IEnumerable<TProperty>> getValue)
        {
            return new ExpFilter<T, TProperty>(value, (a, b) =>
            {
                var compA = a as IComparable;

                if (compA == null)
                {
                    throw new ArgumentException($"{nameof(TProperty)} doesn't implement IComparable.");
                }

                return compA.CompareTo(b) > 0;
            }, getValue);
        }

        public static ExpFilter<T, TProperty> GreaterThanOrEqualTo(TProperty value, Func<T, IEnumerable<TProperty>> getValue)
        {
            return new ExpFilter<T, TProperty>(value, (a, b) =>
            {
                var compA = a as IComparable;

                if (compA == null)
                {
                    throw new ArgumentException($"{nameof(TProperty)} doesn't implement IComparable.");
                }

                return compA.CompareTo(b) >= 0;
            }, getValue);
        }

        public static ExpFilter<T, TProperty> LessThan(TProperty value, Func<T, IEnumerable<TProperty>> getValue)
        {
            return new ExpFilter<T, TProperty>(value, (a, b) =>
            {
                var compA = a as IComparable;

                if (compA == null)
                {
                    throw new ArgumentException($"{nameof(TProperty)} doesn't implement IComparable.");
                }

                return compA.CompareTo(b) < 0;
            }, getValue);
        }

        public static ExpFilter<T, TProperty> LessThanOrEqualTo(TProperty value, Func<T, IEnumerable<TProperty>> getValue)
        {
            return new ExpFilter<T, TProperty>(value, (a, b) =>
            {
                var compA = a as IComparable;

                if (compA == null)
                {
                    throw new ArgumentException($"{nameof(TProperty)} doesn't implement IComparable.");
                }

                return compA.CompareTo(b) <= 0;
            }, getValue);
        }

        public bool IsMatch(T entity)
        {
            if (Value == null)
            {
                return AllowsNull;
            }

            return GetValue(entity).Any(val => Compare(Value, val));
        }
    }
}
