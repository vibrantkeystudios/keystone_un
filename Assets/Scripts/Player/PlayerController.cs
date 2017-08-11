using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float speed = 6f;

	Vector3 movement;
	Rigidbody playerRigidbody;
	int floorMask;
	float camRayLength = 100f;

	void Awake() {
		 floorMask = LayerMask.GetMask("Floor");
		 playerRigidbody = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate() {
		float v = Input.GetAxisRaw("Horizontal");
		float h = Input.GetAxisRaw("Vertical");
		Move(h, -v);
		Turning();
	}

	void Move( float h, float v ) {
		movement.Set(h, 0f, v);
		movement = movement.normalized * speed * Time.deltaTime;
		playerRigidbody.MovePosition(transform.position + movement);
	}

	void Turning() {
		Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit floorHit;

		if(Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) {
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;
			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
			playerRigidbody.MoveRotation(newRotation);
		}
	}
}
