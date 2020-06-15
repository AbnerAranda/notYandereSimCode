using System;
using UnityEngine;

// Token: 0x02000439 RID: 1081
public class UniformSetterScript : MonoBehaviour
{
	// Token: 0x06001C9B RID: 7323 RVA: 0x00158034 File Offset: 0x00156234
	public void Start()
	{
		if (this.MyRenderer == null)
		{
			this.MyRenderer = base.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>();
		}
		if (this.Male)
		{
			this.SetMaleUniform();
		}
		else
		{
			this.SetFemaleUniform();
		}
		if (this.AttachHair)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Hair[this.HairID], base.transform.position, base.transform.rotation);
			this.Head = base.transform.Find("Character/PelvisRoot/Hips/Spine/Spine1/Spine2/Spine3/Neck/Head").transform;
			gameObject.transform.parent = this.Head;
		}
	}

	// Token: 0x06001C9C RID: 7324 RVA: 0x001580E4 File Offset: 0x001562E4
	public void SetMaleUniform()
	{
		this.MyRenderer.sharedMesh = this.MaleUniforms[StudentGlobals.MaleUniform];
		if (StudentGlobals.MaleUniform == 1)
		{
			this.SkinID = 0;
			this.UniformID = 1;
			this.FaceID = 2;
		}
		else if (StudentGlobals.MaleUniform == 2 || StudentGlobals.MaleUniform == 3)
		{
			this.UniformID = 0;
			this.FaceID = 1;
			this.SkinID = 2;
		}
		else if (StudentGlobals.MaleUniform == 4 || StudentGlobals.MaleUniform == 5 || StudentGlobals.MaleUniform == 6)
		{
			this.FaceID = 0;
			this.SkinID = 1;
			this.UniformID = 2;
		}
		this.MyRenderer.materials[this.FaceID].mainTexture = this.SenpaiFace;
		this.MyRenderer.materials[this.SkinID].mainTexture = this.SenpaiSkin;
		this.MyRenderer.materials[this.UniformID].mainTexture = this.MaleUniformTextures[StudentGlobals.MaleUniform];
	}

	// Token: 0x06001C9D RID: 7325 RVA: 0x001581D8 File Offset: 0x001563D8
	public void SetFemaleUniform()
	{
		this.MyRenderer.sharedMesh = this.FemaleUniforms[StudentGlobals.FemaleUniform];
		this.MyRenderer.materials[0].mainTexture = this.FemaleUniformTextures[StudentGlobals.FemaleUniform];
		this.MyRenderer.materials[1].mainTexture = this.FemaleUniformTextures[StudentGlobals.FemaleUniform];
		if (this.StudentID == 0)
		{
			this.MyRenderer.materials[2].mainTexture = this.RyobaFace;
			return;
		}
		if (this.StudentID == 1)
		{
			this.MyRenderer.materials[2].mainTexture = this.AyanoFace;
			return;
		}
		this.MyRenderer.materials[2].mainTexture = this.OsanaFace;
	}

	// Token: 0x0400362E RID: 13870
	public Texture[] FemaleUniformTextures;

	// Token: 0x0400362F RID: 13871
	public Texture[] MaleUniformTextures;

	// Token: 0x04003630 RID: 13872
	public SkinnedMeshRenderer MyRenderer;

	// Token: 0x04003631 RID: 13873
	public Mesh[] FemaleUniforms;

	// Token: 0x04003632 RID: 13874
	public Mesh[] MaleUniforms;

	// Token: 0x04003633 RID: 13875
	public Texture SenpaiFace;

	// Token: 0x04003634 RID: 13876
	public Texture SenpaiSkin;

	// Token: 0x04003635 RID: 13877
	public Texture RyobaFace;

	// Token: 0x04003636 RID: 13878
	public Texture AyanoFace;

	// Token: 0x04003637 RID: 13879
	public Texture OsanaFace;

	// Token: 0x04003638 RID: 13880
	public int FaceID;

	// Token: 0x04003639 RID: 13881
	public int SkinID;

	// Token: 0x0400363A RID: 13882
	public int UniformID;

	// Token: 0x0400363B RID: 13883
	public int StudentID;

	// Token: 0x0400363C RID: 13884
	public bool AttachHair;

	// Token: 0x0400363D RID: 13885
	public bool Male;

	// Token: 0x0400363E RID: 13886
	public Transform Head;

	// Token: 0x0400363F RID: 13887
	public GameObject[] Hair;

	// Token: 0x04003640 RID: 13888
	public int HairID;
}
