using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicEnemy : AEnemy {

	protected override void setBulletDirection() {
		bulletDirection = (target.transform.position - transform.position) / 100;
	}

	protected override void rotate () {
		Vector3 diff = target.transform.position - transform.position;
		diff.Normalize ();
		float rotZ = Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, rotZ - 90);
	}

	protected override void SetEnemy () {
		life = 2;
		scoreValue = 1000;
		weaponList = new List<IWeapon> {new Cannon (0.5f, gameObject, bullet)};
	}
}
