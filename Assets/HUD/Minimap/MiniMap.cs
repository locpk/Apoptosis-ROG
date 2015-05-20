using UnityEngine;
using System.Collections;

public class MiniMap : MonoBehaviour {
	public Transform target;


	public Vector2 TransformPosition(Vector3 position){
		Vector3 offset;
		offset.x = position.x - target.position.x;
		offset.y = position.y - target.position.y;
		offset.z = position.z - target.position.z;
		Vector2 newPosition = new Vector2 (offset.x, offset.y);

		return newPosition;
	}
}
