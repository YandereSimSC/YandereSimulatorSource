using System;
using UnityEngine;

// Token: 0x0200007A RID: 122
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Internal/Property Binding")]
public class PropertyBinding : MonoBehaviour
{
	// Token: 0x06000496 RID: 1174 RVA: 0x0002C4D6 File Offset: 0x0002A6D6
	private void Start()
	{
		this.UpdateTarget();
		if (this.update == PropertyBinding.UpdateCondition.OnStart)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06000497 RID: 1175 RVA: 0x0002C4ED File Offset: 0x0002A6ED
	private void Update()
	{
		if (this.update == PropertyBinding.UpdateCondition.OnUpdate)
		{
			this.UpdateTarget();
		}
	}

	// Token: 0x06000498 RID: 1176 RVA: 0x0002C4FE File Offset: 0x0002A6FE
	private void LateUpdate()
	{
		if (this.update == PropertyBinding.UpdateCondition.OnLateUpdate)
		{
			this.UpdateTarget();
		}
	}

	// Token: 0x06000499 RID: 1177 RVA: 0x0002C50F File Offset: 0x0002A70F
	private void FixedUpdate()
	{
		if (this.update == PropertyBinding.UpdateCondition.OnFixedUpdate)
		{
			this.UpdateTarget();
		}
	}

	// Token: 0x0600049A RID: 1178 RVA: 0x0002C520 File Offset: 0x0002A720
	private void OnValidate()
	{
		if (this.source != null)
		{
			this.source.Reset();
		}
		if (this.target != null)
		{
			this.target.Reset();
		}
	}

	// Token: 0x0600049B RID: 1179 RVA: 0x0002C548 File Offset: 0x0002A748
	[ContextMenu("Update Now")]
	public void UpdateTarget()
	{
		if (this.source != null && this.target != null && this.source.isValid && this.target.isValid)
		{
			if (this.direction == PropertyBinding.Direction.SourceUpdatesTarget)
			{
				this.target.Set(this.source.Get());
				return;
			}
			if (this.direction == PropertyBinding.Direction.TargetUpdatesSource)
			{
				this.source.Set(this.target.Get());
				return;
			}
			if (this.source.GetPropertyType() == this.target.GetPropertyType())
			{
				object obj = this.source.Get();
				if (this.mLastValue == null || !this.mLastValue.Equals(obj))
				{
					this.mLastValue = obj;
					this.target.Set(obj);
					return;
				}
				obj = this.target.Get();
				if (!this.mLastValue.Equals(obj))
				{
					this.mLastValue = obj;
					this.source.Set(obj);
				}
			}
		}
	}

	// Token: 0x04000504 RID: 1284
	public PropertyReference source;

	// Token: 0x04000505 RID: 1285
	public PropertyReference target;

	// Token: 0x04000506 RID: 1286
	public PropertyBinding.Direction direction;

	// Token: 0x04000507 RID: 1287
	public PropertyBinding.UpdateCondition update = PropertyBinding.UpdateCondition.OnUpdate;

	// Token: 0x04000508 RID: 1288
	public bool editMode = true;

	// Token: 0x04000509 RID: 1289
	private object mLastValue;

	// Token: 0x02000639 RID: 1593
	[DoNotObfuscateNGUI]
	public enum UpdateCondition
	{
		// Token: 0x0400450A RID: 17674
		OnStart,
		// Token: 0x0400450B RID: 17675
		OnUpdate,
		// Token: 0x0400450C RID: 17676
		OnLateUpdate,
		// Token: 0x0400450D RID: 17677
		OnFixedUpdate
	}

	// Token: 0x0200063A RID: 1594
	[DoNotObfuscateNGUI]
	public enum Direction
	{
		// Token: 0x0400450F RID: 17679
		SourceUpdatesTarget,
		// Token: 0x04004510 RID: 17680
		TargetUpdatesSource,
		// Token: 0x04004511 RID: 17681
		BiDirectional
	}
}
