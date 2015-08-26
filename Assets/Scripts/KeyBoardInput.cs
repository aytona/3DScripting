using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// NOTE: Do not name script 'Input'
public class KeyBoardInput : MonoBehaviour {
	
	private float angle = 45f;
	private float speed = 5f;
	Dictionary<KeyCode, Vector3> directions;

	void Start()
	{
		directions = new Dictionary<KeyCode, Vector3>()
		{
			{KeyCode.W, Vector3.forward},
			{KeyCode.S, Vector3.back},
			{KeyCode.A, Vector3.left},
			{KeyCode.D, Vector3.right}
		};
	}

	void Update ()
	{
		if(Input.GetAxis("Fire1") != 0)
		{
			this.transform.Rotate(Vector3.up, angle * Time.deltaTime);
		}
		if(Input.GetKeyDown(KeyCode.Space))
		{
			this.transform.Translate(Vector3.right, Space.World);
		}
		if(Input.GetKey(KeyCode.Space))
		{
			this.transform.Rotate(Vector3.up, angle * Time.deltaTime);
		}
		if(Input.GetKeyUp(KeyCode.Space))
		{
			this.transform.Translate(Vector3.left, Space.World);
		}

		foreach(KeyCode direction in directions.Keys)
		{
			if(Input.GetKey(direction))
			{
				this.transform.Translate(directions[direction] * speed * Time.deltaTime, Space.Self);
			}
		}
	}
}
