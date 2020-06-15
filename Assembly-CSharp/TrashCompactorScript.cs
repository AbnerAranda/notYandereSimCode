using System;
using UnityEngine;

// Token: 0x02000435 RID: 1077
public class TrashCompactorScript : MonoBehaviour
{
	// Token: 0x06001C8D RID: 7309 RVA: 0x00156F14 File Offset: 0x00155114
	private void Start()
	{
		if (this.StudentManager.Students[10] != null || this.StudentManager.Students[11] != null)
		{
			this.CompactTrash();
			return;
		}
		for (int i = 1; i < 101; i++)
		{
			if (this.StudentManager.Students[i] != null && !this.StudentManager.Students[i].Male && (this.StudentManager.Students[i].Cosmetic.Hairstyle == 20 || this.StudentManager.Students[i].Cosmetic.Hairstyle == 21 || this.StudentManager.Students[i].Persona == PersonaType.Protective))
			{
				this.CompactTrash();
			}
		}
	}

	// Token: 0x06001C8E RID: 7310 RVA: 0x00156FE0 File Offset: 0x001551E0
	private void Update()
	{
		if (this.TrashCompactorObject.gameObject.activeInHierarchy)
		{
			this.Speed += Time.deltaTime * 0.01f;
			this.TrashCompactorObject.position = Vector3.MoveTowards(this.TrashCompactorObject.position, this.Yandere.position, Time.deltaTime * this.Speed);
			this.TrashCompactorObject.LookAt(this.Yandere.position);
			if (Vector3.Distance(this.TrashCompactorObject.position, this.Yandere.position) < 0.5f)
			{
				Application.Quit();
			}
		}
	}

	// Token: 0x06001C8F RID: 7311 RVA: 0x0015708C File Offset: 0x0015528C
	private void CompactTrash()
	{
		Debug.Log("Taking out the garbage.");
		if (!this.TrashCompactorObject.gameObject.activeInHierarchy)
		{
			SchoolGlobals.SchoolAtmosphereSet = true;
			SchoolGlobals.SchoolAtmosphere = 0f;
			this.StudentManager.SetAtmosphere();
			foreach (StudentScript studentScript in this.StudentManager.Students)
			{
				if (studentScript != null)
				{
					studentScript.gameObject.SetActive(false);
				}
			}
			this.Yandere.gameObject.GetComponent<YandereScript>().NoDebug = true;
			this.TrashCompactorObject.gameObject.SetActive(true);
			this.Jukebox.SetActive(false);
			this.HUD.enabled = false;
		}
	}

	// Token: 0x040035CA RID: 13770
	public StudentManagerScript StudentManager;

	// Token: 0x040035CB RID: 13771
	public JsonScript JSON;

	// Token: 0x040035CC RID: 13772
	public UIPanel HUD;

	// Token: 0x040035CD RID: 13773
	public GameObject Jukebox;

	// Token: 0x040035CE RID: 13774
	public Transform TrashCompactorObject;

	// Token: 0x040035CF RID: 13775
	public Transform Yandere;

	// Token: 0x040035D0 RID: 13776
	public float Speed;
}
