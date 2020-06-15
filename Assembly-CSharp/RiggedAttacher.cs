using System;
using UnityEngine;

// Token: 0x0200049E RID: 1182
public class RiggedAttacher : MonoBehaviour
{
	// Token: 0x06001E41 RID: 7745 RVA: 0x0017C4A2 File Offset: 0x0017A6A2
	private void Start()
	{
		this.Attaching(this.BasePelvisRoot, this.AttachmentPelvisRoot);
	}

	// Token: 0x06001E42 RID: 7746 RVA: 0x0017C4B8 File Offset: 0x0017A6B8
	private void Attaching(Transform Base, Transform Attachment)
	{
		Attachment.transform.SetParent(Base);
		Base.localEulerAngles = Vector3.zero;
		Base.localPosition = Vector3.zero;
		Transform[] componentsInChildren = Base.GetComponentsInChildren<Transform>();
		foreach (Transform transform in Attachment.GetComponentsInChildren<Transform>())
		{
			foreach (Transform transform2 in componentsInChildren)
			{
				if (transform.name == transform2.name)
				{
					transform.SetParent(transform2);
					transform.localEulerAngles = Vector3.zero;
					transform.localPosition = Vector3.zero;
				}
			}
		}
	}

	// Token: 0x04003C74 RID: 15476
	public Transform BasePelvisRoot;

	// Token: 0x04003C75 RID: 15477
	public Transform AttachmentPelvisRoot;
}
