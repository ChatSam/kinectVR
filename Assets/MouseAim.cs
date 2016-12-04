using UnityEngine;
using System.Collections;
using UnitySampleAssets.CrossPlatformInput;

public class MouseAim : MonoBehaviour {

	public Vector3 angleRange = new Vector3(80, 40, 0);
	Vector3 startAngle = new Vector3(72, 0, 270);
	Vector2 gamepadOffset;
//	float startX;
//	float startY;
	bool gamepad = true;
	// Use this for initialization
	void Start () {
		try{
			gamepadOffset = new Vector2 (CrossPlatformInputManager.GetAxisRaw ("padViewHorizontal"), CrossPlatformInputManager.GetAxisRaw ("padViewVertical"));
			gamepad = true;
		}catch{
			Debug.LogWarning("padViewHorizontal and/or padViewVertical is not defined, gamepad right thumbstick input will be ignored");
			gamepad = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		float x = (Input.mousePosition.x / Screen.width) - 0.5f;
		float y = (Input.mousePosition.y / Screen.height) - 0.5f;
		//Debug.Log (Input.mousePosition.x + " " + Screen.width + " " + (startAngle.y + angleRange.y * x) + " " + x);

//		if (x == startX && y == startY) 
//		{
//			x = 0; y = 0;
//		}
		if (gamepad) {
			float jx = (CrossPlatformInputManager.GetAxisRaw ("padViewHorizontal") - gamepadOffset.x) / 2;
			float jy = -(CrossPlatformInputManager.GetAxisRaw ("padViewVertical") - gamepadOffset.y) / 2;
			if (jx != 0 || jy != 0) {
				x = jx;
				y = jy;
			}
		}

//		x /= 2;
//		y /= 2;

		transform.localEulerAngles = new Vector3(startAngle.x - (angleRange.x * y), startAngle.y + (angleRange.y * x), startAngle.z);
	}
}
