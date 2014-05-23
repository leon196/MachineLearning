using UnityEngine;
using System.Collections;
using QLearningFramework;

public class testQLearning : MonoBehaviour {

	internal enum StateNameEnum
	{
		A, B, C, D, E, F
	}	 

	// Use this for initialization
	void Start () {
		Debug.Log ("IN START");
		PathFinding();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	static void PathFinding()
	{
		Debug.Log ("TEST BEGINS");
		QLearning q = new QLearning { Episodes = 1000, Alpha = 0.1, Gamma = 0.9, 
			MaxExploreStepsWithinOneEpisode = 1000 };
		
		QAction fromTo;
		QState state;
		string stateName;
		string stateNameNext;

		//Define the goal/end states
		q.EndStates.Add(StateNameEnum.C.EnumToString()); 

		// State A           
		stateName = StateNameEnum.A.EnumToString();
		q.AddState(state = new QState(stateName, q));
		// action A -> B
		stateNameNext = StateNameEnum.B.EnumToString();
		state.AddAction(fromTo = new QAction(stateName, new QActionName(stateName, stateNameNext) ));
		// action outcome probability
		fromTo.AddActionResult(new QActionResult(fromTo, stateNameNext, 1.0));
		// action A -> D
		stateNameNext = StateNameEnum.D.EnumToString();
		state.AddAction(fromTo = new QAction(stateName, new QActionName(stateName, stateNameNext)));
		// action outcome probability
		fromTo.AddActionResult(new QActionResult(fromTo, stateNameNext, 1.0));
		
		// State B
		stateName = StateNameEnum.B.EnumToString();
		q.States.Add(state = new QState(stateName, q));
		// B -> A
		stateNameNext = StateNameEnum.A.EnumToString();
		state.AddAction(fromTo = new QAction(stateName, new QActionName(stateName, stateNameNext)));
		fromTo.AddActionResult(new QActionResult(fromTo, stateNameNext, 1.0));
		// B -> C
		stateNameNext = StateNameEnum.C.EnumToString();
		state.AddAction(fromTo = new QAction(stateName, new QActionName(stateName, stateNameNext)));
		// action outcome probability
		fromTo.AddActionResult(new QActionResult(fromTo, StateNameEnum.C.EnumToString(), 0.1, 100));
		fromTo.AddActionResult(new QActionResult(fromTo, StateNameEnum.A.EnumToString(), 0.9));
		// B -> E
		stateNameNext = StateNameEnum.E.EnumToString();
		state.AddAction(fromTo = new QAction(stateName, new QActionName(stateName, stateNameNext)));
		fromTo.AddActionResult(new QActionResult(fromTo, stateNameNext, 1.0));
		
		// State C
		stateName = StateNameEnum.C.EnumToString();
		q.States.Add(state = new QState(stateName, q));
		// C -> C
		stateNameNext = stateName;
		state.AddAction(fromTo = new QAction(stateName, new QActionName(stateName, stateNameNext)));
		fromTo.AddActionResult(new QActionResult(fromTo, stateNameNext, 1.0));
		
		// State D
		stateName = StateNameEnum.D.EnumToString();
		q.States.Add(state = new QState(stateName, q));
		// D -> A
		stateNameNext = StateNameEnum.A.EnumToString();
		state.AddAction(fromTo = new QAction(stateName, new QActionName(stateName, stateNameNext)));
		fromTo.AddActionResult(new QActionResult(fromTo, stateNameNext, 1.0));
		// D -> E
		stateNameNext = StateNameEnum.E.EnumToString();
		state.AddAction(fromTo = new QAction(stateName, new QActionName(stateName, stateNameNext)));
		fromTo.AddActionResult(new QActionResult(fromTo, stateNameNext, 1.0));
		
		// State E
		stateName = StateNameEnum.E.EnumToString();
		q.States.Add(state = new QState(stateName, q));
		// E -> B
		stateNameNext = StateNameEnum.B.EnumToString();
		state.AddAction(fromTo = new QAction(stateName, new QActionName(stateName, stateNameNext)));
		fromTo.AddActionResult(new QActionResult(fromTo, stateNameNext, 1.0));
		// E -> D
		stateNameNext = StateNameEnum.D.EnumToString();
		state.AddAction(fromTo = new QAction(stateName, new QActionName(stateName, stateNameNext)));
		fromTo.AddActionResult(new QActionResult(fromTo, stateNameNext, 1.0));
		// E -> F
		stateNameNext = StateNameEnum.F.EnumToString();
		state.AddAction(fromTo = new QAction(stateName, new QActionName(stateName, stateNameNext)));
		fromTo.AddActionResult(new QActionResult(fromTo, stateNameNext, 1.0));
		
		// State F
		stateName = StateNameEnum.F.EnumToString();
		q.States.Add(state = new QState(stateName, q));
		// F -> C
		stateNameNext = StateNameEnum.C.EnumToString();
		state.AddAction(fromTo = new QAction(stateName, new QActionName(stateName, stateNameNext)));
		fromTo.AddActionResult(new QActionResult(fromTo, stateNameNext, 1.0, 100));
		// F -> E
		stateNameNext = StateNameEnum.E.EnumToString();
		state.AddAction(fromTo = new QAction(stateName, new QActionName(stateName, stateNameNext)));
		fromTo.AddActionResult(new QActionResult(fromTo, stateNameNext, 1.0));

		q.RunTraining();
		q.PrintQLearningStructure();
		q.ShowPolicy();

		Debug.Log ("TEST ENDS");
	}
}
