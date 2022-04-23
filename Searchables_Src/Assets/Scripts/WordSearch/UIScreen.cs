using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WordSearch
{
	[RequireComponent(typeof(RectTransform))]
	public class UIScreen : MonoBehaviour
	{
		public string id;

		public List<GameObject> worldObjects;

		[Space]
		public bool showBannerAd;

		public AdsController.BannerPosition bannerPosition;

		public Color bannerPlacementColor;

		public int referenceHeight = 1920;

		public int bannerHeight = 130;

		private GameObject adPlacement;

		public RectTransform RectT
		{
			get
			{
				return base.gameObject.GetComponent<RectTransform>();
			}
		}

		private void Awake()
		{
			int width = Screen.width;
			int height = Screen.height;
			double num = (double)width / (double)height;
			if (num < 0.5625)
			{
				GameObject.Find("Canvas").GetComponent<CanvasScaler>().matchWidthOrHeight = 0f;
			}
			else
			{
				GameObject.Find("Canvas").GetComponent<CanvasScaler>().matchWidthOrHeight = 1f;
			}
		}

		private void OnDestroy()
		{
		}

		public virtual void Initialize()
		{
		}

		public virtual void OnShowing(string data)
		{
		}

		private void OnAdsRemoved()
		{
			if (this.adPlacement != null)
			{
				UnityEngine.Object.Destroy(this.adPlacement);
			}
		}

		private void SetupScreenToShowBannerAds()
		{
			GameObject gameObject = new GameObject("screen_content");
			gameObject.AddComponent<LayoutElement>().preferredHeight = (float)(this.referenceHeight - this.bannerHeight);
			gameObject.transform.SetParent(base.transform, false);
			for (int i = base.transform.childCount - 1; i >= 0; i--)
			{
				Transform child = base.transform.GetChild(i);
				if (child != gameObject.transform)
				{
					child.SetParent(gameObject.transform, false);
					child.SetAsFirstSibling();
				}
			}
			this.adPlacement = new GameObject("ad_placement");
			this.adPlacement.AddComponent<LayoutElement>().preferredHeight = (float)this.bannerHeight;
			this.adPlacement.AddComponent<Image>().color = this.bannerPlacementColor;
			this.adPlacement.transform.SetParent(base.transform, false);
			if (this.bannerPosition == AdsController.BannerPosition.Top)
			{
				this.adPlacement.transform.SetAsFirstSibling();
			}
			else
			{
				this.adPlacement.transform.SetAsLastSibling();
			}
			base.gameObject.AddComponent<VerticalLayoutGroup>();
		}
	}
}
