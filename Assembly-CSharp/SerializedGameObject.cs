using System;

// Token: 0x020004A4 RID: 1188
[Serializable]
public struct SerializedGameObject
{
	// Token: 0x04003C8F RID: 15503
	public bool ActiveInHierarchy;

	// Token: 0x04003C90 RID: 15504
	public bool ActiveSelf;

	// Token: 0x04003C91 RID: 15505
	public bool IsStatic;

	// Token: 0x04003C92 RID: 15506
	public int Layer;

	// Token: 0x04003C93 RID: 15507
	public string Tag;

	// Token: 0x04003C94 RID: 15508
	public string Name;

	// Token: 0x04003C95 RID: 15509
	public string ObjectID;

	// Token: 0x04003C96 RID: 15510
	public SerializedComponent[] SerializedComponents;
}
