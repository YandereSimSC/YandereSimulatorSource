using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

// Token: 0x02000071 RID: 113
public class ByteReader
{
	// Token: 0x06000378 RID: 888 RVA: 0x00020E39 File Offset: 0x0001F039
	public ByteReader(byte[] bytes)
	{
		this.mBuffer = bytes;
	}

	// Token: 0x06000379 RID: 889 RVA: 0x00020E48 File Offset: 0x0001F048
	public ByteReader(TextAsset asset)
	{
		this.mBuffer = asset.bytes;
	}

	// Token: 0x0600037A RID: 890 RVA: 0x00020E5C File Offset: 0x0001F05C
	public static ByteReader Open(string path)
	{
		FileStream fileStream = File.OpenRead(path);
		if (fileStream != null)
		{
			fileStream.Seek(0L, SeekOrigin.End);
			byte[] array = new byte[fileStream.Position];
			fileStream.Seek(0L, SeekOrigin.Begin);
			fileStream.Read(array, 0, array.Length);
			fileStream.Close();
			return new ByteReader(array);
		}
		return null;
	}

	// Token: 0x17000067 RID: 103
	// (get) Token: 0x0600037B RID: 891 RVA: 0x00020EAE File Offset: 0x0001F0AE
	public bool canRead
	{
		get
		{
			return this.mBuffer != null && this.mOffset < this.mBuffer.Length;
		}
	}

	// Token: 0x0600037C RID: 892 RVA: 0x00020ECA File Offset: 0x0001F0CA
	private static string ReadLine(byte[] buffer, int start, int count)
	{
		return Encoding.UTF8.GetString(buffer, start, count);
	}

	// Token: 0x0600037D RID: 893 RVA: 0x00020ED9 File Offset: 0x0001F0D9
	public string ReadLine()
	{
		return this.ReadLine(true);
	}

	// Token: 0x0600037E RID: 894 RVA: 0x00020EE4 File Offset: 0x0001F0E4
	public string ReadLine(bool skipEmptyLines)
	{
		int num = this.mBuffer.Length;
		if (skipEmptyLines)
		{
			while (this.mOffset < num && this.mBuffer[this.mOffset] < 32)
			{
				this.mOffset++;
			}
		}
		int i = this.mOffset;
		if (i < num)
		{
			while (i < num)
			{
				int num2 = (int)this.mBuffer[i++];
				if (num2 == 10 || num2 == 13)
				{
					IL_62:
					string result = ByteReader.ReadLine(this.mBuffer, this.mOffset, i - this.mOffset - 1);
					this.mOffset = i;
					return result;
				}
			}
			i++;
			goto IL_62;
		}
		this.mOffset = num;
		return null;
	}

	// Token: 0x0600037F RID: 895 RVA: 0x00020F80 File Offset: 0x0001F180
	public Dictionary<string, string> ReadDictionary()
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		char[] separator = new char[]
		{
			'='
		};
		while (this.canRead)
		{
			string text = this.ReadLine();
			if (text == null)
			{
				break;
			}
			if (!text.StartsWith("//"))
			{
				string[] array = text.Split(separator, 2, StringSplitOptions.RemoveEmptyEntries);
				if (array.Length == 2)
				{
					string key = array[0].Trim();
					string value = array[1].Trim().Replace("\\n", "\n");
					dictionary[key] = value;
				}
			}
		}
		return dictionary;
	}

	// Token: 0x06000380 RID: 896 RVA: 0x00021000 File Offset: 0x0001F200
	public BetterList<string> ReadCSV()
	{
		ByteReader.mTemp.Clear();
		string text = "";
		bool flag = false;
		int num = 0;
		while (this.canRead)
		{
			if (flag)
			{
				string text2 = this.ReadLine(false);
				if (text2 == null)
				{
					return null;
				}
				text2 = text2.Replace("\\n", "\n");
				text = text + "\n" + text2;
			}
			else
			{
				text = this.ReadLine(true);
				if (text == null)
				{
					return null;
				}
				text = text.Replace("\\n", "\n");
				num = 0;
			}
			int i = num;
			int length = text.Length;
			while (i < length)
			{
				char c = text[i];
				if (c == ',')
				{
					if (!flag)
					{
						ByteReader.mTemp.Add(text.Substring(num, i - num));
						num = i + 1;
					}
				}
				else if (c == '"')
				{
					if (flag)
					{
						if (i + 1 >= length)
						{
							ByteReader.mTemp.Add(text.Substring(num, i - num).Replace("\"\"", "\""));
							return ByteReader.mTemp;
						}
						if (text[i + 1] != '"')
						{
							ByteReader.mTemp.Add(text.Substring(num, i - num).Replace("\"\"", "\""));
							flag = false;
							if (text[i + 1] == ',')
							{
								i++;
								num = i + 1;
							}
						}
						else
						{
							i++;
						}
					}
					else
					{
						num = i + 1;
						flag = true;
					}
				}
				i++;
			}
			if (num < text.Length)
			{
				if (flag)
				{
					continue;
				}
				ByteReader.mTemp.Add(text.Substring(num, text.Length - num));
			}
			else
			{
				ByteReader.mTemp.Add("");
			}
			return ByteReader.mTemp;
		}
		return null;
	}

	// Token: 0x040004B5 RID: 1205
	private byte[] mBuffer;

	// Token: 0x040004B6 RID: 1206
	private int mOffset;

	// Token: 0x040004B7 RID: 1207
	private static BetterList<string> mTemp = new BetterList<string>();
}
