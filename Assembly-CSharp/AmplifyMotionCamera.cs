using System;
using System.Collections.Generic;
using AmplifyMotion;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x020000B3 RID: 179
[AddComponentMenu("")]
[RequireComponent(typeof(Camera))]
public class AmplifyMotionCamera : MonoBehaviour
{
	// Token: 0x170001F8 RID: 504
	// (get) Token: 0x0600097D RID: 2429 RVA: 0x0004AC05 File Offset: 0x00048E05
	public bool Initialized
	{
		get
		{
			return this.m_initialized;
		}
	}

	// Token: 0x170001F9 RID: 505
	// (get) Token: 0x0600097E RID: 2430 RVA: 0x0004AC0D File Offset: 0x00048E0D
	public bool AutoStep
	{
		get
		{
			return this.m_autoStep;
		}
	}

	// Token: 0x170001FA RID: 506
	// (get) Token: 0x0600097F RID: 2431 RVA: 0x0004AC15 File Offset: 0x00048E15
	public bool Overlay
	{
		get
		{
			return this.m_overlay;
		}
	}

	// Token: 0x170001FB RID: 507
	// (get) Token: 0x06000980 RID: 2432 RVA: 0x0004AC1D File Offset: 0x00048E1D
	public Camera Camera
	{
		get
		{
			return this.m_camera;
		}
	}

	// Token: 0x06000981 RID: 2433 RVA: 0x0004AC25 File Offset: 0x00048E25
	public void RegisterObject(AmplifyMotionObjectBase obj)
	{
		this.m_affectedObjectsTable.Add(obj);
		this.m_affectedObjectsChanged = true;
	}

	// Token: 0x06000982 RID: 2434 RVA: 0x0004AC3B File Offset: 0x00048E3B
	public void UnregisterObject(AmplifyMotionObjectBase obj)
	{
		this.m_affectedObjectsTable.Remove(obj);
		this.m_affectedObjectsChanged = true;
	}

	// Token: 0x06000983 RID: 2435 RVA: 0x0004AC54 File Offset: 0x00048E54
	private void UpdateAffectedObjects()
	{
		if (this.m_affectedObjects == null || this.m_affectedObjectsTable.Count != this.m_affectedObjects.Length)
		{
			this.m_affectedObjects = new AmplifyMotionObjectBase[this.m_affectedObjectsTable.Count];
		}
		this.m_affectedObjectsTable.CopyTo(this.m_affectedObjects);
		this.m_affectedObjectsChanged = false;
	}

	// Token: 0x06000984 RID: 2436 RVA: 0x0004ACAC File Offset: 0x00048EAC
	public void LinkTo(AmplifyMotionEffectBase instance, bool overlay)
	{
		this.Instance = instance;
		this.m_camera = base.GetComponent<Camera>();
		this.m_camera.depthTextureMode |= DepthTextureMode.Depth;
		this.InitializeCommandBuffers();
		this.m_overlay = overlay;
		this.m_linked = true;
	}

	// Token: 0x06000985 RID: 2437 RVA: 0x0004ACE8 File Offset: 0x00048EE8
	public void Initialize()
	{
		this.m_step = false;
		this.UpdateMatrices();
		this.m_initialized = true;
	}

	// Token: 0x06000986 RID: 2438 RVA: 0x0004ACFE File Offset: 0x00048EFE
	private void InitializeCommandBuffers()
	{
		this.ShutdownCommandBuffers();
		this.m_renderCB = new CommandBuffer();
		this.m_renderCB.name = "AmplifyMotion.Render";
		this.m_camera.AddCommandBuffer(CameraEvent.BeforeImageEffects, this.m_renderCB);
	}

	// Token: 0x06000987 RID: 2439 RVA: 0x0004AD34 File Offset: 0x00048F34
	private void ShutdownCommandBuffers()
	{
		if (this.m_renderCB != null)
		{
			this.m_camera.RemoveCommandBuffer(CameraEvent.BeforeImageEffects, this.m_renderCB);
			this.m_renderCB.Release();
			this.m_renderCB = null;
		}
	}

	// Token: 0x06000988 RID: 2440 RVA: 0x0004AD63 File Offset: 0x00048F63
	private void Awake()
	{
		this.Transform = base.transform;
	}

	// Token: 0x06000989 RID: 2441 RVA: 0x0004AD71 File Offset: 0x00048F71
	private void OnEnable()
	{
		AmplifyMotionEffectBase.RegisterCamera(this);
	}

	// Token: 0x0600098A RID: 2442 RVA: 0x0004AD79 File Offset: 0x00048F79
	private void OnDisable()
	{
		this.m_initialized = false;
		this.ShutdownCommandBuffers();
		AmplifyMotionEffectBase.UnregisterCamera(this);
	}

	// Token: 0x0600098B RID: 2443 RVA: 0x0004AD8E File Offset: 0x00048F8E
	private void OnDestroy()
	{
		if (this.Instance != null)
		{
			this.Instance.RemoveCamera(this.m_camera);
		}
	}

	// Token: 0x0600098C RID: 2444 RVA: 0x0004ADAF File Offset: 0x00048FAF
	public void StopAutoStep()
	{
		if (this.m_autoStep)
		{
			this.m_autoStep = false;
			this.m_step = true;
		}
	}

	// Token: 0x0600098D RID: 2445 RVA: 0x0004ADC7 File Offset: 0x00048FC7
	public void StartAutoStep()
	{
		this.m_autoStep = true;
	}

	// Token: 0x0600098E RID: 2446 RVA: 0x0004ADD0 File Offset: 0x00048FD0
	public void Step()
	{
		this.m_step = true;
	}

	// Token: 0x0600098F RID: 2447 RVA: 0x0004ADDC File Offset: 0x00048FDC
	private void Update()
	{
		if (!this.m_linked || !this.Instance.isActiveAndEnabled)
		{
			return;
		}
		if (!this.m_initialized)
		{
			this.Initialize();
		}
		if ((this.m_camera.depthTextureMode & DepthTextureMode.Depth) == DepthTextureMode.None)
		{
			this.m_camera.depthTextureMode |= DepthTextureMode.Depth;
		}
	}

	// Token: 0x06000990 RID: 2448 RVA: 0x0004AE30 File Offset: 0x00049030
	private void UpdateMatrices()
	{
		if (!this.m_starting)
		{
			this.PrevViewProjMatrix = this.ViewProjMatrix;
			this.PrevViewProjMatrixRT = this.ViewProjMatrixRT;
		}
		Matrix4x4 worldToCameraMatrix = this.m_camera.worldToCameraMatrix;
		Matrix4x4 gpuprojectionMatrix = GL.GetGPUProjectionMatrix(this.m_camera.projectionMatrix, false);
		this.ViewProjMatrix = gpuprojectionMatrix * worldToCameraMatrix;
		this.InvViewProjMatrix = Matrix4x4.Inverse(this.ViewProjMatrix);
		Matrix4x4 gpuprojectionMatrix2 = GL.GetGPUProjectionMatrix(this.m_camera.projectionMatrix, true);
		this.ViewProjMatrixRT = gpuprojectionMatrix2 * worldToCameraMatrix;
		if (this.m_starting)
		{
			this.PrevViewProjMatrix = this.ViewProjMatrix;
			this.PrevViewProjMatrixRT = this.ViewProjMatrixRT;
		}
	}

	// Token: 0x06000991 RID: 2449 RVA: 0x0004AED8 File Offset: 0x000490D8
	public void FixedUpdateTransform(AmplifyMotionEffectBase inst, CommandBuffer updateCB)
	{
		if (!this.m_initialized)
		{
			this.Initialize();
		}
		if (this.m_affectedObjectsChanged)
		{
			this.UpdateAffectedObjects();
		}
		for (int i = 0; i < this.m_affectedObjects.Length; i++)
		{
			if (this.m_affectedObjects[i].FixedStep)
			{
				this.m_affectedObjects[i].OnUpdateTransform(inst, this.m_camera, updateCB, this.m_starting);
			}
		}
	}

	// Token: 0x06000992 RID: 2450 RVA: 0x0004AF40 File Offset: 0x00049140
	public void UpdateTransform(AmplifyMotionEffectBase inst, CommandBuffer updateCB)
	{
		if (!this.m_initialized)
		{
			this.Initialize();
		}
		if (Time.frameCount > this.m_prevFrameCount && (this.m_autoStep || this.m_step))
		{
			this.UpdateMatrices();
			if (this.m_affectedObjectsChanged)
			{
				this.UpdateAffectedObjects();
			}
			for (int i = 0; i < this.m_affectedObjects.Length; i++)
			{
				if (!this.m_affectedObjects[i].FixedStep)
				{
					this.m_affectedObjects[i].OnUpdateTransform(inst, this.m_camera, updateCB, this.m_starting);
				}
			}
			this.m_starting = false;
			this.m_step = false;
			this.m_prevFrameCount = Time.frameCount;
		}
	}

	// Token: 0x06000993 RID: 2451 RVA: 0x0004AFE4 File Offset: 0x000491E4
	public void RenderReprojectionVectors(RenderTexture destination, float scale)
	{
		this.m_renderCB.SetGlobalMatrix("_AM_MATRIX_CURR_REPROJ", this.PrevViewProjMatrix * this.InvViewProjMatrix);
		this.m_renderCB.SetGlobalFloat("_AM_MOTION_SCALE", scale);
		RenderTexture tex = null;
		this.m_renderCB.Blit(new RenderTargetIdentifier(tex), destination, this.Instance.ReprojectionMaterial);
	}

	// Token: 0x06000994 RID: 2452 RVA: 0x0004B048 File Offset: 0x00049248
	public void PreRenderVectors(RenderTexture motionRT, bool clearColor, float rcpDepthThreshold)
	{
		this.m_renderCB.Clear();
		this.m_renderCB.SetGlobalFloat("_AM_MIN_VELOCITY", this.Instance.MinVelocity);
		this.m_renderCB.SetGlobalFloat("_AM_MAX_VELOCITY", this.Instance.MaxVelocity);
		this.m_renderCB.SetGlobalFloat("_AM_RCP_TOTAL_VELOCITY", 1f / (this.Instance.MaxVelocity - this.Instance.MinVelocity));
		this.m_renderCB.SetGlobalVector("_AM_DEPTH_THRESHOLD", new Vector2(this.Instance.DepthThreshold, rcpDepthThreshold));
		this.m_renderCB.SetRenderTarget(motionRT);
		this.m_renderCB.ClearRenderTarget(true, clearColor, Color.black);
	}

	// Token: 0x06000995 RID: 2453 RVA: 0x0004B10C File Offset: 0x0004930C
	public void RenderVectors(float scale, float fixedScale, Quality quality)
	{
		if (!this.m_initialized)
		{
			this.Initialize();
		}
		float nearClipPlane = this.m_camera.nearClipPlane;
		float farClipPlane = this.m_camera.farClipPlane;
		Vector4 vector;
		if (AmplifyMotionEffectBase.IsD3D)
		{
			vector.x = 1f - farClipPlane / nearClipPlane;
			vector.y = farClipPlane / nearClipPlane;
		}
		else
		{
			vector.x = (1f - farClipPlane / nearClipPlane) / 2f;
			vector.y = (1f + farClipPlane / nearClipPlane) / 2f;
		}
		vector.z = vector.x / farClipPlane;
		vector.w = vector.y / farClipPlane;
		this.m_renderCB.SetGlobalVector("_AM_ZBUFFER_PARAMS", vector);
		if (this.m_affectedObjectsChanged)
		{
			this.UpdateAffectedObjects();
		}
		for (int i = 0; i < this.m_affectedObjects.Length; i++)
		{
			if ((this.m_camera.cullingMask & 1 << this.m_affectedObjects[i].gameObject.layer) != 0)
			{
				this.m_affectedObjects[i].OnRenderVectors(this.m_camera, this.m_renderCB, this.m_affectedObjects[i].FixedStep ? fixedScale : scale, quality);
			}
		}
	}

	// Token: 0x040007FE RID: 2046
	internal AmplifyMotionEffectBase Instance;

	// Token: 0x040007FF RID: 2047
	internal Matrix4x4 PrevViewProjMatrix;

	// Token: 0x04000800 RID: 2048
	internal Matrix4x4 ViewProjMatrix;

	// Token: 0x04000801 RID: 2049
	internal Matrix4x4 InvViewProjMatrix;

	// Token: 0x04000802 RID: 2050
	internal Matrix4x4 PrevViewProjMatrixRT;

	// Token: 0x04000803 RID: 2051
	internal Matrix4x4 ViewProjMatrixRT;

	// Token: 0x04000804 RID: 2052
	internal Transform Transform;

	// Token: 0x04000805 RID: 2053
	private bool m_linked;

	// Token: 0x04000806 RID: 2054
	private bool m_initialized;

	// Token: 0x04000807 RID: 2055
	private bool m_starting = true;

	// Token: 0x04000808 RID: 2056
	private bool m_autoStep = true;

	// Token: 0x04000809 RID: 2057
	private bool m_step;

	// Token: 0x0400080A RID: 2058
	private bool m_overlay;

	// Token: 0x0400080B RID: 2059
	private Camera m_camera;

	// Token: 0x0400080C RID: 2060
	private int m_prevFrameCount;

	// Token: 0x0400080D RID: 2061
	private HashSet<AmplifyMotionObjectBase> m_affectedObjectsTable = new HashSet<AmplifyMotionObjectBase>();

	// Token: 0x0400080E RID: 2062
	private AmplifyMotionObjectBase[] m_affectedObjects;

	// Token: 0x0400080F RID: 2063
	private bool m_affectedObjectsChanged = true;

	// Token: 0x04000810 RID: 2064
	private const CameraEvent m_renderCBEvent = CameraEvent.BeforeImageEffects;

	// Token: 0x04000811 RID: 2065
	private CommandBuffer m_renderCB;
}
