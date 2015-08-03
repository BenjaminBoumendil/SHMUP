using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SenoidalEnemy : AEnemy {

	protected override void move () {
		transform.Translate (-Time.deltaTime * 0.5f, 0f, 0f);
	}

	protected override void setBulletDirection() {
		bulletDirection = (target.transform.position - transform.position) / 200;
	}
	
	protected override void SetEnemy () {
		life = 25;
		scoreValue = 2000;
		weaponList = new List<IWeapon> {new SenoidalCannon (0.4f, gameObject, bullet, 1f, 0.5f)};
	}
}
