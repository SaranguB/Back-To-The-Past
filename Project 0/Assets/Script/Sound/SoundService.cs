using System;
using UnityEngine;


namespace Sound
{
    public class SoundService
    {
        private SoundSO soundSo;
        private AudioSource audioEffects;
        private AudioSource backgroundMusic;

        public SoundService(SoundSO soundSo, AudioSource audioEffectSource, AudioSource backgroundMusicSource)
        {
            this.soundSo = soundSo;
            this.audioEffects = audioEffectSource;
            this.backgroundMusic = backgroundMusicSource;
        }

        public void PlaySoundEffects(SoundType soundType, bool loopSound = false)
        {
            AudioClip clip = GetSoundClip(soundType);
        }

        private AudioClip GetSoundClip(SoundType soundType)
        {
            throw new NotImplementedException();
        }
    }
}
