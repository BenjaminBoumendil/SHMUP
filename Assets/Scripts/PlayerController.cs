using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	private Animator animator;
	private IWeapon activeWeapon;
	private List<IWeapon> weaponList;
	private GameManager gameManager;
    private float moveSpeed = 5f;
//	private AudioSource audioSource;
	public GameObject bullet;
	public int score = 0;
	public int energy = 200;

	private void moveUp() {
		transform.Translate (0, Time.deltaTime * 2f, 0);
	}

	private void SetWeapon(IWeapon _weapon) {
		activeWeapon = _weapon;
	}

	private void inputHandler() {
		Vector3 cameraPos = Camera.main.gameObject.transform.position;

		if (Input.GetKey ("up") && transform.position.y < cameraPos.y + 4.5) {
			transform.Translate(0, Time.deltaTime * moveSpeed, 0);
		} else if (Input.GetKey ("down") && transform.position.y > cameraPos.y - 4.5) {
			transform.Translate(0, -Time.deltaTime * moveSpeed, 0);
		}
		if (Input.GetKey ("left") && transform.position.x > -4.5f) {
			transform.Translate (-Time.deltaTime * moveSpeed, 0, 0);
			animator.SetBool ("Right", false);
			animator.SetBool ("Left", true);
		} else if (Input.GetKey ("right") && transform.position.x < 4.5f) {
			transform.Translate (Time.deltaTime * moveSpeed, 0, 0);
			animator.SetBool ("Left", false);
			animator.SetBool ("Right", true);
		} else {
			animator.SetBool ("Left", false);
			animator.SetBool ("Right", false);
		}

		if (Input.GetKey (KeyCode.Space)) {
			//if (!audioSource.isPlaying)
			//	audioSource.Play();
			SetWeapon (weaponList [0]);
			activeWeapon.Shoot ();
		}

		if (Input.GetKey ("a") && energy >= 1) {
			energy -= 1;
			Time.timeScale = 0.5f;
		} else
			Time.timeScale = 1f;
	}

	private IEnumerator energyRegen() {
		while (true) {
			if (energy <= 190)
				energy += 10;
			else
				energy = 200;
			yield return new WaitForSeconds(1f);
		}
	}

	void OnTriggerEnter2D() {
//		Application.LoadLevel (Application.loadedLevel);
	}

	void Start () {
		//audioSource = GetComponent<AudioSource> ();
		animator = GetComponent<Animator> ();
		gameManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager>();
		weaponList = new List<IWeapon> { new LaserCannon(0.1f, gameObject, bullet) };

		StartCoroutine (energyRegen());
	}

	void Update () {
		if (gameManager.state != GameStates.Pause) {
			if (gameManager.state == GameStates.Run)
				moveUp ();
			inputHandler ();
		}
	}
}
