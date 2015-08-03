using UnityEngine;
using System.Collections;

public class RotatingBullet : ABullet {

	protected override void rotate() {
		transform.Rotate (0f, 0f, 0.3f);
	}
}
