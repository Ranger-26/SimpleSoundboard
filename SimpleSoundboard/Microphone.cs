using System.Threading;
using NAudio.Dsp;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace SimpleSoundboard
{
    public class Microphone
    {
        private WaveInEvent _waveIn;
        
        private BufferedWaveProvider _bufferedWaveProvider;
        
        public Microphone(int microphoneId, int outputId, int sampleRate = 44100, int channelCount = 2)
        {
            
            //initalize wave in and wave out devices
            _waveIn = new WaveInEvent();
            _waveIn.DeviceNumber = microphoneId;
            
            //initalize wave out data
            _bufferedWaveProvider = new BufferedWaveProvider(WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channelCount));

            
            //start recording
            _waveIn.StartRecording();
            _waveIn.DataAvailable += OnDataAvailable;

            AudioEngine.Instance.PlaySound(_bufferedWaveProvider);
            
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