using System;
using System.Collections.Generic;
using Pathfinding.Serialization;

namespace Pathfinding
{
	// Token: 0x02000560 RID: 1376
	public interface IGraphInternals
	{
		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06002454 RID: 9300
		// (set) Token: 0x06002455 RID: 9301
		string SerializedEditorSettings { get; set; }

		// Token: 0x06002456 RID: 9302
		void OnDestroy();

		// Token: 0x06002457 RID: 9303
		void DestroyAllNodes();

		// Token: 0x06002458 RID: 9304
		IEnumerable<Progress> ScanInternal();

		// Token: 0x06002459 RID: 9305
		void SerializeExtraInfo(GraphSerializationContext ctx);

		// Token: 0x0600245A RID: 9306
		void DeserializeExtraInfo(GraphSerializationContext ctx);

		// Token: 0x0600245B RID: 9307
		void PostDeserialization(GraphSerializationContext ctx);

		// Token: 0x0600245C RID: 9308
		void DeserializeSettingsCompatibility(GraphSerializationContext ctx);
	}
}
