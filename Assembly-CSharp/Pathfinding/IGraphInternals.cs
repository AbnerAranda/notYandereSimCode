using System;
using System.Collections.Generic;
using Pathfinding.Serialization;

namespace Pathfinding
{
	// Token: 0x02000576 RID: 1398
	public interface IGraphInternals
	{
		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x060024AA RID: 9386
		// (set) Token: 0x060024AB RID: 9387
		string SerializedEditorSettings { get; set; }

		// Token: 0x060024AC RID: 9388
		void OnDestroy();

		// Token: 0x060024AD RID: 9389
		void DestroyAllNodes();

		// Token: 0x060024AE RID: 9390
		IEnumerable<Progress> ScanInternal();

		// Token: 0x060024AF RID: 9391
		void SerializeExtraInfo(GraphSerializationContext ctx);

		// Token: 0x060024B0 RID: 9392
		void DeserializeExtraInfo(GraphSerializationContext ctx);

		// Token: 0x060024B1 RID: 9393
		void PostDeserialization(GraphSerializationContext ctx);

		// Token: 0x060024B2 RID: 9394
		void DeserializeSettingsCompatibility(GraphSerializationContext ctx);
	}
}
