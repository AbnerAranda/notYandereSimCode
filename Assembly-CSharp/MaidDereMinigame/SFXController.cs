using System;
using MaidDereMinigame.Malee;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x02000507 RID: 1287
	public class SFXController : MonoBehaviour
	{
		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06002043 RID: 8259 RVA: 0x0018A66C File Offset: 0x0018886C
		public static SFXController Instance
		{
			get
			{
				if (SFXController.instance == null)
				{
					SFXController.instance = UnityEngine.Object.FindObjectOfType<SFXController>();
				}
				return SFXController.instance;
			}
		}

		// Token: 0x06002044 RID: 8260 RVA: 0x0018A68A File Offset: 0x0018888A
		private void Awake()
		{
			if (SFXController.Instance != this)
			{
				UnityEngine.Object.Destroy(base.gameObject);
				return;
			}
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		// Token: 0x06002045 RID: 8261 RVA: 0x0018A6B0 File Offset: 0x001888B0
		public static void PlaySound(SFXController.Sounds sound)
		{
			SoundEmitter emitter = SFXController.Instance.GetEmitter(sound);
			AudioSource source = emitter.GetSource();
			if (!source.isPlaying || emitter.interupt)
			{
				source.clip = SFXController.Instance.GetRandomClip(emitter);
				source.Play();
			}
		}

		// Token: 0x06002046 RID: 8262 RVA: 0x0018A6F8 File Offset: 0x001888F8
		private SoundEmitter GetEmitter(SFXController.Sounds sound)
		{
			foreach (SoundEmitter soundEmitter in this.emitters)
			{
				if (soundEmitter.sound == sound)
				{
					return soundEmitter;
				}
			}
			Debug.Log(string.Format("There is no sound emitter created for {0}", sound), this);
			return null;
		}

		// Token: 0x06002047 RID: 8263 RVA: 0x0018A764 File Offset: 0x00188964
		private AudioClip GetRandomClip(SoundEmitter emitter)
		{
			int index = UnityEngine.Random.Range(0, emitter.clips.Count);
			return emitter.clips[index];
		}

		// Token: 0x04003E76 RID: 15990
		private static SFXController instance;

		// Token: 0x04003E77 RID: 15991
		[Reorderable]
		public SoundEmitters emitters;

		// Token: 0x0200071B RID: 1819
		public enum Sounds
		{
			// Token: 0x0400496A RID: 18794
			Countdown,
			// Token: 0x0400496B RID: 18795
			MenuBack,
			// Token: 0x0400496C RID: 18796
			MenuConfirm,
			// Token: 0x0400496D RID: 18797
			ClockTick,
			// Token: 0x0400496E RID: 18798
			DoorBell,
			// Token: 0x0400496F RID: 18799
			GameFail,
			// Token: 0x04004970 RID: 18800
			GameSuccess,
			// Token: 0x04004971 RID: 18801
			Plate,
			// Token: 0x04004972 RID: 18802
			PageTurn,
			// Token: 0x04004973 RID: 18803
			MenuSelect,
			// Token: 0x04004974 RID: 18804
			MaleCustomerGreet,
			// Token: 0x04004975 RID: 18805
			MaleCustomerThank,
			// Token: 0x04004976 RID: 18806
			MaleCustomerLeave,
			// Token: 0x04004977 RID: 18807
			FemaleCustomerGreet,
			// Token: 0x04004978 RID: 18808
			FemaleCustomerThank,
			// Token: 0x04004979 RID: 18809
			FemaleCustomerLeave,
			// Token: 0x0400497A RID: 18810
			MenuOpen
		}
	}
}
