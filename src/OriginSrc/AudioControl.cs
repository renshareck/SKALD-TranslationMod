using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200002C RID: 44
public static class AudioControl
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000478 RID: 1144 RVA: 0x00015EAF File Offset: 0x000140AF
	// (set) Token: 0x06000479 RID: 1145 RVA: 0x00015EC7 File Offset: 0x000140C7
	private static AudioControl.SoundFXPlayer soundFXPlayer
	{
		get
		{
			if (AudioControl._soundFXPlayer == null)
			{
				AudioControl._soundFXPlayer = new AudioControl.SoundFXPlayer();
			}
			return AudioControl._soundFXPlayer;
		}
		set
		{
			AudioControl._soundFXPlayer = value;
		}
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x0600047A RID: 1146 RVA: 0x00015ECF File Offset: 0x000140CF
	// (set) Token: 0x0600047B RID: 1147 RVA: 0x00015EE7 File Offset: 0x000140E7
	private static AudioControl.MusicPlayer musicPlayer
	{
		get
		{
			if (AudioControl._musicPlayer == null)
			{
				AudioControl._musicPlayer = new AudioControl.MusicPlayer();
			}
			return AudioControl._musicPlayer;
		}
		set
		{
			AudioControl._musicPlayer = value;
		}
	}

	// Token: 0x17000003 RID: 3
	// (get) Token: 0x0600047C RID: 1148 RVA: 0x00015EEF File Offset: 0x000140EF
	// (set) Token: 0x0600047D RID: 1149 RVA: 0x00015F07 File Offset: 0x00014107
	private static AudioControl.MoveSoundPlayer moveSoundPlayer
	{
		get
		{
			if (AudioControl._moveSoundPlayer == null)
			{
				AudioControl._moveSoundPlayer = new AudioControl.MoveSoundPlayer();
			}
			return AudioControl._moveSoundPlayer;
		}
		set
		{
			AudioControl._moveSoundPlayer = value;
		}
	}

	// Token: 0x0600047E RID: 1150 RVA: 0x00015F0F File Offset: 0x0001410F
	public static float getBaseMusicVolume()
	{
		return AudioControl.musicPlayer.getBaseVolume();
	}

	// Token: 0x0600047F RID: 1151 RVA: 0x00015F1B File Offset: 0x0001411B
	public static void setMusicVolume(float value)
	{
		AudioControl.musicPlayer.setVolume(value);
	}

	// Token: 0x06000480 RID: 1152 RVA: 0x00015F28 File Offset: 0x00014128
	public static void setSoundVolume(float value)
	{
		AudioControl.soundFXPlayer.setVolume(value);
		AudioControl.moveSoundPlayer.setVolume(value);
	}

	// Token: 0x06000481 RID: 1153 RVA: 0x00015F40 File Offset: 0x00014140
	public static void adjustMusicVolume(int i)
	{
		AudioControl.musicPlayer.adjustVolume(i);
	}

	// Token: 0x06000482 RID: 1154 RVA: 0x00015F4D File Offset: 0x0001414D
	public static string printMusicVolume()
	{
		return AudioControl.musicPlayer.printVolume();
	}

	// Token: 0x06000483 RID: 1155 RVA: 0x00015F59 File Offset: 0x00014159
	public static float getBaseSoundVolume()
	{
		return AudioControl.soundFXPlayer.getBaseVolume();
	}

	// Token: 0x06000484 RID: 1156 RVA: 0x00015F65 File Offset: 0x00014165
	public static void adjustSoundVolume(int i)
	{
		AudioControl.soundFXPlayer.adjustVolume(i);
		AudioControl.moveSoundPlayer.adjustVolume(i);
	}

	// Token: 0x06000485 RID: 1157 RVA: 0x00015F7D File Offset: 0x0001417D
	public static string printSoundVolume()
	{
		return AudioControl.soundFXPlayer.printVolume();
	}

	// Token: 0x06000486 RID: 1158 RVA: 0x00015F89 File Offset: 0x00014189
	public static void playCombatMusic()
	{
		AudioControl.playMusic(MainControl.getDataControl().currentMap.getCombatMusicPath());
	}

	// Token: 0x06000487 RID: 1159 RVA: 0x00015F9F File Offset: 0x0001419F
	public static void playMoveSound()
	{
		AudioControl.moveSoundPlayer.playMoveSound();
	}

	// Token: 0x06000488 RID: 1160 RVA: 0x00015FAB File Offset: 0x000141AB
	public static void playMoveSoundWater()
	{
		AudioControl.moveSoundPlayer.playMoveSoundWater();
	}

	// Token: 0x06000489 RID: 1161 RVA: 0x00015FB7 File Offset: 0x000141B7
	public static void playMoveSoundVegetation()
	{
		AudioControl.moveSoundPlayer.playMoveSoundVegetation();
	}

	// Token: 0x0600048A RID: 1162 RVA: 0x00015FC3 File Offset: 0x000141C3
	public static void playMoveSoundStealth()
	{
		AudioControl.moveSoundPlayer.playMoveSoundStealth();
	}

	// Token: 0x0600048B RID: 1163 RVA: 0x00015FCF File Offset: 0x000141CF
	public static void playCoinSound()
	{
		AudioControl.playSound("Money1");
	}

	// Token: 0x0600048C RID: 1164 RVA: 0x00015FDB File Offset: 0x000141DB
	public static void playPaperSound()
	{
		AudioControl.playSound("ItemPaper1");
	}

	// Token: 0x0600048D RID: 1165 RVA: 0x00015FE7 File Offset: 0x000141E7
	public static void playCoinsOutSound()
	{
		AudioControl.playSound("Money2");
	}

	// Token: 0x0600048E RID: 1166 RVA: 0x00015FF3 File Offset: 0x000141F3
	public static void playDefaultInventorySound()
	{
		AudioControl.playSound("Inventory1");
	}

	// Token: 0x0600048F RID: 1167 RVA: 0x00015FFF File Offset: 0x000141FF
	public static void playMusic(string path)
	{
		AudioControl.musicPlayer.playSound(path);
	}

	// Token: 0x06000490 RID: 1168 RVA: 0x0001600C File Offset: 0x0001420C
	public static void playBumpSound()
	{
		AudioControl.playRandomSound(AudioControl.bumpSounds);
	}

	// Token: 0x06000491 RID: 1169 RVA: 0x00016018 File Offset: 0x00014218
	public static void playStartOfCombatSound()
	{
		AudioControl.playSound("Fanfare4");
	}

	// Token: 0x06000492 RID: 1170 RVA: 0x00016024 File Offset: 0x00014224
	public static void playButtonClick()
	{
		AudioControl.playSound("Click3");
	}

	// Token: 0x06000493 RID: 1171 RVA: 0x00016030 File Offset: 0x00014230
	public static void playUnlockSound()
	{
		AudioControl.playSound("LockUnlock1");
	}

	// Token: 0x06000494 RID: 1172 RVA: 0x0001603C File Offset: 0x0001423C
	public static void playLockpickSound()
	{
		AudioControl.playSound("Lockpick1");
	}

	// Token: 0x06000495 RID: 1173 RVA: 0x00016048 File Offset: 0x00014248
	public static void playQuillSound()
	{
		AudioControl.playRandomSound(AudioControl.quillSounds);
	}

	// Token: 0x06000496 RID: 1174 RVA: 0x00016054 File Offset: 0x00014254
	public static void playRandomSound(List<string> paths)
	{
		AudioControl.soundFXPlayer.playRandomSound(paths);
	}

	// Token: 0x06000497 RID: 1175 RVA: 0x00016061 File Offset: 0x00014261
	public static void playSound(string path)
	{
		AudioControl.soundFXPlayer.playSound(path);
	}

	// Token: 0x06000498 RID: 1176 RVA: 0x0001606E File Offset: 0x0001426E
	public static void stopMusic()
	{
		AudioControl.musicPlayer.stopMusic();
	}

	// Token: 0x06000499 RID: 1177 RVA: 0x0001607A File Offset: 0x0001427A
	public static void update()
	{
		AudioControl.soundFXPlayer.update();
		AudioControl.musicPlayer.update();
	}

	// Token: 0x040000F6 RID: 246
	private static List<string> bumpSounds = new List<string>
	{
		"Bump1",
		"Bump2"
	};

	// Token: 0x040000F7 RID: 247
	private static List<string> quillSounds = new List<string>
	{
		"Quill1",
		"Quill2",
		"Quill3"
	};

	// Token: 0x040000F8 RID: 248
	private static AudioControl.SoundFXPlayer _soundFXPlayer;

	// Token: 0x040000F9 RID: 249
	private static AudioControl.MusicPlayer _musicPlayer;

	// Token: 0x040000FA RID: 250
	private static AudioControl.MoveSoundPlayer _moveSoundPlayer;

	// Token: 0x020001D3 RID: 467
	private abstract class AudioPlayer
	{
		// Token: 0x060016C9 RID: 5833 RVA: 0x00066267 File Offset: 0x00064467
		protected AudioPlayer(float startingVolumeLevel)
		{
			this.volumeControl = new AudioControl.VolumeControl(startingVolumeLevel);
			this.initialize();
			this.validateAudioSources();
		}

		// Token: 0x060016CA RID: 5834
		protected abstract void initialize();

		// Token: 0x060016CB RID: 5835 RVA: 0x00066294 File Offset: 0x00064494
		private void validateAudioSources()
		{
			if (this.audioSources.Count == 0)
			{
				MainControl.logError("Failed to initialize Audio Control!");
				return;
			}
			MainControl.log("Initialized Audio Control with " + this.getAudioSources().Count.ToString() + " sources.");
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x000662E0 File Offset: 0x000644E0
		public bool hasAudioSources()
		{
			return this.audioSources != null && this.audioSources.Count != 0;
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x000662FA File Offset: 0x000644FA
		protected List<AudioSource> getAudioSources()
		{
			return this.audioSources;
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x00066302 File Offset: 0x00064502
		protected void addAudioSource(AudioSource source)
		{
			this.audioSources.Add(source);
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x00066310 File Offset: 0x00064510
		protected AudioClip loadAudioClip(string path)
		{
			return SkaldFileIO.Load<AudioClip>(this.getBasePath() + path);
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x00066324 File Offset: 0x00064524
		public void setVolume(float value)
		{
			this.volumeControl.setVolume(value);
			foreach (AudioSource audioSource in this.getAudioSources())
			{
				audioSource.volume = this.getVolume();
			}
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x00066388 File Offset: 0x00064588
		public float getVolume()
		{
			return this.volumeControl.getVolume();
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x00066395 File Offset: 0x00064595
		public float getBaseVolume()
		{
			return this.volumeControl.getCurrentVolume();
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x000663A4 File Offset: 0x000645A4
		public void adjustVolume(int i)
		{
			this.volumeControl.adjustVolume(i);
			foreach (AudioSource audioSource in this.getAudioSources())
			{
				audioSource.volume = this.getVolume();
			}
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x00066408 File Offset: 0x00064608
		public string printVolume()
		{
			return this.volumeControl.printVolumeString();
		}

		// Token: 0x060016D5 RID: 5845
		protected abstract string getBasePath();

		// Token: 0x060016D6 RID: 5846
		public abstract void update();

		// Token: 0x060016D7 RID: 5847
		public abstract void playSound(string path);

		// Token: 0x060016D8 RID: 5848
		protected abstract AudioSource getAvailableAudioSource();

		// Token: 0x0400073B RID: 1851
		private List<AudioSource> audioSources = new List<AudioSource>();

		// Token: 0x0400073C RID: 1852
		private AudioControl.VolumeControl volumeControl;
	}

	// Token: 0x020001D4 RID: 468
	private class MusicPlayer : AudioControl.AudioPlayer
	{
		// Token: 0x060016D9 RID: 5849 RVA: 0x00066415 File Offset: 0x00064615
		public MusicPlayer() : base(0.4f)
		{
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x00066430 File Offset: 0x00064630
		protected override void initialize()
		{
			foreach (AudioSource audioSource in Object.FindObjectsOfType<AudioSource>())
			{
				if (audioSource.name == "MusicAudio1")
				{
					base.addAudioSource(audioSource);
					return;
				}
			}
		}

		// Token: 0x060016DB RID: 5851 RVA: 0x0006646F File Offset: 0x0006466F
		protected override string getBasePath()
		{
			return "Sounds/Music/";
		}

		// Token: 0x060016DC RID: 5852 RVA: 0x00066476 File Offset: 0x00064676
		public override void update()
		{
			if (this.fade)
			{
				this.fadeOut();
			}
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x00066486 File Offset: 0x00064686
		public void stopMusic()
		{
			this.fade = true;
			this.currentPath = "";
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x0006649C File Offset: 0x0006469C
		private void fadeOut()
		{
			foreach (AudioSource audioSource in base.getAudioSources())
			{
				if (audioSource.isPlaying && audioSource.volume > 0f)
				{
					audioSource.volume -= base.getVolume() / 60f;
				}
				else
				{
					audioSource.Stop();
					this.currentPath = "";
					this.fade = false;
				}
			}
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x00066530 File Offset: 0x00064730
		public override void playSound(string path)
		{
			if (!MainControl.allowAudio())
			{
				return;
			}
			if (path == "")
			{
				return;
			}
			if (path == this.currentPath)
			{
				return;
			}
			this.currentPath = path;
			if (this.fade)
			{
				this.fade = false;
			}
			AudioClip audioClip = base.loadAudioClip(path);
			AudioSource availableAudioSource = this.getAvailableAudioSource();
			if (availableAudioSource == null || audioClip == null)
			{
				return;
			}
			availableAudioSource.clip = audioClip;
			availableAudioSource.volume = base.getVolume();
			availableAudioSource.Play();
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x000665B3 File Offset: 0x000647B3
		protected override AudioSource getAvailableAudioSource()
		{
			if (!base.hasAudioSources())
			{
				return null;
			}
			return base.getAudioSources()[0];
		}

		// Token: 0x0400073D RID: 1853
		private string currentPath = "";

		// Token: 0x0400073E RID: 1854
		private bool fade;
	}

	// Token: 0x020001D5 RID: 469
	private class SoundFXPlayer : AudioControl.AudioPlayer
	{
		// Token: 0x060016E1 RID: 5857 RVA: 0x000665CB File Offset: 0x000647CB
		public SoundFXPlayer() : base(0.2f)
		{
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x000665D8 File Offset: 0x000647D8
		protected override void initialize()
		{
			foreach (AudioSource audioSource in Object.FindObjectsOfType<AudioSource>())
			{
				if (audioSource.name.Contains("SoundAudio"))
				{
					base.addAudioSource(audioSource);
				}
			}
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x00066616 File Offset: 0x00064816
		protected override string getBasePath()
		{
			return "Sounds/SoundFX/";
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x0006661D File Offset: 0x0006481D
		public override void update()
		{
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x00066620 File Offset: 0x00064820
		protected override AudioSource getAvailableAudioSource()
		{
			if (!base.hasAudioSources())
			{
				return null;
			}
			foreach (AudioSource audioSource in base.getAudioSources())
			{
				if (!audioSource.isPlaying)
				{
					return audioSource;
				}
			}
			return null;
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x00066688 File Offset: 0x00064888
		public void playRandomSound(List<string> paths)
		{
			if (paths == null || paths.Count == 0)
			{
				return;
			}
			this.playSound(paths[Random.Range(0, paths.Count)]);
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x000666B0 File Offset: 0x000648B0
		public override void playSound(string path)
		{
			if (!MainControl.allowAudio())
			{
				return;
			}
			if (path == "")
			{
				return;
			}
			AudioClip audioClip = base.loadAudioClip(path);
			AudioSource availableAudioSource = this.getAvailableAudioSource();
			if (audioClip == null)
			{
				MainControl.logError("Could not find audio clip: " + path);
				return;
			}
			if (availableAudioSource == null)
			{
				MainControl.logWarning("Ran out of AudioSources for: " + path);
				return;
			}
			availableAudioSource.volume = base.getVolume();
			availableAudioSource.PlayOneShot(audioClip);
		}
	}

	// Token: 0x020001D6 RID: 470
	private class MoveSoundPlayer : AudioControl.SoundFXPlayer
	{
		// Token: 0x060016E8 RID: 5864 RVA: 0x0006672C File Offset: 0x0006492C
		protected override void initialize()
		{
			foreach (AudioSource audioSource in Object.FindObjectsOfType<AudioSource>())
			{
				if (audioSource.name.Contains("SoundMove"))
				{
					base.addAudioSource(audioSource);
				}
			}
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x0006676A File Offset: 0x0006496A
		private void playAlternatingSound(string path1, string path2)
		{
			if (this.stepAlternator)
			{
				this.playSound(path1);
			}
			else
			{
				this.playSound(path2);
			}
			this.stepAlternator = !this.stepAlternator;
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x00066793 File Offset: 0x00064993
		public void playMoveSound()
		{
			this.playAlternatingSound("MoveNormal1", "MoveNormal2");
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x000667A5 File Offset: 0x000649A5
		public void playMoveSoundWater()
		{
			this.playAlternatingSound("MoveWater1", "MoveWater2");
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x000667B7 File Offset: 0x000649B7
		public void playMoveSoundVegetation()
		{
			this.playAlternatingSound("MoveVegetation1", "MoveVegetation2");
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x000667C9 File Offset: 0x000649C9
		public void playMoveSoundStealth()
		{
			this.playAlternatingSound("MoveStealth1", "MoveStealth2");
		}

		// Token: 0x0400073F RID: 1855
		private bool stepAlternator = true;
	}

	// Token: 0x020001D7 RID: 471
	private class VolumeControl
	{
		// Token: 0x060016EF RID: 5871 RVA: 0x000667EA File Offset: 0x000649EA
		public VolumeControl(float baseVolume)
		{
			this.baseVolume = baseVolume;
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x0006680F File Offset: 0x00064A0F
		public float getVolume()
		{
			return this.baseVolume * this.currentVolume;
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x0006681E File Offset: 0x00064A1E
		public float getCurrentVolume()
		{
			return this.currentVolume;
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x00066828 File Offset: 0x00064A28
		public void adjustVolume(int i)
		{
			if (i < 0)
			{
				this.currentVolume -= 0.2f;
			}
			else
			{
				this.currentVolume += 0.2f;
			}
			if (this.currentVolume < 0f)
			{
				this.currentVolume = 0f;
			}
			else if (this.currentVolume > 1f)
			{
				this.currentVolume = 1f;
			}
			AudioControl.playButtonClick();
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x00066896 File Offset: 0x00064A96
		public void setVolume(float target)
		{
			this.currentVolume = target;
			if (this.currentVolume < 0f)
			{
				this.currentVolume = 0f;
				return;
			}
			if (this.currentVolume > 1f)
			{
				this.currentVolume = 1f;
			}
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x000668D0 File Offset: 0x00064AD0
		public string printVolumeString()
		{
			int num = Mathf.RoundToInt(this.currentVolume * 100f);
			if (num <= 0)
			{
				return "Muted";
			}
			return num.ToString() + "%";
		}

		// Token: 0x04000740 RID: 1856
		private float baseVolume = 0.4f;

		// Token: 0x04000741 RID: 1857
		private float currentVolume = 0.8f;
	}
}
