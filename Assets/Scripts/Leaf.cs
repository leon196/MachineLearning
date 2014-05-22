using UnityEngine;
using System.Collections;
using System;

public class Leaf : MonoBehaviour {

	//private TextMesh text;
	private float energy = 0.0f;
	private float offsetRandom = 0.0f;
	private float speedRandom = 0.0f;

	void Start ()
	{
		//text = GetComponentInChildren<TextMesh>() as TextMesh;
		offsetRandom = UnityEngine.Random.Range(-1.0f, 1.0f);
		speedRandom = UnityEngine.Random.Range(0.5f, 3.0f);

		// Get Energy from Light Intensity on the Grid
		energy = Manager.Instance.GetGrid().GetIntensityPosition(transform.position);
		//Debug.Log(energy);
	}
	
	//void Update ()
	//{
		// Animate Energy Flux
		//energy = Mathf.Cos(Time.time * speedRandom + offsetRandom * Mathf.PI * 2.0f);
		//text.text = String.Format("{0:0.000}", energy);
	//}

	public float Energy {
		get {
			return energy * Mathf.Cos(Time.time * speedRandom + offsetRandom * Mathf.PI * 2.0f);
		}
	}
}
