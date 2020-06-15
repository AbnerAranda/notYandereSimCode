using System;
using UnityEngine;

// Token: 0x020002F1 RID: 753
public class HomeMangaBookScript : MonoBehaviour
{
	// Token: 0x06001741 RID: 5953 RVA: 0x000C790D File Offset: 0x000C5B0D
	private void Start()
	{
		base.transform.eulerAngles = new Vector3(90f, base.transform.eulerAngles.y, base.transform.eulerAngles.z);
	}

	// Token: 0x06001742 RID: 5954 RVA: 0x000C7944 File Offset: 0x000C5B44
	private void Update()
	{
		float y = (this.Manga.Selected == this.ID) ? (base.transform.eulerAngles.y + Time.deltaTime * this.RotationSpeed) : 0f;
		base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, y, base.transform.eulerAngles.z);
	}

	// Token: 0x04001FD8 RID: 8152
	public HomeMangaScript Manga;

	// Token: 0x04001FD9 RID: 8153
	public float RotationSpeed;

	// Token: 0x04001FDA RID: 8154
	public int ID;
}
