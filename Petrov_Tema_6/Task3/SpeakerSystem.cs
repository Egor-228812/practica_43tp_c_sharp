namespace Task3
{
    internal class SpeakerSystem
    {
        int currentvolume;
        public void ChangeVolume(int volume)
        {
            currentvolume = volume;
            Console.WriteLine($"Громкость динамиков изменена на: {volume}");
        }
    }
}
