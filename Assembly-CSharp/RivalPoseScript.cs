using System;
using UnityEngine;

// Token: 0x020000CD RID: 205
public class RivalPoseScript : MonoBehaviour
{
	// Token: 0x06000A11 RID: 2577 RVA: 0x000503A0 File Offset: 0x0004E5A0
	private void Start()
	{
		int femaleUniform = StudentGlobals.FemaleUniform;
		this.MyRenderer.sharedMesh = this.FemaleUniforms[femaleUniform];
		if (femaleUniform == 1)
		{
			this.MyRenderer.materials[0].mainTexture = this.FemaleUniformTextures[femaleUniform];
			this.MyRenderer.materials[1].mainTexture = this.HairTexture;
			this.MyRenderer.materials[2].mainTexture = this.HairTexture;
			this.MyRenderer.materials[3].mainTexture = this.FemaleUniformTextures[femaleUniform];
			return;
		}
		if (femaleUniform == 2)
		{
			this.MyRenderer.materials[0].mainTexture = this.FemaleUniformTextures[femaleUniform];
			this.MyRenderer.materials[1].mainTexture = this.FemaleUniformTextures[femaleUniform];
			this.MyRenderer.materials[2].mainTexture = this.HairTexture;
			this.MyRenderer.materials[3].mainTexture = this.HairTexture;
			return;
		}
		if (femaleUniform == 3)
		{
			this.MyRenderer.materials[0].mainTexture = this.HairTexture;
			this.MyRenderer.materials[1].mainTexture = this.HairTexture;
			this.MyRenderer.materials[2].mainTexture = this.FemaleUniformTextures[femaleUniform];
			this.MyRenderer.materials[3].mainTexture = this.FemaleUniformTextures[femaleUniform];
			return;
		}
		if (femaleUniform == 4)
		{
			this.MyRenderer.materials[0].mainTexture = this.HairTexture;
			this.MyRenderer.materials[1].mainTexture = this.HairTexture;
			this.MyRenderer.materials[2].mainTexture = this.FemaleUniformTextures[femaleUniform];
			this.MyRenderer.materials[3].mainTexture = this.FemaleUniformTextures[femaleUniform];
			return;
		}
		if (femaleUniform == 5)
		{
			this.MyRenderer.materials[0].mainTexture = this.HairTexture;
			this.MyRenderer.materials[1].mainTexture = this.HairTexture;
			this.MyRenderer.materials[2].mainTexture = this.FemaleUniformTextures[femaleUniform];
			this.MyRenderer.materials[3].mainTexture = this.FemaleUniformTextures[femaleUniform];
			return;
		}
		if (femaleUniform == 6)
		{
			this.MyRenderer.materials[0].mainTexture = this.FemaleUniformTextures[femaleUniform];
			this.MyRenderer.materials[1].mainTexture = this.FemaleUniformTextures[femaleUniform];
			this.MyRenderer.materials[2].mainTexture = this.HairTexture;
			this.MyRenderer.materials[3].mainTexture = this.HairTexture;
		}
	}

	// Token: 0x04000A1C RID: 2588
	public GameObject Character;

	// Token: 0x04000A1D RID: 2589
	public SkinnedMeshRenderer MyRenderer;

	// Token: 0x04000A1E RID: 2590
	public Texture[] FemaleUniformTextures;

	// Token: 0x04000A1F RID: 2591
	public Mesh[] FemaleUniforms;

	// Token: 0x04000A20 RID: 2592
	public Texture[] TestTextures;

	// Token: 0x04000A21 RID: 2593
	public Texture HairTexture;

	// Token: 0x04000A22 RID: 2594
	public string[] AnimNames;

	// Token: 0x04000A23 RID: 2595
	public int ID = -1;
}
