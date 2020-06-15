using System;
using UnityEngine;

// Token: 0x02000374 RID: 884
public class PortraitScript : MonoBehaviour
{
	// Token: 0x0600193E RID: 6462 RVA: 0x000EFADC File Offset: 0x000EDCDC
	private void Start()
	{
		this.StudentObject[1].SetActive(false);
		this.StudentObject[2].SetActive(false);
		this.Selected = 1;
		this.UpdateHair();
	}

	// Token: 0x0600193F RID: 6463 RVA: 0x000EFB08 File Offset: 0x000EDD08
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			this.StudentObject[0].SetActive(true);
			this.StudentObject[1].SetActive(false);
			this.StudentObject[2].SetActive(false);
			this.Selected = 1;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			this.StudentObject[0].SetActive(false);
			this.StudentObject[1].SetActive(true);
			this.StudentObject[2].SetActive(false);
			this.Selected = 2;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			this.StudentObject[0].SetActive(false);
			this.StudentObject[1].SetActive(false);
			this.StudentObject[2].SetActive(true);
			this.Selected = 3;
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			this.CurrentHair++;
			if (this.CurrentHair > this.HairSet1.Length - 1)
			{
				this.CurrentHair = 0;
			}
			this.UpdateHair();
		}
	}

	// Token: 0x06001940 RID: 6464 RVA: 0x000EFC00 File Offset: 0x000EDE00
	private void UpdateHair()
	{
		Texture mainTexture = this.HairSet2[this.CurrentHair];
		this.Renderer1.materials[0].mainTexture = mainTexture;
		this.Renderer1.materials[3].mainTexture = mainTexture;
		this.Renderer2.materials[2].mainTexture = mainTexture;
		this.Renderer2.materials[3].mainTexture = mainTexture;
		this.Renderer3.materials[0].mainTexture = mainTexture;
		this.Renderer3.materials[1].mainTexture = mainTexture;
	}

	// Token: 0x04002662 RID: 9826
	public GameObject[] StudentObject;

	// Token: 0x04002663 RID: 9827
	public Renderer Renderer1;

	// Token: 0x04002664 RID: 9828
	public Renderer Renderer2;

	// Token: 0x04002665 RID: 9829
	public Renderer Renderer3;

	// Token: 0x04002666 RID: 9830
	public Texture[] HairSet1;

	// Token: 0x04002667 RID: 9831
	public Texture[] HairSet2;

	// Token: 0x04002668 RID: 9832
	public Texture[] HairSet3;

	// Token: 0x04002669 RID: 9833
	public int Selected;

	// Token: 0x0400266A RID: 9834
	public int CurrentHair;
}
