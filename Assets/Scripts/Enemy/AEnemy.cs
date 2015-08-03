using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AEnemy : MonoBehaviour {
	
	protected List<IWeapon> weaponList;
	protected int scoreValue;
	protected float life;
	protected GameObject target;
	public Vector3 bulletDirection;
	public GameObject exploFx;
	public GameObject bullet;

	protected abstract void SetEnemy();
	protected virtual void move () { }
	protected virtual void rotate () { }

	protected void destroy() {
		if (target == null)
			target = GameObject.FindWithTag ("Player");
		target.GetComponent<PlayerController> ().score += scoreValue;
		GameObject i = Instantiate (exploFx, transform.position, Quaternion.identity) as GameObject;
		Destroy (i, 2f);
		Destroy (gameObject);
	}

	protected virtual void setBulletDirection() {
		bulletDirection = (target.transform.position - transform.position) / 200;
	}

	void OnBecameVisible() {
		enabled = true;
	}

	void OnBecameInvisible() {
		enabled = false;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Bullet") {
			life -= 1;
			if (life <= 0)
				destroy();
		}
	}

	void Start() {
		target = GameObject.FindWithTag ("Player");
		SetEnemy ();
	}

	void Update() {
		setBulletDirection ();
		move ();
		rotate ();
		foreach(IWeapon w in weaponList)
			w.Shoot ();
	}
}