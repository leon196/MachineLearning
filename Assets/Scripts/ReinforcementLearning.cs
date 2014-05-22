using UnityEngine;
using System.Collections;

public class ReinforcementLearning : MonoBehaviour {

	public int bonus = 1;
	public int malus = -1;

<<<<<<< HEAD
	private int[] actions = new int[4];
	private int[] states = new int[3];
	private int[] q;
	// Use this for initialization
	void Start () {
	
		//Initialize array
//		actions = new string["push", "turn", "growFlower", "growLeaf"];
//		states = new string["rootPosition", "flowerPosition", "leafPosition"];
		q = new int[actions.Length] [states.Length];
=======
	// Use this for initialization
	void Start () {
	
>>>>>>> FETCH_HEAD
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
