using UnityEngine;
using System.Collections;

public class MobileControls : MonoBehaviour 
{
	void Update () 
	{
		transform.Translate(Input.acceleration.x, -Input.acceleration.z, 0);
	}
}