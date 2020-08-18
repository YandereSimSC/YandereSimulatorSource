using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020005A0 RID: 1440
	public class AstarProfiler
	{
		// Token: 0x0600272C RID: 10028 RVA: 0x000045DB File Offset: 0x000027DB
		private AstarProfiler()
		{
		}

		// Token: 0x0600272D RID: 10029 RVA: 0x001AC1AC File Offset: 0x001AA3AC
		[Conditional("ProfileAstar")]
		public static void InitializeFastProfile(string[] profileNames)
		{
			AstarProfiler.fastProfileNames = new string[profileNames.Length + 2];
			Array.Copy(profileNames, AstarProfiler.fastProfileNames, profileNames.Length);
			AstarProfiler.fastProfileNames[AstarProfiler.fastProfileNames.Length - 2] = "__Control1__";
			AstarProfiler.fastProfileNames[AstarProfiler.fastProfileNames.Length - 1] = "__Control2__";
			AstarProfiler.fastProfiles = new AstarProfiler.ProfilePoint[AstarProfiler.fastProfileNames.Length];
			for (int i = 0; i < AstarProfiler.fastProfiles.Length; i++)
			{
				AstarProfiler.fastProfiles[i] = new AstarProfiler.ProfilePoint();
			}
		}

		// Token: 0x0600272E RID: 10030 RVA: 0x001AC22D File Offset: 0x001AA42D
		[Conditional("ProfileAstar")]
		public static void StartFastProfile(int tag)
		{
			AstarProfiler.fastProfiles[tag].watch.Start();
		}

		// Token: 0x0600272F RID: 10031 RVA: 0x001AC240 File Offset: 0x001AA440
		[Conditional("ProfileAstar")]
		public static void EndFastProfile(int tag)
		{
			AstarProfiler.ProfilePoint profilePoint = AstarProfiler.fastProfiles[tag];
			profilePoint.totalCalls++;
			profilePoint.watch.Stop();
		}

		// Token: 0x06002730 RID: 10032 RVA: 0x00002ACE File Offset: 0x00000CCE
		[Conditional("ASTAR_UNITY_PRO_PROFILER")]
		public static void EndProfile()
		{
		}

		// Token: 0x06002731 RID: 10033 RVA: 0x001AC264 File Offset: 0x001AA464
		[Conditional("ProfileAstar")]
		public static void StartProfile(string tag)
		{
			AstarProfiler.ProfilePoint profilePoint;
			AstarProfiler.profiles.TryGetValue(tag, out profilePoint);
			if (profilePoint == null)
			{
				profilePoint = new AstarProfiler.ProfilePoint();
				AstarProfiler.profiles[tag] = profilePoint;
			}
			profilePoint.tmpBytes = GC.GetTotalMemory(false);
			profilePoint.watch.Start();
		}

		// Token: 0x06002732 RID: 10034 RVA: 0x001AC2AC File Offset: 0x001AA4AC
		[Conditional("ProfileAstar")]
		public static void EndProfile(string tag)
		{
			if (!AstarProfiler.profiles.ContainsKey(tag))
			{
				UnityEngine.Debug.LogError("Can only end profiling for a tag which has already been started (tag was " + tag + ")");
				return;
			}
			AstarProfiler.ProfilePoint profilePoint = AstarProfiler.profiles[tag];
			profilePoint.totalCalls++;
			profilePoint.watch.Stop();
			profilePoint.totalBytes += GC.GetTotalMemory(false) - profilePoint.tmpBytes;
		}

		// Token: 0x06002733 RID: 10035 RVA: 0x001AC31C File Offset: 0x001AA51C
		[Conditional("ProfileAstar")]
		public static void Reset()
		{
			AstarProfiler.profiles.Clear();
			AstarProfiler.startTime = DateTime.UtcNow;
			if (AstarProfiler.fastProfiles != null)
			{
				for (int i = 0; i < AstarProfiler.fastProfiles.Length; i++)
				{
					AstarProfiler.fastProfiles[i] = new AstarProfiler.ProfilePoint();
				}
			}
		}

		// Token: 0x06002734 RID: 10036 RVA: 0x001AC364 File Offset: 0x001AA564
		[Conditional("ProfileAstar")]
		public static void PrintFastResults()
		{
			if (AstarProfiler.fastProfiles == null)
			{
				return;
			}
			for (int i = 0; i < 1000; i++)
			{
			}
			double num = AstarProfiler.fastProfiles[AstarProfiler.fastProfiles.Length - 2].watch.Elapsed.TotalMilliseconds / 1000.0;
			TimeSpan timeSpan = DateTime.UtcNow - AstarProfiler.startTime;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("============================\n\t\t\t\tProfile results:\n============================\n");
			stringBuilder.Append("Name\t\t|\tTotal Time\t|\tTotal Calls\t|\tAvg/Call\t|\tBytes");
			for (int j = 0; j < AstarProfiler.fastProfiles.Length; j++)
			{
				string text = AstarProfiler.fastProfileNames[j];
				AstarProfiler.ProfilePoint profilePoint = AstarProfiler.fastProfiles[j];
				int totalCalls = profilePoint.totalCalls;
				double num2 = profilePoint.watch.Elapsed.TotalMilliseconds - num * (double)totalCalls;
				if (totalCalls >= 1)
				{
					stringBuilder.Append("\n").Append(text.PadLeft(10)).Append("|   ");
					stringBuilder.Append(num2.ToString("0.0 ").PadLeft(10)).Append(profilePoint.watch.Elapsed.TotalMilliseconds.ToString("(0.0)").PadLeft(10)).Append("|   ");
					stringBuilder.Append(totalCalls.ToString().PadLeft(10)).Append("|   ");
					stringBuilder.Append((num2 / (double)totalCalls).ToString("0.000").PadLeft(10));
				}
			}
			stringBuilder.Append("\n\n============================\n\t\tTotal runtime: ");
			stringBuilder.Append(timeSpan.TotalSeconds.ToString("F3"));
			stringBuilder.Append(" seconds\n============================");
			UnityEngine.Debug.Log(stringBuilder.ToString());
		}

		// Token: 0x06002735 RID: 10037 RVA: 0x001AC53C File Offset: 0x001AA73C
		[Conditional("ProfileAstar")]
		public static void PrintResults()
		{
			TimeSpan timeSpan = DateTime.UtcNow - AstarProfiler.startTime;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("============================\n\t\t\t\tProfile results:\n============================\n");
			int num = 5;
			foreach (KeyValuePair<string, AstarProfiler.ProfilePoint> keyValuePair in AstarProfiler.profiles)
			{
				num = Math.Max(keyValuePair.Key.Length, num);
			}
			stringBuilder.Append(" Name ".PadRight(num)).Append("|").Append(" Total Time\t".PadRight(20)).Append("|").Append(" Total Calls ".PadRight(20)).Append("|").Append(" Avg/Call ".PadRight(20));
			foreach (KeyValuePair<string, AstarProfiler.ProfilePoint> keyValuePair2 in AstarProfiler.profiles)
			{
				double totalMilliseconds = keyValuePair2.Value.watch.Elapsed.TotalMilliseconds;
				int totalCalls = keyValuePair2.Value.totalCalls;
				if (totalCalls >= 1)
				{
					string key = keyValuePair2.Key;
					stringBuilder.Append("\n").Append(key.PadRight(num)).Append("| ");
					stringBuilder.Append(totalMilliseconds.ToString("0.0").PadRight(20)).Append("| ");
					stringBuilder.Append(totalCalls.ToString().PadRight(20)).Append("| ");
					stringBuilder.Append((totalMilliseconds / (double)totalCalls).ToString("0.000").PadRight(20));
					stringBuilder.Append(AstarMath.FormatBytesBinary((int)keyValuePair2.Value.totalBytes).PadLeft(10));
				}
			}
			stringBuilder.Append("\n\n============================\n\t\tTotal runtime: ");
			stringBuilder.Append(timeSpan.TotalSeconds.ToString("F3"));
			stringBuilder.Append(" seconds\n============================");
			UnityEngine.Debug.Log(stringBuilder.ToString());
		}

		// Token: 0x040041BD RID: 16829
		private static readonly Dictionary<string, AstarProfiler.ProfilePoint> profiles = new Dictionary<string, AstarProfiler.ProfilePoint>();

		// Token: 0x040041BE RID: 16830
		private static DateTime startTime = DateTime.UtcNow;

		// Token: 0x040041BF RID: 16831
		public static AstarProfiler.ProfilePoint[] fastProfiles;

		// Token: 0x040041C0 RID: 16832
		public static string[] fastProfileNames;

		// Token: 0x0200075D RID: 1885
		public class ProfilePoint
		{
			// Token: 0x04004A15 RID: 18965
			public Stopwatch watch = new Stopwatch();

			// Token: 0x04004A16 RID: 18966
			public int totalCalls;

			// Token: 0x04004A17 RID: 18967
			public long tmpBytes;

			// Token: 0x04004A18 RID: 18968
			public long totalBytes;
		}
	}
}
