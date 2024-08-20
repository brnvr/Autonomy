using System.Reflection;

namespace AutonomyApi.WebService
{
    public static class IEnumerableExtensions
    {
        public static IOrderedEnumerable<T> OrderBy<T>(this IEnumerable<T> enumerable, string name)
        {
            var type = typeof(T);
            var property = type.GetProperty(name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (property == null)
            {
                throw new Exception($"Type {type.Name} has no property {name}.");
            }

            Func<T, object?> lambda = instance =>
            {
                return property.GetValue(instance);
            };

            return enumerable.OrderBy(lambda);
        }
        public static IOrderedEnumerable<T> OrderByDescending<T>(this IEnumerable<T> enumerable, string name)
        {
            var type = typeof(T);
            var property = type.GetProperty(name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (property == null)
            {
                throw new Exception($"Type {type.Name} has no property {name}.");
            }

            Func<T, object?> lambda = instance =>
            {
                return property.GetValue(instance);
            };

            return enumerable.OrderByDescending(lambda);
        }
    }
}
