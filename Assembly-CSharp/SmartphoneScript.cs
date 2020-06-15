using System;
using UnityEngine;

// Token: 0x020003E6 RID: 998
public class SmartphoneScript : MonoBehaviour
{
	// Token: 0x06001AC2 RID: 6850 RVA: 0x0010C214 File Offset: 0x0010A414
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.CrushingPhone = true;
			this.Prompt.Yandere.PhoneToCrush = this;
			this.Prompt.Yandere.CanMove = false;
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.EmptyGameObject, base.transform.position, Quaternion.identity);
			this.PhoneCrushingSpot = gameObject.transform;
			this.PhoneCrushingSpot.position = new Vector3(this.PhoneCrushingSpot.position.x, this.Prompt.Yandere.transform.position.y, this.PhoneCrushingSpot.position.z);
			this.PhoneCrushingSpot.LookAt(this.Prompt.Yandere.transform.position);
			this.PhoneCrushingSpot.Translate(Vector3.forward * 0.5f);
		}
	}

	// Token: 0x04002B2D RID: 11053
	public Transform PhoneCrushingSpot;

	// Token: 0x04002B2E RID: 11054
	public GameObject EmptyGameObject;

	// Token: 0x04002B2F RID: 11055
	public Texture SmashedTexture;

	// Token: 0x04002B30 RID: 11056
	public GameObject PhoneSmash;

	// Token: 0x04002B31 RID: 11057
	public Renderer MyRenderer;

	// Token: 0x04002B32 RID: 11058
	public PromptScript Prompt;

	// Token: 0x04002B33 RID: 11059
	public MeshFilter MyMesh;

	// Token: 0x04002B34 RID: 11060
	public Mesh SmashedMesh;
}
