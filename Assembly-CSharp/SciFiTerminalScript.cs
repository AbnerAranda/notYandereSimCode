using System;
using UnityEngine;

// Token: 0x020003D1 RID: 977
public class SciFiTerminalScript : MonoBehaviour
{
	// Token: 0x06001A6D RID: 6765 RVA: 0x00103FAA File Offset: 0x001021AA
	private void Start()
	{
		if (this.Student.StudentID != 65)
		{
			base.enabled = false;
			return;
		}
		this.RobotArms = this.Student.StudentManager.RobotArms;
	}

	// Token: 0x06001A6E RID: 6766 RVA: 0x00103FDC File Offset: 0x001021DC
	private void Update()
	{
		if (this.RobotArms != null)
		{
			if ((double)Vector3.Distance(this.RobotArms.TerminalTarget.position, base.transform.position) < 0.3 || (double)Vector3.Distance(this.RobotArms.TerminalTarget.position, this.OtherFinger.position) < 0.3)
			{
				if (!this.Updated)
				{
					this.Updated = true;
					if (!this.RobotArms.On[0])
					{
						this.RobotArms.ActivateArms();
						return;
					}
					this.RobotArms.ToggleWork();
					return;
				}
			}
			else
			{
				this.Updated = false;
			}
		}
	}

	// Token: 0x04002A0B RID: 10763
	public StudentScript Student;

	// Token: 0x04002A0C RID: 10764
	public RobotArmScript RobotArms;

	// Token: 0x04002A0D RID: 10765
	public Transform OtherFinger;

	// Token: 0x04002A0E RID: 10766
	public bool Updated;
}
