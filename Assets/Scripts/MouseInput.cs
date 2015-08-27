using UnityEngine;
using System.Collections;

public class MouseInput : MonoBehaviour {

	private float depthIntoScene = 10f;
	private float defaultDepthIntoScene = 5f;
	private float selectScale = .3f;

	void Update()
	{
		MoveToMouseAtSpecifiedDepth(depthIntoScene);
		// MoveToMouseAtObjectDepth();						// Good if I need to snap-on to other objects/colliders
	}

	void MoveToMouseAtObjectDepth()
	{
		Vector3 mouseScreenPosition = Input.mousePosition;

		// Create a ray that goes into the scene from the camera through the mouse position
		Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);
		float depth;
		RaycastHit hitInfo; // Create a variable to store information about the object hit

		// Check to see if the ray hits any objects in the scene
		// Also pass in hitInfo so that Raycast can store the information about the hit there
		// The out keyword is a parameter modifier used to tell C# that this object should be passed by reference
		// Basically it makes it so we can properly access hitInfo
		// NOTE: Objects we're hoping to hit with our ray must have a collider component
		if(Physics.Raycast(ray, out hitInfo))
		{
			// Move this object to the position we hit
			this.transform.position = new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z);
		}
		else{
			// If we didn't hit anything, set the depth to the arbitrary depth
			depth = depthIntoScene;
			// Now we can reuse our previous code to position the object using the depth we defined here
			MoveToMouseAtSpecifiedDepth(depth);
		}
	}

	void MoveToMouseAtSpecifiedDepth(float depth)
	{
		Vector3 mouseScreenPosition = Input.mousePosition;
		mouseScreenPosition.z = depth;
		Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
		this.transform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, mouseWorldPosition.z);
	}

	// OnMouseDown is called when the user has pressed the mouse button while over the GUI Element or Collider
	public void OnMouseDown()
	{
		// Get the vector from the camera to the object
		Vector3 headingToObject = this.transform.position - Camera.main.transform.position;
		// Find the projection on the forward vector of the camera
		depthIntoScene = Vector3.Dot(headingToObject, Camera.main.transform.forward);
	}

	// OnMouseDrag is called when the user has clicked on a GUI Element or collider and is still holding down the mouse button
	public void OnMouseDrag()
	{
		// When the mouse button is held and we move the mouse, move the object along with it
		// This provides simple click and drag funtionality
		MoveToMouseAtSpecifiedDepth(depthIntoScene);
	}

	// OnMouseEnter is called when the mouse entered the GUI Element or collider
	public void OnMouseEnter()
	{
		// Change the scale of the object to make it clear i's been selected
		this.transform.localScale += new Vector3(selectScale, selectScale, selectScale);
	}

	// OnMouseExit is called when the mouse is no longer over the GUI Element or collider
	public void OnMouseExit()
	{
		// Reset the scale to default when the object is no longer selected
		this.transform.localScale -= new Vector3(selectScale, selectScale, selectScale);
	}

	// OnMouseOver is called every frame while the mouse is over the GUI Element or collider
	public void OnMouseOver()
	{
		// While the ouse is over the object, rotate the object to show it's selected and give us a chance
		// to inspect all sides of the object
		this.transform.Rotate(Vector3.up, 45 * Time.deltaTime, Space.Self);
	}

	// OnMouseUp is called when the user has released the mouse button
	public void OnMouseUp()
	{
		// Always clean up when we're done. Reset the depth into scene back to the default when we're no
		// longer selecting an object
		depthIntoScene =  defaultDepthIntoScene;
	}
}
