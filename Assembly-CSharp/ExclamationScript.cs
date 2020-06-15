using System;
using UnityEngine;

// Token: 0x02000298 RID: 664
public class ExclamationScript : MonoBehaviour
{
	// Token: 0x060013F3 RID: 5107 RVA: 0x000AE798 File Offset: 0x000AC998
	private void Start()
	{
		base.transform.localScale = Vector3.zero;
		this.Graphic.material.SetColor("_TintColor", new Color(0.5f, 0.5f, 0.5f, 0f));
		this.MainCamera = Camera.main;
	}

	// Token: 0x060013F4 RID: 5108 RVA: 0x000AE7F0 File Offset: 0x000AC9F0
	private void Update()
	{
		this.Timer -= Time.deltaTime;
		if (this.Timer > 0f)
		{
			base.transform.LookAt(this.MainCamera.transform);
			if (this.Timer > 1.5f)
			{
				base.transform.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
				this.Alpha = Mathf.Lerp(this.Alpha, 0.5f, Time.deltaTime * 10f);
				this.Graphic.material.SetColor("_TintColor", new Color(0.5f, 0.5f, 0.5f, this.Alpha));
				return;
			}
			if (base.transform.localScale.x > 0.1f)
			{
				base.transform.localScale = Vector3.Lerp(base.transform.localScale, Vector3.zero, Time.deltaTime * 10f);
			}
			else
			{
				base.transform.localScale = Vector3.zero;
			}
			this.Alpha = Mathf.Lerp(this.Alpha, 0f, Time.deltaTime * 10f);
			this.Graphic.material.SetColor("_TintColor", new Color(0.5f, 0.5f, 0.5f, this.Alpha));
		}
	}

	// Token: 0x04001BE3 RID: 7139
	public Renderer Graphic;

	// Token: 0x04001BE4 RID: 7140
	public float Alpha;

	// Token: 0x04001BE5 RID: 7141
	public float Timer;

	// Token: 0x04001BE6 RID: 7142
	public Camera MainCamera;
}
