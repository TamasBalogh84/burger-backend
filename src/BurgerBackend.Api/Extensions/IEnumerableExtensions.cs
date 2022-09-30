namespace BurgerBackend.Api.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Replace<T>(this IEnumerable<T> enumerable, Func<T, bool> selector, T value)
        {
            foreach (var item in enumerable)
            {
                yield return selector(item) ? value : item;
            }
        }
    }
}
