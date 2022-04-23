using System;
using UnityEngine;
using UnityEngine.UI;

namespace WordSearch
{
	public class WordListLayoutGroup : LayoutGroup
	{
		public int spacing;

		public int rows;

		public override void CalculateLayoutInputHorizontal()
		{
			this.CalculateLayoutInputForAxis(0);
		}

		public override void CalculateLayoutInputVertical()
		{
			this.CalculateLayoutInputForAxis(1);
		}

		public override void SetLayoutHorizontal()
		{
			this.SetLayoutAlongAxis(0);
		}

		public override void SetLayoutVertical()
		{
			this.SetLayoutAlongAxis(1);
		}

		private void CalculateLayoutInputForAxis(int axis)
		{
			float num = 0f;
			if (axis == 0)
			{
				float rowPreferredWidth = this.GetRowPreferredWidth();
				float num2 = 0f;
				for (int i = 0; i < base.transform.childCount; i++)
				{
					RectTransform rect = base.transform.GetChild(i) as RectTransform;
					num2 += LayoutUtility.GetPreferredSize(rect, 0) + (float)this.spacing;
					if (num2 >= rowPreferredWidth)
					{
						num = Mathf.Max(num, num2);
						num2 = 0f;
					}
				}
			}
			else if (base.transform.childCount > 0)
			{
				num = LayoutUtility.GetPreferredSize(base.transform.GetChild(0) as RectTransform, 1) * (float)this.rows + (float)(this.spacing * (this.rows - 1));
			}
			num += (float)((axis != 0) ? this.m_Padding.vertical : this.m_Padding.horizontal);
			base.SetLayoutInputForAxis(num, num, num, axis);
		}

		private void SetLayoutAlongAxis(int axis)
		{
			float rowPreferredWidth = this.GetRowPreferredWidth();
			float startOffset = base.GetStartOffset(0, 0f);
			float startOffset2 = base.GetStartOffset(1, 0f);
			Vector2 vector = new Vector2(startOffset, startOffset2);
			for (int i = 0; i < base.transform.childCount; i++)
			{
				RectTransform rect = base.transform.GetChild(i) as RectTransform;
				float preferredSize = LayoutUtility.GetPreferredSize(rect, 0);
				base.SetChildAlongAxis(rect, axis, vector[axis], LayoutUtility.GetPreferredSize(rect, axis));
				vector.x += preferredSize + (float)this.spacing;
				if (vector.x - startOffset >= rowPreferredWidth)
				{
					vector.x = startOffset;
					vector.y += LayoutUtility.GetPreferredSize(rect, 1) + (float)this.spacing;
				}
			}
		}

		private float GetRowPreferredWidth()
		{
			float num = 0f;
			for (int i = 0; i < base.transform.childCount; i++)
			{
				RectTransform rect = base.transform.GetChild(i) as RectTransform;
				if (i != 0)
				{
					num += (float)this.spacing;
				}
				num += LayoutUtility.GetPreferredSize(rect, 0);
			}
			return num / (float)this.rows;
		}
	}
}
