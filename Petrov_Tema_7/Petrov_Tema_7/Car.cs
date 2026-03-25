
namespace Task1
{
    internal class Car
    {
        public void SetSpeed(int speed)
        {
            if (speed > 120)
            {
                throw new SpeedLimitExceededException($"Скорость ТС выше допустимых {speed} км/ч");
            }
            Console.WriteLine($"Скорость {speed} допустима");
        }
    }
}
