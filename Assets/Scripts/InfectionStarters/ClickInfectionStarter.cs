using UnityEngine;
using System.Collections;

public class ClickInfectionStarter : InfectionStart {

	public bool StartInfection (GameController gameController, Vector2 startPosition)
	{
		float infectionRadius = 0.1f; // todo: bigger radius if you have upgrades?

		foreach (var human in GameObject.FindObjectsOfType<PersonMovement>()) {
			float humanScale = human.transform.localScale.x;
			if (Vector2.Distance (human.transform.position, startPosition) < humanScale / 2f + infectionRadius) {
				human.setInfected();
				return true; // infect only the first one that is found
			}
		}

		return false; // a person wasnt clicked and infection wasnt started, allow a new click
	}
}
