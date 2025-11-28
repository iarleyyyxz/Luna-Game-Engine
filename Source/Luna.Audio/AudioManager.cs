using OpenTK.Audio.OpenAL;

namespace Luna.Audio
{
    public class AudioManager : IAudio
    {

        private Dictionary<string, int> buffers = new();
        private Dictionary<string, int> sources = new();

        public void Init()
        {
            ALC.CreateContext(ALC.OpenDevice(null), (int[])null);
        }

        public void PlayMusic(string path, bool loop = true)
        {
            throw new NotImplementedException();
        }

        public void PlaySound(string id, float volume = 1f, bool loop = false)
        {
            if (!buffers.ContainsKey(id)) return;


            int source = AL.GenSource();
            AL.Source(source, ALSourcei.Buffer, buffers[id]);
            AL.Source(source, ALSourcef.Gain, volume);
            AL.Source(source, ALSourceb.Looping, loop);
            AL.SourcePlay(source);


            sources[id] = source;
        }

        public void SetChannelVolume(string channel, float volume)
        {
            throw new NotImplementedException();
        }

        public void Stop(string id)
        {
            throw new NotImplementedException();
        }

        public int LoadWav(string file)
        {
        using var stream = File.OpenRead(file);
        // parse WAV and load to OpenAL buffer
        int buffer = AL.GenBuffer();
        // AL.BufferData(...)
        return buffer;
        }
    }
}