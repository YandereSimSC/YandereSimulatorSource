using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x020000A2 RID: 162
[AddComponentMenu("NGUI/UI/Input Field")]
public class UIInput : MonoBehaviour
{
	// Token: 0x1700015C RID: 348
	// (get) Token: 0x06000784 RID: 1924 RVA: 0x0003E792 File Offset: 0x0003C992
	// (set) Token: 0x06000785 RID: 1925 RVA: 0x0003E7A8 File Offset: 0x0003C9A8
	public string defaultText
	{
		get
		{
			if (this.mDoInit)
			{
				this.Init();
			}
			return this.mDefaultText;
		}
		set
		{
			if (this.mDoInit)
			{
				this.Init();
			}
			this.mDefaultText = value;
			this.UpdateLabel();
		}
	}

	// Token: 0x1700015D RID: 349
	// (get) Token: 0x06000786 RID: 1926 RVA: 0x0003E7C5 File Offset: 0x0003C9C5
	// (set) Token: 0x06000787 RID: 1927 RVA: 0x0003E7DB File Offset: 0x0003C9DB
	public Color defaultColor
	{
		get
		{
			if (this.mDoInit)
			{
				this.Init();
			}
			return this.mDefaultColor;
		}
		set
		{
			this.mDefaultColor = value;
			if (!this.isSelected)
			{
				this.label.color = value;
			}
		}
	}

	// Token: 0x1700015E RID: 350
	// (get) Token: 0x06000788 RID: 1928 RVA: 0x0003E7F8 File Offset: 0x0003C9F8
	public bool inputShouldBeHidden
	{
		get
		{
			return this.hideInput && this.label != null && !this.label.multiLine && this.inputType != UIInput.InputType.Password;
		}
	}

	// Token: 0x1700015F RID: 351
	// (get) Token: 0x06000789 RID: 1929 RVA: 0x0003E82B File Offset: 0x0003CA2B
	// (set) Token: 0x0600078A RID: 1930 RVA: 0x0003E833 File Offset: 0x0003CA33
	[Obsolete("Use UIInput.value instead")]
	public string text
	{
		get
		{
			return this.value;
		}
		set
		{
			this.value = value;
		}
	}

	// Token: 0x17000160 RID: 352
	// (get) Token: 0x0600078B RID: 1931 RVA: 0x0003E83C File Offset: 0x0003CA3C
	// (set) Token: 0x0600078C RID: 1932 RVA: 0x0003E852 File Offset: 0x0003CA52
	public string value
	{
		get
		{
			if (this.mDoInit)
			{
				this.Init();
			}
			return this.mValue;
		}
		set
		{
			this.Set(value, true);
		}
	}

	// Token: 0x0600078D RID: 1933 RVA: 0x0003E85C File Offset: 0x0003CA5C
	public void Set(string value, bool notify = true)
	{
		if (this.mDoInit)
		{
			this.Init();
		}
		if (value == this.value)
		{
			return;
		}
		UIInput.mDrawStart = 0;
		value = this.Validate(value);
		if (this.mValue != value)
		{
			this.mValue = value;
			this.mLoadSavedValue = false;
			if (this.isSelected)
			{
				if (string.IsNullOrEmpty(value))
				{
					this.mSelectionStart = 0;
					this.mSelectionEnd = 0;
				}
				else
				{
					this.mSelectionStart = value.Length;
					this.mSelectionEnd = this.mSelectionStart;
				}
			}
			else if (this.mStarted)
			{
				this.SaveToPlayerPrefs(value);
			}
			this.UpdateLabel();
			if (notify)
			{
				this.ExecuteOnChange();
			}
		}
	}

	// Token: 0x17000161 RID: 353
	// (get) Token: 0x0600078E RID: 1934 RVA: 0x0003E909 File Offset: 0x0003CB09
	// (set) Token: 0x0600078F RID: 1935 RVA: 0x0003E911 File Offset: 0x0003CB11
	[Obsolete("Use UIInput.isSelected instead")]
	public bool selected
	{
		get
		{
			return this.isSelected;
		}
		set
		{
			this.isSelected = value;
		}
	}

	// Token: 0x17000162 RID: 354
	// (get) Token: 0x06000790 RID: 1936 RVA: 0x0003E91A File Offset: 0x0003CB1A
	// (set) Token: 0x06000791 RID: 1937 RVA: 0x0003E927 File Offset: 0x0003CB27
	public bool isSelected
	{
		get
		{
			return UIInput.selection == this;
		}
		set
		{
			if (!value)
			{
				if (this.isSelected)
				{
					UICamera.selectedObject = null;
					return;
				}
			}
			else
			{
				UICamera.selectedObject = base.gameObject;
			}
		}
	}

	// Token: 0x17000163 RID: 355
	// (get) Token: 0x06000792 RID: 1938 RVA: 0x0003E946 File Offset: 0x0003CB46
	// (set) Token: 0x06000793 RID: 1939 RVA: 0x0003E962 File Offset: 0x0003CB62
	public int cursorPosition
	{
		get
		{
			if (!this.isSelected)
			{
				return this.value.Length;
			}
			return this.mSelectionEnd;
		}
		set
		{
			if (this.isSelected)
			{
				this.mSelectionEnd = value;
				this.UpdateLabel();
			}
		}
	}

	// Token: 0x17000164 RID: 356
	// (get) Token: 0x06000794 RID: 1940 RVA: 0x0003E979 File Offset: 0x0003CB79
	// (set) Token: 0x06000795 RID: 1941 RVA: 0x0003E995 File Offset: 0x0003CB95
	public int selectionStart
	{
		get
		{
			if (!this.isSelected)
			{
				return this.value.Length;
			}
			return this.mSelectionStart;
		}
		set
		{
			if (this.isSelected)
			{
				this.mSelectionStart = value;
				this.UpdateLabel();
			}
		}
	}

	// Token: 0x17000165 RID: 357
	// (get) Token: 0x06000796 RID: 1942 RVA: 0x0003E946 File Offset: 0x0003CB46
	// (set) Token: 0x06000797 RID: 1943 RVA: 0x0003E962 File Offset: 0x0003CB62
	public int selectionEnd
	{
		get
		{
			if (!this.isSelected)
			{
				return this.value.Length;
			}
			return this.mSelectionEnd;
		}
		set
		{
			if (this.isSelected)
			{
				this.mSelectionEnd = value;
				this.UpdateLabel();
			}
		}
	}

	// Token: 0x17000166 RID: 358
	// (get) Token: 0x06000798 RID: 1944 RVA: 0x0003E9AC File Offset: 0x0003CBAC
	public UITexture caret
	{
		get
		{
			return this.mCaret;
		}
	}

	// Token: 0x06000799 RID: 1945 RVA: 0x0003E9B4 File Offset: 0x0003CBB4
	public string Validate(string val)
	{
		if (string.IsNullOrEmpty(val))
		{
			return "";
		}
		StringBuilder stringBuilder = new StringBuilder(val.Length);
		foreach (char c in val)
		{
			if (this.onValidate != null)
			{
				c = this.onValidate(stringBuilder.ToString(), stringBuilder.Length, c);
			}
			else if (this.validation != UIInput.Validation.None)
			{
				c = this.Validate(stringBuilder.ToString(), stringBuilder.Length, c);
			}
			if (c != '\0')
			{
				stringBuilder.Append(c);
			}
		}
		if (this.characterLimit > 0 && stringBuilder.Length > this.characterLimit)
		{
			return stringBuilder.ToString(0, this.characterLimit);
		}
		return stringBuilder.ToString();
	}

	// Token: 0x0600079A RID: 1946 RVA: 0x0003EA6C File Offset: 0x0003CC6C
	public void Start()
	{
		if (this.mStarted)
		{
			return;
		}
		if (this.selectOnTab != null)
		{
			if (base.GetComponent<UIKeyNavigation>() == null)
			{
				base.gameObject.AddComponent<UIKeyNavigation>().onDown = this.selectOnTab;
			}
			this.selectOnTab = null;
			NGUITools.SetDirty(this, "last change");
		}
		if (this.mLoadSavedValue && !string.IsNullOrEmpty(this.savedAs))
		{
			this.LoadValue();
		}
		else
		{
			this.value = this.mValue.Replace("\\n", "\n");
		}
		this.mStarted = true;
	}

	// Token: 0x0600079B RID: 1947 RVA: 0x0003EB08 File Offset: 0x0003CD08
	protected void Init()
	{
		if (this.mDoInit && this.label != null)
		{
			this.mDoInit = false;
			this.mDefaultText = this.label.text;
			this.mDefaultColor = this.label.color;
			this.mEllipsis = this.label.overflowEllipsis;
			if (this.label.alignment == NGUIText.Alignment.Justified)
			{
				this.label.alignment = NGUIText.Alignment.Left;
				Debug.LogWarning("Input fields using labels with justified alignment are not supported at this time", this);
			}
			this.mAlignment = this.label.alignment;
			this.mPosition = this.label.cachedTransform.localPosition.x;
			this.UpdateLabel();
		}
	}

	// Token: 0x0600079C RID: 1948 RVA: 0x0003EBC2 File Offset: 0x0003CDC2
	protected void SaveToPlayerPrefs(string val)
	{
		if (!string.IsNullOrEmpty(this.savedAs))
		{
			if (string.IsNullOrEmpty(val))
			{
				PlayerPrefs.DeleteKey(this.savedAs);
				return;
			}
			PlayerPrefs.SetString(this.savedAs, val);
		}
	}

	// Token: 0x0600079D RID: 1949 RVA: 0x0003EBF4 File Offset: 0x0003CDF4
	protected virtual void OnSelect(bool isSelected)
	{
		if (isSelected)
		{
			if (this.label != null)
			{
				this.label.supportEncoding = false;
			}
			if (this.mOnGUI == null)
			{
				this.mOnGUI = base.gameObject.AddComponent<UIInputOnGUI>();
			}
			this.OnSelectEvent();
			return;
		}
		if (this.mOnGUI != null)
		{
			UnityEngine.Object.Destroy(this.mOnGUI);
			this.mOnGUI = null;
		}
		this.OnDeselectEvent();
	}

	// Token: 0x0600079E RID: 1950 RVA: 0x0003EC6C File Offset: 0x0003CE6C
	protected void OnSelectEvent()
	{
		this.mSelectTime = Time.frameCount;
		UIInput.selection = this;
		if (this.mDoInit)
		{
			this.Init();
		}
		if (this.label != null)
		{
			this.mEllipsis = this.label.overflowEllipsis;
			this.label.overflowEllipsis = false;
		}
		if (this.label != null && NGUITools.GetActive(this))
		{
			this.mSelectMe = Time.frameCount;
		}
	}

	// Token: 0x0600079F RID: 1951 RVA: 0x0003ECE4 File Offset: 0x0003CEE4
	protected void OnDeselectEvent()
	{
		if (this.mDoInit)
		{
			this.Init();
		}
		if (this.label != null)
		{
			this.label.overflowEllipsis = this.mEllipsis;
		}
		if (this.label != null && NGUITools.GetActive(this))
		{
			this.mValue = this.value;
			if (string.IsNullOrEmpty(this.mValue))
			{
				this.label.text = this.mDefaultText;
				this.label.color = this.mDefaultColor;
			}
			else
			{
				this.label.text = this.mValue;
			}
			Input.imeCompositionMode = IMECompositionMode.Auto;
			this.label.alignment = this.mAlignment;
		}
		UIInput.selection = null;
		this.UpdateLabel();
		if (this.submitOnUnselect)
		{
			this.Submit();
		}
	}

	// Token: 0x060007A0 RID: 1952 RVA: 0x0003EDB4 File Offset: 0x0003CFB4
	protected virtual void Update()
	{
		if (!this.isSelected || this.mSelectTime == Time.frameCount)
		{
			return;
		}
		if (this.mDoInit)
		{
			this.Init();
		}
		if (this.mSelectMe != -1 && this.mSelectMe != Time.frameCount)
		{
			this.mSelectMe = -1;
			this.mSelectionEnd = (string.IsNullOrEmpty(this.mValue) ? 0 : this.mValue.Length);
			UIInput.mDrawStart = 0;
			this.mSelectionStart = (this.selectAllTextOnFocus ? 0 : this.mSelectionEnd);
			this.label.color = this.activeTextColor;
			Vector2 vector = (UICamera.current != null && UICamera.current.cachedCamera != null) ? UICamera.current.cachedCamera.WorldToScreenPoint(this.label.worldCorners[0]) : this.label.worldCorners[0];
			vector.y = (float)Screen.height - vector.y;
			Input.imeCompositionMode = IMECompositionMode.On;
			Input.compositionCursorPos = vector;
			this.UpdateLabel();
			if (string.IsNullOrEmpty(Input.inputString))
			{
				return;
			}
		}
		string compositionString = Input.compositionString;
		if (string.IsNullOrEmpty(compositionString) && !string.IsNullOrEmpty(Input.inputString))
		{
			foreach (char c in Input.inputString)
			{
				if (c >= ' ' && c != '' && c != '' && c != '' && c != '' && c != '')
				{
					this.Insert(c.ToString());
				}
			}
		}
		if (UIInput.mLastIME != compositionString)
		{
			this.mSelectionEnd = (string.IsNullOrEmpty(compositionString) ? this.mSelectionStart : (this.mValue.Length + compositionString.Length));
			UIInput.mLastIME = compositionString;
			this.UpdateLabel();
			this.ExecuteOnChange();
		}
		if (this.mCaret != null && this.mNextBlink < RealTime.time)
		{
			this.mNextBlink = RealTime.time + 0.5f;
			this.mCaret.enabled = !this.mCaret.enabled;
		}
		if (this.isSelected && this.mLastAlpha != this.label.finalAlpha)
		{
			this.UpdateLabel();
		}
		if (this.mCam == null)
		{
			this.mCam = UICamera.FindCameraForLayer(base.gameObject.layer);
		}
		if (this.mCam != null)
		{
			bool flag = false;
			if (this.label.multiLine)
			{
				bool flag2 = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
				if (this.onReturnKey == UIInput.OnReturnKey.Submit)
				{
					flag = flag2;
				}
				else
				{
					flag = !flag2;
				}
			}
			if (UICamera.GetKeyDown(this.mCam.submitKey0) || (this.mCam.submitKey0 == KeyCode.Return && UICamera.GetKeyDown(KeyCode.KeypadEnter)))
			{
				if (flag)
				{
					this.Insert("\n");
				}
				else
				{
					if (UICamera.controller.current != null)
					{
						UICamera.controller.clickNotification = UICamera.ClickNotification.None;
					}
					UICamera.currentKey = this.mCam.submitKey0;
					this.Submit();
				}
			}
			if (UICamera.GetKeyDown(this.mCam.submitKey1) || (this.mCam.submitKey1 == KeyCode.Return && UICamera.GetKeyDown(KeyCode.KeypadEnter)))
			{
				if (flag)
				{
					this.Insert("\n");
				}
				else
				{
					if (UICamera.controller.current != null)
					{
						UICamera.controller.clickNotification = UICamera.ClickNotification.None;
					}
					UICamera.currentKey = this.mCam.submitKey1;
					this.Submit();
				}
			}
			if (!this.mCam.useKeyboard && UICamera.GetKeyUp(KeyCode.Tab))
			{
				this.OnKey(KeyCode.Tab);
			}
		}
	}

	// Token: 0x060007A1 RID: 1953 RVA: 0x0003F198 File Offset: 0x0003D398
	private void OnKey(KeyCode key)
	{
		int frameCount = Time.frameCount;
		if (UIInput.mIgnoreKey == frameCount)
		{
			return;
		}
		if (this.mCam != null && (key == this.mCam.cancelKey0 || key == this.mCam.cancelKey1))
		{
			UIInput.mIgnoreKey = frameCount;
			this.isSelected = false;
			return;
		}
		if (key == KeyCode.Tab)
		{
			UIInput.mIgnoreKey = frameCount;
			this.isSelected = false;
			UIKeyNavigation component = base.GetComponent<UIKeyNavigation>();
			if (component != null)
			{
				component.OnKey(KeyCode.Tab);
			}
		}
	}

	// Token: 0x060007A2 RID: 1954 RVA: 0x0003F216 File Offset: 0x0003D416
	protected void DoBackspace()
	{
		if (!string.IsNullOrEmpty(this.mValue))
		{
			if (this.mSelectionStart == this.mSelectionEnd)
			{
				if (this.mSelectionStart < 1)
				{
					return;
				}
				this.mSelectionEnd--;
			}
			this.Insert("");
		}
	}

	// Token: 0x060007A3 RID: 1955 RVA: 0x0003F258 File Offset: 0x0003D458
	public virtual bool ProcessEvent(Event ev)
	{
		if (this.label == null)
		{
			return false;
		}
		RuntimePlatform platform = Application.platform;
		bool flag = (platform == RuntimePlatform.OSXEditor || platform == RuntimePlatform.OSXPlayer) ? ((ev.modifiers & EventModifiers.Command) > EventModifiers.None) : ((ev.modifiers & EventModifiers.Control) > EventModifiers.None);
		if ((ev.modifiers & EventModifiers.Alt) != EventModifiers.None)
		{
			flag = false;
		}
		bool flag2 = (ev.modifiers & EventModifiers.Shift) > EventModifiers.None;
		KeyCode keyCode = ev.keyCode;
		if (keyCode <= KeyCode.C)
		{
			if (keyCode == KeyCode.Backspace)
			{
				ev.Use();
				this.DoBackspace();
				return true;
			}
			if (keyCode == KeyCode.A)
			{
				if (flag)
				{
					ev.Use();
					this.mSelectionStart = 0;
					this.mSelectionEnd = this.mValue.Length;
					this.UpdateLabel();
				}
				return true;
			}
			if (keyCode == KeyCode.C)
			{
				if (flag)
				{
					ev.Use();
					NGUITools.clipboard = this.GetSelection();
				}
				return true;
			}
		}
		else if (keyCode <= KeyCode.X)
		{
			if (keyCode == KeyCode.V)
			{
				if (flag)
				{
					ev.Use();
					this.Insert(NGUITools.clipboard);
				}
				return true;
			}
			if (keyCode == KeyCode.X)
			{
				if (flag)
				{
					ev.Use();
					NGUITools.clipboard = this.GetSelection();
					this.Insert("");
				}
				return true;
			}
		}
		else
		{
			if (keyCode == KeyCode.Delete)
			{
				ev.Use();
				if (!string.IsNullOrEmpty(this.mValue))
				{
					if (this.mSelectionStart == this.mSelectionEnd)
					{
						if (this.mSelectionStart >= this.mValue.Length)
						{
							return true;
						}
						this.mSelectionEnd++;
					}
					this.Insert("");
				}
				return true;
			}
			switch (keyCode)
			{
			case KeyCode.UpArrow:
				ev.Use();
				if (this.onUpArrow != null)
				{
					this.onUpArrow();
				}
				else if (!string.IsNullOrEmpty(this.mValue))
				{
					this.mSelectionEnd = this.label.GetCharacterIndex(this.mSelectionEnd, KeyCode.UpArrow);
					if (this.mSelectionEnd != 0)
					{
						this.mSelectionEnd += UIInput.mDrawStart;
					}
					if (!flag2)
					{
						this.mSelectionStart = this.mSelectionEnd;
					}
					this.UpdateLabel();
				}
				return true;
			case KeyCode.DownArrow:
				ev.Use();
				if (this.onDownArrow != null)
				{
					this.onDownArrow();
				}
				else if (!string.IsNullOrEmpty(this.mValue))
				{
					this.mSelectionEnd = this.label.GetCharacterIndex(this.mSelectionEnd, KeyCode.DownArrow);
					if (this.mSelectionEnd != this.label.processedText.Length)
					{
						this.mSelectionEnd += UIInput.mDrawStart;
					}
					else
					{
						this.mSelectionEnd = this.mValue.Length;
					}
					if (!flag2)
					{
						this.mSelectionStart = this.mSelectionEnd;
					}
					this.UpdateLabel();
				}
				return true;
			case KeyCode.RightArrow:
				ev.Use();
				if (!string.IsNullOrEmpty(this.mValue))
				{
					this.mSelectionEnd = Mathf.Min(this.mSelectionEnd + 1, this.mValue.Length);
					if (!flag2)
					{
						this.mSelectionStart = this.mSelectionEnd;
					}
					this.UpdateLabel();
				}
				return true;
			case KeyCode.LeftArrow:
				ev.Use();
				if (!string.IsNullOrEmpty(this.mValue))
				{
					this.mSelectionEnd = Mathf.Max(this.mSelectionEnd - 1, 0);
					if (!flag2)
					{
						this.mSelectionStart = this.mSelectionEnd;
					}
					this.UpdateLabel();
				}
				return true;
			case KeyCode.Home:
				ev.Use();
				if (!string.IsNullOrEmpty(this.mValue))
				{
					if (this.label.multiLine)
					{
						this.mSelectionEnd = this.label.GetCharacterIndex(this.mSelectionEnd, KeyCode.Home);
					}
					else
					{
						this.mSelectionEnd = 0;
					}
					if (!flag2)
					{
						this.mSelectionStart = this.mSelectionEnd;
					}
					this.UpdateLabel();
				}
				return true;
			case KeyCode.End:
				ev.Use();
				if (!string.IsNullOrEmpty(this.mValue))
				{
					if (this.label.multiLine)
					{
						this.mSelectionEnd = this.label.GetCharacterIndex(this.mSelectionEnd, KeyCode.End);
					}
					else
					{
						this.mSelectionEnd = this.mValue.Length;
					}
					if (!flag2)
					{
						this.mSelectionStart = this.mSelectionEnd;
					}
					this.UpdateLabel();
				}
				return true;
			case KeyCode.PageUp:
				ev.Use();
				if (!string.IsNullOrEmpty(this.mValue))
				{
					this.mSelectionEnd = 0;
					if (!flag2)
					{
						this.mSelectionStart = this.mSelectionEnd;
					}
					this.UpdateLabel();
				}
				return true;
			case KeyCode.PageDown:
				ev.Use();
				if (!string.IsNullOrEmpty(this.mValue))
				{
					this.mSelectionEnd = this.mValue.Length;
					if (!flag2)
					{
						this.mSelectionStart = this.mSelectionEnd;
					}
					this.UpdateLabel();
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x060007A4 RID: 1956 RVA: 0x0003F6C8 File Offset: 0x0003D8C8
	protected virtual void Insert(string text)
	{
		string leftText = this.GetLeftText();
		string rightText = this.GetRightText();
		int length = rightText.Length;
		StringBuilder stringBuilder = new StringBuilder(leftText.Length + rightText.Length + text.Length);
		stringBuilder.Append(leftText);
		int i = 0;
		int length2 = text.Length;
		while (i < length2)
		{
			char c = text[i];
			if (c == '\b')
			{
				this.DoBackspace();
			}
			else
			{
				if (this.characterLimit > 0 && stringBuilder.Length + length >= this.characterLimit)
				{
					break;
				}
				if (this.onValidate != null)
				{
					c = this.onValidate(stringBuilder.ToString(), stringBuilder.Length, c);
				}
				else if (this.validation != UIInput.Validation.None)
				{
					c = this.Validate(stringBuilder.ToString(), stringBuilder.Length, c);
				}
				if (c != '\0')
				{
					stringBuilder.Append(c);
				}
			}
			i++;
		}
		this.mSelectionStart = stringBuilder.Length;
		this.mSelectionEnd = this.mSelectionStart;
		int j = 0;
		int length3 = rightText.Length;
		while (j < length3)
		{
			char c2 = rightText[j];
			if (this.onValidate != null)
			{
				c2 = this.onValidate(stringBuilder.ToString(), stringBuilder.Length, c2);
			}
			else if (this.validation != UIInput.Validation.None)
			{
				c2 = this.Validate(stringBuilder.ToString(), stringBuilder.Length, c2);
			}
			if (c2 != '\0')
			{
				stringBuilder.Append(c2);
			}
			j++;
		}
		this.mValue = stringBuilder.ToString();
		this.UpdateLabel();
		this.ExecuteOnChange();
	}

	// Token: 0x060007A5 RID: 1957 RVA: 0x0003F850 File Offset: 0x0003DA50
	protected string GetLeftText()
	{
		int num = Mathf.Min(new int[]
		{
			this.mSelectionStart,
			this.mSelectionEnd,
			this.mValue.Length
		});
		if (!string.IsNullOrEmpty(this.mValue) && num >= 0)
		{
			return this.mValue.Substring(0, num);
		}
		return "";
	}

	// Token: 0x060007A6 RID: 1958 RVA: 0x0003F8B0 File Offset: 0x0003DAB0
	protected string GetRightText()
	{
		int num = Mathf.Max(this.mSelectionStart, this.mSelectionEnd);
		if (!string.IsNullOrEmpty(this.mValue) && num < this.mValue.Length)
		{
			return this.mValue.Substring(num);
		}
		return "";
	}

	// Token: 0x060007A7 RID: 1959 RVA: 0x0003F8FC File Offset: 0x0003DAFC
	protected string GetSelection()
	{
		if (string.IsNullOrEmpty(this.mValue) || this.mSelectionStart == this.mSelectionEnd)
		{
			return "";
		}
		int num = Mathf.Min(this.mSelectionStart, this.mSelectionEnd);
		int num2 = Mathf.Max(this.mSelectionStart, this.mSelectionEnd);
		return this.mValue.Substring(num, num2 - num);
	}

	// Token: 0x060007A8 RID: 1960 RVA: 0x0003F960 File Offset: 0x0003DB60
	protected int GetCharUnderMouse()
	{
		Vector3[] worldCorners = this.label.worldCorners;
		Ray currentRay = UICamera.currentRay;
		Plane plane = new Plane(worldCorners[0], worldCorners[1], worldCorners[2]);
		float distance;
		if (!plane.Raycast(currentRay, out distance))
		{
			return 0;
		}
		return UIInput.mDrawStart + this.label.GetCharacterIndexAtPosition(currentRay.GetPoint(distance), false);
	}

	// Token: 0x060007A9 RID: 1961 RVA: 0x0003F9C4 File Offset: 0x0003DBC4
	protected virtual void OnPress(bool isPressed)
	{
		if (isPressed && this.isSelected && this.label != null && (UICamera.currentScheme == UICamera.ControlScheme.Mouse || UICamera.currentScheme == UICamera.ControlScheme.Touch))
		{
			this.selectionEnd = this.GetCharUnderMouse();
			if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
			{
				this.selectionStart = this.mSelectionEnd;
			}
		}
	}

	// Token: 0x060007AA RID: 1962 RVA: 0x0003FA29 File Offset: 0x0003DC29
	protected virtual void OnDrag(Vector2 delta)
	{
		if (this.label != null && (UICamera.currentScheme == UICamera.ControlScheme.Mouse || UICamera.currentScheme == UICamera.ControlScheme.Touch))
		{
			this.selectionEnd = this.GetCharUnderMouse();
		}
	}

	// Token: 0x060007AB RID: 1963 RVA: 0x0003FA54 File Offset: 0x0003DC54
	private void OnDisable()
	{
		this.Cleanup();
	}

	// Token: 0x060007AC RID: 1964 RVA: 0x0003FA5C File Offset: 0x0003DC5C
	protected virtual void Cleanup()
	{
		if (this.mHighlight)
		{
			this.mHighlight.enabled = false;
		}
		if (this.mCaret)
		{
			this.mCaret.enabled = false;
		}
		if (this.mBlankTex)
		{
			NGUITools.Destroy(this.mBlankTex);
			this.mBlankTex = null;
		}
	}

	// Token: 0x060007AD RID: 1965 RVA: 0x0003FABC File Offset: 0x0003DCBC
	public void Submit()
	{
		if (NGUITools.GetActive(this))
		{
			this.mValue = this.value;
			if (UIInput.current == null)
			{
				UIInput.current = this;
				EventDelegate.Execute(this.onSubmit);
				UIInput.current = null;
			}
			this.SaveToPlayerPrefs(this.mValue);
		}
	}

	// Token: 0x060007AE RID: 1966 RVA: 0x0003FB10 File Offset: 0x0003DD10
	public void UpdateLabel()
	{
		if (this.label != null)
		{
			if (this.mDoInit)
			{
				this.Init();
			}
			bool isSelected = this.isSelected;
			string value = this.value;
			bool flag = string.IsNullOrEmpty(value) && string.IsNullOrEmpty(Input.compositionString);
			this.label.color = ((flag && !isSelected) ? this.mDefaultColor : this.activeTextColor);
			string text;
			if (flag)
			{
				text = (isSelected ? "" : this.mDefaultText);
				this.label.alignment = this.mAlignment;
			}
			else
			{
				if (this.inputType == UIInput.InputType.Password)
				{
					text = "";
					string str = "*";
					INGUIFont bitmapFont = this.label.bitmapFont;
					if (bitmapFont != null && bitmapFont.bmFont != null && bitmapFont.bmFont.GetGlyph(42) == null)
					{
						str = "x";
					}
					int i = 0;
					int length = value.Length;
					while (i < length)
					{
						text += str;
						i++;
					}
				}
				else
				{
					text = value;
				}
				int num = isSelected ? Mathf.Min(text.Length, this.cursorPosition) : 0;
				string str2 = text.Substring(0, num);
				if (isSelected)
				{
					str2 += Input.compositionString;
				}
				text = str2 + text.Substring(num, text.Length - num);
				if (isSelected && this.label.overflowMethod == UILabel.Overflow.ClampContent && this.label.maxLineCount == 1)
				{
					int num2 = this.label.CalculateOffsetToFit(text);
					if (num2 == 0)
					{
						UIInput.mDrawStart = 0;
						this.label.alignment = this.mAlignment;
					}
					else if (num < UIInput.mDrawStart)
					{
						UIInput.mDrawStart = num;
						this.label.alignment = NGUIText.Alignment.Left;
					}
					else if (num2 < UIInput.mDrawStart)
					{
						UIInput.mDrawStart = num2;
						this.label.alignment = NGUIText.Alignment.Left;
					}
					else
					{
						num2 = this.label.CalculateOffsetToFit(text.Substring(0, num));
						if (num2 > UIInput.mDrawStart)
						{
							UIInput.mDrawStart = num2;
							this.label.alignment = NGUIText.Alignment.Right;
						}
					}
					if (UIInput.mDrawStart != 0)
					{
						text = text.Substring(UIInput.mDrawStart, text.Length - UIInput.mDrawStart);
					}
				}
				else
				{
					UIInput.mDrawStart = 0;
					this.label.alignment = this.mAlignment;
				}
			}
			this.label.text = text;
			if (isSelected)
			{
				int num3 = this.mSelectionStart - UIInput.mDrawStart;
				int num4 = this.mSelectionEnd - UIInput.mDrawStart;
				if (this.mBlankTex == null)
				{
					this.mBlankTex = new Texture2D(2, 2, TextureFormat.ARGB32, false);
					for (int j = 0; j < 2; j++)
					{
						for (int k = 0; k < 2; k++)
						{
							this.mBlankTex.SetPixel(k, j, Color.white);
						}
					}
					this.mBlankTex.Apply();
				}
				if (num3 != num4)
				{
					if (this.mHighlight == null)
					{
						this.mHighlight = this.label.cachedGameObject.AddWidget(int.MaxValue);
						this.mHighlight.name = "Input Highlight";
						this.mHighlight.mainTexture = this.mBlankTex;
						this.mHighlight.fillGeometry = false;
						this.mHighlight.pivot = this.label.pivot;
						this.mHighlight.SetAnchor(this.label.cachedTransform);
					}
					else
					{
						this.mHighlight.pivot = this.label.pivot;
						this.mHighlight.mainTexture = this.mBlankTex;
						this.mHighlight.MarkAsChanged();
						this.mHighlight.enabled = true;
					}
				}
				if (this.mCaret == null)
				{
					this.mCaret = this.label.cachedGameObject.AddWidget(int.MaxValue);
					this.mCaret.name = "Input Caret";
					this.mCaret.mainTexture = this.mBlankTex;
					this.mCaret.fillGeometry = false;
					this.mCaret.pivot = this.label.pivot;
					this.mCaret.SetAnchor(this.label.cachedTransform);
				}
				else
				{
					this.mCaret.pivot = this.label.pivot;
					this.mCaret.mainTexture = this.mBlankTex;
					this.mCaret.MarkAsChanged();
					this.mCaret.enabled = true;
				}
				if (num3 != num4)
				{
					this.label.PrintOverlay(num3, num4, this.mCaret.geometry, this.mHighlight.geometry, this.caretColor, this.selectionColor);
					this.mHighlight.enabled = this.mHighlight.geometry.hasVertices;
				}
				else
				{
					this.label.PrintOverlay(num3, num4, this.mCaret.geometry, null, this.caretColor, this.selectionColor);
					if (this.mHighlight != null)
					{
						this.mHighlight.enabled = false;
					}
				}
				this.mNextBlink = RealTime.time + 0.5f;
				this.mLastAlpha = this.label.finalAlpha;
				return;
			}
			this.Cleanup();
		}
	}

	// Token: 0x060007AF RID: 1967 RVA: 0x00040034 File Offset: 0x0003E234
	protected char Validate(string text, int pos, char ch)
	{
		if (this.validation == UIInput.Validation.None || !base.enabled)
		{
			return ch;
		}
		if (this.validation == UIInput.Validation.Integer)
		{
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
			if (ch == '-' && pos == 0 && !text.Contains("-"))
			{
				return ch;
			}
		}
		else if (this.validation == UIInput.Validation.Float)
		{
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
			if (ch == '-' && pos == 0 && !text.Contains("-"))
			{
				return ch;
			}
			if (ch == '.' && !text.Contains("."))
			{
				return ch;
			}
		}
		else if (this.validation == UIInput.Validation.Alphanumeric)
		{
			if (ch >= 'A' && ch <= 'Z')
			{
				return ch;
			}
			if (ch >= 'a' && ch <= 'z')
			{
				return ch;
			}
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
		}
		else if (this.validation == UIInput.Validation.Username)
		{
			if (ch >= 'A' && ch <= 'Z')
			{
				return ch - 'A' + 'a';
			}
			if (ch >= 'a' && ch <= 'z')
			{
				return ch;
			}
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
		}
		else if (this.validation == UIInput.Validation.Filename)
		{
			if (ch == ':')
			{
				return '\0';
			}
			if (ch == '/')
			{
				return '\0';
			}
			if (ch == '\\')
			{
				return '\0';
			}
			if (ch == '<')
			{
				return '\0';
			}
			if (ch == '>')
			{
				return '\0';
			}
			if (ch == '|')
			{
				return '\0';
			}
			if (ch == '^')
			{
				return '\0';
			}
			if (ch == '*')
			{
				return '\0';
			}
			if (ch == ';')
			{
				return '\0';
			}
			if (ch == '"')
			{
				return '\0';
			}
			if (ch == '`')
			{
				return '\0';
			}
			if (ch == '\t')
			{
				return '\0';
			}
			if (ch == '\n')
			{
				return '\0';
			}
			return ch;
		}
		else if (this.validation == UIInput.Validation.Name)
		{
			char c = (text.Length > 0) ? text[Mathf.Clamp(pos, 0, text.Length - 1)] : ' ';
			char c2 = (text.Length > 0) ? text[Mathf.Clamp(pos + 1, 0, text.Length - 1)] : '\n';
			if (ch >= 'a' && ch <= 'z')
			{
				if (c == ' ')
				{
					return ch - 'a' + 'A';
				}
				return ch;
			}
			else if (ch >= 'A' && ch <= 'Z')
			{
				if (c != ' ' && c != '\'')
				{
					return ch - 'A' + 'a';
				}
				return ch;
			}
			else if (ch == '\'')
			{
				if (c != ' ' && c != '\'' && c2 != '\'' && !text.Contains("'"))
				{
					return ch;
				}
			}
			else if (ch == ' ' && c != ' ' && c != '\'' && c2 != ' ' && c2 != '\'')
			{
				return ch;
			}
		}
		return '\0';
	}

	// Token: 0x060007B0 RID: 1968 RVA: 0x0004026D File Offset: 0x0003E46D
	protected void ExecuteOnChange()
	{
		if (UIInput.current == null && EventDelegate.IsValid(this.onChange))
		{
			UIInput.current = this;
			EventDelegate.Execute(this.onChange);
			UIInput.current = null;
		}
	}

	// Token: 0x060007B1 RID: 1969 RVA: 0x000402A0 File Offset: 0x0003E4A0
	public void RemoveFocus()
	{
		this.isSelected = false;
	}

	// Token: 0x060007B2 RID: 1970 RVA: 0x000402A9 File Offset: 0x0003E4A9
	public void SaveValue()
	{
		this.SaveToPlayerPrefs(this.mValue);
	}

	// Token: 0x060007B3 RID: 1971 RVA: 0x000402B8 File Offset: 0x0003E4B8
	public void LoadValue()
	{
		if (!string.IsNullOrEmpty(this.savedAs))
		{
			string text = this.mValue.Replace("\\n", "\n");
			this.mValue = "";
			this.value = (PlayerPrefs.HasKey(this.savedAs) ? PlayerPrefs.GetString(this.savedAs) : text);
		}
	}

	// Token: 0x040006CF RID: 1743
	public static UIInput current;

	// Token: 0x040006D0 RID: 1744
	public static UIInput selection;

	// Token: 0x040006D1 RID: 1745
	public UILabel label;

	// Token: 0x040006D2 RID: 1746
	public UIInput.InputType inputType;

	// Token: 0x040006D3 RID: 1747
	public UIInput.OnReturnKey onReturnKey;

	// Token: 0x040006D4 RID: 1748
	public UIInput.KeyboardType keyboardType;

	// Token: 0x040006D5 RID: 1749
	public bool hideInput;

	// Token: 0x040006D6 RID: 1750
	[NonSerialized]
	public bool selectAllTextOnFocus = true;

	// Token: 0x040006D7 RID: 1751
	public bool submitOnUnselect;

	// Token: 0x040006D8 RID: 1752
	public UIInput.Validation validation;

	// Token: 0x040006D9 RID: 1753
	public int characterLimit;

	// Token: 0x040006DA RID: 1754
	public string savedAs;

	// Token: 0x040006DB RID: 1755
	[HideInInspector]
	[SerializeField]
	private GameObject selectOnTab;

	// Token: 0x040006DC RID: 1756
	public Color activeTextColor = Color.white;

	// Token: 0x040006DD RID: 1757
	public Color caretColor = new Color(1f, 1f, 1f, 0.8f);

	// Token: 0x040006DE RID: 1758
	public Color selectionColor = new Color(1f, 0.8745098f, 0.5529412f, 0.5f);

	// Token: 0x040006DF RID: 1759
	public List<EventDelegate> onSubmit = new List<EventDelegate>();

	// Token: 0x040006E0 RID: 1760
	public List<EventDelegate> onChange = new List<EventDelegate>();

	// Token: 0x040006E1 RID: 1761
	public UIInput.OnValidate onValidate;

	// Token: 0x040006E2 RID: 1762
	[SerializeField]
	[HideInInspector]
	protected string mValue;

	// Token: 0x040006E3 RID: 1763
	[NonSerialized]
	protected string mDefaultText = "";

	// Token: 0x040006E4 RID: 1764
	[NonSerialized]
	protected Color mDefaultColor = Color.white;

	// Token: 0x040006E5 RID: 1765
	[NonSerialized]
	protected float mPosition;

	// Token: 0x040006E6 RID: 1766
	[NonSerialized]
	protected bool mDoInit = true;

	// Token: 0x040006E7 RID: 1767
	[NonSerialized]
	protected NGUIText.Alignment mAlignment = NGUIText.Alignment.Left;

	// Token: 0x040006E8 RID: 1768
	[NonSerialized]
	protected bool mLoadSavedValue = true;

	// Token: 0x040006E9 RID: 1769
	protected static int mDrawStart = 0;

	// Token: 0x040006EA RID: 1770
	protected static string mLastIME = "";

	// Token: 0x040006EB RID: 1771
	[NonSerialized]
	protected int mSelectionStart;

	// Token: 0x040006EC RID: 1772
	[NonSerialized]
	protected int mSelectionEnd;

	// Token: 0x040006ED RID: 1773
	[NonSerialized]
	protected UITexture mHighlight;

	// Token: 0x040006EE RID: 1774
	[NonSerialized]
	protected UITexture mCaret;

	// Token: 0x040006EF RID: 1775
	[NonSerialized]
	protected Texture2D mBlankTex;

	// Token: 0x040006F0 RID: 1776
	[NonSerialized]
	protected float mNextBlink;

	// Token: 0x040006F1 RID: 1777
	[NonSerialized]
	protected float mLastAlpha;

	// Token: 0x040006F2 RID: 1778
	[NonSerialized]
	protected string mCached = "";

	// Token: 0x040006F3 RID: 1779
	[NonSerialized]
	protected int mSelectMe = -1;

	// Token: 0x040006F4 RID: 1780
	[NonSerialized]
	protected int mSelectTime = -1;

	// Token: 0x040006F5 RID: 1781
	[NonSerialized]
	protected bool mStarted;

	// Token: 0x040006F6 RID: 1782
	[NonSerialized]
	private UIInputOnGUI mOnGUI;

	// Token: 0x040006F7 RID: 1783
	[NonSerialized]
	private UICamera mCam;

	// Token: 0x040006F8 RID: 1784
	[NonSerialized]
	private bool mEllipsis;

	// Token: 0x040006F9 RID: 1785
	private static int mIgnoreKey = 0;

	// Token: 0x040006FA RID: 1786
	[NonSerialized]
	public Action onUpArrow;

	// Token: 0x040006FB RID: 1787
	[NonSerialized]
	public Action onDownArrow;

	// Token: 0x02000678 RID: 1656
	[DoNotObfuscateNGUI]
	public enum InputType
	{
		// Token: 0x040045AC RID: 17836
		Standard,
		// Token: 0x040045AD RID: 17837
		AutoCorrect,
		// Token: 0x040045AE RID: 17838
		Password
	}

	// Token: 0x02000679 RID: 1657
	[DoNotObfuscateNGUI]
	public enum Validation
	{
		// Token: 0x040045B0 RID: 17840
		None,
		// Token: 0x040045B1 RID: 17841
		Integer,
		// Token: 0x040045B2 RID: 17842
		Float,
		// Token: 0x040045B3 RID: 17843
		Alphanumeric,
		// Token: 0x040045B4 RID: 17844
		Username,
		// Token: 0x040045B5 RID: 17845
		Name,
		// Token: 0x040045B6 RID: 17846
		Filename
	}

	// Token: 0x0200067A RID: 1658
	[DoNotObfuscateNGUI]
	public enum KeyboardType
	{
		// Token: 0x040045B8 RID: 17848
		Default,
		// Token: 0x040045B9 RID: 17849
		ASCIICapable,
		// Token: 0x040045BA RID: 17850
		NumbersAndPunctuation,
		// Token: 0x040045BB RID: 17851
		URL,
		// Token: 0x040045BC RID: 17852
		NumberPad,
		// Token: 0x040045BD RID: 17853
		PhonePad,
		// Token: 0x040045BE RID: 17854
		NamePhonePad,
		// Token: 0x040045BF RID: 17855
		EmailAddress
	}

	// Token: 0x0200067B RID: 1659
	[DoNotObfuscateNGUI]
	public enum OnReturnKey
	{
		// Token: 0x040045C1 RID: 17857
		Default,
		// Token: 0x040045C2 RID: 17858
		Submit,
		// Token: 0x040045C3 RID: 17859
		NewLine
	}

	// Token: 0x0200067C RID: 1660
	// (Invoke) Token: 0x06002B46 RID: 11078
	public delegate char OnValidate(string text, int charIndex, char addedChar);
}
