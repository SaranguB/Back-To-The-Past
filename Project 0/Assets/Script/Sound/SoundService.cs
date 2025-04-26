using System;
using UnityEngine;

namespace Audio
{
    public class SoundService
    {
        private SoundSO soundSo;
        private AudioSource audioEffects;
        private AudioSource backgroundMusic;

        public SoundService(SoundSO soundSo, AudioSource audioEffectSource, AudioSource backgroundMusicSource)
        {
            this.soundSo = soundSo;
            audioEffects = audioEffectSource;
            backgroundMusic = backgroundMusicSource;
        }

        public void PlaySoundEffects(SoundType soundType, bool loopSound = false)
        {
            AudioClip clip = GetSoundClip(soundType);
            if (clip != null)
            {
                audioEffects.loop = loopSound;
                audioEffects.clip = clip;
                audioEffects.PlayOneShot(clip);
            }
            else
                Debug.LogError("No Audio Clip selected.");
        }

        public void PlayBackgroundMusic(SoundType soundType, bool loopSound = true)
        {
            AudioClip clip = GetSoundClip(soundType);
            if (clip != null)
            {
                backgroundMusic.loop = loopSound;
                backgroundMusic.clip = clip;
                backgroundMusic.Play();
            }
            else
                Debug.LogError("No Audio Clip selected.");

        }

        public bool IsAudioEffectsPlaying()
            => audioEffects.isPlaying;

        private AudioClip GetSoundClip(SoundType soundType)
        {
            Sounds sound = Array.Find(soundSo.audioList, item => item.soundType == soundType);
            if (sound.audioClip != null)
                return sound.audioClip;
            return null;
        }

        public void StopPlayingSoundEffect()
            => audioEffects.Stop();

        public void StopBackgroundSong()
            => backgroundMusic.Stop();
    }
}
