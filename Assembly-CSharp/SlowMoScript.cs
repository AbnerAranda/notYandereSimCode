using System;
using UnityEngine;

// Token: 0x020003E5 RID: 997
public class SlowMoScript : MonoBehaviour
{
	// Token: 0x06001AC0 RID: 6848 RVA: 0x0010C0F8 File Offset: 0x0010A2F8
	private void Update()
	{
		if (Input.GetKeyDown("s"))
		{
			this.Spinning = !this.Spinning;
		}
		if (Input.GetKeyDown("a"))
		{
			Time.timeScale = 0.1f;
		}
		if (Input.GetKeyDown("-"))
		{
			Time.timeScale -= 1f;
		}
		if (Input.GetKeyDown("="))
		{
			Time.timeScale += 1f;
		}
		if (Input.GetKeyDown("z"))
		{
			this.Speed += Time.deltaTime;
		}
		if (this.Speed > 0f)
		{
			base.transform.position += new Vector3(Time.deltaTime * 0.1f, 0f, Time.deltaTime * 0.1f);
		}
		if (this.Spinning)
		{
			base.transform.parent.transform.localEulerAngles += new Vector3(0f, Time.deltaTime * 36f, 0f);
		}
	}

	// Token: 0x04002B2B RID: 11051
	public bool Spinning;

	// Token: 0x04002B2C RID: 11052
	public float Speed;
}
