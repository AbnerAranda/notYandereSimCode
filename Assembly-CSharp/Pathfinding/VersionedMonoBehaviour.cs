using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020005A6 RID: 1446
	public abstract class VersionedMonoBehaviour : MonoBehaviour, ISerializationCallbackReceiver, IVersionedMonoBehaviourInternal
	{
		// Token: 0x0600270A RID: 9994 RVA: 0x001AEED3 File Offset: 0x001AD0D3
		protected virtual void Awake()
		{
			if (Application.isPlaying)
			{
				this.version = this.OnUpgradeSerializedData(int.MaxValue, true);
			}
		}

		// Token: 0x0600270B RID: 9995 RVA: 0x001AEEEE File Offset: 0x001AD0EE
		private void Reset()
		{
			this.version = this.OnUpgradeSerializedData(int.MaxValue, true);
		}

		// Token: 0x0600270C RID: 9996 RVA: 0x00002ACE File Offset: 0x00000CCE
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		// Token: 0x0600270D RID: 9997 RVA: 0x001AEF02 File Offset: 0x001AD102
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			this.version = this.OnUpgradeSerializedData(this.version, false);
		}

		// Token: 0x0600270E RID: 9998 RVA: 0x00022944 File Offset: 0x00020B44
		protected virtual int OnUpgradeSerializedData(int version, bool unityThread)
		{
			return 1;
		}

		// Token: 0x0600270F RID: 9999 RVA: 0x001AEF17 File Offset: 0x001AD117
		int IVersionedMonoBehaviourInternal.OnUpgradeSerializedData(int version, bool unityThread)
		{
			return this.OnUpgradeSerializedData(version, unityThread);
		}

		// Token: 0x04004265 RID: 16997
		[SerializeField]
		[HideInInspector]
		private int version;
	}
}
