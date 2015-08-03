using UnityEngine;
using System.Collections;

public class SeekerBullet : ABullet {

	private Vector3 target;

	protected override void rotate() {
		Vector3 diff = target - transform.position;
		float rotZ = Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, rotZ - 90);
	}

	private IEnumerator seeker() {
		yield return new WaitForSeconds (0.5f);
		state = States.Stop;
    	rotate ();
		yield return new WaitForSeconds (0.5f);
		SetMovement (new LinearMovement((transform.position - target) / 50));
		state = States.Run;

	}

	protected override void coroutinesLauncher () {
		target = GameObject.FindGameObjectWithTag ("Player").transform.position;
		StartCoroutine (seeker());
	}
}
