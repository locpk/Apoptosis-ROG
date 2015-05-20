using UnityEngine;
using System.Collections;

public class Trace : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = new Vector3(100 * PhotonNetwork.player.ID, transform.position.y, transform.position.z); ;
	}
}
