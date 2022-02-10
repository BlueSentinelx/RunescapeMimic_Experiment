using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUIControl : MonoBehaviour
{

	public float currentX = 0f;

	public Transform lookAtTarget;
	public Vector3 offset;
	public Transform CamTransform;
	private Camera cam;


	private void Start()
	{
		cam = GetComponent<Camera>();
		CamTransform = cam.transform;

	}

	private void LateUpdate()
	{
		offset = Quaternion.AngleAxis(currentX / 100.0f,Vector3.up) * offset;

		CamTransform.position = lookAtTarget.position + offset;

		CamTransform.LookAt(lookAtTarget.position);

	}
}