using System;
using System.Diagnostics;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020005BA RID: 1466
	public class Profile
	{
		// Token: 0x060027A6 RID: 10150 RVA: 0x001B3771 File Offset: 0x001B1971
		public int ControlValue()
		{
			return this.control;
		}

		// Token: 0x060027A7 RID: 10151 RVA: 0x001B3779 File Offset: 0x001B1979
		public Profile(string name)
		{
			this.name = name;
			this.watch = new Stopwatch();
		}

		// Token: 0x060027A8 RID: 10152 RVA: 0x00002ACE File Offset: 0x00000CCE
		public static void WriteCSV(string path, params Profile[] profiles)
		{
		}

		// Token: 0x060027A9 RID: 10153 RVA: 0x001B379E File Offset: 0x001B199E
		public void Run(Action action)
		{
			action();
		}

		// Token: 0x060027AA RID: 10154 RVA: 0x001B37A6 File Offset: 0x001B19A6
		[Conditional("PROFILE")]
		public void Start()
		{
			this.watch.Start();
		}

		// Token: 0x060027AB RID: 10155 RVA: 0x001B37B3 File Offset: 0x001B19B3
		[Conditional("PROFILE")]
		public void Stop()
		{
			this.counter++;
			this.watch.Stop();
		}

		// Token: 0x060027AC RID: 10156 RVA: 0x001B37CE File Offset: 0x001B19CE
		[Conditional("PROFILE")]
		public void Log()
		{
			UnityEngine.Debug.Log(this.ToString());
		}

		// Token: 0x060027AD RID: 10157 RVA: 0x001B37DB File Offset: 0x001B19DB
		[Conditional("PROFILE")]
		public void ConsoleLog()
		{
			Console.WriteLine(this.ToString());
		}

		// Token: 0x060027AE RID: 10158 RVA: 0x001B37E8 File Offset: 0x001B19E8
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

		// Token: 0x060027AF RID: 10159 RVA: 0x001B3864 File Offset: 0x001B1A64
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

		// Token: 0x060027B0 RID: 10160 RVA: 0x001B38E0 File Offset: 0x001B1AE0
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

		// Token: 0x040042A1 RID: 17057
		private const bool PROFILE_MEM = false;

		// Token: 0x040042A2 RID: 17058
		public readonly string name;

		// Token: 0x040042A3 RID: 17059
		private readonly Stopwatch watch;

		// Token: 0x040042A4 RID: 17060
		private int counter;

		// Token: 0x040042A5 RID: 17061
		private long mem;

		// Token: 0x040042A6 RID: 17062
		private long smem;

		// Token: 0x040042A7 RID: 17063
		private int control = 1073741824;

		// Token: 0x040042A8 RID: 17064
		private const bool dontCountFirst = false;
	}
}
