using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using Pathfinding.Util;
using Pathfinding.WindowsStore;
using UnityEngine;

namespace Pathfinding.Serialization
{
	// Token: 0x020005BF RID: 1471
	public class TinyJsonDeserializer
	{
		// Token: 0x06002821 RID: 10273 RVA: 0x001B6CE9 File Offset: 0x001B4EE9
		public static object Deserialize(string text, Type type, object populate = null)
		{
			return new TinyJsonDeserializer
			{
				reader = new StringReader(text)
			}.Deserialize(type, populate);
		}

		// Token: 0x06002822 RID: 10274 RVA: 0x001B6D04 File Offset: 0x001B4F04
		private object Deserialize(Type tp, object populate = null)
		{
			Type typeInfo = WindowsStoreCompatibility.GetTypeInfo(tp);
			if (typeInfo.IsEnum)
			{
				return Enum.Parse(tp, this.EatField());
			}
			if (this.TryEat('n'))
			{
				this.Eat("ull");
				this.TryEat(',');
				return null;
			}
			if (object.Equals(tp, typeof(float)))
			{
				return float.Parse(this.EatField(), TinyJsonDeserializer.numberFormat);
			}
			if (object.Equals(tp, typeof(int)))
			{
				return int.Parse(this.EatField(), TinyJsonDeserializer.numberFormat);
			}
			if (object.Equals(tp, typeof(uint)))
			{
				return uint.Parse(this.EatField(), TinyJsonDeserializer.numberFormat);
			}
			if (object.Equals(tp, typeof(bool)))
			{
				return bool.Parse(this.EatField());
			}
			if (object.Equals(tp, typeof(string)))
			{
				return this.EatField();
			}
			if (object.Equals(tp, typeof(Version)))
			{
				return new Version(this.EatField());
			}
			if (object.Equals(tp, typeof(Vector2)))
			{
				this.Eat("{");
				Vector2 vector = default(Vector2);
				this.EatField();
				vector.x = float.Parse(this.EatField(), TinyJsonDeserializer.numberFormat);
				this.EatField();
				vector.y = float.Parse(this.EatField(), TinyJsonDeserializer.numberFormat);
				this.Eat("}");
				return vector;
			}
			if (object.Equals(tp, typeof(Vector3)))
			{
				this.Eat("{");
				Vector3 vector2 = default(Vector3);
				this.EatField();
				vector2.x = float.Parse(this.EatField(), TinyJsonDeserializer.numberFormat);
				this.EatField();
				vector2.y = float.Parse(this.EatField(), TinyJsonDeserializer.numberFormat);
				this.EatField();
				vector2.z = float.Parse(this.EatField(), TinyJsonDeserializer.numberFormat);
				this.Eat("}");
				return vector2;
			}
			if (object.Equals(tp, typeof(Pathfinding.Util.Guid)))
			{
				this.Eat("{");
				this.EatField();
				Pathfinding.Util.Guid guid = Pathfinding.Util.Guid.Parse(this.EatField());
				this.Eat("}");
				return guid;
			}
			if (object.Equals(tp, typeof(LayerMask)))
			{
				this.Eat("{");
				this.EatField();
				LayerMask layerMask = int.Parse(this.EatField());
				this.Eat("}");
				return layerMask;
			}
			if (object.Equals(tp, typeof(List<string>)))
			{
				IList list = new List<string>();
				this.Eat("[");
				while (!this.TryEat(']'))
				{
					list.Add(this.Deserialize(typeof(string), null));
					this.TryEat(',');
				}
				return list;
			}
			if (typeInfo.IsArray)
			{
				List<object> list2 = new List<object>();
				this.Eat("[");
				while (!this.TryEat(']'))
				{
					list2.Add(this.Deserialize(tp.GetElementType(), null));
					this.TryEat(',');
				}
				Array array = Array.CreateInstance(tp.GetElementType(), list2.Count);
				list2.ToArray().CopyTo(array, 0);
				return array;
			}
			if (object.Equals(tp, typeof(Mesh)) || object.Equals(tp, typeof(Texture2D)) || object.Equals(tp, typeof(Transform)) || object.Equals(tp, typeof(GameObject)))
			{
				return this.DeserializeUnityObject();
			}
			object obj = populate ?? Activator.CreateInstance(tp);
			this.Eat("{");
			while (!this.TryEat('}'))
			{
				string name = this.EatField();
				Type type = tp;
				FieldInfo fieldInfo = null;
				while (fieldInfo == null && type != null)
				{
					fieldInfo = type.GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
					type = type.BaseType;
				}
				if (fieldInfo == null)
				{
					this.SkipFieldData();
				}
				else
				{
					fieldInfo.SetValue(obj, this.Deserialize(fieldInfo.FieldType, null));
				}
				this.TryEat(',');
			}
			return obj;
		}

		// Token: 0x06002823 RID: 10275 RVA: 0x001B7144 File Offset: 0x001B5344
		private UnityEngine.Object DeserializeUnityObject()
		{
			this.Eat("{");
			UnityEngine.Object result = this.DeserializeUnityObjectInner();
			this.Eat("}");
			return result;
		}

		// Token: 0x06002824 RID: 10276 RVA: 0x001B7164 File Offset: 0x001B5364
		private UnityEngine.Object DeserializeUnityObjectInner()
		{
			string a = this.EatField();
			if (a == "InstanceID")
			{
				this.EatField();
				a = this.EatField();
			}
			if (a != "Name")
			{
				throw new Exception("Expected 'Name' field");
			}
			string text = this.EatField();
			if (text == null)
			{
				return null;
			}
			if (this.EatField() != "Type")
			{
				throw new Exception("Expected 'Type' field");
			}
			string text2 = this.EatField();
			if (text2.IndexOf(',') != -1)
			{
				text2 = text2.Substring(0, text2.IndexOf(','));
			}
			Type type = WindowsStoreCompatibility.GetTypeInfo(typeof(AstarPath)).Assembly.GetType(text2);
			type = (type ?? WindowsStoreCompatibility.GetTypeInfo(typeof(Transform)).Assembly.GetType(text2));
			if (object.Equals(type, null))
			{
				Debug.LogError("Could not find type '" + text2 + "'. Cannot deserialize Unity reference");
				return null;
			}
			this.EatWhitespace();
			if ((ushort)this.reader.Peek() == 34)
			{
				if (this.EatField() != "GUID")
				{
					throw new Exception("Expected 'GUID' field");
				}
				string b = this.EatField();
				UnityReferenceHelper[] array = UnityEngine.Object.FindObjectsOfType<UnityReferenceHelper>();
				int i = 0;
				while (i < array.Length)
				{
					UnityReferenceHelper unityReferenceHelper = array[i];
					if (unityReferenceHelper.GetGUID() == b)
					{
						if (object.Equals(type, typeof(GameObject)))
						{
							return unityReferenceHelper.gameObject;
						}
						return unityReferenceHelper.GetComponent(type);
					}
					else
					{
						i++;
					}
				}
			}
			UnityEngine.Object[] array2 = Resources.LoadAll(text, type);
			for (int j = 0; j < array2.Length; j++)
			{
				if (array2[j].name == text || array2.Length == 1)
				{
					return array2[j];
				}
			}
			return null;
		}

		// Token: 0x06002825 RID: 10277 RVA: 0x001B731C File Offset: 0x001B551C
		private void EatWhitespace()
		{
			while (char.IsWhiteSpace((char)this.reader.Peek()))
			{
				this.reader.Read();
			}
		}

		// Token: 0x06002826 RID: 10278 RVA: 0x001B7340 File Offset: 0x001B5540
		private void Eat(string s)
		{
			this.EatWhitespace();
			for (int i = 0; i < s.Length; i++)
			{
				char c = (char)this.reader.Read();
				if (c != s[i])
				{
					throw new Exception(string.Concat(new string[]
					{
						"Expected '",
						s[i].ToString(),
						"' found '",
						c.ToString(),
						"'\n\n...",
						this.reader.ReadLine()
					}));
				}
			}
		}

		// Token: 0x06002827 RID: 10279 RVA: 0x001B73D0 File Offset: 0x001B55D0
		private string EatUntil(string c, bool inString)
		{
			this.builder.Length = 0;
			bool flag = false;
			for (;;)
			{
				int num = this.reader.Peek();
				if (!flag && (ushort)num == 34)
				{
					inString = !inString;
				}
				char c2 = (char)num;
				if (num == -1)
				{
					break;
				}
				if (!flag && c2 == '\\')
				{
					flag = true;
					this.reader.Read();
				}
				else
				{
					if (!inString && c.IndexOf(c2) != -1)
					{
						goto IL_7D;
					}
					this.builder.Append(c2);
					this.reader.Read();
					flag = false;
				}
			}
			throw new Exception("Unexpected EOF");
			IL_7D:
			return this.builder.ToString();
		}

		// Token: 0x06002828 RID: 10280 RVA: 0x001B7465 File Offset: 0x001B5665
		private bool TryEat(char c)
		{
			this.EatWhitespace();
			if ((char)this.reader.Peek() == c)
			{
				this.reader.Read();
				return true;
			}
			return false;
		}

		// Token: 0x06002829 RID: 10281 RVA: 0x001B748B File Offset: 0x001B568B
		private string EatField()
		{
			string result = this.EatUntil("\",}]", this.TryEat('"'));
			this.TryEat('"');
			this.TryEat(':');
			this.TryEat(',');
			return result;
		}

		// Token: 0x0600282A RID: 10282 RVA: 0x001B74BC File Offset: 0x001B56BC
		private void SkipFieldData()
		{
			int num = 0;
			for (;;)
			{
				this.EatUntil(",{}[]", false);
				char c = (char)this.reader.Peek();
				if (c <= '[')
				{
					if (c != ',')
					{
						if (c != '[')
						{
							break;
						}
						goto IL_3E;
					}
					else if (num == 0)
					{
						goto Block_8;
					}
				}
				else
				{
					if (c != ']')
					{
						if (c == '{')
						{
							goto IL_3E;
						}
						if (c != '}')
						{
							break;
						}
					}
					num--;
					if (num < 0)
					{
						return;
					}
				}
				IL_68:
				this.reader.Read();
				continue;
				IL_3E:
				num++;
				goto IL_68;
			}
			goto IL_5D;
			Block_8:
			this.reader.Read();
			return;
			IL_5D:
			throw new Exception("Should not reach this part");
		}

		// Token: 0x0400425C RID: 16988
		private TextReader reader;

		// Token: 0x0400425D RID: 16989
		private static readonly NumberFormatInfo numberFormat = NumberFormatInfo.InvariantInfo;

		// Token: 0x0400425E RID: 16990
		private StringBuilder builder = new StringBuilder();
	}
}
