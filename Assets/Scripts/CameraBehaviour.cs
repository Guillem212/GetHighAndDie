using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

	[Range(0, 20)]
	public float followSpeed = 10f;

	private Vector3 newPos;

	public Transform player;

	void LateUpdate(){
		newPos = player.position;
		newPos.z = -10;
		newPos.y += 2;
		transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
	}
}
