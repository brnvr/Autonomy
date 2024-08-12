namespace AutonomyApi.WebService
{
    public delegate IEnumerable<T> Filter<T>(IEnumerable<T> conditions);
}
