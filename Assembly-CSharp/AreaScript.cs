using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000D0 RID: 208
public class AreaScript : MonoBehaviour
{
	// Token: 0x06000A19 RID: 2585 RVA: 0x00050714 File Offset: 0x0004E914
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Student"))
		{
			StudentScript component = other.GetComponent<StudentScript>();
			this.Students.Add(component);
			this.Population++;
		}
	}

	// Token: 0x06000A1A RID: 2586 RVA: 0x00050750 File Offset: 0x0004E950
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Student"))
		{
			StudentScript component = other.GetComponent<StudentScript>();
			this.Students.Remove(component);
			this.Population--;
		}
	}

	// Token: 0x04000A29 RID: 2601
	[Header("Do not touch any of these values. They get updated at runtime.")]
	[Tooltip("The amount of students in this area.")]
	public int Population;

	// Token: 0x04000A2A RID: 2602
	[Tooltip("A list of students in this area.")]
	public List<StudentScript> Students;

	// Token: 0x04000A2B RID: 2603
	[Tooltip("This area's crowd. Students will go here.")]
	public List<StudentScript> Crowd;
}
