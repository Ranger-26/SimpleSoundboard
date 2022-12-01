using System.Threading;
using NAudio.Wave;

namespace SimpleSoundboard
{
    public class Microphone
    {
        private WaveInEvent _waveIn;

        private WaveOutEvent _waveOut;
        
        private BufferedWaveProvider _bufferedWaveProvider; 
        
        public Microphone(int microphoneId, int outputId)
        {
            //initalize wave in and wave out devices
            _waveIn = new WaveInEvent();
            _waveIn.DeviceNumber = microphoneId;
            _waveOut = new WaveOutEvent();
            _waveOut.DeviceNumber = outputId;
            
            //initalize wave out data
            _bufferedWaveProvider = new BufferedWaveProvider(_waveIn.WaveFormat);
            _waveOut.Init(_bufferedWaveProvider);
            _waveOut.Play();
            
            //start recording
            _waveIn.StartRecording();
            _waveIn.DataAvailable += OnDataAvailable;

            //run main loop on another thread
            Thread thread = new Thread(MainLoop);
            thread.Start();
        }

        public void OnDataAvailable(object sender, WaveInEventArgs e)
        {
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