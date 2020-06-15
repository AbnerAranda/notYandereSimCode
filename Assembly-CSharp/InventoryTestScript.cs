using System;
using UnityEngine;

// Token: 0x0200030F RID: 783
public class InventoryTestScript : MonoBehaviour
{
	// Token: 0x060017A6 RID: 6054 RVA: 0x000D0E9C File Offset: 0x000CF09C
	private void Start()
	{
		this.RightGrid.localScale = new Vector3(0f, 0f, 0f);
		this.LeftGrid.localScale = new Vector3(0f, 0f, 0f);
		Time.timeScale = 1f;
	}

	// Token: 0x060017A7 RID: 6055 RVA: 0x000D0EF4 File Offset: 0x000CF0F4
	private void Update()
	{
		if (Input.GetKeyDown("space"))
		{
			this.Open = !this.Open;
		}
		AnimationState animationState = this.SkirtAnimation["InverseSkirtOpen"];
		AnimationState animationState2 = this.GirlAnimation["f02_inventory_00"];
		if (this.Open)
		{
			this.RightGrid.localScale = Vector3.MoveTowards(this.RightGrid.localScale, new Vector3(0.9f, 0.9f, 0.9f), Time.deltaTime * 10f);
			this.LeftGrid.localScale = Vector3.MoveTowards(this.LeftGrid.localScale, new Vector3(0.9f, 0.9f, 0.9f), Time.deltaTime * 10f);
			base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, Mathf.Lerp(base.transform.position.z, 0.37f, Time.deltaTime * 10f));
			animationState.time = Mathf.Lerp(animationState2.time, 1f, Time.deltaTime * 10f);
			animationState2.time = animationState.time;
			this.Alpha = Mathf.Lerp(this.Alpha, 1f, Time.deltaTime * 10f);
			this.SkirtRenderer.material.color = new Color(1f, 1f, 1f, this.Alpha);
			this.GirlRenderer.materials[0].color = new Color(0f, 0f, 0f, this.Alpha);
			this.GirlRenderer.materials[1].color = new Color(0f, 0f, 0f, this.Alpha);
			if (Input.GetKeyDown("right"))
			{
				this.Column++;
				this.UpdateHighlight();
			}
			if (Input.GetKeyDown("left"))
			{
				this.Column--;
				this.UpdateHighlight();
			}
			if (Input.GetKeyDown("up"))
			{
				this.Row--;
				this.UpdateHighlight();
			}
			if (Input.GetKeyDown("down"))
			{
				this.Row++;
				this.UpdateHighlight();
			}
		}
		else
		{
			this.RightGrid.localScale = Vector3.MoveTowards(this.RightGrid.localScale, new Vector3(0f, 0f, 0f), Time.deltaTime * 10f);
			this.LeftGrid.localScale = Vector3.MoveTowards(this.LeftGrid.localScale, new Vector3(0f, 0f, 0f), Time.deltaTime * 10f);
			base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, Mathf.Lerp(base.transform.position.z, 1f, Time.deltaTime * 10f));
			animationState.time = Mathf.Lerp(animationState2.time, 0f, Time.deltaTime * 10f);
			animationState2.time = animationState.time;
			this.Alpha = Mathf.Lerp(this.Alpha, 0f, Time.deltaTime * 10f);
			this.SkirtRenderer.material.color = new Color(1f, 1f, 1f, this.Alpha);
			this.GirlRenderer.materials[0].color = new Color(0f, 0f, 0f, this.Alpha);
			this.GirlRenderer.materials[1].color = new Color(0f, 0f, 0f, this.Alpha);
		}
		for (int i = 0; i < this.Items.Length; i++)
		{
			if (this.Items[i].Clicked)
			{
				Debug.Log(string.Concat(new object[]
				{
					"Item width is ",
					this.Items[i].InventoryItem.Width,
					" and item height is ",
					this.Items[i].InventoryItem.Height,
					". Open space is: ",
					this.OpenSpace
				}));
				if (this.Items[i].InventoryItem.Height * this.Items[i].InventoryItem.Width < this.OpenSpace)
				{
					Debug.Log("We might have enough open space to add the item to the inventory.");
					this.CheckOpenSpace();
					if (this.UseGrid == 1)
					{
						this.Items[i].transform.parent = this.LeftGridItemParent;
						float inventorySize = this.Items[i].InventoryItem.InventorySize;
						this.Items[i].transform.localScale = new Vector3(inventorySize, inventorySize, inventorySize);
						this.Items[i].transform.localEulerAngles = new Vector3(90f, 180f, 0f);
						this.Items[i].transform.localPosition = this.Items[i].InventoryItem.InventoryPosition;
						int j = 1;
						if (this.UseColumn == 1)
						{
							while (j < this.Items[i].InventoryItem.Height + 1)
							{
								this.LeftSpaces1[j] = true;
								j++;
							}
						}
						else if (this.UseColumn == 2)
						{
							while (j < this.Items[i].InventoryItem.Height + 1)
							{
								this.LeftSpaces2[j] = true;
								j++;
							}
						}
						if (this.UseColumn > 1)
						{
							this.Items[i].transform.localPosition -= new Vector3(0.05f * (float)(this.UseColumn - 1), 0f, 0f);
						}
					}
				}
				this.Items[i].Clicked = false;
			}
		}
	}

	// Token: 0x060017A8 RID: 6056 RVA: 0x000D1530 File Offset: 0x000CF730
	private void CheckOpenSpace()
	{
		this.UseColumn = 0;
		this.UseGrid = 0;
		int i;
		for (i = 1; i < this.LeftSpaces1.Length; i++)
		{
			if (this.UseGrid == 0 && !this.LeftSpaces1[i])
			{
				this.UseColumn = 1;
				this.UseGrid = 1;
			}
		}
		i = 1;
		if (this.UseGrid == 0)
		{
			while (i < this.LeftSpaces2.Length)
			{
				if (this.UseGrid == 0 && !this.LeftSpaces2[i])
				{
					this.UseColumn = 2;
					this.UseGrid = 1;
				}
				i++;
			}
		}
	}

	// Token: 0x060017A9 RID: 6057 RVA: 0x000D15BC File Offset: 0x000CF7BC
	private void UpdateHighlight()
	{
		if (this.Column == 5)
		{
			if (this.Grid == 1)
			{
				this.Grid = 2;
			}
			else
			{
				this.Grid = 1;
			}
			this.Column = 1;
		}
		else if (this.Column == 0)
		{
			if (this.Grid == 1)
			{
				this.Grid = 2;
			}
			else
			{
				this.Grid = 1;
			}
			this.Column = 4;
		}
		if (this.Row == 6)
		{
			this.Row = 1;
		}
		else if (this.Row == 0)
		{
			this.Row = 5;
		}
		if (this.Grid == 1)
		{
			this.Highlight.transform.parent = this.LeftGridHighlightParent;
		}
		else
		{
			this.Highlight.transform.parent = this.RightGridHighlightParent;
		}
		this.Highlight.localPosition = new Vector3((float)this.Column, (float)(this.Row * -1), 0f);
	}

	// Token: 0x040021BA RID: 8634
	public SimpleDetectClickScript[] Items;

	// Token: 0x040021BB RID: 8635
	public Animation SkirtAnimation;

	// Token: 0x040021BC RID: 8636
	public Animation GirlAnimation;

	// Token: 0x040021BD RID: 8637
	public GameObject Skirt;

	// Token: 0x040021BE RID: 8638
	public GameObject Girl;

	// Token: 0x040021BF RID: 8639
	public Renderer SkirtRenderer;

	// Token: 0x040021C0 RID: 8640
	public Renderer GirlRenderer;

	// Token: 0x040021C1 RID: 8641
	public Transform RightGridHighlightParent;

	// Token: 0x040021C2 RID: 8642
	public Transform LeftGridHighlightParent;

	// Token: 0x040021C3 RID: 8643
	public Transform RightGridItemParent;

	// Token: 0x040021C4 RID: 8644
	public Transform LeftGridItemParent;

	// Token: 0x040021C5 RID: 8645
	public Transform Highlight;

	// Token: 0x040021C6 RID: 8646
	public Transform RightGrid;

	// Token: 0x040021C7 RID: 8647
	public Transform LeftGrid;

	// Token: 0x040021C8 RID: 8648
	public float Alpha;

	// Token: 0x040021C9 RID: 8649
	public bool Open = true;

	// Token: 0x040021CA RID: 8650
	public int OpenSpace = 1;

	// Token: 0x040021CB RID: 8651
	public int UseColumn;

	// Token: 0x040021CC RID: 8652
	public int UseGrid;

	// Token: 0x040021CD RID: 8653
	public int Column = 1;

	// Token: 0x040021CE RID: 8654
	public int Grid = 1;

	// Token: 0x040021CF RID: 8655
	public int Row = 1;

	// Token: 0x040021D0 RID: 8656
	public bool[] LeftSpaces1;

	// Token: 0x040021D1 RID: 8657
	public bool[] LeftSpaces2;

	// Token: 0x040021D2 RID: 8658
	public bool[] LeftSpaces3;

	// Token: 0x040021D3 RID: 8659
	public bool[] LeftSpaces4;

	// Token: 0x040021D4 RID: 8660
	public bool[] RightSpaces1;

	// Token: 0x040021D5 RID: 8661
	public bool[] RightSpaces2;

	// Token: 0x040021D6 RID: 8662
	public bool[] RightSpaces3;

	// Token: 0x040021D7 RID: 8663
	public bool[] RightSpaces4;
}
