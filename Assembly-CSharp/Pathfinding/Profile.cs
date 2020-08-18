using System;
using System.Diagnostics;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020005A4 RID: 1444
	public class Profile
	{
		// Token: 0x06002750 RID: 10064 RVA: 0x001ADB85 File Offset: 0x001ABD85
		public int ControlValue()
		{
			return this.control;
		}

		// Token: 0x06002751 RID: 10065 RVA: 0x001ADB8D File Offset: 0x001ABD8D
		public Profile(string name)
		{
			this.name = name;
			this.watch = new Stopwatch();
		}

		// Token: 0x06002752 RID: 10066 RVA: 0x00002ACE File Offset: 0x00000CCE
		public static void WriteCSV(string path, params Profile[] profiles)
		{
		}

		// Token: 0x06002753 RID: 10067 RVA: 0x001ADBB2 File Offset: 0x001ABDB2
		public void Run(Action action)
		{
			action();
		}

		// Token: 0x06002754 RID: 10068 RVA: 0x001ADBBA File Offset: 0x001ABDBA
		[Conditional("PROFILE")]
		public void Start()
		{
			this.watch.Start();
		}

		// Token: 0x06002755 RID: 10069 RVA: 0x001ADBC7 File Offset: 0x001ABDC7
		[Conditional("PROFILE")]
		public void Stop()
		{
			this.counter++;
			this.watch.Stop();
		}

		// Token: 0x06002756 RID: 10070 RVA: 0x001ADBE2 File Offset: 0x001ABDE2
		[Conditional("PROFILE")]
		public void Log()
		{
			UnityEngine.Debug.Log(this.ToString());
		}

		// Token: 0x06002757 RID: 10071 RVA: 0x001ADBEF File Offset: 0x001ABDEF
		[Conditional("PROFILE")]
		public void ConsoleLog()
		{
			Console.WriteLine(this.ToString());
		}

		// Token: 0x06002758 RID: 10072 RVA: 0x001ADBFC File Offset: 0x001ABDFC
		[Conditional("PROFILE")]
		public void Stop(int control)
		{
			this.counter++;
			this.watch.Stop();
			if (this.control == 1073741824)
			{
				this.control = control;
				return;
			}
			if (this.control != control)
			{
				throw new Exception(string.Concat(new object[]
				{
					"Control numbers do not match ",
					this.control,
					" != ",
					control
				}));
			}
		}

		// Token: 0x06002759 RID: 10073 RVA: 0x001ADC78 File Offset: 0x001ABE78
		[Conditional("PROFILE")]
		public void Control(Profile other)
		{
			if (this.ControlValue() != other.ControlValue())
			{
				throw new Exception(string.Concat(new object[]
				{
					"Control numbers do not match (",
					this.name,
					" ",
					other.name,
					") ",
					this.ControlValue(),
					" != ",
					other.ControlValue()
				}));
			}
		}

		// Token: 0x0600275A RID: 10074 RVA: 0x001ADCF4 File Offset: 0x001ABEF4
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.name,
				" #",
				this.counter,
				" ",
				this.watch.Elapsed.TotalMilliseconds.ToString("0.0 ms"),
				" avg: ",
				(this.watch.Elapsed.TotalMilliseconds / (double)this.counter).ToString("0.00 ms")
			});
		}

		// Token: 0x040041C3 RID: 16835
		private const bool PROFILE_MEM = false;

		// Token: 0x040041C4 RID: 16836
		public readonly string name;

		// Token: 0x040041C5 RID: 16837
		private readonly Stopwatch watch;

		// Token: 0x040041C6 RID: 16838
		private int counter;

		// Token: 0x040041C7 RID: 16839
		private long mem;

		// Token: 0x040041C8 RID: 16840
		private long smem;

		// Token: 0x040041C9 RID: 16841
		private int control = 1073741824;

		// Token: 0x040041CA RID: 16842
		private const bool dontCountFirst = false;
	}
}
