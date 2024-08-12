namespace AutonomyApi.WebService
{
    public static class FilterCondition
    {
        public static bool Contains(string str, string? substr)
        {
            if (string.IsNullOrEmpty(substr))
            {
                return true;
            }

            return str.Contains(substr);
        }
    }
}
