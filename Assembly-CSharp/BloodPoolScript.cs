using System;
using UnityEngine;

// Token: 0x020000DF RID: 223
public class BloodPoolScript : MonoBehaviour
{
	// Token: 0x06000A57 RID: 2647 RVA: 0x00055970 File Offset: 0x00053B70
	private void Start()
	{
		if (PlayerGlobals.PantiesEquipped == 7 && this.Blood)
		{
			this.TargetSize *= 0.5f;
		}
		if (GameGlobals.CensorBlood)
		{
			this.MyRenderer.material.color = new Color(1f, 1f, 1f, 1f);
			this.MyRenderer.material.mainTexture = this.Flower;
		}
		base.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		Vector3 position = base.transform.position;
		if (position.x > 125f || position.x < -125f || position.z > 200f || position.z < -100f)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		if (Application.loadedLevelName == "IntroScene" || Application.loadedLevelName == "NewIntroScene")
		{
			this.MyRenderer.material.SetColor("_TintColor", new Color(0.1f, 0.1f, 0.1f));
		}
	}

	// Token: 0x06000A58 RID: 2648 RVA: 0x00055A9C File Offset: 0x00053C9C
	private void Update()
	{
		if (this.Grow)
		{
			base.transform.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(this.TargetSize, this.TargetSize, this.TargetSize), Time.deltaTime);
			if (base.transform.localScale.x > this.TargetSize * 0.99f)
			{
				this.Grow = false;
			}
		}
	}

	// Token: 0x06000A59 RID: 2649 RVA: 0x00055B0D File Offset: 0x00053D0D
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "BloodSpawner")
		{
			this.Grow = true;
		}
	}

	// Token: 0x04000AB0 RID: 2736
	public float TargetSize;

	// Token: 0x04000AB1 RID: 2737
	public bool Blood = true;

	// Token: 0x04000AB2 RID: 2738
	public bool Grow;

	// Token: 0x04000AB3 RID: 2739
	public Renderer MyRenderer;

	// Token: 0x04000AB4 RID: 2740
	public Texture Flower;
}
