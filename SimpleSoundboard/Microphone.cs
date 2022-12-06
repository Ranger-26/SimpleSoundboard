using System;
using System.Diagnostics;
using System.Threading;
using NAudio.Dsp;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace SimpleSoundboard
{
    public class Microphone
    {
        private WaveInEvent _waveIn;

        private WaveOutEvent _waveOut;
        
        private BufferedWaveProvider _bufferedWaveProvider;
        
        public Microphone(int microphoneId, int outputId, int sampleRate = 44100, int channelCount = 2)
        {
            
            //initalize wave in and wave out devices
            _waveIn = new WaveInEvent();
            _waveIn.DeviceNumber = microphoneId;
            
            //initalize wave out data
            _bufferedWaveProvider = new BufferedWaveProvider(_waveIn.WaveFormat);

            _waveOut = new WaveOutEvent();
            _waveOut.DeviceNumber = outputId;
            _waveOut.Init(_bufferedWaveProvider);
            
            
            AudioEngine.Instance.PlaySound(_bufferedWaveProvider);
            //start recording
            _waveIn.StartRecording();
            _waveIn.DataAvailable += OnDataAvailable;

            
            //run main loop on another thread
            Thread thread = new Thread(MainLoop);
            thread.Start();
        }

        public void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            //Console.WriteLine("Detected sound!");
            _bufferedWaveProvider.AddSamples(e.Buffer, 0 , e.BytesRecorded);
        }
        
        public void MainLoop()
        {
            while (true)
            {
                
            }
        }
    }
}