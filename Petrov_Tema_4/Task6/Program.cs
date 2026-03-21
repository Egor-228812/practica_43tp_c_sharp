int[][] dates = new int[][]
{
    new int[] { 1, 1, 2024 },    // 31.12.2023
    new int[] { 1, 3, 2024 },    // 29.02.2024 високосны
    new int[] { 15, 5, 2025 }    // 14.05.2025
};

foreach (var date in dates)
{
    int d = date[0], m = date[1], y = date[2];
    DateHelper.PrevDate(ref d, ref m, ref y);
    Console.WriteLine($"Предыдущая дата для {date[0]:D2}.{date[1]:D2}.{date[2]}: {d:D2}.{m:D2}.{y}");
}
public static class DateHelper
{
    public static void PrevDate(ref int D, ref int M, ref int Y)
    {
        D--;
        if (D >= 1) return;

        M--;
        if (M < 1)
        {
            M = 12;
            Y--;
        }

        int daysInMonth = GetDaysInMonth(M, Y);
        D = daysInMonth;
    }
    private static int GetDaysInMonth(int month, int year)
    {
        switch (month)
        {
            case 2:
                return IsLeapYear(year) ? 29 : 28;
            case 4:
            case 6:
            case 9:
            case 11:
                return 30;
            default:
                return 31;
        }
    }

    private static bool IsLeapYear(int year)
    {
        return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
    }
}
