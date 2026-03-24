using Task3;

public delegate void VolumeChangedHandler(int newVolume);
class Program
{
    static void Main()
    {
        VolumeControl volumeControl = new VolumeControl();
        Display display = new Display();
        SpeakerSystem speakerSystem = new SpeakerSystem();
        volumeControl.VolumeChanged += display.ShowVolume;
        volumeControl.VolumeChanged += speakerSystem.ChangeVolume;
        Console.WriteLine("Введите громкость: ");
        int volume = int.Parse(Console.ReadLine());
        volumeControl.SetVolume(volume);
    }
}