using System;
using UnityEngine;

// Token: 0x02000391 RID: 913
public class RendererListScript : MonoBehaviour
{
	// Token: 0x060019B1 RID: 6577 RVA: 0x000FBF70 File Offset: 0x000FA170
	private void Start()
	{
		Transform[] componentsInChildren = base.gameObject.GetComponentsInChildren<Transform>();
		int num = 0;
		foreach (Transform transform in componentsInChildren)
		{
			if (transform.gameObject.GetComponent<Renderer>() != null)
			{
				this.Renderers[num] = transform.gameObject.GetComponent<Renderer>();
				num++;
			}
		}
	}

	// Token: 0x060019B2 RID: 6578 RVA: 0x000FBFC8 File Offset: 0x000FA1C8
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftControl))
		{
			foreach (Renderer renderer in this.Renderers)
			{
				renderer.enabled = !renderer.enabled;
			}
		}
	}

	// Token: 0x040027CE RID: 10190
	public Renderer[] Renderers;
}
