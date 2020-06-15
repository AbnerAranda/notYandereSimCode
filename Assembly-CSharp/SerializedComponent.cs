using System;

// Token: 0x020004A3 RID: 1187
[Serializable]
public struct SerializedComponent
{
	// Token: 0x04003C85 RID: 15493
	public string OwnerID;

	// Token: 0x04003C86 RID: 15494
	public string TypePath;

	// Token: 0x04003C87 RID: 15495
	public ValueDict PropertyValues;

	// Token: 0x04003C88 RID: 15496
	public ReferenceDict PropertyReferences;

	// Token: 0x04003C89 RID: 15497
	public ValueDict FieldValues;

	// Token: 0x04003C8A RID: 15498
	public ReferenceDict FieldReferences;

	// Token: 0x04003C8B RID: 15499
	public ReferenceArrayDict PropertyReferenceArrays;

	// Token: 0x04003C8C RID: 15500
	public ReferenceArrayDict FieldReferenceArrays;

	// Token: 0x04003C8D RID: 15501
	public bool IsEnabled;

	// Token: 0x04003C8E RID: 15502
	public bool IsMonoBehaviour;
}
