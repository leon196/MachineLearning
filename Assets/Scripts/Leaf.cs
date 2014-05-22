using UnityEngine;
using System.Collections;

public class Leaf : MonoBehaviour {

	private TextMesh text;
	private float energy = 0.0f;
	private float offsetRandom = 0.0f;
	private float speedRandom = 0.0f;

	void Start ()
	{
		text = GetComponentInChildren<TextMesh>() as TextMesh;
		offsetRandom = Random.Range(-1.0f, 1.0f);
		speedRandom = Random.Range(0.5f, 3.0f);
	}
	
	void Update ()
	{
		energy = Mathf.Cos(Time.time * speedRandom + offsetRandom * Mathf.PI * 2.0f);
		text.text = "" + energy;
	}

	public float Energy { get { return energy; } }
}
