using System;
using UnityEngine;

// Token: 0x0200031B RID: 795
public class LaptopScript : MonoBehaviour
{
	// Token: 0x060017F7 RID: 6135 RVA: 0x000D3AB8 File Offset: 0x000D1CB8
	private void Start()
	{
		if (SchoolGlobals.SCP)
		{
			this.LaptopScreen.localScale = Vector3.zero;
			this.LaptopCamera.enabled = false;
			this.SCP.SetActive(false);
			base.enabled = false;
			return;
		}
		this.SCPRenderer.sharedMesh = this.Uniforms[StudentGlobals.FemaleUniform];
		Animation component = this.SCP.GetComponent<Animation>();
		component["f02_scp_00"].speed = 0f;
		component["f02_scp_00"].time = 0f;
		this.MyAudio = base.GetComponent<AudioSource>();
	}

	// Token: 0x060017F8 RID: 6136 RVA: 0x000D3B54 File Offset: 0x000D1D54
	private void Update()
	{
		if (this.FirstFrame == 2)
		{
			this.LaptopCamera.enabled = false;
		}
		this.FirstFrame++;
		if (!this.Off)
		{
			Animation component = this.SCP.GetComponent<Animation>();
			if (!this.React)
			{
				if (this.Yandere.transform.position.x > base.transform.position.x + 1f && Vector3.Distance(this.Yandere.transform.position, new Vector3(base.transform.position.x, 4f, base.transform.position.z)) < 2f && this.Yandere.Followers == 0)
				{
					this.EventSubtitle.transform.localScale = new Vector3(1f, 1f, 1f);
					component["f02_scp_00"].time = 0f;
					this.LaptopCamera.enabled = true;
					component.Play();
					this.Hair.enabled = true;
					this.Jukebox.Dip = 0.5f;
					this.MyAudio.Play();
					this.React = true;
				}
			}
			else
			{
				this.MyAudio.pitch = Time.timeScale;
				this.MyAudio.volume = 1f;
				if (this.Yandere.transform.position.y > base.transform.position.y + 3f || this.Yandere.transform.position.y < base.transform.position.y - 3f)
				{
					this.MyAudio.volume = 0f;
				}
				for (int i = 0; i < this.Cues.Length; i++)
				{
					if (this.MyAudio.time > this.Cues[i])
					{
						this.EventSubtitle.text = this.Subs[i];
					}
				}
				if (this.MyAudio.time >= this.MyAudio.clip.length - 1f || this.MyAudio.time == 0f)
				{
					component["f02_scp_00"].speed = 1f;
					this.Timer += Time.deltaTime;
				}
				else
				{
					component["f02_scp_00"].time = this.MyAudio.time;
				}
				if (this.Timer > 1f || Vector3.Distance(this.Yandere.transform.position, new Vector3(base.transform.position.x, 4f, base.transform.position.z)) > 5f)
				{
					this.TurnOff();
				}
			}
			if (this.Yandere.StudentManager.Clock.HourTime > 16f || this.Yandere.Police.FadeOut)
			{
				this.TurnOff();
				return;
			}
		}
		else
		{
			if (this.LaptopScreen.localScale.x > 0.1f)
			{
				this.LaptopScreen.localScale = Vector3.Lerp(this.LaptopScreen.localScale, Vector3.zero, Time.deltaTime * 10f);
				return;
			}
			if (base.enabled)
			{
				this.LaptopScreen.localScale = Vector3.zero;
				this.Hair.enabled = false;
				base.enabled = false;
			}
		}
	}

	// Token: 0x060017F9 RID: 6137 RVA: 0x000D3EE8 File Offset: 0x000D20E8
	private void TurnOff()
	{
		this.MyAudio.clip = this.ShutDown;
		this.MyAudio.Play();
		this.EventSubtitle.text = string.Empty;
		SchoolGlobals.SCP = true;
		this.LaptopCamera.enabled = false;
		this.Jukebox.Dip = 1f;
		this.React = false;
		this.Off = true;
	}

	// Token: 0x04002261 RID: 8801
	public SkinnedMeshRenderer SCPRenderer;

	// Token: 0x04002262 RID: 8802
	public Camera LaptopCamera;

	// Token: 0x04002263 RID: 8803
	public JukeboxScript Jukebox;

	// Token: 0x04002264 RID: 8804
	public YandereScript Yandere;

	// Token: 0x04002265 RID: 8805
	public AudioSource MyAudio;

	// Token: 0x04002266 RID: 8806
	public DynamicBone Hair;

	// Token: 0x04002267 RID: 8807
	public Transform LaptopScreen;

	// Token: 0x04002268 RID: 8808
	public AudioClip ShutDown;

	// Token: 0x04002269 RID: 8809
	public GameObject SCP;

	// Token: 0x0400226A RID: 8810
	public bool React;

	// Token: 0x0400226B RID: 8811
	public bool Off;

	// Token: 0x0400226C RID: 8812
	public float[] Cues;

	// Token: 0x0400226D RID: 8813
	public string[] Subs;

	// Token: 0x0400226E RID: 8814
	public Mesh[] Uniforms;

	// Token: 0x0400226F RID: 8815
	public int FirstFrame;

	// Token: 0x04002270 RID: 8816
	public float Timer;

	// Token: 0x04002271 RID: 8817
	public UILabel EventSubtitle;
}
