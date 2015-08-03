using UnityEngine;
using System.Collections;

public interface IMovement {

	void Move(GameObject entity);
}

public abstract class AMovement : IMovement {

	protected const float BULLET_SPEED = 50f;
	protected Vector3 direction;

	public AMovement() { }

	public AMovement(Vector3 _direction) {
		direction = _direction;
	}

	public abstract void Move (GameObject go);
}

public class LinearMovement : AMovement {

	public LinearMovement(Vector3 _direction) : base(_direction) { }

	public override void Move(GameObject go) {
		go.transform.Translate(Time.deltaTime * direction * BULLET_SPEED);
	}
}

public class SenoidalMovement : AMovement {

	private float amplitude;
	private float frequency;
	
	public SenoidalMovement(Vector3 _direction, float _amplitude, float _frequency) : base(_direction) {
		amplitude = _amplitude;
		frequency = _frequency;
	}

	public override void Move(GameObject go) {
		float xMovement = amplitude * (Mathf.Sin(2 * Mathf.PI * frequency * Time.time) - Mathf.Sin(2 * Mathf.PI * frequency * (Time.time - Time.deltaTime)));
		go.transform.Translate(xMovement, Time.deltaTime * direction.y * BULLET_SPEED, Time.deltaTime * 0f);
	}
}
