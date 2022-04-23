using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace WordSearch
{
	public class AdsController : SingletonComponent<AdsController>
	{
		public enum BannerPosition
		{
			Top,
			Bottom
		}

		public enum InterstitialType
		{
			UnityAds,
			AdMob
		}

		private enum BannerState
		{
			Idle,
			Loading,
			Loaded
		}

		[SerializeField]
		private bool enableAdMobBannerAds;

		[SerializeField]
		private string androidBannerAdUnitID;

		[SerializeField]
		private string iosBannerAdUnitID;

		[SerializeField]
		private bool enableInterstitialAds;

		[SerializeField]
		private AdsController.InterstitialType interstitialType;

		[SerializeField]
		private string androidInterstitialAdUnitID;

		[SerializeField]
		private string iosInterstitialAdUnitID;

		[SerializeField]
		private bool enableUnityAdsInEditor;

		[SerializeField]
		private string zoneId;

		private AdsController.BannerState topBannerState;

		private AdsController.BannerState bottomBannerState;

		private bool isInterstitialAdLoaded;

		private Action interstitialAdClosedCallback;

		private Action _OnAdsRemoved_k__BackingField;

		public bool IsAdsEanbledInPlatform
		{
			get
			{
				return true;
			}
		}

		public bool IsBannerAdsEnabled
		{
			get
			{
				return this.IsAdsEanbledInPlatform && this.enableAdMobBannerAds;
			}
		}

		public bool IsInterstitialAdsEnabled
		{
			get
			{
				return this.IsAdsEanbledInPlatform && this.enableInterstitialAds;
			}
		}

		public Action OnAdsRemoved
		{
			get;
			set;
		}

		private string BannderAdUnitId
		{
			get
			{
				return this.androidBannerAdUnitID;
			}
		}

		private string InterstitialAdUnitId
		{
			get
			{
				return this.androidInterstitialAdUnitID;
			}
		}

		private void Start()
		{
		}

		private void OnDestroy()
		{
		}

		public bool ShowInterstitialAd(Action onAdClosed = null)
		{
			bool result = false;
			if (this.IsInterstitialAdsEnabled)
			{
				AdsController.InterstitialType interstitialType = this.interstitialType;
				if (interstitialType != AdsController.InterstitialType.UnityAds)
				{
					if (interstitialType != AdsController.InterstitialType.AdMob)
					{
					}
				}
				else
				{
					UnityEngine.Debug.LogError("[AdsController] Unity Ads are not enabled in services");
				}
			}
			return result;
		}
	}
}
