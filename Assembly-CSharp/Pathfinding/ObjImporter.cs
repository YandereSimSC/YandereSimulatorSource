using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000579 RID: 1401
	public class ObjImporter
	{
		// Token: 0x06002612 RID: 9746 RVA: 0x001A4390 File Offset: 0x001A2590
		public static Mesh ImportFile(string filePath)
		{
			if (!File.Exists(filePath))
			{
				Debug.LogError("No file was found at '" + filePath + "'");
				return null;
			}
			ObjImporter.meshStruct meshStruct = ObjImporter.createMeshStruct(filePath);
			ObjImporter.populateMeshStruct(ref meshStruct);
			Vector3[] array = new Vector3[meshStruct.faceData.Length];
			Vector2[] array2 = new Vector2[meshStruct.faceData.Length];
			Vector3[] array3 = new Vector3[meshStruct.faceData.Length];
			int num = 0;
			foreach (Vector3 vector in meshStruct.faceData)
			{
				array[num] = meshStruct.vertices[(int)vector.x - 1];
				if (vector.y >= 1f)
				{
					array2[num] = meshStruct.uv[(int)vector.y - 1];
				}
				if (vector.z >= 1f)
				{
					array3[num] = meshStruct.normals[(int)vector.z - 1];
				}
				num++;
			}
			Mesh mesh = new Mesh();
			mesh.vertices = array;
			mesh.uv = array2;
			mesh.normals = array3;
			mesh.triangles = meshStruct.triangles;
			mesh.RecalculateBounds();
			return mesh;
		}

		// Token: 0x06002613 RID: 9747 RVA: 0x001A44C8 File Offset: 0x001A26C8
		private static ObjImporter.meshStruct createMeshStruct(string filename)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			ObjImporter.meshStruct result = default(ObjImporter.meshStruct);
			result.fileName = filename;
			StreamReader streamReader = File.OpenText(filename);
			string s = streamReader.ReadToEnd();
			streamReader.Dispose();
			using (StringReader stringReader = new StringReader(s))
			{
				string text = stringReader.ReadLine();
				char[] separator = new char[]
				{
					' '
				};
				while (text != null)
				{
					if (!text.StartsWith("f ") && !text.StartsWith("v ") && !text.StartsWith("vt ") && !text.StartsWith("vn "))
					{
						text = stringReader.ReadLine();
						if (text != null)
						{
							text = text.Replace("  ", " ");
						}
					}
					else
					{
						text = text.Trim();
						string[] array = text.Split(separator, 50);
						string a = array[0];
						if (!(a == "v"))
						{
							if (!(a == "vt"))
							{
								if (!(a == "vn"))
								{
									if (a == "f")
									{
										num5 = num5 + array.Length - 1;
										num += 3 * (array.Length - 2);
									}
								}
								else
								{
									num4++;
								}
							}
							else
							{
								num3++;
							}
						}
						else
						{
							num2++;
						}
						text = stringReader.ReadLine();
						if (text != null)
						{
							text = text.Replace("  ", " ");
						}
					}
				}
			}
			result.triangles = new int[num];
			result.vertices = new Vector3[num2];
			result.uv = new Vector2[num3];
			result.normals = new Vector3[num4];
			result.faceData = new Vector3[num5];
			return result;
		}

		// Token: 0x06002614 RID: 9748 RVA: 0x001A469C File Offset: 0x001A289C
		private static void populateMeshStruct(ref ObjImporter.meshStruct mesh)
		{
			StreamReader streamReader = File.OpenText(mesh.fileName);
			string s = streamReader.ReadToEnd();
			streamReader.Close();
			using (StringReader stringReader = new StringReader(s))
			{
				string text = stringReader.ReadLine();
				char[] separator = new char[]
				{
					' '
				};
				char[] separator2 = new char[]
				{
					'/'
				};
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				int num5 = 0;
				int num6 = 0;
				int num7 = 0;
				while (text != null)
				{
					if (!text.StartsWith("f ") && !text.StartsWith("v ") && !text.StartsWith("vt ") && !text.StartsWith("vn ") && !text.StartsWith("g ") && !text.StartsWith("usemtl ") && !text.StartsWith("mtllib ") && !text.StartsWith("vt1 ") && !text.StartsWith("vt2 ") && !text.StartsWith("vc ") && !text.StartsWith("usemap "))
					{
						text = stringReader.ReadLine();
						if (text != null)
						{
							text = text.Replace("  ", " ");
						}
					}
					else
					{
						text = text.Trim();
						string[] array = text.Split(separator, 50);
						string text2 = array[0];
						uint num8 = <PrivateImplementationDetails>.ComputeStringHash(text2);
						if (num8 <= 1179241374u)
						{
							if (num8 <= 1128908517u)
							{
								if (num8 != 990293175u)
								{
									if (num8 == 1128908517u)
									{
										if (text2 == "vn")
										{
											mesh.normals[num4] = new Vector3(Convert.ToSingle(array[1]), Convert.ToSingle(array[2]), Convert.ToSingle(array[3]));
											num4++;
										}
									}
								}
								else if (!(text2 == "mtllib"))
								{
								}
							}
							else if (num8 != 1146808303u)
							{
								if (num8 != 1163585922u)
								{
									if (num8 == 1179241374u)
									{
										if (!(text2 == "vc"))
										{
										}
									}
								}
								else if (text2 == "vt1")
								{
									mesh.uv[num6] = new Vector2(Convert.ToSingle(array[1]), Convert.ToSingle(array[2]));
									num6++;
								}
							}
							else if (text2 == "vt2")
							{
								mesh.uv[num7] = new Vector2(Convert.ToSingle(array[1]), Convert.ToSingle(array[2]));
								num7++;
							}
						}
						else if (num8 <= 1498016135u)
						{
							if (num8 != 1297068826u)
							{
								if (num8 != 1328799683u)
								{
									if (num8 == 1498016135u)
									{
										if (text2 == "vt")
										{
											mesh.uv[num5] = new Vector2(Convert.ToSingle(array[1]), Convert.ToSingle(array[2]));
											num5++;
										}
									}
								}
								else if (!(text2 == "usemtl"))
								{
								}
							}
							else if (!(text2 == "usemap"))
							{
							}
						}
						else if (num8 != 3792446982u)
						{
							if (num8 != 3809224601u)
							{
								if (num8 == 4077666505u)
								{
									if (text2 == "v")
									{
										mesh.vertices[num3] = new Vector3(Convert.ToSingle(array[1]), Convert.ToSingle(array[2]), Convert.ToSingle(array[3]));
										num3++;
									}
								}
							}
							else if (text2 == "f")
							{
								int num9 = 1;
								List<int> list = new List<int>();
								while (num9 < array.Length && (array[num9] ?? "").Length > 0)
								{
									Vector3 vector = default(Vector3);
									string[] array2 = array[num9].Split(separator2, 3);
									vector.x = (float)Convert.ToInt32(array2[0]);
									if (array2.Length > 1)
									{
										if (array2[1] != "")
										{
											vector.y = (float)Convert.ToInt32(array2[1]);
										}
										vector.z = (float)Convert.ToInt32(array2[2]);
									}
									num9++;
									mesh.faceData[num2] = vector;
									list.Add(num2);
									num2++;
								}
								num9 = 1;
								while (num9 + 2 < array.Length)
								{
									mesh.triangles[num] = list[0];
									num++;
									mesh.triangles[num] = list[num9];
									num++;
									mesh.triangles[num] = list[num9 + 1];
									num++;
									num9++;
								}
							}
						}
						else if (!(text2 == "g"))
						{
						}
						text = stringReader.ReadLine();
						if (text != null)
						{
							text = text.Replace("  ", " ");
						}
					}
				}
			}
		}

		// Token: 0x02000749 RID: 1865
		private struct meshStruct
		{
			// Token: 0x040049AE RID: 18862
			public Vector3[] vertices;

			// Token: 0x040049AF RID: 18863
			public Vector3[] normals;

			// Token: 0x040049B0 RID: 18864
			public Vector2[] uv;

			// Token: 0x040049B1 RID: 18865
			public Vector2[] uv1;

			// Token: 0x040049B2 RID: 18866
			public Vector2[] uv2;

			// Token: 0x040049B3 RID: 18867
			public int[] triangles;

			// Token: 0x040049B4 RID: 18868
			public int[] faceVerts;

			// Token: 0x040049B5 RID: 18869
			public int[] faceUVs;

			// Token: 0x040049B6 RID: 18870
			public Vector3[] faceData;

			// Token: 0x040049B7 RID: 18871
			public string name;

			// Token: 0x040049B8 RID: 18872
			public string fileName;
		}
	}
}
