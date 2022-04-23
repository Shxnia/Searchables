using GoogleMobileAds.Api;
using System;

namespace WordSearch
{
	public class AdMob_Banner : SingletonComponent<AdMob_Banner>
	{
		private BannerView bannerView;

		private new void Awake()
		{
			if (Entity.admob_banner_flag)
			{
				this.bannerView = new BannerView("ca-app-pub-7423364019751577/8607289581", AdSize.Banner, AdPosition.Bottom);
				AdRequest.Builder builder = new AdRequest.Builder();
				AdRequest request = new AdRequest.Builder().Build();
				request = builder.Build();
				this.bannerView.LoadAd(request);
				this.bannerView.Hide();
				Entity.admob_banner_flag = false;
			}
		}

		private void Start()
		{
		}

		public void show_BannerView()
		{
			this.bannerView.Show();
		}

		public void hide_BannerView()
		{
			this.bannerView.Hide();
		}
	}
}
