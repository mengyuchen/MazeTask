using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class WayPointManager : MonoBehaviour {

	[SerializeField] private CollisionCheck[] wayPoints;
	[SerializeField] private GameObject player;
	float timer;
	

	// Use this for initialization
	void Start () {
		wayPoints = FindObjectsOfType<CollisionCheck>();
		// player = Valve.VR.InteractionSystem.Player.instace;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		for (int i = 0; i < wayPoints.Length; i ++){
			// if (wayPoints[i].triggered){
			// 	// record
			// 	Debug.Log(wayPoints[i].transform.name);
			// 	Debug.Log(player.transform.position.x);
			// 	Debug.Log(player.transform.position.y);
			// 	Debug.Log(player.transform.rotation);
			// 	Debug.Log(timer);
			// }
		}
	}
}
