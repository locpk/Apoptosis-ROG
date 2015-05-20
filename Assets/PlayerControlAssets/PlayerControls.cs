﻿using UnityEngine;
using System.Collections;
using System.Linq;

public class PlayerControls : MonoBehaviour {

	// the speed at which the camera moves;
	public float cameraSpeed;
	// the unit selector
	public GameObject unitSelector;
	// the target selector
	public GameObject targetSelector;	
	// the list of selected units
	public System.Collections.Generic.List<GameObject> selectedUnits;
	// the list of selected targets
	public System.Collections.Generic.List<GameObject> selectedTargets;
	// the units movement script
	public Cell cellMover;
    
	public float sW = 5;

	//PUBLIC FUNCTIONS-----------------------------
    public void StopAllUnits()
    {
        foreach (var item in selectedUnits)
        {
            item.GetComponent<Cell>().SetTarget(null);
            item.GetComponent<Cell>().SetDestination(item.transform.position);
        }
    }

    public void DuplicateCells()
    {
        foreach (var item in selectedUnits)
        {
            if (item.GetComponent<Cell>().m_currentProteins % 2 == 0 && item.GetComponent<Cell>().m_currentProteins > 0)
            {
                item.GetComponent<Split>().Divide();
            }
        }
    }
	//END OF PUBLIC FUNCTIONS----------------------


	// Use this for initialization
	void Start () {
		selectedUnits.Clear ();
		selectedTargets.Clear ();
        GameObject.Find("InstructionsMenu").SetActive(true);
	}

	// Update is called once per frame
	void Update () {
        GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
		//select units
		if (Input.GetMouseButton (0))
			InstantiateUnitSelector ();
		else if (GameObject.FindGameObjectsWithTag ("UnitSelector").Length > 0) {
			SelectUnits();
			Destroy (GameObject.FindGameObjectWithTag ("UnitSelector"));
		}
		//camera scroll
		if (Input.GetKey(KeyCode.F))
			ScrollCamera ();
		if (Input.GetMouseButton (1) && !Input.GetKey(KeyCode.M)) {
			InstantiateTargetSelector();
		}
		else if (GameObject.FindGameObjectsWithTag ("TargetSelector").Length > 0) {
			//set targets
			if (selectedUnits.Count > 0) {
				SelectTargets();
			}
			//set waypoint
			if (selectedTargets.Count == 0) {
				foreach (var item in selectedTargets) {
					item.GetComponent<Cell>().SetTarget(null);
				}
				SetWaypoint ();
			}
			Destroy (GameObject.FindGameObjectWithTag ("TargetSelector"));
		}
		//multiply selected units
		if (Input.GetKeyUp(KeyCode.D)) {
            DuplicateCells();
		}
		//stop all actions
		if (Input.GetKeyDown(KeyCode.S)) {
            StopAllUnits();
		}
	}

	void ScrollCamera()
	{
		Vector3 toPos = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
		toPos.y = Camera.main.transform.position.y + (Input.GetAxis("Vertical") * Camera.main.transform.position.y);
		Vector3 fromPos = Camera.main.transform.position;
		Camera.main.transform.position = Vector3.MoveTowards (fromPos, toPos, Time.deltaTime * cameraSpeed * Vector3.Distance(fromPos,toPos));
	}
	
	void InstantiateUnitSelector() {
		if (GameObject.FindGameObjectsWithTag ("UnitSelector").Length == 0) {
			Vector3 instantiateAtPos = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
			instantiateAtPos.y = 0;
			Instantiate (unitSelector, instantiateAtPos, Quaternion.Euler(90, 0, 0));
		}
		
		Vector3 mousePos = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
		mousePos.y = 0;
		GameObject.FindGameObjectWithTag("UnitSelector").GetComponent<SphereCollider>().radius = Vector3.Distance (mousePos, GameObject.FindGameObjectWithTag("UnitSelector").transform.position);
		GameObject.FindGameObjectWithTag("UnitSelector").transform.localScale = Vector3.one * GameObject.FindGameObjectWithTag("UnitSelector").GetComponent<SphereCollider>().radius * 10;
	}

	void InstantiateTargetSelector() {
		if (GameObject.FindGameObjectsWithTag ("TargetSelector").Length == 0) {
			Vector3 instantiateAtPos = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
			instantiateAtPos.y = 0;
			Instantiate (targetSelector, instantiateAtPos, Quaternion.Euler(90, 0, 0));
		}
		
		Vector3 mousePos = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
		mousePos.y = 0;
		GameObject.FindGameObjectWithTag("TargetSelector").GetComponent<SphereCollider>().radius = Vector3.Distance (mousePos, GameObject.FindGameObjectWithTag("TargetSelector").transform.position);
		GameObject.FindGameObjectWithTag("TargetSelector").transform.localScale = Vector3.one * GameObject.FindGameObjectWithTag("TargetSelector").GetComponent<SphereCollider>().radius * 10;
	}
	
	void SelectUnits() {
		selectedUnits.Clear ();
		System.Collections.Generic.List<GameObject> cells = GameObject.FindGameObjectsWithTag ("Unit").ToList();
		for (int i = 0; i < cells.Count; i++) {
			Vector3 unitPos = new Vector3(cells[i].transform.position.x,0,cells[i].transform.position.z);
			if (GameObject.FindGameObjectWithTag("UnitSelector").GetComponent<SphereCollider>().radius >= Vector3.Distance(GameObject.FindGameObjectWithTag("UnitSelector").transform.position, unitPos) - cells[i].GetComponent<SphereCollider>().radius * 4) {
				selectedUnits.Add(cells[i]);
			}
		}
		for (int i = 0; i < selectedUnits.Count; i++) {
			while (i < selectedUnits.Count && !selectedUnits[i].GetComponent<PhotonView>().isMine) {
				selectedUnits.RemoveAt(i);
			}
		}
	}
	
	void SelectTargets() {
		selectedTargets.Clear ();
		System.Collections.Generic.List<GameObject> targets = GameObject.FindGameObjectsWithTag ("Target").ToList();
		for (int i = 0; i < targets.Count; i++) {
			targets[i] = targets[i].transform.parent.gameObject;
		}
		for (int i = 0; i < targets.Count; i++) {
			Vector3 targetPos = new Vector3(targets[i].transform.position.x,0,targets[i].transform.position.z);
			if (GameObject.FindGameObjectWithTag("TargetSelector").GetComponent<SphereCollider>().radius >= Vector3.Distance(GameObject.FindGameObjectWithTag("TargetSelector").transform.position, targetPos) - targets[i].GetComponent<SphereCollider>().radius * 4) {
				selectedTargets.Add(targets[i]);
			}
		}
		for (int i = 0; i < selectedTargets.Count; i++) {
			while (i < selectedTargets.Count && selectedTargets[i].GetComponent<PhotonView>().isMine) {
				selectedTargets.RemoveAt(i);
			}
		}
		foreach (var item in selectedUnits) {
			if (selectedTargets.Count > 0) {
				item.GetComponent<Cell>().SetTarget(selectedTargets[0]);
			}
		}
	}

	void SetWaypoint() {
		foreach (var item in selectedUnits) {
			item.GetComponent<Cell>().SetTarget(null);
			item.GetComponent<Cell>().SetDestination();
		}
	}
}