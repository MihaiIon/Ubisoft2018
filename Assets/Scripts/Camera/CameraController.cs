using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	[SerializeField] float smoothTime = 0.5f;		// Controls the speed at wich the camera adjusts to the player position
	[SerializeField] float threshold = 2f;		// Threshold distance for the camera to start moving to adjust to the player's position

	private GameObject player;
	private Vector3 offset;
	private bool inMovement = false;
	private Vector3 velocity = Vector3.zero;
	private float follow = 1f;

	// Use this for initialization
	void Start () {
        enabled = false;
	}
	
	// Update is called once per frame
	void LateUpdate () {

		MoveCamera ();
		OrientCamera ();
	}

	// Updates the position of the camera to follow the player
	private void MoveCamera()
	{
		Vector3 start = transform.position;
		Vector3 end = player.transform.position + offset;
		float distance = Vector3.Distance (start, end);

		if(distance > threshold)
		{
			inMovement = true;
		}
		else if(distance < 0.1)
		{
			inMovement = false;
		}

		if(inMovement)
		{
			// Allows the camera to go faster to catch up with the player
			if (Vector3.Magnitude (velocity) > 5)
				follow = Mathf.Lerp(follow, 3f, Time.deltaTime);
			else
				follow = Mathf.Lerp(follow, 1f, Time.deltaTime);

			transform.position = Vector3.SmoothDamp (start, end, ref velocity, smoothTime / follow);
		}
	}

	// Updates the rotation of the camera to follow the player
	private void OrientCamera()
	{
		// Tilts Camera down when player is under the cam
		// TODO
	}
}
