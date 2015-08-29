using UnityEngine;
using System.Collections;

public class AttackCenter : MonoBehaviour {

	public GameObject player;

	private float speed = 1f;

	void Start()
	{
		speed += Random.Range(0f, 2f);
	}

	void Update()
	{
		MoveTowardsTarget();
		PulseObject();
	}

	private void MoveTowardsTarget()
	{
		Vector3 targetPosition = player.transform.position;
		Vector3 currentPosition = this.transform.position;

		if(Vector3.Distance(currentPosition, targetPosition) > 1)
		{
			Vector3 directionOfTravel = targetPosition - currentPosition;
			directionOfTravel.Normalize();

			this.transform.Translate((directionOfTravel.x * speed * Time.deltaTime), (directionOfTravel.y * speed * Time.deltaTime), (directionOfTravel.z * speed * Time.deltaTime));
		}
		else{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
		}
	}

	private void PulseObject()
	{
		float rate = .5f;
		float maxScale = 1.2f;
		float minScale = .8f;

		float scale = (Mathf.Sin (Time.time * (rate * 2 * Mathf.PI)) + 1f)/2f;

		scale = Mathf.Lerp (minScale, maxScale, scale);
		transform.localScale = new Vector3(scale,scale,scale);
	}
}
