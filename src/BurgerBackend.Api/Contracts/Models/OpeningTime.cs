namespace BurgerBackend.Api.Contracts.Models
{
    public class OpeningTime
    {
        public string Day { get; init; } = string.Empty;

        public string OpeningStartTime { get; init; } = string.Empty;

        public string OpeningEndTime { get; init; } = string.Empty;
    }
}
