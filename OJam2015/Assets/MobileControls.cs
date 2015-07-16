using UnityEngine;
using System.Collections;

public class MobileControls : MonoBehaviour 
{
	void Update () 
	{
		transform.Translate(Input.acceleration.x * Time.deltaTime, -Input.acceleration.z * Time.deltaTime, 0);
	}
}