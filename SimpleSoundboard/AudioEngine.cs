using System;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace SimpleSoundboard
{
    public class AudioEngine : IDisposable
    {
        private readonly WaveOutEvent outputDevice;
        private readonly MixingSampleProvider mixer;

        public static AudioEngine Instance = new AudioEngine(waveOutDevice:3);

        private ISampleProvider _curSampleProvider;
        
        public AudioEngine(int sampleRate = 44100, int channelCount = 2, int waveOutDevice = 0)
        {
            outputDevice = new WaveOutEvent();
            mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channelCount))
            {
                 ReadFully = true
            };
            outputDevice.DeviceNumber = waveOutDevice;
            outputDevice.Init(mixer);
            outputDevice.Play();
        }

        public void PlaySound(string fileName)
        {
            var input = new AudioFileReader(fileName);
            AddMixerInput(input);
            input.Dispose();
        }

        public void PlaySound(IWaveProvider waveProvider)
        {
            PlaySound(waveProvider.ToSampleProvider());
        }

        public void PlaySound(ISampleProvider provider)
        {
            AddMixerInput(provider);
        }
        
        private ISampleProvider ConvertToRightChannelCount(ISampleProvider input)
        {
            if (input.WaveFormat.Channels == mixer.WaveFormat.Channels)
            {
                return input;
            }
            if (input.WaveFormat.Channels == 1 && mixer.WaveFormat.Channels == 2)
            {
                return new MonoToStereoSampleProvider(input);
            }
            throw new NotImplementedException("Not yet implemented this channel count conversion");
        }
        
        private void AddMixerInput(ISampleProvider input, bool clearPrevious = true)
        {
            if (clearPrevious)
            {
                mixer.RemoveMixerInput(_curSampleProvider);
            }
            mixer.AddMixerInput(ConvertToRightChannelCount(input));
            _curSampleProvider = input;
        }

        public void Dispose()
        {
            outputDevice?.Dispose();
        }
    }
}