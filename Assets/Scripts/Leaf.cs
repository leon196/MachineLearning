using UnityEngine;
using System.Collections;

public class Leaf : MonoBehaviour {

	private TextMesh text;
	private float intensity = 0.0f;
	private float intensityRandom = 0.0f;
	private float speedRandom = 0.0f;

	void Start ()
	{
		text = GetComponentInChildren<TextMesh>() as TextMesh;
		intensityRandom = Random.Range(-1.0f, 1.0f);
		speedRandom = Random.Range(0.5f, 3.0f);
	}
	
	void Update ()
	{
		intensity = Mathf.Cos(Time.time * speedRandom + intensityRandom * Mathf.PI * 2.0f);
		text.text = "" + intensity;
	}

	public float Intensity { get { return intensity; } }
}
