using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private GameManager gameManager;

	private void moveUp() {
		transform.Translate (0, Time.deltaTime * 2f, 0);
	}

	void Start() {
		gameManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager>();
	}

	void Update () {
		if (gameManager.state != GameStates.Pause) {
			if (gameManager.state == GameStates.Run)
				moveUp ();
		}
	}
}
