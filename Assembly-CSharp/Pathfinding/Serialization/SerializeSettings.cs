using System;

namespace Pathfinding.Serialization
{
	// Token: 0x020005BB RID: 1467
	public class SerializeSettings
	{
		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x0600280F RID: 10255 RVA: 0x001B669B File Offset: 0x001B489B
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

		// Token: 0x04004256 RID: 16982
		public bool nodes = true;

		// Token: 0x04004257 RID: 16983
		[Obsolete("There is no support for pretty printing the json anymore")]
		public bool prettyPrint;

		// Token: 0x04004258 RID: 16984
		public bool editorSettings;
	}
}
