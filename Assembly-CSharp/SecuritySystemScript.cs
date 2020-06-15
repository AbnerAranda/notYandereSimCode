using System;
using UnityEngine;

// Token: 0x020003D5 RID: 981
public class SecuritySystemScript : MonoBehaviour
{
	// Token: 0x06001A79 RID: 6777 RVA: 0x00104456 File Offset: 0x00102656
	private void Start()
	{
		if (!SchoolGlobals.HighSecurity)
		{
			base.enabled = false;
			this.Prompt.Hide();
			this.Prompt.enabled = false;
		}
	}

	// Token: 0x06001A7A RID: 6778 RVA: 0x00104480 File Offset: 0x00102680
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			for (int i = 0; i < this.Cameras.Length; i++)
			{
				this.Cameras[i].transform.parent.transform.parent.gameObject.GetComponent<AudioSource>().Stop();
				this.Cameras[i].gameObject.SetActive(false);
			}
			for (int i = 0; i < this.Detectors.Length; i++)
			{
				this.Detectors[i].MyCollider.enabled = false;
				this.Detectors[i].enabled = false;
			}
			base.GetComponent<AudioSource>().Play();
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			this.Evidence = false;
			base.enabled = false;
		}
	}

	// Token: 0x04002A1B RID: 10779
	public PromptScript Prompt;

	// Token: 0x04002A1C RID: 10780
	public bool Evidence;

	// Token: 0x04002A1D RID: 10781
	public bool Masked;

	// Token: 0x04002A1E RID: 10782
	public SecurityCameraScript[] Cameras;

	// Token: 0x04002A1F RID: 10783
	public MetalDetectorScript[] Detectors;
}
