using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	private float speed = 3f;
	private float rotate = 90f;
	private float pulseRate = .5f;
	private float maxScale = 1.2f;
	private float minScale = .8f;

	void Update()
	{
		// Changes the object's position using Time.deltaTime to have consistent
		// results no matter the FPS of the player
		// this.transform.position = new Vector3(transform.position.x, transform.position.y + (speed * Time.deltaTime), transform.position.z);

		// Another way to change the object's position
		// between the world's axis(space.world) or the object's axis(space.self)
		// this.transform.Translate(0f, 0f, (speed * Time.deltaTime), Space.World);

		// Rotate object using Euler angles
		// this.transform.Rotate(0f, (rotate * Time.deltaTime), 0f, Space.Self);

		// Rotate the object using a vector
		// this.transform.Rotate(new Vector3(1f, 1f, 1f), rotate * Time.deltaTime, Space.Self);

		// Scale the object
		// float scale = 5f;
		// this.transform.localScale = new Vector3(scale, scale, scale);

		MoveTowardsTarget(Vector3.zero);
		RotateTowardsTarget(Vector3.zero);
		PulseObject();
	}

	private void MoveTowardsTarget(Vector3 targetPosition)
	{
		Vector3 currentPosition = this.transform.position;
		// Check to see if we're close enough to the target
		// This check prevents us from oscillating back and forth over the target
		if (Vector3.Distance(currentPosition, targetPosition) > 1)
		{
			// Get the direction we need, by subtracting the current position from the target position
			Vector3 directionOfTravel = targetPosition - currentPosition;
			// Normalize the direction, since we only want the direction information
			directionOfTravel.Normalize();

			this.transform.Translate((directionOfTravel.x * speed * Time.deltaTime), (directionOfTravel.y * speed * Time.deltaTime), (directionOfTravel.z * speed * Time.deltaTime), Space.World);
		}
	}

	private void RotateTowardsTarget(Vector3 targetPosition)
	{
		Vector3 currentPosition = this.transform.position;
		Quaternion currentRotation = this.transform.rotation;

		// Get the directio needed
		Vector3 directionOfLook = targetPosition - currentPosition;

		// Get target rotation
		Quaternion targetRotation = Quaternion.LookRotation(directionOfLook);

		// Apply the rotation
		transform.rotation = Quaternion.RotateTowards(currentRotation, targetRotation, rotate * Time.deltaTime);
	}

	private void PulseObject()
	{
		// Make sure the frequency matches the rate specified
		// Then get the amplitude to be between 0 and 1
		float scale = (Mathf.Sin(Time.time * (pulseRate * 2 * Mathf.PI)) + 1f)/2f;

		// Then interpolate that value between our min and max
		scale = Mathf.Lerp(minScale, maxScale, scale);

		transform.localScale = new Vector3(scale, scale, scale);
	}
}
