using System;
using System.Linq;
using UnityEngine;

// Token: 0x0200042D RID: 1069
internal class ToiletFlushScript : MonoBehaviour
{
	// Token: 0x06001C72 RID: 7282 RVA: 0x00155FDD File Offset: 0x001541DD
	private void Start()
	{
		this.StudentManager = UnityEngine.Object.FindObjectOfType<StudentManagerScript>();
		this.Toilet = this.StudentManager.Students[11].gameObject;
		this.toilet = this.Toilet;
	}

	// Token: 0x06001C73 RID: 7283 RVA: 0x0015600F File Offset: 0x0015420F
	private void Update()
	{
		this.Flush(this.toilet);
	}

	// Token: 0x06001C74 RID: 7284 RVA: 0x00156020 File Offset: 0x00154220
	private void Flush(GameObject toilet)
	{
		if (this.Toilet != null)
		{
			this.Toilet = null;
		}
		if (toilet.activeInHierarchy)
		{
			int length = UnityEngine.Random.Range(1, 15);
			toilet.name = this.RandomSound(length);
			base.name = this.RandomSound(length);
			toilet.SetActive(false);
		}
	}

	// Token: 0x06001C75 RID: 7285 RVA: 0x00156074 File Offset: 0x00154274
	private string RandomSound(int Length)
	{
		return new string((from s in Enumerable.Repeat<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZ", Length)
		select s[ToiletFlushScript.random.Next(s.Length)]).ToArray<char>());
	}

	// Token: 0x040035A0 RID: 13728
	[Header("=== Toilet Related ===")]
	public GameObject Toilet;

	// Token: 0x040035A1 RID: 13729
	private GameObject toilet;

	// Token: 0x040035A2 RID: 13730
	private static System.Random random = new System.Random();

	// Token: 0x040035A3 RID: 13731
	private StudentManagerScript StudentManager;
}
