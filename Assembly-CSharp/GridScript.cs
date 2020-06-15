using System;
using UnityEngine;

// Token: 0x020002DC RID: 732
public class GridScript : MonoBehaviour
{
	// Token: 0x060016F2 RID: 5874 RVA: 0x000BEA08 File Offset: 0x000BCC08
	private void Start()
	{
		while (this.ID < this.Rows * this.Columns)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.Tile, new Vector3((float)this.Row, 0f, (float)this.Column), Quaternion.identity).transform.parent = base.transform;
			this.Row++;
			if (this.Row > this.Rows)
			{
				this.Row = 1;
				this.Column++;
			}
			this.ID++;
		}
		base.transform.localScale = new Vector3(4f, 4f, 4f);
		base.transform.position = new Vector3(-52f, 0f, -52f);
	}

	// Token: 0x04001E64 RID: 7780
	public GameObject Tile;

	// Token: 0x04001E65 RID: 7781
	public int Row;

	// Token: 0x04001E66 RID: 7782
	public int Column;

	// Token: 0x04001E67 RID: 7783
	public int Rows = 25;

	// Token: 0x04001E68 RID: 7784
	public int Columns = 25;

	// Token: 0x04001E69 RID: 7785
	public int ID;
}
