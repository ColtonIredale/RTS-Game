using UnityEngine;
using System.Collections;

public class TargetScript : MonoBehaviour {
	Plane plane;
	public Transform transformh;
	void Start (){
		plane = new Plane(Vector3.up, Vector3.zero);
	}

	void Update (){
		if (Input.GetMouseButtonDown (0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			float fDist = 0.0f;
			plane.Raycast (ray, out fDist); 
			transform.position = ray.GetPoint (fDist);
		}
	}
}
