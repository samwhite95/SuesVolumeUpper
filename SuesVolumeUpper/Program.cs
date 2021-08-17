using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.VisualBasic.CompilerServices;
using NAudio;
using NAudio.Mixer;
using NAudio.Wave;

namespace SuesVolumeUpper
{
    class Program
    {

        
        static void Main(string[] args)
        {
            int waveInDeviceNumber = 100;
            for (int n = 0; n <WaveIn.DeviceCount; n++)
            {
                var caps = WaveIn.GetCapabilities(n);
                Console.WriteLine($"{n}: {caps.ProductName}");
                if (LikeOperator.LikeString(caps.ProductName, "*rophone (Yeti Cla*", Microsoft.VisualBasic.CompareMethod.Text))
                {
                    waveInDeviceNumber = n;
                }
            }
            
            var desiredVolume = 100;
            if(waveInDeviceNumber == 100)
            {
                Console.WriteLine("please press the corresponding number");
                var deviceNumber = Console.ReadKey();

                waveInDeviceNumber = Int32.Parse(deviceNumber.KeyChar.ToString());
            }

            var mixerLine = new MixerLine((IntPtr)waveInDeviceNumber, 0, MixerFlags.WaveIn);
            foreach (var control in mixerLine.Controls)
            {
                if (control.ControlType == MixerControlType.Volume)
                {
                    var volumeControl = control as UnsignedMixerControl;
                    volumeControl.Percent = desiredVolume;
                    break;
                }
            }
            
        }
    }
}
