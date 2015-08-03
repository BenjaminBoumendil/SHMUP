using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WhirligigEnemy : AEnemy {

	protected override void SetEnemy () {
		life = 50;
		scoreValue = 5000;
		GetComponent<Rigidbody2D> ().AddTorque (50);
		weaponList = new List<IWeapon> {new StarCannon (0.2f, gameObject, bullet)};
	}
}
