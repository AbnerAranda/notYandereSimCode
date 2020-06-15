using System;
using System.Collections.Generic;
using AmplifyMotion;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x020000B6 RID: 182
[AddComponentMenu("")]
public class AmplifyMotionObjectBase : MonoBehaviour
{
	// Token: 0x170001FE RID: 510
	// (get) Token: 0x0600099B RID: 2459 RVA: 0x0004B280 File Offset: 0x00049480
	internal bool FixedStep
	{
		get
		{
			return this.m_fixedStep;
		}
	}

	// Token: 0x170001FF RID: 511
	// (get) Token: 0x0600099C RID: 2460 RVA: 0x0004B288 File Offset: 0x00049488
	internal int ObjectId
	{
		get
		{
			return this.m_objectId;
		}
	}

	// Token: 0x17000200 RID: 512
	// (get) Token: 0x0600099D RID: 2461 RVA: 0x0004B290 File Offset: 0x00049490
	public ObjectType Type
	{
		get
		{
			return this.m_type;
		}
	}

	// Token: 0x0600099E RID: 2462 RVA: 0x0004B298 File Offset: 0x00049498
	internal void RegisterCamera(AmplifyMotionCamera camera)
	{
		Camera component = camera.GetComponent<Camera>();
		if ((component.cullingMask & 1 << base.gameObject.layer) != 0 && !this.m_states.ContainsKey(component))
		{
			MotionState value;
			switch (this.m_type)
			{
			case ObjectType.Solid:
				value = new SolidState(camera, this);
				break;
			case ObjectType.Skinned:
				value = new SkinnedState(camera, this);
				break;
			case ObjectType.Cloth:
				value = new ClothState(camera, this);
				break;
			case ObjectType.Particle:
				value = new ParticleState(camera, this);
				break;
			default:
				throw new Exception("[AmplifyMotion] Invalid object type.");
			}
			camera.RegisterObject(this);
			this.m_states.Add(component, value);
		}
	}

	// Token: 0x0600099F RID: 2463 RVA: 0x0004B340 File Offset: 0x00049540
	internal void UnregisterCamera(AmplifyMotionCamera camera)
	{
		Camera component = camera.GetComponent<Camera>();
		MotionState motionState;
		if (this.m_states.TryGetValue(component, out motionState))
		{
			camera.UnregisterObject(this);
			if (this.m_states.TryGetValue(component, out motionState))
			{
				motionState.Shutdown();
			}
			this.m_states.Remove(component);
		}
	}

	// Token: 0x060009A0 RID: 2464 RVA: 0x0004B390 File Offset: 0x00049590
	private bool InitializeType()
	{
		Renderer component = base.GetComponent<Renderer>();
		if (AmplifyMotionEffectBase.CanRegister(base.gameObject, false))
		{
			if (base.GetComponent<ParticleSystem>() != null)
			{
				this.m_type = ObjectType.Particle;
				AmplifyMotionEffectBase.RegisterObject(this);
			}
			else if (component != null)
			{
				if (component.GetType() == typeof(MeshRenderer))
				{
					this.m_type = ObjectType.Solid;
				}
				else if (component.GetType() == typeof(SkinnedMeshRenderer))
				{
					if (base.GetComponent<Cloth>() != null)
					{
						this.m_type = ObjectType.Cloth;
					}
					else
					{
						this.m_type = ObjectType.Skinned;
					}
				}
				AmplifyMotionEffectBase.RegisterObject(this);
			}
		}
		return component != null;
	}

	// Token: 0x060009A1 RID: 2465 RVA: 0x0004B440 File Offset: 0x00049640
	private void OnEnable()
	{
		bool flag = this.InitializeType();
		if (flag)
		{
			if (this.m_type == ObjectType.Cloth)
			{
				this.m_fixedStep = false;
			}
			else if (this.m_type == ObjectType.Solid)
			{
				Rigidbody component = base.GetComponent<Rigidbody>();
				if (component != null && component.interpolation == RigidbodyInterpolation.None && !component.isKinematic)
				{
					this.m_fixedStep = true;
				}
			}
		}
		if (this.m_applyToChildren)
		{
			foreach (object obj in base.gameObject.transform)
			{
				AmplifyMotionEffectBase.RegisterRecursivelyS(((Transform)obj).gameObject);
			}
		}
		if (!flag)
		{
			base.enabled = false;
		}
	}

	// Token: 0x060009A2 RID: 2466 RVA: 0x0004B500 File Offset: 0x00049700
	private void OnDisable()
	{
		AmplifyMotionEffectBase.UnregisterObject(this);
	}

	// Token: 0x060009A3 RID: 2467 RVA: 0x0004B508 File Offset: 0x00049708
	private void TryInitializeStates()
	{
		foreach (KeyValuePair<Camera, MotionState> keyValuePair in this.m_states)
		{
			MotionState value = keyValuePair.Value;
			if (value.Owner.Initialized && !value.Error && !value.Initialized)
			{
				value.Initialize();
			}
		}
	}

	// Token: 0x060009A4 RID: 2468 RVA: 0x0004B55F File Offset: 0x0004975F
	private void Start()
	{
		if (AmplifyMotionEffectBase.Instance != null)
		{
			this.TryInitializeStates();
		}
		this.m_lastPosition = base.transform.position;
	}

	// Token: 0x060009A5 RID: 2469 RVA: 0x0004B585 File Offset: 0x00049785
	private void Update()
	{
		if (AmplifyMotionEffectBase.Instance != null)
		{
			this.TryInitializeStates();
		}
	}

	// Token: 0x060009A6 RID: 2470 RVA: 0x0004B59C File Offset: 0x0004979C
	private static void RecursiveResetMotionAtFrame(Transform transform, AmplifyMotionObjectBase obj, int frame)
	{
		if (obj != null)
		{
			obj.m_resetAtFrame = frame;
		}
		foreach (object obj2 in transform)
		{
			Transform transform2 = (Transform)obj2;
			AmplifyMotionObjectBase.RecursiveResetMotionAtFrame(transform2, transform2.GetComponent<AmplifyMotionObjectBase>(), frame);
		}
	}

	// Token: 0x060009A7 RID: 2471 RVA: 0x0004B604 File Offset: 0x00049804
	public void ResetMotionNow()
	{
		AmplifyMotionObjectBase.RecursiveResetMotionAtFrame(base.transform, this, Time.frameCount);
	}

	// Token: 0x060009A8 RID: 2472 RVA: 0x0004B617 File Offset: 0x00049817
	public void ResetMotionAtFrame(int frame)
	{
		AmplifyMotionObjectBase.RecursiveResetMotionAtFrame(base.transform, this, frame);
	}

	// Token: 0x060009A9 RID: 2473 RVA: 0x0004B626 File Offset: 0x00049826
	private void CheckTeleportReset(AmplifyMotionEffectBase inst)
	{
		if (Vector3.SqrMagnitude(base.transform.position - this.m_lastPosition) > inst.MinResetDeltaDistSqr)
		{
			AmplifyMotionObjectBase.RecursiveResetMotionAtFrame(base.transform, this, Time.frameCount + inst.ResetFrameDelay);
		}
	}

	// Token: 0x060009AA RID: 2474 RVA: 0x0004B664 File Offset: 0x00049864
	internal void OnUpdateTransform(AmplifyMotionEffectBase inst, Camera camera, CommandBuffer updateCB, bool starting)
	{
		MotionState motionState;
		if (this.m_states.TryGetValue(camera, out motionState) && !motionState.Error)
		{
			this.CheckTeleportReset(inst);
			bool flag = this.m_resetAtFrame > 0 && Time.frameCount >= this.m_resetAtFrame;
			motionState.UpdateTransform(updateCB, starting || flag);
		}
		this.m_lastPosition = base.transform.position;
	}

	// Token: 0x060009AB RID: 2475 RVA: 0x0004B6CC File Offset: 0x000498CC
	internal void OnRenderVectors(Camera camera, CommandBuffer renderCB, float scale, Quality quality)
	{
		MotionState motionState;
		if (this.m_states.TryGetValue(camera, out motionState) && !motionState.Error)
		{
			motionState.RenderVectors(camera, renderCB, scale, quality);
			if (this.m_resetAtFrame > 0 && Time.frameCount >= this.m_resetAtFrame)
			{
				this.m_resetAtFrame = -1;
			}
		}
	}

	// Token: 0x04000812 RID: 2066
	internal static bool ApplyToChildren = true;

	// Token: 0x04000813 RID: 2067
	[SerializeField]
	private bool m_applyToChildren = AmplifyMotionObjectBase.ApplyToChildren;

	// Token: 0x04000814 RID: 2068
	private ObjectType m_type;

	// Token: 0x04000815 RID: 2069
	private Dictionary<Camera, MotionState> m_states = new Dictionary<Camera, MotionState>();

	// Token: 0x04000816 RID: 2070
	private bool m_fixedStep;

	// Token: 0x04000817 RID: 2071
	private int m_objectId;

	// Token: 0x04000818 RID: 2072
	private Vector3 m_lastPosition = Vector3.zero;

	// Token: 0x04000819 RID: 2073
	private int m_resetAtFrame = -1;

	// Token: 0x020006A7 RID: 1703
	public enum MinMaxCurveState
	{
		// Token: 0x040046DA RID: 18138
		Scalar,
		// Token: 0x040046DB RID: 18139
		Curve,
		// Token: 0x040046DC RID: 18140
		TwoCurves,
		// Token: 0x040046DD RID: 18141
		TwoScalars
	}
}
