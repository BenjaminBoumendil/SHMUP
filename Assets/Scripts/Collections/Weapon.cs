using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IWeapon {
	void Shoot();
}

public abstract class AWeapon : IWeapon {
	protected float fireDelay;
	protected float timeSinceLastShoot;
	protected GameObject owner;
	protected Vector3 bulletDirection;
	protected AEnemy ownerScript;
	public GameObject projectilePrefab;
	
	public AWeapon(float _fireDelay, GameObject _owner, GameObject _projectilePrefab) {
		owner = _owner;
		fireDelay = _fireDelay;
		projectilePrefab = _projectilePrefab;
		bulletDirection = Vector3.zero;
		ownerScript = owner.GetComponent<AEnemy>();
	}

	public AWeapon(float _fireDelay, GameObject _owner, GameObject _projectilePrefab, Vector3 _bulletDirection) {
		owner = _owner;
		fireDelay = _fireDelay;
		projectilePrefab = _projectilePrefab;
		bulletDirection = _bulletDirection;
		ownerScript = owner.GetComponent<AEnemy>();
	}

	protected void setBulletMovement(AMovement movement, List<GameObject> bulletList) { 
		foreach (GameObject b in bulletList)
			b.GetComponent<ABullet>().SetMovement(movement);
	}

	protected void setBulletMovement(List<AMovement> movementList, List<GameObject> bulletList) { 
		for (int i = 0; i < bulletList.Count; i++) {
			bulletList[i].GetComponent<ABullet>().SetMovement(movementList[i]);
		}
	}
	
	public abstract void Shoot ();
}

public class LaserCannon : AWeapon {

	public LaserCannon(float _fireDelay, GameObject _owner, GameObject _projectilePrefab) : base(_fireDelay, _owner, _projectilePrefab) { }

	public override void Shoot() {
		if (Time.time > fireDelay + timeSinceLastShoot) {
			List<GameObject>  bulletList = new List<GameObject>();

			bulletList.Add((GameObject) GameObject.Instantiate(projectilePrefab,
				      	new Vector3(owner.transform.position.x, owner.transform.position.y + 1f, owner.transform.position.z), Quaternion.identity));
			bulletList.Add((GameObject) GameObject.Instantiate(projectilePrefab,
			            new Vector3(owner.transform.position.x + 0.3f, owner.transform.position.y + 0.6f, owner.transform.position.z), Quaternion.identity));
			bulletList.Add((GameObject) GameObject.Instantiate(projectilePrefab,
			            new Vector3(owner.transform.position.x - 0.3f, owner.transform.position.y + 0.6f, owner.transform.position.z), Quaternion.identity));
			setBulletMovement(new LinearMovement(new Vector3(0f, 0.5f, 0f)), bulletList);
			timeSinceLastShoot = Time.time;
		}
	}
}

public class Cannon : AWeapon {

	public Cannon(float _fireDelay, GameObject _owner, GameObject _projectilePrefab) : base(_fireDelay, _owner, _projectilePrefab) { }
	
	public override void Shoot() {
		if (Time.time > fireDelay + timeSinceLastShoot) {
			List<GameObject>  bulletList = new List<GameObject>();
			Transform[] bulletPos = owner.GetComponentsInChildren<Transform>();
		
			bulletList.Add((GameObject) GameObject.Instantiate(projectilePrefab, bulletPos[1].position, Quaternion.identity));
			setBulletMovement(new LinearMovement(ownerScript.bulletDirection), bulletList);
			timeSinceLastShoot = Time.time;
		}
	}
}

public class DoubleCannon : AWeapon {

	public DoubleCannon(float _fireDelay, GameObject _owner, GameObject _projectilePrefab) : base(_fireDelay, _owner, _projectilePrefab) { }
	
	public override void Shoot() {
		if (Time.time > fireDelay + timeSinceLastShoot) {
			List<GameObject>  bulletList = new List<GameObject>();
			Transform[] bulletPos = owner.GetComponentsInChildren<Transform>();
			
			bulletList.Add((GameObject) GameObject.Instantiate(projectilePrefab, bulletPos[1].position, Quaternion.identity));
			bulletList.Add((GameObject) GameObject.Instantiate(projectilePrefab, bulletPos[2].position, Quaternion.identity));
			setBulletMovement(new LinearMovement(ownerScript.bulletDirection), bulletList);
			timeSinceLastShoot = Time.time;
		}
	}
}

public class TenCannon : AWeapon {

	public TenCannon(float _fireDelay, GameObject _owner, GameObject _projectilePrefab) : base(_fireDelay, _owner, _projectilePrefab) { }

	public override void Shoot() {
		if (Time.time > fireDelay + timeSinceLastShoot) {
			List<GameObject>  bulletList = new List<GameObject>();
			Transform[] bulletPos = owner.GetComponentsInChildren<Transform>();

			for (int i = 1; i <= 10; i++)
				bulletList.Add((GameObject) GameObject.Instantiate(projectilePrefab, bulletPos[i].position, Quaternion.identity));
			setBulletMovement(new LinearMovement(ownerScript.bulletDirection), bulletList);
			timeSinceLastShoot = Time.time;
		}
	}
}

public class SenoidalCannon : AWeapon {

	private float amplitude;
	private float frequency;

	public SenoidalCannon(float _fireDelay, GameObject _owner, GameObject _projectilePrefab, float _amplitude, float _frequency) : base(_fireDelay, _owner, _projectilePrefab) {
		amplitude = _amplitude;
		frequency = _frequency;
	}

	public override void Shoot() {
		if (Time.time > fireDelay + timeSinceLastShoot) {
			List<GameObject>  bulletList = new List<GameObject>();
			Transform[] bulletPos = owner.GetComponentsInChildren<Transform>();

			bulletList.Add((GameObject) GameObject.Instantiate(projectilePrefab, bulletPos[1].position, Quaternion.identity));
			setBulletMovement(new SenoidalMovement(ownerScript.bulletDirection, amplitude, frequency), bulletList);
			timeSinceLastShoot = Time.time;
		}
	}
}

public class SenoidalTenCannon : SenoidalCannon {
	
	public SenoidalTenCannon(float _fireDelay, GameObject _owner, GameObject _projectilePrefab, float _amplitude, float _frequency) : base(_fireDelay, _owner, _projectilePrefab, _amplitude, _frequency) { }
	
	public override void Shoot() {
		if (Time.time > fireDelay + timeSinceLastShoot) {
			List<GameObject>  bulletList = new List<GameObject>();
			Transform[] bulletPos = owner.GetComponentsInChildren<Transform>();
			
			for (int i = 1; i <= 10; i++)
				bulletList.Add((GameObject) GameObject.Instantiate(projectilePrefab, bulletPos[i].position, Quaternion.identity));
			setBulletMovement(new SenoidalMovement(ownerScript.bulletDirection, 0.2f, 0.4f), bulletList);
			timeSinceLastShoot = Time.time;
		}
	}
}

public class StarCannon : AWeapon {
	
	public StarCannon(float _fireDelay, GameObject _owner, GameObject _projectilePrefab) : base(_fireDelay, _owner, _projectilePrefab) { }
	
	public override void Shoot() {
		if (Time.time > fireDelay + timeSinceLastShoot) {
			List<GameObject>  bulletList = new List<GameObject>();
			Transform[] bulletPos = owner.GetComponentsInChildren<Transform>();
		
			bulletList.Add((GameObject) GameObject.Instantiate(projectilePrefab, bulletPos[1].position, Quaternion.identity));
			bulletList.Add((GameObject) GameObject.Instantiate(projectilePrefab, bulletPos[2].position, Quaternion.identity));
			bulletList.Add((GameObject) GameObject.Instantiate(projectilePrefab, bulletPos[3].position, Quaternion.identity));

			int i = 1;
			foreach (GameObject b in bulletList) {
				b.GetComponent<ABullet>().SetMovement(new LinearMovement((bulletPos[i].position - owner.transform.position) / 100));
				i++;
			}
			timeSinceLastShoot = Time.time;
		}
	}
}

public class MultiDirectionalCannon : AWeapon {

	public MultiDirectionalCannon(float _fireDelay, GameObject _owner, GameObject _projectilePrefab, Vector3 _bulletDirection) : base(_fireDelay, _owner, _projectilePrefab, _bulletDirection) { }

	public override void Shoot() {
		if (Time.time > fireDelay + timeSinceLastShoot) {
			List<GameObject>  bulletList = new List<GameObject>();
			List<AMovement> movementList = new List<AMovement>();
			Transform[] bulletPos = owner.GetComponentsInChildren<Transform>();

			for (int i = 0; i < 3; i++)
				bulletList.Add((GameObject) GameObject.Instantiate(projectilePrefab, bulletPos[1].position, Quaternion.identity));

			movementList.Add(new LinearMovement(bulletDirection));
			movementList.Add(new LinearMovement(new Vector3(bulletDirection.x + 0.02f, bulletDirection.y, 0f)));
			movementList.Add(new LinearMovement(new Vector3(bulletDirection.x - 0.02f, bulletDirection.y, 0f)));

			setBulletMovement(movementList, bulletList);
			timeSinceLastShoot = Time.time;
		}
	}
}
