using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000323 RID: 803
public class LoadingScript : MonoBehaviour
{
	// Token: 0x06001810 RID: 6160 RVA: 0x000D6011 File Offset: 0x000D4211
	private void Start()
	{
		SceneManager.LoadScene("SchoolScene");
	}
}
