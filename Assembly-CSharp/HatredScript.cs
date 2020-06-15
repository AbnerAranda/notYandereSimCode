using System;
using UnityEngine;

// Token: 0x020000CC RID: 204
public class HatredScript : MonoBehaviour
{
	// Token: 0x06000A0F RID: 2575 RVA: 0x00050392 File Offset: 0x0004E592
	private void Start()
	{
		this.Character.SetActive(false);
	}

	// Token: 0x04000A04 RID: 2564
	public DepthOfFieldScatter DepthOfField;

	// Token: 0x04000A05 RID: 2565
	public HomeDarknessScript HomeDarkness;

	// Token: 0x04000A06 RID: 2566
	public HomeCameraScript HomeCamera;

	// Token: 0x04000A07 RID: 2567
	public GrayscaleEffect Grayscale;

	// Token: 0x04000A08 RID: 2568
	public Bloom Bloom;

	// Token: 0x04000A09 RID: 2569
	public GameObject CrackPanel;

	// Token: 0x04000A0A RID: 2570
	public AudioSource Voiceover;

	// Token: 0x04000A0B RID: 2571
	public GameObject SenpaiPhoto;

	// Token: 0x04000A0C RID: 2572
	public GameObject RivalPhotos;

	// Token: 0x04000A0D RID: 2573
	public GameObject Character;

	// Token: 0x04000A0E RID: 2574
	public GameObject Panties;

	// Token: 0x04000A0F RID: 2575
	public GameObject Yandere;

	// Token: 0x04000A10 RID: 2576
	public GameObject Shrine;

	// Token: 0x04000A11 RID: 2577
	public Transform AntennaeR;

	// Token: 0x04000A12 RID: 2578
	public Transform AntennaeL;

	// Token: 0x04000A13 RID: 2579
	public Transform Corkboard;

	// Token: 0x04000A14 RID: 2580
	public UISprite CrackDarkness;

	// Token: 0x04000A15 RID: 2581
	public UISprite Darkness;

	// Token: 0x04000A16 RID: 2582
	public UITexture Crack;

	// Token: 0x04000A17 RID: 2583
	public UITexture Logo;

	// Token: 0x04000A18 RID: 2584
	public bool Begin;

	// Token: 0x04000A19 RID: 2585
	public float Timer;

	// Token: 0x04000A1A RID: 2586
	public int Phase;

	// Token: 0x04000A1B RID: 2587
	public Texture[] CrackTexture;
}
