using System;
using UnityEngine;

// Token: 0x020002A5 RID: 677
public class FoldedUniformScript : MonoBehaviour
{
	// Token: 0x0600141A RID: 5146 RVA: 0x000B0FF8 File Offset: 0x000AF1F8
	private void Start()
	{
		this.Yandere = GameObject.Find("YandereChan").GetComponent<YandereScript>();
		bool flag = false;
		if (this.Spare && !GameGlobals.SpareUniform)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			flag = true;
		}
		if (!flag && this.Clean && this.Prompt.Button[0] != null)
		{
			this.Prompt.HideButton[0] = true;
			this.Yandere.StudentManager.NewUniforms++;
			this.Yandere.StudentManager.UpdateStudents(0);
			this.Yandere.StudentManager.Uniforms[this.Yandere.StudentManager.NewUniforms] = base.transform;
			Debug.Log("A new uniform has appeared. There are now " + this.Yandere.StudentManager.NewUniforms + " new uniforms at school.");
		}
	}

	// Token: 0x0600141B RID: 5147 RVA: 0x000B10E8 File Offset: 0x000AF2E8
	private void Update()
	{
		if (this.Clean)
		{
			this.InPosition = this.Yandere.StudentManager.LockerRoomArea.bounds.Contains(base.transform.position);
			if (this.Yandere.MyRenderer.sharedMesh == this.Yandere.Towel)
			{
				Debug.Log("Yandere-chan is wearing a towel.");
			}
			if (this.Yandere.Bloodiness == 0f)
			{
				Debug.Log("Yandere-chan is not bloody.");
			}
			if (this.InPosition)
			{
				Debug.Log("This uniform is in the locker room.");
			}
			if (this.Yandere.MyRenderer.sharedMesh != this.Yandere.Towel || this.Yandere.Bloodiness != 0f || !this.InPosition)
			{
				this.Prompt.HideButton[0] = true;
			}
			else
			{
				this.Prompt.HideButton[0] = false;
			}
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.SteamCloud, this.Yandere.transform.position + Vector3.up * 0.81f, Quaternion.identity);
				this.Yandere.Character.GetComponent<Animation>().CrossFade("f02_stripping_00");
				this.Yandere.CurrentUniformOrigin = 2;
				this.Yandere.Stripping = true;
				this.Yandere.CanMove = false;
				this.Timer += Time.deltaTime;
			}
			if (this.Timer > 0f)
			{
				this.Timer += Time.deltaTime;
				if (this.Timer > 1.5f)
				{
					this.Yandere.Schoolwear = 1;
					this.Yandere.ChangeSchoolwear();
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
		}
	}

	// Token: 0x04001C6C RID: 7276
	public YandereScript Yandere;

	// Token: 0x04001C6D RID: 7277
	public PromptScript Prompt;

	// Token: 0x04001C6E RID: 7278
	public GameObject SteamCloud;

	// Token: 0x04001C6F RID: 7279
	public bool InPosition = true;

	// Token: 0x04001C70 RID: 7280
	public bool Clean;

	// Token: 0x04001C71 RID: 7281
	public bool Spare;

	// Token: 0x04001C72 RID: 7282
	public float Timer;

	// Token: 0x04001C73 RID: 7283
	public int Type;
}
