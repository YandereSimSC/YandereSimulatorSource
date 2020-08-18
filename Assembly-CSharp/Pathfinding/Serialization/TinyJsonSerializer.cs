using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using Pathfinding.Util;
using Pathfinding.WindowsStore;
using UnityEngine;

namespace Pathfinding.Serialization
{
	// Token: 0x020005BE RID: 1470
	public class TinyJsonSerializer
	{
		// Token: 0x06002813 RID: 10259 RVA: 0x001B66B8 File Offset: 0x001B48B8
		public static void Serialize(object obj, StringBuilder output)
		{
			new TinyJsonSerializer
			{
				output = output
			}.Serialize(obj);
		}

		// Token: 0x06002814 RID: 10260 RVA: 0x001B66CC File Offset: 0x001B48CC
		private TinyJsonSerializer()
		{
			this.serializers[typeof(float)] = delegate(object v)
			{
				this.output.Append(((float)v).ToString("R", TinyJsonSerializer.invariantCulture));
			};
			this.serializers[typeof(bool)] = delegate(object v)
			{
				this.output.Append(((bool)v) ? "true" : "false");
			};
			this.serializers[typeof(Version)] = (this.serializers[typeof(uint)] = (this.serializers[typeof(int)] = delegate(object v)
			{
				this.output.Append(v.ToString());
			}));
			this.serializers[typeof(string)] = delegate(object v)
			{
				this.output.AppendFormat("\"{0}\"", v.ToString().Replace("\"", "\\\""));
			};
			this.serializers[typeof(Vector2)] = delegate(object v)
			{
				StringBuilder stringBuilder = this.output;
				string format = "{{ \"x\": {0}, \"y\": {1} }}";
				Vector2 vector = (Vector2)v;
				object arg = vector.x.ToString("R", TinyJsonSerializer.invariantCulture);
				vector = (Vector2)v;
				stringBuilder.AppendFormat(format, arg, vector.y.ToString("R", TinyJsonSerializer.invariantCulture));
			};
			this.serializers[typeof(Vector3)] = delegate(object v)
			{
				StringBuilder stringBuilder = this.output;
				string format = "{{ \"x\": {0}, \"y\": {1}, \"z\": {2} }}";
				Vector3 vector = (Vector3)v;
				object arg = vector.x.ToString("R", TinyJsonSerializer.invariantCulture);
				vector = (Vector3)v;
				object arg2 = vector.y.ToString("R", TinyJsonSerializer.invariantCulture);
				vector = (Vector3)v;
				stringBuilder.AppendFormat(format, arg, arg2, vector.z.ToString("R", TinyJsonSerializer.invariantCulture));
			};
			this.serializers[typeof(Pathfinding.Util.Guid)] = delegate(object v)
			{
				this.output.AppendFormat("{{ \"value\": \"{0}\" }}", v.ToString());
			};
			this.serializers[typeof(LayerMask)] = delegate(object v)
			{
				this.output.AppendFormat("{{ \"value\": {0} }}", ((LayerMask)v).ToString());
			};
		}

		// Token: 0x06002815 RID: 10261 RVA: 0x001B6830 File Offset: 0x001B4A30
		private void Serialize(object obj)
		{
			if (obj == null)
			{
				this.output.Append("null");
				return;
			}
			Type type = obj.GetType();
			Type typeInfo = WindowsStoreCompatibility.GetTypeInfo(type);
			if (this.serializers.ContainsKey(type))
			{
				this.serializers[type](obj);
				return;
			}
			if (typeInfo.IsEnum)
			{
				this.output.Append("\"" + obj.ToString() + "\"");
				return;
			}
			if (obj is IList)
			{
				this.output.Append("[");
				IList list = obj as IList;
				for (int i = 0; i < list.Count; i++)
				{
					if (i != 0)
					{
						this.output.Append(", ");
					}
					this.Serialize(list[i]);
				}
				this.output.Append("]");
				return;
			}
			if (obj is UnityEngine.Object)
			{
				this.SerializeUnityObject(obj as UnityEngine.Object);
				return;
			}
			bool flag = typeInfo.GetCustomAttributes(typeof(JsonOptInAttribute), true).Length != 0;
			this.output.Append("{");
			bool flag2 = false;
			do
			{
				foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
				{
					if (!(fieldInfo.DeclaringType != type) && ((!flag && fieldInfo.IsPublic) || fieldInfo.GetCustomAttributes(typeof(JsonMemberAttribute), true).Length != 0))
					{
						if (flag2)
						{
							this.output.Append(", ");
						}
						flag2 = true;
						this.output.AppendFormat("\"{0}\": ", fieldInfo.Name);
						this.Serialize(fieldInfo.GetValue(obj));
					}
				}
				type = type.BaseType;
			}
			while (!(type == null));
			this.output.Append("}");
		}

		// Token: 0x06002816 RID: 10262 RVA: 0x001B6A09 File Offset: 0x001B4C09
		private void QuotedField(string name, string contents)
		{
			this.output.AppendFormat("\"{0}\": \"{1}\"", name, contents);
		}

		// Token: 0x06002817 RID: 10263 RVA: 0x001B6A20 File Offset: 0x001B4C20
		private void SerializeUnityObject(UnityEngine.Object obj)
		{
			if (obj == null)
			{
				this.Serialize(null);
				return;
			}
			this.output.Append("{");
			string name = obj.name;
			this.QuotedField("Name", name);
			this.output.Append(", ");
			this.QuotedField("Type", obj.GetType().FullName);
			Component component = obj as Component;
			GameObject gameObject = obj as GameObject;
			if (component != null || gameObject != null)
			{
				if (component != null && gameObject == null)
				{
					gameObject = component.gameObject;
				}
				UnityReferenceHelper unityReferenceHelper = gameObject.GetComponent<UnityReferenceHelper>();
				if (unityReferenceHelper == null)
				{
					Debug.Log("Adding UnityReferenceHelper to Unity Reference '" + obj.name + "'");
					unityReferenceHelper = gameObject.AddComponent<UnityReferenceHelper>();
				}
				unityReferenceHelper.Reset();
				this.output.Append(", ");
				this.QuotedField("GUID", unityReferenceHelper.GetGUID().ToString());
			}
			this.output.Append("}");
		}

		// Token: 0x04004259 RID: 16985
		private StringBuilder output = new StringBuilder();

		// Token: 0x0400425A RID: 16986
		private Dictionary<Type, Action<object>> serializers = new Dictionary<Type, Action<object>>();

		// Token: 0x0400425B RID: 16987
		private static readonly CultureInfo invariantCulture = CultureInfo.InvariantCulture;
	}
}
