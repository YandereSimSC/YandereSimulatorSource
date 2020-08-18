using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x02000072 RID: 114
[Serializable]
public class EventDelegate
{
	// Token: 0x17000068 RID: 104
	// (get) Token: 0x06000382 RID: 898 RVA: 0x000211BD File Offset: 0x0001F3BD
	// (set) Token: 0x06000383 RID: 899 RVA: 0x000211C5 File Offset: 0x0001F3C5
	public MonoBehaviour target
	{
		get
		{
			return this.mTarget;
		}
		set
		{
			this.mTarget = value;
			this.mCachedCallback = null;
			this.mRawDelegate = false;
			this.mCached = false;
			this.mMethod = null;
			this.mParameterInfos = null;
			this.mParameters = null;
		}
	}

	// Token: 0x17000069 RID: 105
	// (get) Token: 0x06000384 RID: 900 RVA: 0x000211F8 File Offset: 0x0001F3F8
	// (set) Token: 0x06000385 RID: 901 RVA: 0x00021200 File Offset: 0x0001F400
	public string methodName
	{
		get
		{
			return this.mMethodName;
		}
		set
		{
			this.mMethodName = value;
			this.mCachedCallback = null;
			this.mRawDelegate = false;
			this.mCached = false;
			this.mMethod = null;
			this.mParameterInfos = null;
			this.mParameters = null;
		}
	}

	// Token: 0x1700006A RID: 106
	// (get) Token: 0x06000386 RID: 902 RVA: 0x00021233 File Offset: 0x0001F433
	public EventDelegate.Parameter[] parameters
	{
		get
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			return this.mParameters;
		}
	}

	// Token: 0x1700006B RID: 107
	// (get) Token: 0x06000387 RID: 903 RVA: 0x00021249 File Offset: 0x0001F449
	public bool isValid
	{
		get
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			return (this.mRawDelegate && this.mCachedCallback != null) || (this.mTarget != null && !string.IsNullOrEmpty(this.mMethodName));
		}
	}

	// Token: 0x1700006C RID: 108
	// (get) Token: 0x06000388 RID: 904 RVA: 0x0002128C File Offset: 0x0001F48C
	public bool isEnabled
	{
		get
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			if (this.mRawDelegate && this.mCachedCallback != null)
			{
				return true;
			}
			if (this.mTarget == null)
			{
				return false;
			}
			MonoBehaviour monoBehaviour = this.mTarget;
			return monoBehaviour == null || monoBehaviour.enabled;
		}
	}

	// Token: 0x06000389 RID: 905 RVA: 0x000045DB File Offset: 0x000027DB
	public EventDelegate()
	{
	}

	// Token: 0x0600038A RID: 906 RVA: 0x000212E1 File Offset: 0x0001F4E1
	public EventDelegate(EventDelegate.Callback call)
	{
		this.Set(call);
	}

	// Token: 0x0600038B RID: 907 RVA: 0x000212F0 File Offset: 0x0001F4F0
	public EventDelegate(MonoBehaviour target, string methodName)
	{
		this.Set(target, methodName);
	}

	// Token: 0x0600038C RID: 908 RVA: 0x00021300 File Offset: 0x0001F500
	private static string GetMethodName(EventDelegate.Callback callback)
	{
		return callback.Method.Name;
	}

	// Token: 0x0600038D RID: 909 RVA: 0x0002130D File Offset: 0x0001F50D
	private static bool IsValid(EventDelegate.Callback callback)
	{
		return callback != null && callback.Method != null;
	}

	// Token: 0x0600038E RID: 910 RVA: 0x00021320 File Offset: 0x0001F520
	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return !this.isValid;
		}
		if (obj is EventDelegate.Callback)
		{
			EventDelegate.Callback callback = obj as EventDelegate.Callback;
			if (callback.Equals(this.mCachedCallback))
			{
				return true;
			}
			MonoBehaviour y = callback.Target as MonoBehaviour;
			return this.mTarget == y && string.Equals(this.mMethodName, EventDelegate.GetMethodName(callback));
		}
		else
		{
			if (obj is EventDelegate)
			{
				EventDelegate eventDelegate = obj as EventDelegate;
				return this.mTarget == eventDelegate.mTarget && string.Equals(this.mMethodName, eventDelegate.mMethodName);
			}
			return false;
		}
	}

	// Token: 0x0600038F RID: 911 RVA: 0x000213BE File Offset: 0x0001F5BE
	public override int GetHashCode()
	{
		return EventDelegate.s_Hash;
	}

	// Token: 0x06000390 RID: 912 RVA: 0x000213C8 File Offset: 0x0001F5C8
	private void Set(EventDelegate.Callback call)
	{
		this.Clear();
		if (call != null && EventDelegate.IsValid(call))
		{
			this.mTarget = (call.Target as MonoBehaviour);
			if (this.mTarget == null)
			{
				this.mRawDelegate = true;
				this.mCachedCallback = call;
				this.mMethodName = null;
				return;
			}
			this.mMethodName = EventDelegate.GetMethodName(call);
			this.mRawDelegate = false;
		}
	}

	// Token: 0x06000391 RID: 913 RVA: 0x0002142E File Offset: 0x0001F62E
	public void Set(MonoBehaviour target, string methodName)
	{
		this.Clear();
		this.mTarget = target;
		this.mMethodName = methodName;
	}

	// Token: 0x06000392 RID: 914 RVA: 0x00021444 File Offset: 0x0001F644
	private void Cache()
	{
		this.mCached = true;
		if (this.mRawDelegate)
		{
			return;
		}
		if ((this.mCachedCallback == null || this.mCachedCallback.Target as MonoBehaviour != this.mTarget || EventDelegate.GetMethodName(this.mCachedCallback) != this.mMethodName) && this.mTarget != null && !string.IsNullOrEmpty(this.mMethodName))
		{
			Type type = this.mTarget.GetType();
			this.mMethod = null;
			while (type != null)
			{
				try
				{
					this.mMethod = type.GetMethod(this.mMethodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
					if (this.mMethod != null)
					{
						break;
					}
				}
				catch (Exception)
				{
				}
				type = type.BaseType;
			}
			if (this.mMethod == null)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"Could not find method '",
					this.mMethodName,
					"' on ",
					this.mTarget.GetType()
				}), this.mTarget);
				return;
			}
			if (this.mMethod.ReturnType != typeof(void))
			{
				Debug.LogError(string.Concat(new object[]
				{
					this.mTarget.GetType(),
					".",
					this.mMethodName,
					" must have a 'void' return type."
				}), this.mTarget);
				return;
			}
			this.mParameterInfos = this.mMethod.GetParameters();
			if (this.mParameterInfos.Length == 0)
			{
				this.mCachedCallback = (EventDelegate.Callback)Delegate.CreateDelegate(typeof(EventDelegate.Callback), this.mTarget, this.mMethodName);
				this.mArgs = null;
				this.mParameters = null;
				return;
			}
			this.mCachedCallback = null;
			if (this.mParameters == null || this.mParameters.Length != this.mParameterInfos.Length)
			{
				this.mParameters = new EventDelegate.Parameter[this.mParameterInfos.Length];
				int i = 0;
				int num = this.mParameters.Length;
				while (i < num)
				{
					this.mParameters[i] = new EventDelegate.Parameter();
					i++;
				}
			}
			int j = 0;
			int num2 = this.mParameters.Length;
			while (j < num2)
			{
				this.mParameters[j].expectedType = this.mParameterInfos[j].ParameterType;
				j++;
			}
		}
	}

	// Token: 0x06000393 RID: 915 RVA: 0x000216A0 File Offset: 0x0001F8A0
	public bool Execute()
	{
		if (!this.mCached)
		{
			this.Cache();
		}
		if (this.mCachedCallback != null)
		{
			this.mCachedCallback();
			return true;
		}
		if (this.mMethod != null)
		{
			if (this.mParameters == null || this.mParameters.Length == 0)
			{
				this.mMethod.Invoke(this.mTarget, null);
			}
			else
			{
				if (this.mArgs == null || this.mArgs.Length != this.mParameters.Length)
				{
					this.mArgs = new object[this.mParameters.Length];
				}
				int i = 0;
				int num = this.mParameters.Length;
				while (i < num)
				{
					this.mArgs[i] = this.mParameters[i].value;
					i++;
				}
				try
				{
					this.mMethod.Invoke(this.mTarget, this.mArgs);
				}
				catch (ArgumentException ex)
				{
					string text = "Error calling ";
					if (this.mTarget == null)
					{
						text += this.mMethod.Name;
					}
					else
					{
						text = string.Concat(new object[]
						{
							text,
							this.mTarget.GetType(),
							".",
							this.mMethod.Name
						});
					}
					text = text + ": " + ex.Message;
					text += "\n  Expected: ";
					if (this.mParameterInfos.Length == 0)
					{
						text += "no arguments";
					}
					else
					{
						text += this.mParameterInfos[0];
						for (int j = 1; j < this.mParameterInfos.Length; j++)
						{
							text = text + ", " + this.mParameterInfos[j].ParameterType;
						}
					}
					text += "\n  Received: ";
					if (this.mParameters.Length == 0)
					{
						text += "no arguments";
					}
					else
					{
						text += this.mParameters[0].type;
						for (int k = 1; k < this.mParameters.Length; k++)
						{
							text = text + ", " + this.mParameters[k].type;
						}
					}
					text += "\n";
					Debug.LogError(text);
				}
				int l = 0;
				int num2 = this.mArgs.Length;
				while (l < num2)
				{
					if (this.mParameterInfos[l].IsIn || this.mParameterInfos[l].IsOut)
					{
						this.mParameters[l].value = this.mArgs[l];
					}
					this.mArgs[l] = null;
					l++;
				}
			}
			return true;
		}
		return false;
	}

	// Token: 0x06000394 RID: 916 RVA: 0x0002194C File Offset: 0x0001FB4C
	public void Clear()
	{
		this.mTarget = null;
		this.mMethodName = null;
		this.mRawDelegate = false;
		this.mCachedCallback = null;
		this.mParameters = null;
		this.mCached = false;
		this.mMethod = null;
		this.mParameterInfos = null;
		this.mArgs = null;
	}

	// Token: 0x06000395 RID: 917 RVA: 0x00021998 File Offset: 0x0001FB98
	public override string ToString()
	{
		if (this.mTarget != null)
		{
			string text = this.mTarget.GetType().ToString();
			int num = text.LastIndexOf('.');
			if (num > 0)
			{
				text = text.Substring(num + 1);
			}
			if (!string.IsNullOrEmpty(this.methodName))
			{
				return text + "/" + this.methodName;
			}
			return text + "/[delegate]";
		}
		else
		{
			if (!this.mRawDelegate)
			{
				return null;
			}
			return "[delegate]";
		}
	}

	// Token: 0x06000396 RID: 918 RVA: 0x00021A18 File Offset: 0x0001FC18
	public static void Execute(List<EventDelegate> list)
	{
		if (list != null)
		{
			for (int i = 0; i < list.Count; i++)
			{
				EventDelegate eventDelegate = list[i];
				if (eventDelegate != null)
				{
					try
					{
						eventDelegate.Execute();
					}
					catch (Exception ex)
					{
						if (ex.InnerException != null)
						{
							Debug.LogException(ex.InnerException);
						}
						else
						{
							Debug.LogException(ex);
						}
					}
					if (i >= list.Count)
					{
						break;
					}
					if (list[i] != eventDelegate)
					{
						continue;
					}
					if (eventDelegate.oneShot)
					{
						list.RemoveAt(i);
						continue;
					}
				}
			}
		}
	}

	// Token: 0x06000397 RID: 919 RVA: 0x00021AA0 File Offset: 0x0001FCA0
	public static bool IsValid(List<EventDelegate> list)
	{
		if (list != null)
		{
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				EventDelegate eventDelegate = list[i];
				if (eventDelegate != null && eventDelegate.isValid)
				{
					return true;
				}
				i++;
			}
		}
		return false;
	}

	// Token: 0x06000398 RID: 920 RVA: 0x00021ADC File Offset: 0x0001FCDC
	public static EventDelegate Set(List<EventDelegate> list, EventDelegate.Callback callback)
	{
		if (list != null)
		{
			EventDelegate eventDelegate = new EventDelegate(callback);
			list.Clear();
			list.Add(eventDelegate);
			return eventDelegate;
		}
		return null;
	}

	// Token: 0x06000399 RID: 921 RVA: 0x00021B03 File Offset: 0x0001FD03
	public static void Set(List<EventDelegate> list, EventDelegate del)
	{
		if (list != null)
		{
			list.Clear();
			list.Add(del);
		}
	}

	// Token: 0x0600039A RID: 922 RVA: 0x00021B15 File Offset: 0x0001FD15
	public static EventDelegate Add(List<EventDelegate> list, EventDelegate.Callback callback)
	{
		return EventDelegate.Add(list, callback, false);
	}

	// Token: 0x0600039B RID: 923 RVA: 0x00021B20 File Offset: 0x0001FD20
	public static EventDelegate Add(List<EventDelegate> list, EventDelegate.Callback callback, bool oneShot)
	{
		if (list != null)
		{
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				EventDelegate eventDelegate = list[i];
				if (eventDelegate != null && eventDelegate.Equals(callback))
				{
					return eventDelegate;
				}
				i++;
			}
			EventDelegate eventDelegate2 = new EventDelegate(callback);
			eventDelegate2.oneShot = oneShot;
			list.Add(eventDelegate2);
			return eventDelegate2;
		}
		Debug.LogWarning("Attempting to add a callback to a list that's null");
		return null;
	}

	// Token: 0x0600039C RID: 924 RVA: 0x00021B7B File Offset: 0x0001FD7B
	public static void Add(List<EventDelegate> list, EventDelegate ev)
	{
		EventDelegate.Add(list, ev, ev.oneShot);
	}

	// Token: 0x0600039D RID: 925 RVA: 0x00021B8C File Offset: 0x0001FD8C
	public static void Add(List<EventDelegate> list, EventDelegate ev, bool oneShot)
	{
		if (ev.mRawDelegate || ev.target == null || string.IsNullOrEmpty(ev.methodName))
		{
			EventDelegate.Add(list, ev.mCachedCallback, oneShot);
			return;
		}
		if (list != null)
		{
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				EventDelegate eventDelegate = list[i];
				if (eventDelegate != null && eventDelegate.Equals(ev))
				{
					return;
				}
				i++;
			}
			EventDelegate eventDelegate2 = new EventDelegate(ev.target, ev.methodName);
			eventDelegate2.oneShot = oneShot;
			if (ev.mParameters != null && ev.mParameters.Length != 0)
			{
				eventDelegate2.mParameters = new EventDelegate.Parameter[ev.mParameters.Length];
				for (int j = 0; j < ev.mParameters.Length; j++)
				{
					eventDelegate2.mParameters[j] = ev.mParameters[j];
				}
			}
			list.Add(eventDelegate2);
			return;
		}
		Debug.LogWarning("Attempting to add a callback to a list that's null");
	}

	// Token: 0x0600039E RID: 926 RVA: 0x00021C74 File Offset: 0x0001FE74
	public static bool Remove(List<EventDelegate> list, EventDelegate.Callback callback)
	{
		if (list != null)
		{
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				EventDelegate eventDelegate = list[i];
				if (eventDelegate != null && eventDelegate.Equals(callback))
				{
					list.RemoveAt(i);
					return true;
				}
				i++;
			}
		}
		return false;
	}

	// Token: 0x0600039F RID: 927 RVA: 0x00021CB8 File Offset: 0x0001FEB8
	public static bool Remove(List<EventDelegate> list, EventDelegate ev)
	{
		if (list != null)
		{
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				EventDelegate eventDelegate = list[i];
				if (eventDelegate != null && eventDelegate.Equals(ev))
				{
					list.RemoveAt(i);
					return true;
				}
				i++;
			}
		}
		return false;
	}

	// Token: 0x040004B8 RID: 1208
	[SerializeField]
	private MonoBehaviour mTarget;

	// Token: 0x040004B9 RID: 1209
	[SerializeField]
	private string mMethodName;

	// Token: 0x040004BA RID: 1210
	[SerializeField]
	private EventDelegate.Parameter[] mParameters;

	// Token: 0x040004BB RID: 1211
	public bool oneShot;

	// Token: 0x040004BC RID: 1212
	[NonSerialized]
	private EventDelegate.Callback mCachedCallback;

	// Token: 0x040004BD RID: 1213
	[NonSerialized]
	private bool mRawDelegate;

	// Token: 0x040004BE RID: 1214
	[NonSerialized]
	private bool mCached;

	// Token: 0x040004BF RID: 1215
	[NonSerialized]
	private MethodInfo mMethod;

	// Token: 0x040004C0 RID: 1216
	[NonSerialized]
	private ParameterInfo[] mParameterInfos;

	// Token: 0x040004C1 RID: 1217
	[NonSerialized]
	private object[] mArgs;

	// Token: 0x040004C2 RID: 1218
	private static int s_Hash = "EventDelegate".GetHashCode();

	// Token: 0x02000631 RID: 1585
	[Serializable]
	public class Parameter
	{
		// Token: 0x06002A8C RID: 10892 RVA: 0x001C3FBB File Offset: 0x001C21BB
		public Parameter()
		{
		}

		// Token: 0x06002A8D RID: 10893 RVA: 0x001C3FD3 File Offset: 0x001C21D3
		public Parameter(UnityEngine.Object obj, string field)
		{
			this.obj = obj;
			this.field = field;
		}

		// Token: 0x06002A8E RID: 10894 RVA: 0x001C3FF9 File Offset: 0x001C21F9
		public Parameter(object val)
		{
			this.mValue = val;
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06002A8F RID: 10895 RVA: 0x001C4018 File Offset: 0x001C2218
		// (set) Token: 0x06002A90 RID: 10896 RVA: 0x001C4129 File Offset: 0x001C2329
		public object value
		{
			get
			{
				if (this.mValue != null)
				{
					return this.mValue;
				}
				if (!this.cached)
				{
					this.cached = true;
					this.fieldInfo = null;
					this.propInfo = null;
					if (this.obj != null && !string.IsNullOrEmpty(this.field))
					{
						Type type = this.obj.GetType();
						this.propInfo = type.GetProperty(this.field);
						if (this.propInfo == null)
						{
							this.fieldInfo = type.GetField(this.field);
						}
					}
				}
				if (this.propInfo != null)
				{
					return this.propInfo.GetValue(this.obj, null);
				}
				if (this.fieldInfo != null)
				{
					return this.fieldInfo.GetValue(this.obj);
				}
				if (this.obj != null)
				{
					return this.obj;
				}
				if (this.expectedType != null && this.expectedType.IsValueType)
				{
					return null;
				}
				return Convert.ChangeType(null, this.expectedType);
			}
			set
			{
				this.mValue = value;
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06002A91 RID: 10897 RVA: 0x001C4132 File Offset: 0x001C2332
		public Type type
		{
			get
			{
				if (this.mValue != null)
				{
					return this.mValue.GetType();
				}
				if (this.obj == null)
				{
					return typeof(void);
				}
				return this.obj.GetType();
			}
		}

		// Token: 0x040044EF RID: 17647
		public UnityEngine.Object obj;

		// Token: 0x040044F0 RID: 17648
		public string field;

		// Token: 0x040044F1 RID: 17649
		[NonSerialized]
		private object mValue;

		// Token: 0x040044F2 RID: 17650
		[NonSerialized]
		public Type expectedType = typeof(void);

		// Token: 0x040044F3 RID: 17651
		[NonSerialized]
		public bool cached;

		// Token: 0x040044F4 RID: 17652
		[NonSerialized]
		public PropertyInfo propInfo;

		// Token: 0x040044F5 RID: 17653
		[NonSerialized]
		public FieldInfo fieldInfo;
	}

	// Token: 0x02000632 RID: 1586
	// (Invoke) Token: 0x06002A93 RID: 10899
	public delegate void Callback();
}
