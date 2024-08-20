using System.Data.SqlTypes;

namespace AutonomyApi.WebService.DynamicFilters
{
    public class ComparisonFilter<T, TProperty> : IDynamicFilter<T>
    {
        public TProperty Value { get; }
        public Func<T, IEnumerable<TProperty>> GetValue { get; }
        public Func<TProperty, TProperty, bool> Compare { get; }
        public bool AllowsNull { get; set; } = true;

        public ComparisonFilter(TProperty value, Func<TProperty, TProperty, bool> compare, Func<T, IEnumerable<TProperty>> getValue)
        {
            GetValue = getValue;
            Value = value;
            Compare = compare;
        }

        public static ComparisonFilter<T, TProperty> Equals(TProperty value, Func<T, IEnumerable<TProperty>> getValue)
        {
            return new ComparisonFilter<T, TProperty>(value, (a, b) => a is null || a.Equals(b), getValue);
        }

        public static ComparisonFilter<T, TProperty> GreaterThan(TProperty value, Func<T, IEnumerable<TProperty>> getValue)
        {
            return new ComparisonFilter<T, TProperty>(value, (a, b) =>
            {
                var compA = a as IComparable;

                if (compA == null)
                {
                    throw new ArgumentException($"{nameof(TProperty)} doesn't implement IComparable.");
                }

                return compA.CompareTo(b) > 0;
            }, getValue);
        }

        public static ComparisonFilter<T, TProperty> GreaterThanOrEqualTo(TProperty value, Func<T, IEnumerable<TProperty>> getValue)
        {
            return new ComparisonFilter<T, TProperty>(value, (a, b) =>
            {
                var compA = a as IComparable;

                if (compA == null)
                {
                    throw new ArgumentException($"{nameof(TProperty)} doesn't implement IComparable.");
                }

                return compA.CompareTo(b) >= 0;
            }, getValue);
        }

        public static ComparisonFilter<T, TProperty> LessThan(TProperty value, Func<T, IEnumerable<TProperty>> getValue)
        {
            return new ComparisonFilter<T, TProperty>(value, (a, b) =>
            {
                var compA = a as IComparable;

                if (compA == null)
                {
                    throw new ArgumentException($"{nameof(TProperty)} doesn't implement IComparable.");
                }

                return compA.CompareTo(b) < 0;
            }, getValue);
        }

        public static ComparisonFilter<T, TProperty> LessThanOrEqualTo(TProperty value, Func<T, IEnumerable<TProperty>> getValue)
        {
            return new ComparisonFilter<T, TProperty>(value, (a, b) =>
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
