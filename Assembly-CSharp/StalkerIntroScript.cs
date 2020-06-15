using System;
using UnityEngine;
using UnityEngine.PostProcessing;

// Token: 0x020003F4 RID: 1012
public class StalkerIntroScript : MonoBehaviour
{
	// Token: 0x06001AF1 RID: 6897 RVA: 0x0010F444 File Offset: 0x0010D644
	private void Start()
	{
		RenderSettings.ambientIntensity = 8f;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		base.transform.position = new Vector3(12.5f, 5f, 13f);
		base.transform.LookAt(this.Moon);
		this.CameraFocus.parent = base.transform;
		this.CameraFocus.localPosition = new Vector3(0f, 0f, 100f);
		this.CameraFocus.parent = null;
		this.UpdateDOF(3f);
		this.DOF = 4f;
		this.Alpha = 1f;
	}

	// Token: 0x06001AF2 RID: 6898 RVA: 0x0010F4F4 File Offset: 0x0010D6F4
	private void Update()
	{
		this.Moon.LookAt(base.transform);
		if (this.Phase == 0)
		{
			if (Input.GetKeyDown("space"))
			{
				this.Timer = 2f;
				this.Alpha = 0f;
			}
			this.Alpha = Mathf.MoveTowards(this.Alpha, 0f, Time.deltaTime * 0.5f);
			this.Darkness.material.color = new Color(0f, 0f, 0f, this.Alpha);
			if (this.Alpha == 0f)
			{
				this.Timer += Time.deltaTime;
				if (this.Timer > 2f)
				{
					this.Phase++;
					return;
				}
			}
		}
		else if (this.Phase == 1)
		{
			if (this.Speed == 0f)
			{
				this.Yandere.MyAnimation.Play();
			}
			if (Input.GetKeyDown("space") && this.Yandere.MyAnimation["f02_jumpOverWall_00"].time < 12f)
			{
				this.Yandere.MyAnimation["f02_jumpOverWall_00"].time = 12f;
				this.Speed = 100f;
			}
			this.Speed += Time.deltaTime;
			base.transform.position = Vector3.Lerp(base.transform.position, new Vector3(11.5f, 1f, 13f), Time.deltaTime * this.Speed);
			this.CameraFocus.position = Vector3.Lerp(this.CameraFocus.position, new Vector3(13.62132f, 1f, 15.12132f), Time.deltaTime * this.Speed);
			this.DOF = Mathf.Lerp(this.DOF, 2f, Time.deltaTime * this.Speed);
			this.UpdateDOF(this.DOF);
			base.transform.LookAt(this.CameraFocus);
			if (this.Yandere.MyAnimation["f02_jumpOverWall_00"].time > 13f)
			{
				this.Yandere.transform.position = new Vector3(13.15f, 0f, 13f);
				base.transform.position = new Vector3(12.75f, 1.3f, 12.4f);
				base.transform.eulerAngles = new Vector3(0f, 45f, 0f);
				this.UpdateDOF(0.5f);
				this.DOF = 0.5f;
				this.Speed = -1f;
				this.Phase++;
				return;
			}
		}
		else if (this.Phase == 2)
		{
			if (Input.GetKeyDown("space"))
			{
				this.Speed = 100f;
			}
			this.Speed += Time.deltaTime;
			if (this.Speed > 0f)
			{
				base.transform.position = Vector3.Lerp(base.transform.position, new Vector3(13.15f, 1.51515f, 14.92272f), Time.deltaTime * this.Speed);
				base.transform.eulerAngles = Vector3.Lerp(base.transform.eulerAngles, new Vector3(15f, 180f, 0f), Time.deltaTime * this.Speed * 2f);
				this.DOF = Mathf.MoveTowards(this.DOF, 2f, Time.deltaTime * this.Speed);
				this.UpdateDOF(this.DOF);
				if (this.Speed > 4f)
				{
					this.DOF = 2f;
					this.UpdateDOF(this.DOF);
					this.RPGCamera.enabled = true;
					this.Yandere.enabled = true;
					this.Phase++;
				}
			}
		}
	}

	// Token: 0x06001AF3 RID: 6899 RVA: 0x0010F904 File Offset: 0x0010DB04
	private void UpdateDOF(float Value)
	{
		DepthOfFieldModel.Settings settings = this.Profile.depthOfField.settings;
		settings.focusDistance = Value;
		settings.aperture = 5.6f;
		this.Profile.depthOfField.settings = settings;
	}

	// Token: 0x04002BBE RID: 11198
	public PostProcessingProfile Profile;

	// Token: 0x04002BBF RID: 11199
	public StalkerYandereScript Yandere;

	// Token: 0x04002BC0 RID: 11200
	public RPG_Camera RPGCamera;

	// Token: 0x04002BC1 RID: 11201
	public Transform CameraFocus;

	// Token: 0x04002BC2 RID: 11202
	public Transform Moon;

	// Token: 0x04002BC3 RID: 11203
	public Renderer Darkness;

	// Token: 0x04002BC4 RID: 11204
	public float Alpha;

	// Token: 0x04002BC5 RID: 11205
	public float Speed;

	// Token: 0x04002BC6 RID: 11206
	public float Timer;

	// Token: 0x04002BC7 RID: 11207
	public float DOF;

	// Token: 0x04002BC8 RID: 11208
	public int Phase;

	// Token: 0x04002BC9 RID: 11209
	public GameObject[] Neighborhood;
}
