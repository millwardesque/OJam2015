using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {
	public virtual void OnEnter() { }
	public virtual void OnExit() { }
	public virtual void OnUpdate() { }
}
