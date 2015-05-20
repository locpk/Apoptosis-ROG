using UnityEngine;
using System.Collections;

public class bsvfovr : MonoBehaviour {

	NavMeshAgent navAgent;
	// Use this for initialization
	void Start () 
	{
		navAgent = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		if (Input.GetMouseButtonUp (1)) 
		{
			if (Physics.Raycast (ray, out hit, 100)) 
			{
				navAgent.SetDestination (hit.point);
			}
			
		}	
	}
}
