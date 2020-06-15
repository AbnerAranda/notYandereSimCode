using System;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020005BB RID: 1467
	[ExecuteInEditMode]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_unity_reference_helper.php")]
	public class UnityReferenceHelper : MonoBehaviour
	{
		// Token: 0x060027B1 RID: 10161 RVA: 0x001B3977 File Offset: 0x001B1B77
		public string GetGUID()
		{
			return this.guid;
		}

		// Token: 0x060027B2 RID: 10162 RVA: 0x001B397F File Offset: 0x001B1B7F
		public void Awake()
		{
			this.Reset();
		}

		// Token: 0x060027B3 RID: 10163 RVA: 0x001B3988 File Offset: 0x001B1B88
		public void Reset()
		{
			if (string.IsNullOrEmpty(this.guid))
			{
				this.guid = Pathfinding.Util.Guid.NewGuid().ToString();
				Debug.Log("Created new GUID - " + this.guid);
				return;
			}
			foreach (UnityReferenceHelper unityReferenceHelper in UnityEngine.Object.FindObjectsOfType(typeof(UnityReferenceHelper)) as UnityReferenceHelper[])
			{
				if (unityReferenceHelper != this && this.guid == unityReferenceHelper.guid)
				{
					this.guid = Pathfinding.Util.Guid.NewGuid().ToString();
					Debug.Log("Created new GUID - " + this.guid);
					return;
				}
			}
		}

		// Token: 0x040042A9 RID: 17065
		[HideInInspector]
		[SerializeField]
		private string guid;
	}
}
