namespace AutonomyApi.WebService.DynamicFilters
{
    public class TextMatchFilter<T> : IDynamicFilter<T>
    {
        public string? Substring { get; set; }
        public bool CaseSensitive { get; set; }
        public bool AllowsNull { get; set; }
        public Func<T, string> GetText { get; set; }

        public TextMatchFilter(Func<T, string> getText, string? substring, bool caseSensitive = false, bool allowsNull = true)
        {
            GetText = getText;
            Substring = substring;
            CaseSensitive = caseSensitive;
            AllowsNull = allowsNull;
        }

        public bool IsMatch(T entity)
        {
            if (Substring == null)
            {
                return AllowsNull;
            }

            if (CaseSensitive)
            {
                return GetText(entity).Contains(Substring);
            }

            return GetText(entity).ToUpper().Contains(Substring.ToUpper());
        }
    }
}
