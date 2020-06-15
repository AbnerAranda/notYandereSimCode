using System;

namespace Pathfinding.Serialization
{
	// Token: 0x020005D1 RID: 1489
	public class SerializeSettings
	{
		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06002865 RID: 10341 RVA: 0x001BC287 File Offset: 0x001BA487
		public static SerializeSettings Settings
		{
			get
			{
				return new SerializeSettings
				{
					nodes = false
				};
			}
		}

		// Token: 0x04004334 RID: 17204
		public bool nodes = true;

		// Token: 0x04004335 RID: 17205
		[Obsolete("There is no support for pretty printing the json anymore")]
		public bool prettyPrint;

		// Token: 0x04004336 RID: 17206
		public bool editorSettings;
	}
}
