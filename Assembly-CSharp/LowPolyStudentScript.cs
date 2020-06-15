using System;
using UnityEngine;

// Token: 0x02000327 RID: 807
public class LowPolyStudentScript : MonoBehaviour
{
	// Token: 0x0600181C RID: 6172 RVA: 0x000D6CCC File Offset: 0x000D4ECC
	private void Update()
	{
		if ((float)this.Student.StudentManager.LowDetailThreshold > 0f)
		{
			if (this.Student.Prompt.DistanceSqr > (float)this.Student.StudentManager.LowDetailThreshold)
			{
				if (!this.MyMesh.enabled)
				{
					this.Student.MyRenderer.enabled = false;
					this.MyMesh.enabled = true;
					return;
				}
			}
			else if (this.MyMesh.enabled)
			{
				this.Student.MyRenderer.enabled = true;
				this.MyMesh.enabled = false;
				return;
			}
		}
		else if (this.MyMesh.enabled)
		{
			this.Student.MyRenderer.enabled = true;
			this.MyMesh.enabled = false;
		}
	}

	// Token: 0x040022EB RID: 8939
	public StudentScript Student;

	// Token: 0x040022EC RID: 8940
	public Renderer TeacherMesh;

	// Token: 0x040022ED RID: 8941
	public Renderer MyMesh;
}
