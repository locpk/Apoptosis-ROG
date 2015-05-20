using UnityEngine;
using System.Collections;

public class Blip : MonoBehaviour {
	public Transform target;
	MiniMap map;
	RectTransform myRectTransform;



	// Use this for initialization
	void Start () {
		map = GetComponentInParent<MiniMap> ();
		myRectTransform = GetComponent<RectTransform> ();
	}
	
	void Update () {
			Vector2 newPosition = map.TransformPosition (target.position);

			myRectTransform.localPosition = newPosition;
	}
}

