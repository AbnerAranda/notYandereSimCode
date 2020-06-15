using System;
using UnityEngine;

// Token: 0x0200043A RID: 1082
public class UniformSwapperScript : MonoBehaviour
{
	// Token: 0x06001C9F RID: 7327 RVA: 0x00158294 File Offset: 0x00156494
	private void Start()
	{
		int maleUniform = StudentGlobals.MaleUniform;
		this.MyRenderer.sharedMesh = this.UniformMeshes[maleUniform];
		Texture mainTexture = this.UniformTextures[maleUniform];
		if (maleUniform == 1)
		{
			this.SkinID = 0;
			this.UniformID = 1;
			this.FaceID = 2;
		}
		else if (maleUniform == 2)
		{
			this.UniformID = 0;
			this.FaceID = 1;
			this.SkinID = 2;
		}
		else if (maleUniform == 3)
		{
			this.UniformID = 0;
			this.FaceID = 1;
			this.SkinID = 2;
		}
		else if (maleUniform == 4)
		{
			this.FaceID = 0;
			this.SkinID = 1;
			this.UniformID = 2;
		}
		else if (maleUniform == 5)
		{
			this.FaceID = 0;
			this.SkinID = 1;
			this.UniformID = 2;
		}
		else if (maleUniform == 6)
		{
			this.FaceID = 0;
			this.SkinID = 1;
			this.UniformID = 2;
		}
		this.MyRenderer.materials[this.FaceID].mainTexture = this.FaceTexture;
		this.MyRenderer.materials[this.SkinID].mainTexture = mainTexture;
		this.MyRenderer.materials[this.UniformID].mainTexture = mainTexture;
	}

	// Token: 0x06001CA0 RID: 7328 RVA: 0x001583B3 File Offset: 0x001565B3
	private void LateUpdate()
	{
		if (this.LookTarget != null)
		{
			this.Head.LookAt(this.LookTarget);
		}
	}

	// Token: 0x04003641 RID: 13889
	public Texture[] UniformTextures;

	// Token: 0x04003642 RID: 13890
	public Mesh[] UniformMeshes;

	// Token: 0x04003643 RID: 13891
	public Texture FaceTexture;

	// Token: 0x04003644 RID: 13892
	public SkinnedMeshRenderer MyRenderer;

	// Token: 0x04003645 RID: 13893
	public int UniformID;

	// Token: 0x04003646 RID: 13894
	public int FaceID;

	// Token: 0x04003647 RID: 13895
	public int SkinID;

	// Token: 0x04003648 RID: 13896
	public Transform LookTarget;

	// Token: 0x04003649 RID: 13897
	public Transform Head;
}
