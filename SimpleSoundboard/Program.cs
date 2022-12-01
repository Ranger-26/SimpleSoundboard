using System;
using NAudio.Wave;

namespace SimpleSoundboard
{
    internal class Program
    {
        private static WaveOutEvent _SoundEffectWaveOut;

        private static int VB_OUTPUT = 3;
        private static int MIC_INPUT = 0;
        
        
        public static void Main(string[] args)
        {
           Microphone m = new Microphone(MIC_INPUT, VB_OUTPUT);

           _SoundEffectWaveOut = new WaveOutEvent();
           _SoundEffectWaveOut.DeviceNumber = VB_OUTPUT;
           
           
           Console.WriteLine("----------WaveIn Devices:-------------------");
           for (int n = -1; n < WaveIn.DeviceCount; n++)
           {
               var caps = WaveIn.GetCapabilities(n);
               Console.WriteLine($"{n}: {caps.ProductName}");
           }
            
           Console.WriteLine("----------WaveOut Devices:-------------------");
           for (int n = -1; n < WaveOut.DeviceCount; n++)
           {
               var caps = WaveOut.GetCapabilities(n);
               Console.WriteLine($"{n}: {caps.ProductName}");
           }
           
           while (true)
           {
                
               var input = Console.ReadLine();
               //quit if q is being pressed
               if (input.Equals("q"))
               {
                   break;
               }
               PlaySoundboard();

           }
        }
        /*
        public static void PlaySoundboard()
        {
            Console.WriteLine("Playing soundboard");            
            //set device number
            int deviceNumber = 3;
            
            //load in audio file
            var audioFile = new NAudio.Wave.AudioFileReader(@"C:\Users\siddh\Desktop\vine-boom.mp3");
            
            //create and set up new waveout device with audio file
            var waveOut = new NAudio.Wave.WaveOut();
            waveOut.DeviceNumber = deviceNumber;
            waveOut.Init(audioFile);
            
            //play audio file
            waveOut.Play();
            //dispose resources
            audioFile.Dispose();
            waveOut.Dispose();
        }
        */
        
        
        public static void PlaySoundboard()
        {
            Console.WriteLine("Playing soundboard");
            //set device number
            
            //paths
            //C:\Users\s2104427\Desktop\Vine-boom-sound-effect.mp3
            //C:\Users\siddh\Desktop\vine-boom.mp3
            var audioFile = new NAudio.Wave.AudioFileReader(@"C:\Users\s2104427\Desktop\Vine-boom-sound-effect.mp3");
            //var waveOut = new NAudio.Wave.WaveOut();
            //waveOut.DeviceNumber = deviceNumber;
            //_SoundEffectWaveOut.Dispose();
            _SoundEffectWaveOut.Init(audioFile);
            _SoundEffectWaveOut.Play();
            
            
            
            //Disposing waveout breaks this????
            
            //disposing audiofile makes sound play twice???

            //audioFile.Dispose();
            //_SoundEffectWaveOut.Dispose();
            
        }
        
    }
}