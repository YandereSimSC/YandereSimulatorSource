using System;
using UnityEngine;

namespace MaidDereMinigame.Malee
{
	// Token: 0x0200050C RID: 1292
	public class ReorderableAttribute : PropertyAttribute
	{
		// Token: 0x06002053 RID: 8275 RVA: 0x00186255 File Offset: 0x00184455
		public ReorderableAttribute() : this(null)
		{
		}

		// Token: 0x06002054 RID: 8276 RVA: 0x0018625E File Offset: 0x0018445E
		public ReorderableAttribute(string elementNameProperty) : this(true, true, true, elementNameProperty, null, null)
		{
		}

		// Token: 0x06002055 RID: 8277 RVA: 0x0018626C File Offset: 0x0018446C
		public ReorderableAttribute(string elementNameProperty, string elementIconPath) : this(true, true, true, elementNameProperty, null, elementIconPath)
		{
		}

		// Token: 0x06002056 RID: 8278 RVA: 0x0018627A File Offset: 0x0018447A
		public ReorderableAttribute(string elementNameProperty, string elementNameOverride, string elementIconPath) : this(true, true, true, elementNameProperty, elementNameOverride, elementIconPath)
		{
		}

		// Token: 0x06002057 RID: 8279 RVA: 0x00186288 File Offset: 0x00184488
		public ReorderableAttribute(bool add, bool remove, bool draggable, string elementNameProperty = null, string elementIconPath = null) : this(add, remove, draggable, elementNameProperty, null, elementIconPath)
		{
		}

		// Token: 0x06002058 RID: 8280 RVA: 0x00186298 File Offset: 0x00184498
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

		// Token: 0x04003E06 RID: 15878
		public bool add;

		// Token: 0x04003E07 RID: 15879
		public bool remove;

		// Token: 0x04003E08 RID: 15880
		public bool draggable;

		// Token: 0x04003E09 RID: 15881
		public bool singleLine;

		// Token: 0x04003E0A RID: 15882
		public bool paginate;

		// Token: 0x04003E0B RID: 15883
		public bool sortable;

		// Token: 0x04003E0C RID: 15884
		public int pageSize;

		// Token: 0x04003E0D RID: 15885
		public string elementNameProperty;

		// Token: 0x04003E0E RID: 15886
		public string elementNameOverride;

		// Token: 0x04003E0F RID: 15887
		public string elementIconPath;
	}
}
