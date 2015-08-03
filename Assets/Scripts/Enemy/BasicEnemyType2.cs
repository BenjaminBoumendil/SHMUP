using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicEnemyType2 : BasicEnemy {

	protected override void SetEnemy () {
		life = 2;
		scoreValue = 1000;
		weaponList = new List<IWeapon> {new DoubleCannon(0.5f, gameObject, bullet)};
	}
}
