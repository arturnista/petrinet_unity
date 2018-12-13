using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFadeout : MonoBehaviour {

	public float time = 1f;

	private float fadeoutAmount;
	private SpriteRenderer spriteRenderer;
	private Color spriteColor;

	void Awake () {
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteColor = spriteRenderer.color;
		fadeoutAmount = time / 1f;
	}
	
	void Update () {
		spriteColor.a -= fadeoutAmount * Time.deltaTime;
		spriteRenderer.color = spriteColor;

		if(spriteColor.a < 0) Destroy(gameObject);
	}
}
