#region Using
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;
#endregion

namespace ligne7
{
    class Son
    {
        AudioEngine engine;
        SoundBank soundBank;
        WaveBank waveBank;
        AudioCategory musicCategory;
        Cue cue;

        string assetName;

        public Son()
        {
            // Initialize audio objects.
            engine = new AudioEngine("Content/Musique/lignesond.xgs");
            waveBank = new WaveBank(engine, "Content/Musique/wave.xwb");
            soundBank = new SoundBank(engine, "Content/Musique/sond.xsb");
        }

        public void LoadContentAndPlay(string assetName)
        {
            // Play the sound.
            this.assetName = assetName;
            cue = soundBank.GetCue(assetName);
            cue.Play();
        }

        public void ChangeVolume(float volume)
        {
            engine.Update();
            musicCategory = engine.GetCategory("Default");
            musicCategory.SetVolume(volume);
        }

        public void StopSon()
        {
            cue.Stop(AudioStopOptions.Immediate);
        }
    }
}
