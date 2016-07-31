using UnityEngine;
using System.Collections;

public class UnitScript : MonoBehaviour {
	public Transform target;
	public float speed;
	private bool atTar = false;
	private bool isSpawned = false;
	void Start () {
		
	}
	void Update () {
		Vector3 dir = target.transform.position - transform.position;
		float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis (angle, Vector3.up);
		//transform.Translate (Vector3.forward);
	}
}
