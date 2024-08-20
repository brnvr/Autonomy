namespace AutonomyApi.WebService.DynamicFilters
{
    public class SubstringFilter<T> : IDynamicFilter<T>
    {
        public string? Substring { get; set; }
        public bool CaseSensitive { get; set; }
        public bool AllowsNull { get; set; } = true;
        public Func<T, IEnumerable<string?>> GetText { get; set; }

        public SubstringFilter(string? substring, Func<T, string?> getText)
        {
            GetText = input => [ getText(input) ];
            Substring = substring;
        }

        public SubstringFilter(string? substring, Func<T, IEnumerable<string?>> getText)
        {
            GetText = getText;
            Substring = substring;
        }

        public bool IsMatch(T entity)
        {
            if (Substring == null && !AllowsNull)
            {
                return false;
            }

            if (CaseSensitive)
            {
                return GetText(entity)
                .Any(str => (str ?? "")
                .Contains(Substring ?? ""));
            }

            return GetText(entity)
                .Any(str => (str ?? "")
                .ToUpper()
                .Contains((Substring ?? "").ToUpper()));
        }
    }
}
