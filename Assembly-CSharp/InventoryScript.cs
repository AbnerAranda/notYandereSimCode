using System;
using System.Globalization;
using UnityEngine;

// Token: 0x0200030E RID: 782
public class InventoryScript : MonoBehaviour
{
	// Token: 0x060017A3 RID: 6051 RVA: 0x000D0E50 File Offset: 0x000CF050
	private void Start()
	{
		this.PantyShots = PlayerGlobals.PantyShots;
		this.Money = PlayerGlobals.Money;
		this.UpdateMoney();
	}

	// Token: 0x060017A4 RID: 6052 RVA: 0x000D0E6E File Offset: 0x000CF06E
	public void UpdateMoney()
	{
		this.MoneyLabel.text = "$" + this.Money.ToString("F2", NumberFormatInfo.InvariantInfo);
	}

	// Token: 0x04002194 RID: 8596
	public SchemesScript Schemes;

	// Token: 0x04002195 RID: 8597
	public bool ModifiedUniform;

	// Token: 0x04002196 RID: 8598
	public bool DirectionalMic;

	// Token: 0x04002197 RID: 8599
	public bool DuplicateSheet;

	// Token: 0x04002198 RID: 8600
	public bool AnswerSheet;

	// Token: 0x04002199 RID: 8601
	public bool MaskingTape;

	// Token: 0x0400219A RID: 8602
	public bool RivalPhone;

	// Token: 0x0400219B RID: 8603
	public bool LockPick;

	// Token: 0x0400219C RID: 8604
	public bool Headset;

	// Token: 0x0400219D RID: 8605
	public bool FakeID;

	// Token: 0x0400219E RID: 8606
	public bool IDCard;

	// Token: 0x0400219F RID: 8607
	public bool Book;

	// Token: 0x040021A0 RID: 8608
	public bool AmnesiaBomb;

	// Token: 0x040021A1 RID: 8609
	public bool SmokeBomb;

	// Token: 0x040021A2 RID: 8610
	public bool StinkBomb;

	// Token: 0x040021A3 RID: 8611
	public bool LethalPoison;

	// Token: 0x040021A4 RID: 8612
	public bool ChemicalPoison;

	// Token: 0x040021A5 RID: 8613
	public bool EmeticPoison;

	// Token: 0x040021A6 RID: 8614
	public bool RatPoison;

	// Token: 0x040021A7 RID: 8615
	public bool HeadachePoison;

	// Token: 0x040021A8 RID: 8616
	public bool Tranquilizer;

	// Token: 0x040021A9 RID: 8617
	public bool Sedative;

	// Token: 0x040021AA RID: 8618
	public bool Cigs;

	// Token: 0x040021AB RID: 8619
	public bool Ring;

	// Token: 0x040021AC RID: 8620
	public bool Rose;

	// Token: 0x040021AD RID: 8621
	public bool Sake;

	// Token: 0x040021AE RID: 8622
	public bool Soda;

	// Token: 0x040021AF RID: 8623
	public bool Bra;

	// Token: 0x040021B0 RID: 8624
	public bool CabinetKey;

	// Token: 0x040021B1 RID: 8625
	public bool CaseKey;

	// Token: 0x040021B2 RID: 8626
	public bool SafeKey;

	// Token: 0x040021B3 RID: 8627
	public bool ShedKey;

	// Token: 0x040021B4 RID: 8628
	public int MysteriousKeys;

	// Token: 0x040021B5 RID: 8629
	public int RivalPhoneID;

	// Token: 0x040021B6 RID: 8630
	public int PantyShots;

	// Token: 0x040021B7 RID: 8631
	public float Money;

	// Token: 0x040021B8 RID: 8632
	public bool[] ShrineCollectibles;

	// Token: 0x040021B9 RID: 8633
	public UILabel MoneyLabel;
}
