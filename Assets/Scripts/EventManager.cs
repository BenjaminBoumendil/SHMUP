using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {

	private GameManager gameManager;

	public delegate void FirstEvent();
	public event FirstEvent firstEvent;

	private void CheckFirstEvent() {
		if (Time.time - gameManager.timer >= 40f && firstEvent != null)
			firstEvent ();
	}

	void Start () {
		gameManager = GetComponent<GameManager> ();
	}

	void Update () {
		CheckFirstEvent ();
	}
}
