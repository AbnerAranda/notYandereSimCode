using System;
using UnityEngine;

// Token: 0x0200007A RID: 122
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Internal/Property Binding")]
public class PropertyBinding : MonoBehaviour
{
	// Token: 0x06000497 RID: 1175 RVA: 0x0002C4FE File Offset: 0x0002A6FE
	private void Start()
	{
		this.UpdateTarget();
		if (this.update == PropertyBinding.UpdateCondition.OnStart)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06000498 RID: 1176 RVA: 0x0002C515 File Offset: 0x0002A715
	private void Update()
	{
		if (this.update == PropertyBinding.UpdateCondition.OnUpdate)
		{
			this.UpdateTarget();
		}
	}

	// Token: 0x06000499 RID: 1177 RVA: 0x0002C526 File Offset: 0x0002A726
	private void LateUpdate()
	{
		if (this.update == PropertyBinding.UpdateCondition.OnLateUpdate)
		{
			this.UpdateTarget();
		}
	}

	// Token: 0x0600049A RID: 1178 RVA: 0x0002C537 File Offset: 0x0002A737
	private void FixedUpdate()
	{
		if (this.update == PropertyBinding.UpdateCondition.OnFixedUpdate)
		{
			this.UpdateTarget();
		}
	}

	// Token: 0x0600049B RID: 1179 RVA: 0x0002C548 File Offset: 0x0002A748
	private void OnValidate()
	{
		if (this.source != null)
		{
			this.source.Reset();
		}
		if (this.target != null)
		{
			this.target.Reset();
		}
	}

	// Token: 0x0600049C RID: 1180 RVA: 0x0002C570 File Offset: 0x0002A770
	[ContextMenu("Update Now")]
	public void UpdateTarget()
	{
		if (this.source != null && this.target != null && this.source.isValid && this.target.isValid)
		{
			if (this.direction == PropertyBinding.Direction.SourceUpdatesTarget)
			{
				this.target.Set(this.source.Get());
				return;
			}
			if (this.direction == PropertyBinding.Direction.TargetUpdatesSource)
			{
				this.source.Set(this.target.Get());
				return;
			}
			if (this.source.GetPropertyType() == this.target.GetPropertyType())
			{
				object obj = this.source.Get();
				if (this.mLastValue == null || !this.mLastValue.Equals(obj))
				{
					this.mLastValue = obj;
					this.target.Set(obj);
					return;
				}
				obj = this.target.Get();
				if (!this.mLastValue.Equals(obj))
				{
					this.mLastValue = obj;
					this.source.Set(obj);
				}
			}
		}
	}

	// Token: 0x04000506 RID: 1286
	public PropertyReference source;

	// Token: 0x04000507 RID: 1287
	public PropertyReference target;

	// Token: 0x04000508 RID: 1288
	public PropertyBinding.Direction direction;

	// Token: 0x04000509 RID: 1289
	public PropertyBinding.UpdateCondition update = PropertyBinding.UpdateCondition.OnUpdate;

	// Token: 0x0400050A RID: 1290
	public bool editMode = true;

	// Token: 0x0400050B RID: 1291
	private object mLastValue;

	// Token: 0x0200064F RID: 1615
	[DoNotObfuscateNGUI]
	public enum UpdateCondition
	{
		// Token: 0x040045E8 RID: 17896
		OnStart,
		// Token: 0x040045E9 RID: 17897
		OnUpdate,
		// Token: 0x040045EA RID: 17898
		OnLateUpdate,
		// Token: 0x040045EB RID: 17899
		OnFixedUpdate
	}

	// Token: 0x02000650 RID: 1616
	[DoNotObfuscateNGUI]
	public enum Direction
	{
		// Token: 0x040045ED RID: 17901
		SourceUpdatesTarget,
		// Token: 0x040045EE RID: 17902
		TargetUpdatesSource,
		// Token: 0x040045EF RID: 17903
		BiDirectional
	}
}
