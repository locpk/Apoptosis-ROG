using UnityEngine;
using System.Collections;

public class FogOfWarSight : MonoBehaviour {
	public float radius = 10.0f;
	public LayerMask layermask = -1;
	public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		foreach (Collider col in Physics.OverlapSphere(player.transform.position, radius, layermask))
		         {
			col.SendMessage("Found", SendMessageOptions.DontRequireReceiver);
		}
	}
}
