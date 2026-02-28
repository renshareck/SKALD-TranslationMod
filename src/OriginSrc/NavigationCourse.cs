using System;
using System.Collections.Generic;
using System.Drawing;

// Token: 0x02000177 RID: 375
[Serializable]
public class NavigationCourse
{
	// Token: 0x0600141D RID: 5149 RVA: 0x00059609 File Offset: 0x00057809
	public void clearCourse()
	{
		this.course.Clear();
	}

	// Token: 0x0600141E RID: 5150 RVA: 0x00059616 File Offset: 0x00057816
	public Point popFirstNode()
	{
		Point result = this.course[0];
		this.course.RemoveAt(0);
		return result;
	}

	// Token: 0x0600141F RID: 5151 RVA: 0x00059630 File Offset: 0x00057830
	public Point getDestination()
	{
		Point result = new Point(-1, -1);
		if (this.course.Count != 0)
		{
			result.X = this.course[this.course.Count - 1].X;
			result.Y = this.course[this.course.Count - 1].Y;
		}
		return result;
	}

	// Token: 0x06001420 RID: 5152 RVA: 0x000596A4 File Offset: 0x000578A4
	public string printCourse()
	{
		string text = "";
		foreach (Point point in this.course)
		{
			text = string.Concat(new string[]
			{
				text,
				point.X.ToString(),
				" / ",
				point.Y.ToString(),
				"\n"
			});
		}
		MainControl.log(text);
		return text;
	}

	// Token: 0x06001421 RID: 5153 RVA: 0x00059744 File Offset: 0x00057944
	public int getLength()
	{
		return this.course.Count;
	}

	// Token: 0x06001422 RID: 5154 RVA: 0x00059751 File Offset: 0x00057951
	public void addNode(int x, int y)
	{
		this.course.Add(new Point(x, y));
	}

	// Token: 0x06001423 RID: 5155 RVA: 0x00059765 File Offset: 0x00057965
	public void reverseCourse()
	{
		this.course.Reverse();
	}

	// Token: 0x06001424 RID: 5156 RVA: 0x00059772 File Offset: 0x00057972
	public bool hasNodes()
	{
		return this.course != null && this.course.Count > 0;
	}

	// Token: 0x0400051C RID: 1308
	public List<Point> course = new List<Point>();
}
