using System;
using UnityEngine;

// Token: 0x020003E3 RID: 995
public class SkinnedMeshUpdater : MonoBehaviour
{
	// Token: 0x06001AB7 RID: 6839 RVA: 0x0010B58D File Offset: 0x0010978D
	public void Start()
	{
		this.GlassesCheck();
	}

	// Token: 0x06001AB8 RID: 6840 RVA: 0x0010B598 File Offset: 0x00109798
	public void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.TransformEffect, this.Prompt.Yandere.Hips.position, Quaternion.identity);
			this.Prompt.Yandere.CharacterAnimation.Play(this.Prompt.Yandere.IdleAnim);
			this.Prompt.Yandere.CanMove = false;
			this.Prompt.Yandere.Egg = true;
			this.BreastR.name = "RightBreast";
			this.BreastL.name = "LeftBreast";
			this.Timer = 1f;
			this.ID++;
			if (this.ID == this.Characters.Length)
			{
				this.ID = 1;
			}
			this.Prompt.Yandere.Hairstyle = 120 + this.ID;
			this.Prompt.Yandere.UpdateHair();
			this.GlassesCheck();
			this.UpdateSkin();
		}
		if (this.Timer > 0f)
		{
			this.Timer = Mathf.MoveTowards(this.Timer, 0f, Time.deltaTime);
			if (this.Timer == 0f)
			{
				this.Prompt.Yandere.CanMove = true;
			}
		}
	}

	// Token: 0x06001AB9 RID: 6841 RVA: 0x0010B6FC File Offset: 0x001098FC
	public void UpdateSkin()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Characters[this.ID], Vector3.zero, Quaternion.identity);
		this.TempRenderer = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
		this.UpdateMeshRenderer(this.TempRenderer);
		UnityEngine.Object.Destroy(gameObject);
		this.MyRenderer.materials[0].mainTexture = this.Bodies[this.ID];
		this.MyRenderer.materials[1].mainTexture = this.Bodies[this.ID];
		this.MyRenderer.materials[2].mainTexture = this.Faces[this.ID];
	}

	// Token: 0x06001ABA RID: 6842 RVA: 0x0010B7A4 File Offset: 0x001099A4
	private void UpdateMeshRenderer(SkinnedMeshRenderer newMeshRenderer)
	{
		SkinnedMeshUpdater.<>c__DisplayClass16_0 CS$<>8__locals1 = new SkinnedMeshUpdater.<>c__DisplayClass16_0();
		CS$<>8__locals1.newMeshRenderer = newMeshRenderer;
		SkinnedMeshRenderer myRenderer = this.Prompt.Yandere.MyRenderer;
		myRenderer.sharedMesh = CS$<>8__locals1.newMeshRenderer.sharedMesh;
		Transform[] componentsInChildren = this.Prompt.Yandere.transform.GetComponentsInChildren<Transform>(true);
		Transform[] array = new Transform[CS$<>8__locals1.newMeshRenderer.bones.Length];
		int boneOrder;
		int boneOrder2;
		for (boneOrder = 0; boneOrder < CS$<>8__locals1.newMeshRenderer.bones.Length; boneOrder = boneOrder2 + 1)
		{
			array[boneOrder] = Array.Find<Transform>(componentsInChildren, (Transform c) => c.name == CS$<>8__locals1.newMeshRenderer.bones[boneOrder].name);
			boneOrder2 = boneOrder;
		}
		myRenderer.bones = array;
	}

	// Token: 0x06001ABB RID: 6843 RVA: 0x0010B878 File Offset: 0x00109A78
	private void GlassesCheck()
	{
		this.FumiGlasses.SetActive(false);
		this.NinaGlasses.SetActive(false);
		if (this.ID == 7)
		{
			this.FumiGlasses.SetActive(true);
			return;
		}
		if (this.ID == 8)
		{
			this.NinaGlasses.SetActive(true);
		}
	}

	// Token: 0x04002B08 RID: 11016
	public SkinnedMeshRenderer MyRenderer;

	// Token: 0x04002B09 RID: 11017
	public GameObject TransformEffect;

	// Token: 0x04002B0A RID: 11018
	public GameObject[] Characters;

	// Token: 0x04002B0B RID: 11019
	public PromptScript Prompt;

	// Token: 0x04002B0C RID: 11020
	public GameObject BreastR;

	// Token: 0x04002B0D RID: 11021
	public GameObject BreastL;

	// Token: 0x04002B0E RID: 11022
	public GameObject FumiGlasses;

	// Token: 0x04002B0F RID: 11023
	public GameObject NinaGlasses;

	// Token: 0x04002B10 RID: 11024
	private SkinnedMeshRenderer TempRenderer;

	// Token: 0x04002B11 RID: 11025
	public Texture[] Bodies;

	// Token: 0x04002B12 RID: 11026
	public Texture[] Faces;

	// Token: 0x04002B13 RID: 11027
	public float Timer;

	// Token: 0x04002B14 RID: 11028
	public int ID;
}
