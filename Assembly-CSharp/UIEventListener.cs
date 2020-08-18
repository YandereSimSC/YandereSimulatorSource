using System;
using UnityEngine;

// Token: 0x02000080 RID: 128
[AddComponentMenu("NGUI/Internal/Event Listener")]
public class UIEventListener : MonoBehaviour
{
	// Token: 0x17000099 RID: 153
	// (get) Token: 0x06000508 RID: 1288 RVA: 0x00030DF0 File Offset: 0x0002EFF0
	private bool isColliderEnabled
	{
		get
		{
			if (!this.needsActiveCollider)
			{
				return true;
			}
			Collider component = base.GetComponent<Collider>();
			if (component != null)
			{
				return component.enabled;
			}
			Collider2D component2 = base.GetComponent<Collider2D>();
			return component2 != null && component2.enabled;
		}
	}

	// Token: 0x06000509 RID: 1289 RVA: 0x00030E36 File Offset: 0x0002F036
	private void OnSubmit()
	{
		if (this.isColliderEnabled && this.onSubmit != null)
		{
			this.onSubmit(base.gameObject);
		}
	}

	// Token: 0x0600050A RID: 1290 RVA: 0x00030E59 File Offset: 0x0002F059
	private void OnClick()
	{
		if (this.isColliderEnabled && this.onClick != null)
		{
			this.onClick(base.gameObject);
		}
	}

	// Token: 0x0600050B RID: 1291 RVA: 0x00030E7C File Offset: 0x0002F07C
	private void OnDoubleClick()
	{
		if (this.isColliderEnabled && this.onDoubleClick != null)
		{
			this.onDoubleClick(base.gameObject);
		}
	}

	// Token: 0x0600050C RID: 1292 RVA: 0x00030E9F File Offset: 0x0002F09F
	private void OnHover(bool isOver)
	{
		if (this.isColliderEnabled && this.onHover != null)
		{
			this.onHover(base.gameObject, isOver);
		}
	}

	// Token: 0x0600050D RID: 1293 RVA: 0x00030EC3 File Offset: 0x0002F0C3
	private void OnPress(bool isPressed)
	{
		if (this.isColliderEnabled && this.onPress != null)
		{
			this.onPress(base.gameObject, isPressed);
		}
	}

	// Token: 0x0600050E RID: 1294 RVA: 0x00030EE7 File Offset: 0x0002F0E7
	private void OnSelect(bool selected)
	{
		if (this.isColliderEnabled && this.onSelect != null)
		{
			this.onSelect(base.gameObject, selected);
		}
	}

	// Token: 0x0600050F RID: 1295 RVA: 0x00030F0B File Offset: 0x0002F10B
	private void OnScroll(float delta)
	{
		if (this.isColliderEnabled && this.onScroll != null)
		{
			this.onScroll(base.gameObject, delta);
		}
	}

	// Token: 0x06000510 RID: 1296 RVA: 0x00030F2F File Offset: 0x0002F12F
	private void OnDragStart()
	{
		if (this.onDragStart != null)
		{
			this.onDragStart(base.gameObject);
		}
	}

	// Token: 0x06000511 RID: 1297 RVA: 0x00030F4A File Offset: 0x0002F14A
	private void OnDrag(Vector2 delta)
	{
		if (this.onDrag != null)
		{
			this.onDrag(base.gameObject, delta);
		}
	}

	// Token: 0x06000512 RID: 1298 RVA: 0x00030F66 File Offset: 0x0002F166
	private void OnDragOver()
	{
		if (this.isColliderEnabled && this.onDragOver != null)
		{
			this.onDragOver(base.gameObject);
		}
	}

	// Token: 0x06000513 RID: 1299 RVA: 0x00030F89 File Offset: 0x0002F189
	private void OnDragOut()
	{
		if (this.isColliderEnabled && this.onDragOut != null)
		{
			this.onDragOut(base.gameObject);
		}
	}

	// Token: 0x06000514 RID: 1300 RVA: 0x00030FAC File Offset: 0x0002F1AC
	private void OnDragEnd()
	{
		if (this.onDragEnd != null)
		{
			this.onDragEnd(base.gameObject);
		}
	}

	// Token: 0x06000515 RID: 1301 RVA: 0x00030FC7 File Offset: 0x0002F1C7
	private void OnDrop(GameObject go)
	{
		if (this.isColliderEnabled && this.onDrop != null)
		{
			this.onDrop(base.gameObject, go);
		}
	}

	// Token: 0x06000516 RID: 1302 RVA: 0x00030FEB File Offset: 0x0002F1EB
	private void OnKey(KeyCode key)
	{
		if (this.isColliderEnabled && this.onKey != null)
		{
			this.onKey(base.gameObject, key);
		}
	}

	// Token: 0x06000517 RID: 1303 RVA: 0x0003100F File Offset: 0x0002F20F
	private void OnTooltip(bool show)
	{
		if (this.isColliderEnabled && this.onTooltip != null)
		{
			this.onTooltip(base.gameObject, show);
		}
	}

	// Token: 0x06000518 RID: 1304 RVA: 0x00031034 File Offset: 0x0002F234
	public void Clear()
	{
		this.onSubmit = null;
		this.onClick = null;
		this.onDoubleClick = null;
		this.onHover = null;
		this.onPress = null;
		this.onSelect = null;
		this.onScroll = null;
		this.onDragStart = null;
		this.onDrag = null;
		this.onDragOver = null;
		this.onDragOut = null;
		this.onDragEnd = null;
		this.onDrop = null;
		this.onKey = null;
		this.onTooltip = null;
	}

	// Token: 0x06000519 RID: 1305 RVA: 0x000310AC File Offset: 0x0002F2AC
	public static UIEventListener Get(GameObject go)
	{
		UIEventListener uieventListener = go.GetComponent<UIEventListener>();
		if (uieventListener == null)
		{
			uieventListener = go.AddComponent<UIEventListener>();
		}
		return uieventListener;
	}

	// Token: 0x04000554 RID: 1364
	public object parameter;

	// Token: 0x04000555 RID: 1365
	public UIEventListener.VoidDelegate onSubmit;

	// Token: 0x04000556 RID: 1366
	public UIEventListener.VoidDelegate onClick;

	// Token: 0x04000557 RID: 1367
	public UIEventListener.VoidDelegate onDoubleClick;

	// Token: 0x04000558 RID: 1368
	public UIEventListener.BoolDelegate onHover;

	// Token: 0x04000559 RID: 1369
	public UIEventListener.BoolDelegate onPress;

	// Token: 0x0400055A RID: 1370
	public UIEventListener.BoolDelegate onSelect;

	// Token: 0x0400055B RID: 1371
	public UIEventListener.FloatDelegate onScroll;

	// Token: 0x0400055C RID: 1372
	public UIEventListener.VoidDelegate onDragStart;

	// Token: 0x0400055D RID: 1373
	public UIEventListener.VectorDelegate onDrag;

	// Token: 0x0400055E RID: 1374
	public UIEventListener.VoidDelegate onDragOver;

	// Token: 0x0400055F RID: 1375
	public UIEventListener.VoidDelegate onDragOut;

	// Token: 0x04000560 RID: 1376
	public UIEventListener.VoidDelegate onDragEnd;

	// Token: 0x04000561 RID: 1377
	public UIEventListener.ObjectDelegate onDrop;

	// Token: 0x04000562 RID: 1378
	public UIEventListener.KeyCodeDelegate onKey;

	// Token: 0x04000563 RID: 1379
	public UIEventListener.BoolDelegate onTooltip;

	// Token: 0x04000564 RID: 1380
	public bool needsActiveCollider = true;

	// Token: 0x02000644 RID: 1604
	// (Invoke) Token: 0x06002AB0 RID: 10928
	public delegate void VoidDelegate(GameObject go);

	// Token: 0x02000645 RID: 1605
	// (Invoke) Token: 0x06002AB4 RID: 10932
	public delegate void BoolDelegate(GameObject go, bool state);

	// Token: 0x02000646 RID: 1606
	// (Invoke) Token: 0x06002AB8 RID: 10936
	public delegate void FloatDelegate(GameObject go, float delta);

	// Token: 0x02000647 RID: 1607
	// (Invoke) Token: 0x06002ABC RID: 10940
	public delegate void VectorDelegate(GameObject go, Vector2 delta);

	// Token: 0x02000648 RID: 1608
	// (Invoke) Token: 0x06002AC0 RID: 10944
	public delegate void ObjectDelegate(GameObject go, GameObject obj);

	// Token: 0x02000649 RID: 1609
	// (Invoke) Token: 0x06002AC4 RID: 10948
	public delegate void KeyCodeDelegate(GameObject go, KeyCode key);
}
