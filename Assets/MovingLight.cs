using UnityEngine;
using System.Collections;

public class MovingLight : MonoBehaviour {
	//very primitive!
	float count = 0;
	float swingAngle = 25;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.localEulerAngles = new Vector3 ((swingAngle * ((Mathf.Sin(count))) + 90) , 90, 90);
		count += Time.deltaTime;// / factor;
		//factor += Time.deltaTime / 100;
		swingAngle /= (1 + Time.deltaTime / 100);
	}
}
