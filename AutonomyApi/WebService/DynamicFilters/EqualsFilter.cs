namespace AutonomyApi.WebService.DynamicFilters
{
    public class EqualsFilter<T, TProperty> : IDynamicFilter<T>
    {
        public TProperty Value { get; set; }
        public Func<T, TProperty> GetValue { get; set; }

        public EqualsFilter(Func<T, TProperty> getValue, TProperty value)
        {
            GetValue = getValue;
            Value = value;
        }

        public bool IsMatch(T entity)
        {
            var val = GetValue(entity);

            if (val == null)
            {
                return Value == null;
            }

            return val.Equals(Value);
        }
    }
}
