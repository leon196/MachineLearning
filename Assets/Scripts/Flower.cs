using UnityEngine;
using System;
using System.Collections;

public class Flower : MonoBehaviour {

	//private TextMesh text;
	private float energy = 0.0f;
	private float offsetRandom = 0.0f;
	private float speedRandom = 0.0f;

	void Start ()
	{
		//text = GetComponentInChildren<TextMesh>() as TextMesh;
		offsetRandom = UnityEngine.Random.Range(-1.0f, 1.0f);
		speedRandom = UnityEngine.Random.Range(0.5f, 3.0f);
		energy = UnityEngine.Random.Range(0.4f, 0.8f);
	}
	
	//void Update ()
	//{
		//energy = Mathf.Cos(Time.time * speedRandom + offsetRandom * Mathf.PI * 2.0f);
		//text.text = String.Format("{0:0.000}", energy);
	//}

	public float Energy { get { return energy * Mathf.Cos(Time.time * speedRandom + offsetRandom * Mathf.PI * 2.0f); } }
}
