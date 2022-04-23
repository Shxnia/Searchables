using System;
using UnityEngine;
using UnityEngine.UI;

namespace WordSearch
{
	[RequireComponent(typeof(Image))]
	public class ImageSpriteSheet : MonoBehaviour
	{
		[SerializeField]
		private Sprite spriteSheet;

		[SerializeField]
		private int numberOfFrames;

		[SerializeField]
		private int framesASecond;

		private Image image;

		private Sprite[] sprites;

		private float time;

		private void Start()
		{
			this.sprites = new Sprite[this.numberOfFrames];
			float num = (float)this.spriteSheet.texture.width / (float)this.numberOfFrames;
			float height = (float)this.spriteSheet.texture.height;
			for (int i = 0; i < this.numberOfFrames; i++)
			{
				this.sprites[i] = Sprite.Create(this.spriteSheet.texture, new Rect((float)i * num, 0f, num, height), new Vector2(0.5f, 0.5f));
			}
			this.image = base.gameObject.GetComponent<Image>();
			this.image.sprite = this.sprites[0];
		}

		private void Update()
		{
			this.time += Time.deltaTime;
			int num = (int)(this.time / (1f / (float)this.framesASecond)) % this.numberOfFrames;
			this.image.sprite = this.sprites[num];
		}
	}
}
