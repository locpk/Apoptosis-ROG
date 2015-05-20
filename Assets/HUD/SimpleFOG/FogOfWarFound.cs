using UnityEngine;
using System.Collections;

public class FogOfWarFound : MonoBehaviour {
	private bool found = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (found) {
			gameObject.GetComponentInChildren<Renderer> ().enabled = true;
		} else {
			gameObject.GetComponentInChildren<Renderer> ().enabled = false;
		}
		found = false;
	}

	void Found(){
		found = true;
	}
}
