using System;
using System.Collections.Generic;
using Pathfinding.WindowsStore;

namespace Pathfinding.Serialization
{
	// Token: 0x020005D0 RID: 1488
	public class GraphMeta
	{
		// Token: 0x06002863 RID: 10339 RVA: 0x001BC214 File Offset: 0x001BA414
		public Type GetGraphType(int index)
		{
			if (string.IsNullOrEmpty(this.typeNames[index]))
			{
				return null;
			}
			Type type = WindowsStoreCompatibility.GetTypeInfo(typeof(AstarPath)).Assembly.GetType(this.typeNames[index]);
			if (!object.Equals(type, null))
			{
				return type;
			}
			throw new Exception("No graph of type '" + this.typeNames[index] + "' could be created, type does not exist");
		}

		// Token: 0x04004330 RID: 17200
		public Version version;

		// Token: 0x04004331 RID: 17201
		public int graphs;

		// Token: 0x04004332 RID: 17202
		public List<string> guids;

		// Token: 0x04004333 RID: 17203
		public List<string> typeNames;
	}
}
