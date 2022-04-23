using System;
using System.Collections.Generic;
using UnityEngine;

namespace WordSearch
{
	public class EffectSound : SingletonComponent<EffectSound>
	{
		public AudioSource audioSound;

		public List<AudioClip> ClipSystem;

		public List<AudioClip> ClipGameSound;

		private bool chk1 = true;

		private bool chk2 = true;

		private new void Awake()
		{
		}

		private void Start()
		{
			this.audioSound.volume = 1f;
		}

		public void SoundOnOff(int power)
		{
			this.audioSound.volume = (float)power;
			PlayerPrefs.SetInt("IsSound", power);
			SingletonComponent<WordSearchController>.Instance.IsSound = power;
		}

		public void SystemSound(int num)
		{
			this.audioSound.PlayOneShot(this.ClipSystem[num]);
		}

		public void GameSound(int num)
		{
			this.audioSound.PlayOneShot(this.ClipGameSound[num]);
		}

		public void SoundStop()
		{
			if (this.audioSound.isPlaying)
			{
				this.audioSound.Stop();
			}
		}

		public void SoundPlay()
		{
			this.audioSound.Play();
		}
	}
}
