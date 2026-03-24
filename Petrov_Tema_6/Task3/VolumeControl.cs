using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    internal class VolumeControl
    {
        public event VolumeChangedHandler VolumeChanged;
        public void SetVolume(int newVolume)
        {
            VolumeChanged?.Invoke(newVolume);
        }
    }
}
