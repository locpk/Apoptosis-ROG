using UnityEngine;
using System.Collections;

public class logo : MonoBehaviour {

    public float timer = 4.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
       
        if (timer <= 0.0f || Input.anyKeyDown)
        {
            Application.LoadLevel("HowToPlay");
            
        }
	}
}
