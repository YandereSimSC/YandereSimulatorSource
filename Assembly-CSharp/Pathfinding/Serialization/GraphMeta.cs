using System;
using System.Collections.Generic;
using Pathfinding.WindowsStore;

namespace Pathfinding.Serialization
{
	// Token: 0x020005BA RID: 1466
	public class GraphMeta
	{
		// Token: 0x0600280D RID: 10253 RVA: 0x001B6628 File Offset: 0x001B4828
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

		// Token: 0x04004252 RID: 16978
		public Version version;

		// Token: 0x04004253 RID: 16979
		public int graphs;

		// Token: 0x04004254 RID: 16980
		public List<string> guids;

		// Token: 0x04004255 RID: 16981
		public List<string> typeNames;
	}
}
