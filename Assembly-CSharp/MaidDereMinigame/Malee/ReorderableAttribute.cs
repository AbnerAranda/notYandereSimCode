using System;
using UnityEngine;

namespace MaidDereMinigame.Malee
{
	// Token: 0x0200051C RID: 1308
	public class ReorderableAttribute : PropertyAttribute
	{
		// Token: 0x06002095 RID: 8341 RVA: 0x0018B771 File Offset: 0x00189971
		public ReorderableAttribute() : this(null)
		{
		}

		// Token: 0x06002096 RID: 8342 RVA: 0x0018B77A File Offset: 0x0018997A
		public ReorderableAttribute(string elementNameProperty) : this(true, true, true, elementNameProperty, null, null)
		{
		}

		// Token: 0x06002097 RID: 8343 RVA: 0x0018B788 File Offset: 0x00189988
		public ReorderableAttribute(string elementNameProperty, string elementIconPath) : this(true, true, true, elementNameProperty, null, elementIconPath)
		{
		}

		// Token: 0x06002098 RID: 8344 RVA: 0x0018B796 File Offset: 0x00189996
		public ReorderableAttribute(string elementNameProperty, string elementNameOverride, string elementIconPath) : this(true, true, true, elementNameProperty, elementNameOverride, elementIconPath)
		{
		}

		// Token: 0x06002099 RID: 8345 RVA: 0x0018B7A4 File Offset: 0x001899A4
		public ReorderableAttribute(bool add, bool remove, bool draggable, string elementNameProperty = null, string elementIconPath = null) : this(add, remove, draggable, elementNameProperty, null, elementIconPath)
		{
		}

		// Token: 0x0600209A RID: 8346 RVA: 0x0018B7B4 File Offset: 0x001899B4
		public ReorderableAttribute(bool add, bool remove, bool draggable, string elementNameProperty = null, string elementNameOverride = null, string elementIconPath = null)
		{
			this.add = add;
			this.remove = remove;
			this.draggable = draggable;
			this.sortable = true;
			this.elementNameProperty = elementNameProperty;
			this.elementNameOverride = elementNameOverride;
			this.elementIconPath = elementIconPath;
		}

		// Token: 0x04003EB6 RID: 16054
		public bool add;

		// Token: 0x04003EB7 RID: 16055
		public bool remove;

		// Token: 0x04003EB8 RID: 16056
		public bool draggable;

		// Token: 0x04003EB9 RID: 16057
		public bool singleLine;

		// Token: 0x04003EBA RID: 16058
		public bool paginate;

		// Token: 0x04003EBB RID: 16059
		public bool sortable;

		// Token: 0x04003EBC RID: 16060
		public int pageSize;

		// Token: 0x04003EBD RID: 16061
		public string elementNameProperty;

		// Token: 0x04003EBE RID: 16062
		public string elementNameOverride;

		// Token: 0x04003EBF RID: 16063
		public string elementIconPath;
	}
}
