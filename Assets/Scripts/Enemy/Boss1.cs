using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boss1 : AEnemy {

	private enum States {
		Start,
		Phase1,
		Phase2,
	}

	private States state;
	private float baseLife;
	public GameObject ovalBullet;
	public GameObject ovalRotatingBullet;
	public GameObject seekerBullet;

	private delegate void PhaseOne();
	private event PhaseOne phaseOne;
	private delegate void PhaseTwo();
	private event PhaseOne phaseTwo;

	private IEnumerator SetWeaponPhaseOne() {
		while (true) {
			weaponList.Add (new TenCannon (0.3f, gameObject, ovalBullet));
			yield return new WaitForSeconds (2f);
			weaponList.Clear ();
			yield return new WaitForSeconds(1f);
			weaponList.Add (new SenoidalTenCannon(0.5f, gameObject, bullet, 1f, 0.8f));
			yield return new WaitForSeconds(3f);
			weaponList.Clear ();
			yield return new WaitForSeconds(1f);
		}
	}

	private IEnumerator SetWeaponPhaseTwo() {
		while (true) {
			weaponList.Add (new TenCannon (0.3f, gameObject, ovalRotatingBullet));
			yield return new WaitForSeconds(2f);
			weaponList.Clear ();
			yield return new WaitForSeconds(1f);
			weaponList.Add (new TenCannon (0.5f, gameObject, seekerBullet));
			yield return new WaitForSeconds(1f);
			weaponList.Clear ();
			yield return new WaitForSeconds(2f);
		}
	}

	private void SetEnemyPhaseOne() {
		phaseOne -= SetEnemyPhaseOne;
		StartCoroutine (SetWeaponPhaseOne());
	}

	private void SetEnemyPhaseTwo() {
		phaseTwo -= SetEnemyPhaseTwo;
		StopAllCoroutines();
		weaponList.Clear ();
		StartCoroutine (SetWeaponPhaseTwo());
	}

	private void PhaseHandler() {
		if (state == States.Phase1 && phaseOne != null)
			phaseOne ();
		else if (life <= baseLife / 2 && phaseTwo != null)
			phaseTwo ();
	}

	protected override void setBulletDirection() {
		bulletDirection = (target.transform.position - transform.position) / 100;
	}

	protected override void move() {
		if (Camera.main.gameObject.transform.position.y + 5f <= transform.position.y)
			transform.Translate (new Vector3 (0f, Time.deltaTime * 5f, 0f));
		else
			state = States.Phase1;
	}

	protected override void SetEnemy () {
		transform.Rotate (new Vector3(0, 0, 180));
		state = States.Start;
		baseLife = life = 1000;
		scoreValue = 10000;
		weaponList = new List<IWeapon> ();
		phaseOne += SetEnemyPhaseOne;
		phaseTwo += SetEnemyPhaseTwo;
	}

	void OnGUI() {
        GUI.Box(new Rect(((Screen.width - Camera.main.pixelWidth) / 1.9f) + 150, 1, Camera.main.pixelWidth - 155, 20), life.ToString());
	}

	void Update() {
		setBulletDirection ();
		PhaseHandler ();
		move ();
		if (state != States.Start && weaponList != null) {
			foreach (IWeapon w in weaponList)
				w.Shoot ();
		}
	}
}
