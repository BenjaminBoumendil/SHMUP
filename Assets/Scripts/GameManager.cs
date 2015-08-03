using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GameStates {
	Pause,
	Stop,
	Slow,
	Run,
}

public sealed class GameManager : MonoBehaviour {

	// Singleton
	public static GameManager Instance { get { return Nested.instance; } }

	private GameManager() { }

	private class Nested {
		internal static readonly GameManager instance = new GameManager();
		static Nested() { }
	}

	// GameManager
	private PlayerController playerController;
	public List<GameObject> bossList;
	public EventManager eventManager;
	public GameObject player;
	public GameStates state;
	public float timer;

	private void inputHandler() {
		if (Input.GetKeyDown("p")) {
			state = state == GameStates.Run ? GameStates.Pause : GameStates.Run;
			Time.timeScale = Time.timeScale == 0f ? 1f : 0f;
		}
	}

	// Event
	public void FirstEvent() {
		eventManager.firstEvent -= FirstEvent;
		state = GameStates.Stop;
		Vector3 cameraPos = Camera.main.gameObject.transform.position;
		Instantiate (bossList[0], new Vector3(cameraPos.x, cameraPos.y + 12f, 0f), Quaternion.identity);
	}

	void OnEnable() {
		eventManager.firstEvent += FirstEvent;
	}


	void OnGUI() {
        GUI.Label(new Rect((Screen.width - Camera.main.pixelWidth) / 1.9f, 1, 150, 20), playerController.score.ToString());

		GUI.Box (new Rect((Screen.width - Camera.main.pixelWidth) / 1.9f, Screen.height - 20, Camera.main.pixelWidth - 5, 20), playerController.energy.ToString());
	}

	void Start () {
		state = GameStates.Run;
		playerController = player.GetComponent<PlayerController> ();
		timer = Time.time;
	}

	void Update() {
		inputHandler ();
	}

}
