
namespace Task1
{
    internal class SpeedLimitExceededException : Exception
    {
        SpeedLimitExceededException() { }
        public SpeedLimitExceededException(string message) : base (message) {}
    }
}
