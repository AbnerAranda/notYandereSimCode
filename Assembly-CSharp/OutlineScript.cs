using System;
using HighlightingSystem;
using UnityEngine;

// Token: 0x02000354 RID: 852
public class OutlineScript : MonoBehaviour
{
	// Token: 0x060018B7 RID: 6327 RVA: 0x000E33B6 File Offset: 0x000E15B6
	public void Awake()
	{
		this.h = base.GetComponent<Highlighter>();
		if (this.h == null)
		{
			this.h = base.gameObject.AddComponent<Highlighter>();
		}
	}

	// Token: 0x060018B8 RID: 6328 RVA: 0x000E33E3 File Offset: 0x000E15E3
	private void Update()
	{
		this.h.ConstantOnImmediate(this.color);
	}

	// Token: 0x0400248C RID: 9356
	public YandereScript Yandere;

	// Token: 0x0400248D RID: 9357
	public Highlighter h;

	// Token: 0x0400248E RID: 9358
	public Color color = new Color(1f, 1f, 1f, 1f);
}
