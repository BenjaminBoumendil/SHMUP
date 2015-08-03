using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DestroyerEnemy : AEnemy {

	protected override void SetEnemy () {
		life = 50;
		scoreValue = 4000;
		weaponList = new List<IWeapon> {new MultiDirectionalCannon(1f, gameObject, bullet, (target.transform.position - transform.position) / 550)};
	}

	protected override void move() {
		transform.Translate (Vector3.left * Time.deltaTime / 5);
	}
}
