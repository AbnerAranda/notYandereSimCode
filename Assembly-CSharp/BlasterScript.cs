using System;
using UnityEngine;

// Token: 0x020000DB RID: 219
public class BlasterScript : MonoBehaviour
{
	// Token: 0x06000A4B RID: 2635 RVA: 0x00055352 File Offset: 0x00053552
	private void Start()
	{
		this.Skull.localScale = Vector3.zero;
		this.Beam.localScale = Vector3.zero;
	}

	// Token: 0x06000A4C RID: 2636 RVA: 0x00055374 File Offset: 0x00053574
	private void Update()
	{
		AnimationState animationState = base.GetComponent<Animation>()["Blast"];
		if (animationState.time > 1f)
		{
			this.Beam.localScale = Vector3.Lerp(this.Beam.localScale, new Vector3(15f, 1f, 1f), Time.deltaTime * 10f);
			this.Eyes.material.color = new Color(1f, 0f, 0f, 1f);
		}
		if (animationState.time >= animationState.length)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000A4D RID: 2637 RVA: 0x0005541C File Offset: 0x0005361C
	private void LateUpdate()
	{
		AnimationState animationState = base.GetComponent<Animation>()["Blast"];
		this.Size = ((animationState.time < 1.5f) ? Mathf.Lerp(this.Size, 2f, Time.deltaTime * 5f) : Mathf.Lerp(this.Size, 0f, Time.deltaTime * 10f));
		this.Skull.localScale = new Vector3(this.Size, this.Size, this.Size);
	}

	// Token: 0x04000A98 RID: 2712
	public Transform Skull;

	// Token: 0x04000A99 RID: 2713
	public Renderer Eyes;

	// Token: 0x04000A9A RID: 2714
	public Transform Beam;

	// Token: 0x04000A9B RID: 2715
	public float Size;
}
