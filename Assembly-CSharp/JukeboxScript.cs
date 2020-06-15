using System;
using UnityEngine;

// Token: 0x02000316 RID: 790
public class JukeboxScript : MonoBehaviour
{
	// Token: 0x060017DF RID: 6111 RVA: 0x000D2218 File Offset: 0x000D0418
	public void Start()
	{
		if (this.BGM == 0)
		{
			this.BGM = UnityEngine.Random.Range(0, 8);
		}
		else
		{
			this.BGM++;
			if (this.BGM > 8)
			{
				this.BGM = 1;
			}
		}
		if (this.BGM == 1)
		{
			this.FullSanities = this.OriginalFull;
			this.HalfSanities = this.OriginalHalf;
			this.NoSanities = this.OriginalNo;
		}
		else if (this.BGM == 2)
		{
			this.FullSanities = this.AlternateFull;
			this.HalfSanities = this.AlternateHalf;
			this.NoSanities = this.AlternateNo;
		}
		else if (this.BGM == 3)
		{
			this.FullSanities = this.ThirdFull;
			this.HalfSanities = this.ThirdHalf;
			this.NoSanities = this.ThirdNo;
		}
		else if (this.BGM == 4)
		{
			this.FullSanities = this.FourthFull;
			this.HalfSanities = this.FourthHalf;
			this.NoSanities = this.FourthNo;
		}
		else if (this.BGM == 5)
		{
			this.FullSanities = this.FifthFull;
			this.HalfSanities = this.FifthHalf;
			this.NoSanities = this.FifthNo;
		}
		else if (this.BGM == 6)
		{
			this.FullSanities = this.SixthFull;
			this.HalfSanities = this.SixthHalf;
			this.NoSanities = this.SixthNo;
		}
		else if (this.BGM == 7)
		{
			this.FullSanities = this.SeventhFull;
			this.HalfSanities = this.SeventhHalf;
			this.NoSanities = this.SeventhNo;
		}
		else if (this.BGM == 8)
		{
			this.FullSanities = this.EighthFull;
			this.HalfSanities = this.EighthHalf;
			this.NoSanities = this.EighthNo;
		}
		if (!SchoolGlobals.SchoolAtmosphereSet)
		{
			SchoolGlobals.SchoolAtmosphereSet = true;
			SchoolGlobals.SchoolAtmosphere = 1f;
		}
		int num;
		if (SchoolAtmosphere.Type == SchoolAtmosphereType.High)
		{
			num = 3;
		}
		else if (SchoolAtmosphere.Type == SchoolAtmosphereType.Medium)
		{
			num = 2;
		}
		else
		{
			num = 1;
		}
		this.FullSanity.clip = this.FullSanities[num];
		this.HalfSanity.clip = this.HalfSanities[num];
		this.NoSanity.clip = this.NoSanities[num];
		this.Volume = 0.25f;
		this.FullSanity.volume = 0f;
		this.Hitman.time = 26f;
	}

	// Token: 0x060017E0 RID: 6112 RVA: 0x000D2478 File Offset: 0x000D0678
	private void Update()
	{
		if (!this.Yandere.PauseScreen.Show && !this.Yandere.EasterEggMenu.activeInHierarchy && Input.GetKeyDown(KeyCode.M))
		{
			this.StartStopMusic();
		}
		if (!this.Egg)
		{
			if (!this.Yandere.Police.Clock.SchoolBell.isPlaying && !this.Yandere.StudentManager.MemorialScene.enabled)
			{
				if (!this.StartMusic)
				{
					this.FullSanity.Play();
					this.HalfSanity.Play();
					this.NoSanity.Play();
					this.StartMusic = true;
				}
				if (this.Yandere.Sanity >= 66.6666641f)
				{
					this.FullSanity.volume = Mathf.MoveTowards(this.FullSanity.volume, this.Volume * this.Dip - this.ClubDip, 0.0166666675f * this.FadeSpeed);
					this.HalfSanity.volume = Mathf.MoveTowards(this.HalfSanity.volume, 0f, 0.0166666675f * this.FadeSpeed);
					this.NoSanity.volume = Mathf.MoveTowards(this.NoSanity.volume, 0f, 0.0166666675f * this.FadeSpeed);
				}
				else if (this.Yandere.Sanity >= 33.3333321f)
				{
					this.FullSanity.volume = Mathf.MoveTowards(this.FullSanity.volume, 0f, 0.0166666675f * this.FadeSpeed);
					this.HalfSanity.volume = Mathf.MoveTowards(this.HalfSanity.volume, this.Volume * this.Dip - this.ClubDip, 0.0166666675f * this.FadeSpeed);
					this.NoSanity.volume = Mathf.MoveTowards(this.NoSanity.volume, 0f, 0.0166666675f * this.FadeSpeed);
				}
				else
				{
					this.FullSanity.volume = Mathf.MoveTowards(this.FullSanity.volume, 0f, 0.0166666675f * this.FadeSpeed);
					this.HalfSanity.volume = Mathf.MoveTowards(this.HalfSanity.volume, 0f, 0.0166666675f * this.FadeSpeed);
					this.NoSanity.volume = Mathf.MoveTowards(this.NoSanity.volume, this.Volume * this.Dip - this.ClubDip, 0.0166666675f * this.FadeSpeed);
				}
			}
		}
		else
		{
			this.AttackOnTitan.volume = Mathf.MoveTowards(this.AttackOnTitan.volume, this.Volume * this.Dip, 0.166666672f);
			this.Megalovania.volume = Mathf.MoveTowards(this.Megalovania.volume, this.Volume * this.Dip, 0.166666672f);
			this.MissionMode.volume = Mathf.MoveTowards(this.MissionMode.volume, this.Volume * this.Dip, 0.166666672f);
			this.Skeletons.volume = Mathf.MoveTowards(this.Skeletons.volume, this.Volume * this.Dip, 0.166666672f);
			this.Vaporwave.volume = Mathf.MoveTowards(this.Vaporwave.volume, this.Volume * this.Dip, 0.166666672f);
			this.AzurLane.volume = Mathf.MoveTowards(this.AzurLane.volume, this.Volume * this.Dip, 0.166666672f);
			this.LifeNote.volume = Mathf.MoveTowards(this.LifeNote.volume, this.Volume * this.Dip, 0.166666672f);
			this.Berserk.volume = Mathf.MoveTowards(this.Berserk.volume, this.Volume * this.Dip, 0.166666672f);
			this.Metroid.volume = Mathf.MoveTowards(this.Metroid.volume, this.Volume * this.Dip, 0.166666672f);
			this.Nuclear.volume = Mathf.MoveTowards(this.Nuclear.volume, this.Volume * this.Dip, 0.166666672f);
			this.Slender.volume = Mathf.MoveTowards(this.Slender.volume, this.Volume * this.Dip, 0.166666672f);
			this.Sukeban.volume = Mathf.MoveTowards(this.Sukeban.volume, this.Volume * this.Dip, 0.166666672f);
			this.Custom.volume = Mathf.MoveTowards(this.Custom.volume, this.Volume * this.Dip, 0.166666672f);
			this.Hatred.volume = Mathf.MoveTowards(this.Hatred.volume, this.Volume * this.Dip, 0.166666672f);
			this.Hitman.volume = Mathf.MoveTowards(this.Hitman.volume, this.Volume * this.Dip, 0.166666672f);
			this.Touhou.volume = Mathf.MoveTowards(this.Touhou.volume, this.Volume * this.Dip, 0.166666672f);
			this.Falcon.volume = Mathf.MoveTowards(this.Falcon.volume, this.Volume * this.Dip, 0.166666672f);
			this.Miyuki.volume = Mathf.MoveTowards(this.Miyuki.volume, this.Volume * this.Dip, 0.166666672f);
			this.Demon.volume = Mathf.MoveTowards(this.Demon.volume, this.Volume * this.Dip, 0.166666672f);
			this.Ebola.volume = Mathf.MoveTowards(this.Ebola.volume, this.Volume * this.Dip, 0.166666672f);
			this.Ninja.volume = Mathf.MoveTowards(this.Ninja.volume, this.Volume * this.Dip, 0.166666672f);
			this.Punch.volume = Mathf.MoveTowards(this.Punch.volume, this.Volume * this.Dip, 0.166666672f);
			this.Galo.volume = Mathf.MoveTowards(this.Galo.volume, this.Volume * this.Dip, 0.166666672f);
			this.Jojo.volume = Mathf.MoveTowards(this.Jojo.volume, this.Volume * this.Dip, 0.166666672f);
			this.Lied.volume = Mathf.MoveTowards(this.Lied.volume, this.Volume * this.Dip, 0.166666672f);
			this.Nier.volume = Mathf.MoveTowards(this.Nier.volume, this.Volume * this.Dip, 0.166666672f);
			this.Sith.volume = Mathf.MoveTowards(this.Sith.volume, this.Volume * this.Dip, 0.166666672f);
			this.DK.volume = Mathf.MoveTowards(this.DK.volume, this.Volume * this.Dip, 0.166666672f);
			this.Horror.volume = Mathf.MoveTowards(this.Horror.volume, this.Volume * this.Dip, 0.166666672f);
		}
		if (!this.Yandere.PauseScreen.Show && !this.Yandere.Noticed && this.Yandere.CanMove && this.Yandere.EasterEggMenu.activeInHierarchy && !this.Egg)
		{
			if (Input.GetKeyDown(KeyCode.T) || Input.GetKeyDown(KeyCode.Alpha4))
			{
				this.Egg = true;
				this.KillVolume();
				this.AttackOnTitan.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.P))
			{
				this.Egg = true;
				this.KillVolume();
				this.Nuclear.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.H))
			{
				this.Egg = true;
				this.KillVolume();
				this.Hatred.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.B))
			{
				this.Egg = true;
				this.KillVolume();
				this.Sukeban.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Z))
			{
				this.Egg = true;
				this.KillVolume();
				this.Slender.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.G))
			{
				this.Egg = true;
				this.KillVolume();
				this.Galo.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.L))
			{
				this.Egg = true;
				this.KillVolume();
				this.Hitman.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.S))
			{
				this.Egg = true;
				this.KillVolume();
				this.Skeletons.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.K))
			{
				this.Egg = true;
				this.KillVolume();
				this.DK.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.C))
			{
				this.Egg = true;
				this.KillVolume();
				this.Touhou.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.F))
			{
				this.Egg = true;
				this.KillVolume();
				this.Falcon.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.O))
			{
				this.Egg = true;
				this.KillVolume();
				this.Punch.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.U))
			{
				this.Egg = true;
				this.KillVolume();
				this.Megalovania.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.Q))
			{
				this.Egg = true;
				this.KillVolume();
				this.Metroid.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.Y))
			{
				this.Egg = true;
				this.KillVolume();
				this.Ninja.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.F5) || Input.GetKeyDown(KeyCode.W))
			{
				this.Egg = true;
				this.KillVolume();
				this.Ebola.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.Alpha6))
			{
				this.Egg = true;
				this.KillVolume();
				this.Demon.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.D))
			{
				this.Egg = true;
				this.KillVolume();
				this.Sith.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.F2))
			{
				this.Egg = true;
				this.KillVolume();
				this.Horror.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.F3))
			{
				this.Egg = true;
				this.KillVolume();
				this.LifeNote.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.F6) || Input.GetKeyDown(KeyCode.F9))
			{
				this.Egg = true;
				this.KillVolume();
				this.Lied.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.F7))
			{
				this.Egg = true;
				this.KillVolume();
				this.Berserk.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.F8))
			{
				this.Egg = true;
				this.KillVolume();
				this.Nier.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.V))
			{
				this.Egg = true;
				this.KillVolume();
				this.Vaporwave.enabled = true;
			}
		}
	}

	// Token: 0x060017E1 RID: 6113 RVA: 0x000D303C File Offset: 0x000D123C
	public void StartStopMusic()
	{
		if (this.Custom.isPlaying)
		{
			this.Egg = false;
			this.Custom.Stop();
			this.FadeSpeed = 1f;
			this.StartMusic = false;
			this.Volume = this.LastVolume;
			this.Start();
			return;
		}
		if (this.Volume == 0f)
		{
			this.FadeSpeed = 1f;
			this.StartMusic = false;
			this.Volume = this.LastVolume;
			this.Start();
			return;
		}
		this.LastVolume = this.Volume;
		this.FadeSpeed = 10f;
		this.Volume = 0f;
	}

	// Token: 0x060017E2 RID: 6114 RVA: 0x000D30E1 File Offset: 0x000D12E1
	public void Shipgirl()
	{
		this.Egg = true;
		this.KillVolume();
		this.AzurLane.enabled = true;
	}

	// Token: 0x060017E3 RID: 6115 RVA: 0x000D30FC File Offset: 0x000D12FC
	public void MiyukiMusic()
	{
		this.Egg = true;
		this.KillVolume();
		this.Miyuki.enabled = true;
	}

	// Token: 0x060017E4 RID: 6116 RVA: 0x000D3117 File Offset: 0x000D1317
	public void KillVolume()
	{
		this.FullSanity.volume = 0f;
		this.HalfSanity.volume = 0f;
		this.NoSanity.volume = 0f;
		this.Volume = 0.5f;
	}

	// Token: 0x060017E5 RID: 6117 RVA: 0x000D3154 File Offset: 0x000D1354
	public void GameOver()
	{
		this.AttackOnTitan.Stop();
		this.Megalovania.Stop();
		this.MissionMode.Stop();
		this.Skeletons.Stop();
		this.Vaporwave.Stop();
		this.AzurLane.Stop();
		this.LifeNote.Stop();
		this.Berserk.Stop();
		this.Metroid.Stop();
		this.Nuclear.Stop();
		this.Sukeban.Stop();
		this.Custom.Stop();
		this.Slender.Stop();
		this.Hatred.Stop();
		this.Hitman.Stop();
		this.Horror.Stop();
		this.Touhou.Stop();
		this.Falcon.Stop();
		this.Miyuki.Stop();
		this.Ebola.Stop();
		this.Punch.Stop();
		this.Ninja.Stop();
		this.Jojo.Stop();
		this.Galo.Stop();
		this.Lied.Stop();
		this.Nier.Stop();
		this.Sith.Stop();
		this.DK.Stop();
		this.Confession.Stop();
		this.FullSanity.Stop();
		this.HalfSanity.Stop();
		this.NoSanity.Stop();
	}

	// Token: 0x060017E6 RID: 6118 RVA: 0x000D32C1 File Offset: 0x000D14C1
	public void PlayJojo()
	{
		this.Egg = true;
		this.KillVolume();
		this.Jojo.enabled = true;
	}

	// Token: 0x060017E7 RID: 6119 RVA: 0x000D32DC File Offset: 0x000D14DC
	public void PlayCustom()
	{
		this.Egg = true;
		this.KillVolume();
		this.Custom.enabled = true;
		this.Custom.Play();
	}

	// Token: 0x040021FE RID: 8702
	public YandereScript Yandere;

	// Token: 0x040021FF RID: 8703
	public AudioSource SFX;

	// Token: 0x04002200 RID: 8704
	public AudioSource AttackOnTitan;

	// Token: 0x04002201 RID: 8705
	public AudioSource Megalovania;

	// Token: 0x04002202 RID: 8706
	public AudioSource MissionMode;

	// Token: 0x04002203 RID: 8707
	public AudioSource Skeletons;

	// Token: 0x04002204 RID: 8708
	public AudioSource Vaporwave;

	// Token: 0x04002205 RID: 8709
	public AudioSource AzurLane;

	// Token: 0x04002206 RID: 8710
	public AudioSource LifeNote;

	// Token: 0x04002207 RID: 8711
	public AudioSource Berserk;

	// Token: 0x04002208 RID: 8712
	public AudioSource Metroid;

	// Token: 0x04002209 RID: 8713
	public AudioSource Nuclear;

	// Token: 0x0400220A RID: 8714
	public AudioSource Slender;

	// Token: 0x0400220B RID: 8715
	public AudioSource Sukeban;

	// Token: 0x0400220C RID: 8716
	public AudioSource Custom;

	// Token: 0x0400220D RID: 8717
	public AudioSource Hatred;

	// Token: 0x0400220E RID: 8718
	public AudioSource Hitman;

	// Token: 0x0400220F RID: 8719
	public AudioSource Horror;

	// Token: 0x04002210 RID: 8720
	public AudioSource Touhou;

	// Token: 0x04002211 RID: 8721
	public AudioSource Falcon;

	// Token: 0x04002212 RID: 8722
	public AudioSource Miyuki;

	// Token: 0x04002213 RID: 8723
	public AudioSource Ebola;

	// Token: 0x04002214 RID: 8724
	public AudioSource Demon;

	// Token: 0x04002215 RID: 8725
	public AudioSource Ninja;

	// Token: 0x04002216 RID: 8726
	public AudioSource Punch;

	// Token: 0x04002217 RID: 8727
	public AudioSource Galo;

	// Token: 0x04002218 RID: 8728
	public AudioSource Jojo;

	// Token: 0x04002219 RID: 8729
	public AudioSource Lied;

	// Token: 0x0400221A RID: 8730
	public AudioSource Nier;

	// Token: 0x0400221B RID: 8731
	public AudioSource Sith;

	// Token: 0x0400221C RID: 8732
	public AudioSource DK;

	// Token: 0x0400221D RID: 8733
	public AudioSource Confession;

	// Token: 0x0400221E RID: 8734
	public AudioSource FullSanity;

	// Token: 0x0400221F RID: 8735
	public AudioSource HalfSanity;

	// Token: 0x04002220 RID: 8736
	public AudioSource NoSanity;

	// Token: 0x04002221 RID: 8737
	public AudioSource Chase;

	// Token: 0x04002222 RID: 8738
	public float LastVolume;

	// Token: 0x04002223 RID: 8739
	public float FadeSpeed;

	// Token: 0x04002224 RID: 8740
	public float ClubDip;

	// Token: 0x04002225 RID: 8741
	public float Volume;

	// Token: 0x04002226 RID: 8742
	public int Track;

	// Token: 0x04002227 RID: 8743
	public int BGM;

	// Token: 0x04002228 RID: 8744
	public float Dip = 1f;

	// Token: 0x04002229 RID: 8745
	public bool StartMusic;

	// Token: 0x0400222A RID: 8746
	public bool Egg;

	// Token: 0x0400222B RID: 8747
	public AudioClip[] FullSanities;

	// Token: 0x0400222C RID: 8748
	public AudioClip[] HalfSanities;

	// Token: 0x0400222D RID: 8749
	public AudioClip[] NoSanities;

	// Token: 0x0400222E RID: 8750
	public AudioClip[] OriginalFull;

	// Token: 0x0400222F RID: 8751
	public AudioClip[] OriginalHalf;

	// Token: 0x04002230 RID: 8752
	public AudioClip[] OriginalNo;

	// Token: 0x04002231 RID: 8753
	public AudioClip[] AlternateFull;

	// Token: 0x04002232 RID: 8754
	public AudioClip[] AlternateHalf;

	// Token: 0x04002233 RID: 8755
	public AudioClip[] AlternateNo;

	// Token: 0x04002234 RID: 8756
	public AudioClip[] ThirdFull;

	// Token: 0x04002235 RID: 8757
	public AudioClip[] ThirdHalf;

	// Token: 0x04002236 RID: 8758
	public AudioClip[] ThirdNo;

	// Token: 0x04002237 RID: 8759
	public AudioClip[] FourthFull;

	// Token: 0x04002238 RID: 8760
	public AudioClip[] FourthHalf;

	// Token: 0x04002239 RID: 8761
	public AudioClip[] FourthNo;

	// Token: 0x0400223A RID: 8762
	public AudioClip[] FifthFull;

	// Token: 0x0400223B RID: 8763
	public AudioClip[] FifthHalf;

	// Token: 0x0400223C RID: 8764
	public AudioClip[] FifthNo;

	// Token: 0x0400223D RID: 8765
	public AudioClip[] SixthFull;

	// Token: 0x0400223E RID: 8766
	public AudioClip[] SixthHalf;

	// Token: 0x0400223F RID: 8767
	public AudioClip[] SixthNo;

	// Token: 0x04002240 RID: 8768
	public AudioClip[] SeventhFull;

	// Token: 0x04002241 RID: 8769
	public AudioClip[] SeventhHalf;

	// Token: 0x04002242 RID: 8770
	public AudioClip[] SeventhNo;

	// Token: 0x04002243 RID: 8771
	public AudioClip[] EighthFull;

	// Token: 0x04002244 RID: 8772
	public AudioClip[] EighthHalf;

	// Token: 0x04002245 RID: 8773
	public AudioClip[] EighthNo;
}
