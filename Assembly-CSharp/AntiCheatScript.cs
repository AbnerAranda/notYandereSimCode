using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020000C9 RID: 201
public class AntiCheatScript : MonoBehaviour
{
	// Token: 0x06000A03 RID: 2563 RVA: 0x0004FA9A File Offset: 0x0004DC9A
	private void Start()
	{
		this.MyAudio = base.GetComponent<AudioSource>();
	}

	// Token: 0x06000A04 RID: 2564 RVA: 0x0004FAA8 File Offset: 0x0004DCA8
	private void Update()
	{
		if (this.Check && !this.MyAudio.isPlaying)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}

	// Token: 0x06000A05 RID: 2565 RVA: 0x0004FADC File Offset: 0x0004DCDC
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "YandereChan")
		{
			this.Jukebox.SetActive(false);
			this.Check = true;
			this.MyAudio.Play();
		}
	}

	// Token: 0x040009F1 RID: 2545
	public AudioSource MyAudio;

	// Token: 0x040009F2 RID: 2546
	public GameObject Jukebox;

	// Token: 0x040009F3 RID: 2547
	public bool Check;
}
