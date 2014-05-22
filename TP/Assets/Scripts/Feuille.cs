using UnityEngine;
using System.Collections;

public class Feuille : MonoBehaviour {

	private TextMesh text;
	private float intensity = 0.0f;
	private float intensitySeed;

	void Start ()
	{
		text = GetComponentInChildren<TextMesh>() as TextMesh;
		intensitySeed = Random.Range(0.5f, 2.0f);
	}
	
	void Update ()
	{
		intensity = Mathf.Cos(Time.time * intensitySeed);
		text.text = "" + intensity;
	}

	public float Intensity { get { return intensity; } }
}
