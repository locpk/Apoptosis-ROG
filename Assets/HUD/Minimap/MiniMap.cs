using UnityEngine;
using System.Collections;

public class MiniMap : MonoBehaviour {
	public Transform target;
	public GameObject map;

	Camera miniMapCamera;

	//Rect minimapGUIBorder, miniMapRectangle;
    Rect miniMapRectangle;

	public Vector2 TransformPosition(Vector3 position){
		Vector3 offset;
		offset.x = position.x - target.position.x;
		offset.y = position.y - target.position.y;
		offset.z = position.z - target.position.z;
		Vector2 newPosition = new Vector2 (offset.x, offset.y);

		return newPosition;
	}

	void Start(){
		miniMapRectangle = new Rect (map.GetComponentInChildren<RectTransform> ().rect);
		//minimapGUIBorder = new Rect (0, Screen.height - 150, 150, Screen.height);

		GameObject miniCam = new GameObject ("MiniMapCamera", typeof(Camera));
		miniMapCamera = miniCam.GetComponent<Camera> ();
		SetUpMinimapCamera ();
	}

	private void SetUpMinimapCamera(){
		miniMapCamera.transform.parent = this.transform;
		miniMapCamera.transform.position = new Vector3(0.0f, 20.0f, 0.0f);
		miniMapCamera.transform.Rotate (Vector3.right, 90.0f);
		miniMapCamera.orthographic = true;
		miniMapCamera.orthographicSize = 10.0f;

		int layerMask = 0;
		layerMask |= 1 << LayerMask.NameToLayer ("MiniMap");
		layerMask |= 1 << LayerMask.NameToLayer ("Layer1");

		miniMapCamera.cullingMask = layerMask;

		miniMapCamera.rect = new Rect (miniMapRectangle.x / Screen.width, miniMapRectangle.y / Screen.height,
		                              miniMapRectangle.width / Screen.width, miniMapRectangle.height / Screen.height);
	}
}
