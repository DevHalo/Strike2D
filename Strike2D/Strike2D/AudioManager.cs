using System.Collections.Generic;
using System.Linq;
using CSCore;
using CSCore.Codecs;
using CSCore.SoundOut;
using Microsoft.Xna.Framework;

namespace Strike2D
{
    public class AudioManager
    {
        public Dictionary<string, SoundContainer> Sounds = new Dictionary<string, SoundContainer>();

        /// <summary>
        /// Loads a sound into memory and returns it
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public SoundContainer LoadSound(string fileName, string key)
        {
            ISoundOut soundEffect;
            IWaveSource source;
            SoundContainer newSound = null;

            switch (fileName.Split('.').Last())
            {
                case "mp3":
                    Debug.WriteLineVerbose("Loading " + fileName + " type of MP3");
                    source = CodecFactory.Instance.GetCodec(fileName);

                    soundEffect = WasapiOut.IsSupportedOnCurrentPlatform ? 
                        (ISoundOut) new WasapiOut() : new DirectSoundOut(); 
                    
                    soundEffect.Initialize(source);

                    newSound = new SoundContainer(soundEffect, source);
                    
                    Sounds.Add(key, newSound);
                    break;
            }

            return newSound;
        }

        /// <summary>
        /// Disposes a specific sound effect
        /// </summary>
        public void Dispose(string key)
        {
            if (Sounds.ContainsKey(key))
            {
                SoundContainer sound = Sounds[key];
                sound.Dispose();

                Sounds.Remove(key);
            }
            else
            {
                Debug.WriteLineVerbose("Missing sound \"" + key + "\"", Debug.DebugType.Warning);
            }
        }

        /// <summary>
        /// Disposes all sounds loaded in memory
        /// </summary>
        public void DisposeAll()
        {
            foreach (KeyValuePair<string, SoundContainer> sound in Sounds)
            {
                sound.Value.Dispose();
            }
            
            Sounds.Clear();
        }
    }

    public class SoundContainer
    {
        private ISoundOut sound;
        private IWaveSource source;

        public SoundContainer(ISoundOut sound, IWaveSource source)
        {
            this.sound = sound;
            this.source = source;
        }

        public void Dispose()
        {
            sound.Dispose();
            source.Dispose();
        }

        public void Play(int volume)
        {
            sound.Volume = MathHelper.Clamp((float)volume / Settings.MasterVolume, 0f, 1f);
            Debug.WriteLineVerbose(sound.Volume.ToString());

            sound.Play();
        }

        public void Pause()
        {
            sound.Pause();
        }

        public void Stop()
        {
            sound.Stop();
        }
    }
}