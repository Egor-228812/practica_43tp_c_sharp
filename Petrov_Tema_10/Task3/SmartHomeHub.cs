using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class SmartHomeHub
    {
        private List<IDevice> devices = new List<IDevice>();
        private string state;

        public void AddDevice(IDevice device)
        {
            devices.Add(device);
        }

        public void RemoveDevice(IDevice device)
        {
            devices.Remove(device);
        }

        public void SetState(string newState)
        {
            state = newState;
            NotifyDevices();
        }

        private void NotifyDevices()
        {
            foreach (var device in devices)
            {
                device.Update(state);
            }
        }
    }
}
