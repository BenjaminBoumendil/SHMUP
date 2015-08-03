using UnityEngine;
using System.Collections;

public class ABullet : MonoBehaviour {

	protected enum States {
		Stop,
		Run,
	}

	private AMovement movementType;
	protected States state;
	public GameObject exploFx;

	protected IEnumerator destroy(float time = 0) {
		yield return new WaitForSeconds (time);
		GameObject i = Instantiate (exploFx, transform.position, Quaternion.identity) as GameObject;
		Destroy(i, 2f);
		Destroy (gameObject);
	}

	protected virtual void rotate() { }
	protected virtual void coroutinesLauncher() { }

	public void SetMovement(AMovement _movementType) {
		movementType = _movementType;
	}

	void OnBecameInvisible() {
		Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag != tag)
			StartCoroutine (destroy ());
	}

	void Start() {
		state = States.Run;
		coroutinesLauncher ();
	}

	void Update () {
		if (state == States.Run)
			rotate ();
			movementType.Move (gameObject);
	}
}
