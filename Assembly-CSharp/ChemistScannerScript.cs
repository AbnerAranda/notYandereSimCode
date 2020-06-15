using System;
using UnityEngine;

// Token: 0x02000235 RID: 565
public class ChemistScannerScript : MonoBehaviour
{
	// Token: 0x0600123F RID: 4671 RVA: 0x00081B80 File Offset: 0x0007FD80
	private void Update()
	{
		if (this.Student.Ragdoll != null && this.Student.Ragdoll.enabled)
		{
			this.MyRenderer.materials[1].mainTexture = this.DeadEyes;
			base.enabled = false;
			return;
		}
		if (this.Student.Dying)
		{
			if (this.MyRenderer.materials[1].mainTexture != this.AlarmedEyes)
			{
				this.MyRenderer.materials[1].mainTexture = this.AlarmedEyes;
				return;
			}
		}
		else if (this.Student.Emetic || this.Student.Lethal || this.Student.Tranquil || this.Student.Headache)
		{
			if (this.MyRenderer.materials[1].mainTexture != this.Textures[6])
			{
				this.MyRenderer.materials[1].mainTexture = this.Textures[6];
				return;
			}
		}
		else if (this.Student.Grudge)
		{
			if (this.MyRenderer.materials[1].mainTexture != this.Textures[1])
			{
				this.MyRenderer.materials[1].mainTexture = this.Textures[1];
				return;
			}
		}
		else if (this.Student.LostTeacherTrust)
		{
			if (this.MyRenderer.materials[1].mainTexture != this.SadEyes)
			{
				this.MyRenderer.materials[1].mainTexture = this.SadEyes;
				return;
			}
		}
		else if (this.Student.WitnessedMurder || this.Student.WitnessedCorpse)
		{
			if (this.MyRenderer.materials[1].mainTexture != this.AlarmedEyes)
			{
				this.MyRenderer.materials[1].mainTexture = this.AlarmedEyes;
				return;
			}
		}
		else
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > 2f)
			{
				while (this.ID == this.PreviousID)
				{
					this.ID = UnityEngine.Random.Range(0, this.Textures.Length);
				}
				this.MyRenderer.materials[1].mainTexture = this.Textures[this.ID];
				this.PreviousID = this.ID;
				this.Timer = 0f;
			}
		}
	}

	// Token: 0x0400159B RID: 5531
	public StudentScript Student;

	// Token: 0x0400159C RID: 5532
	public Renderer MyRenderer;

	// Token: 0x0400159D RID: 5533
	public Texture AlarmedEyes;

	// Token: 0x0400159E RID: 5534
	public Texture DeadEyes;

	// Token: 0x0400159F RID: 5535
	public Texture SadEyes;

	// Token: 0x040015A0 RID: 5536
	public Texture[] Textures;

	// Token: 0x040015A1 RID: 5537
	public float Timer;

	// Token: 0x040015A2 RID: 5538
	public int PreviousID;

	// Token: 0x040015A3 RID: 5539
	public int ID;
}
