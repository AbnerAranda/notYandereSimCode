using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200049C RID: 1180
public class ModelSwapScript : MonoBehaviour
{
	// Token: 0x06001E3B RID: 7739 RVA: 0x0017C405 File Offset: 0x0017A605
	public void Update()
	{
		Input.GetKeyDown("z");
	}

	// Token: 0x06001E3C RID: 7740 RVA: 0x0017C412 File Offset: 0x0017A612
	public void Attach(GameObject Attachment, bool Inactives)
	{
		base.StartCoroutine(this.Attach_Threat(this.PelvisRoot, Attachment, Inactives));
	}

	// Token: 0x06001E3D RID: 7741 RVA: 0x0017C429 File Offset: 0x0017A629
	public IEnumerator Attach_Threat(Transform PelvisRoot, GameObject Attachment, bool Inactives)
	{
		Attachment.transform.SetParent(PelvisRoot);
		PelvisRoot.localEulerAngles = Vector3.zero;
		PelvisRoot.localPosition = Vector3.zero;
		Transform[] componentsInChildren = PelvisRoot.GetComponentsInChildren<Transform>(Inactives);
		foreach (Transform transform in Attachment.GetComponentsInChildren<Transform>(Inactives))
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
		yield return null;
		yield break;
	}

	// Token: 0x04003C71 RID: 15473
	public Transform PelvisRoot;

	// Token: 0x04003C72 RID: 15474
	public GameObject Attachment;
}
