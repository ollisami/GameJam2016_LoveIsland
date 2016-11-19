using System;
using UnityEngine;

public class LoveProjectileInfectionStarter : InfectionStart
{
	public bool StartInfection (GameController gameController, UnityEngine.Vector2 startPosition)
	{
		GameObject loveProjectilePrefab = Resources.Load ("LoveProjectile") as GameObject; // from resources folder

		GameObject.Instantiate(loveProjectilePrefab, startPosition, Quaternion.Euler(0, 0, 0));
		GameObject.Instantiate(loveProjectilePrefab, startPosition, Quaternion.Euler(0, 0, 90));
		GameObject.Instantiate(loveProjectilePrefab, startPosition, Quaternion.Euler(0, 0, 180));
		GameObject.Instantiate(loveProjectilePrefab, startPosition, Quaternion.Euler(0, 0, 270));

		return true;
	}	
}

