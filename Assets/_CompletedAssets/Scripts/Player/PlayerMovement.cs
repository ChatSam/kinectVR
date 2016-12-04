using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;
using trinus;

namespace CompleteProject
{
	public class PlayerMovement : MonoBehaviour
	{
		Transform cameraTransform;
		public float speed = 6f;            // The speed that the player will move at.


		Vector3 movement;                   // The vector to store the direction of the player's movement.
		Animator anim;                      // Reference to the animator component.
		Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
		#if !MOBILE_INPUT
		int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
		float camRayLength = 100f;          // The length of the ray from the camera into the scene.
		#endif

		TrinusProcessor trinusProcessor;

		void Awake ()
		{
			#if !MOBILE_INPUT
			// Create a layer mask for the floor layer.
			floorMask = LayerMask.GetMask ("Floor");
			#endif

			// Set up references.
			anim = GetComponent <Animator> ();
			playerRigidbody = GetComponent <Rigidbody> ();

			trinusProcessor = GameObject.Find ("TrinusManager").GetComponent<TrinusProcessor> ();
		}


		void FixedUpdate ()
		{
			// Store the input axes.
			float h = CrossPlatformInputManager.GetAxisRaw("Horizontal");
			float v = CrossPlatformInputManager.GetAxisRaw("Vertical");

			// Move the player around the scene.
			Move (h, v);

			// Turn the player to face the mouse cursor.
			//Turning ();

			// Animate the player.
			//Animating (h, v);
		}


		void Move (float h, float v)
		{
			// Set the movement vector based on the axis input.
			//movement.Set (h, 0f, v);
			//movement.Set ((Camera.main.transform.forward *h).x, 0f, (Camera.main.transform.right *v).z);
			if (cameraTransform == null && trinusProcessor.getMainCamera () != null) {
				cameraTransform = trinusProcessor.getMainCamera ().transform;
			}
			if (cameraTransform != null)
				movement = GetVectorRelativeToObject (new Vector3 (h, 0f, v), cameraTransform);

			// Normalise the movement vector and make it proportional to the speed per second.
			movement = movement.normalized * speed * Time.deltaTime;

			// Move the player to it's current position plus the movement.
			playerRigidbody.MovePosition (transform.position + movement);
		}


		void Turning ()
		{
			#if !MOBILE_INPUT
			// Create a ray from the mouse cursor on screen in the direction of the camera.
			Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

			// Create a RaycastHit variable to store information about what was hit by the ray.
			RaycastHit floorHit;

			// Perform the raycast and if it hits something on the floor layer...
			if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
			{
				// Create a vector from the player to the point on the floor the raycast from the mouse hit.
				Vector3 playerToMouse = floorHit.point - transform.position;

				// Ensure the vector is entirely along the floor plane.
				playerToMouse.y = 0f;

				// Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
				Quaternion newRotatation = Quaternion.LookRotation (playerToMouse);

				// Set the player's rotation to this new rotation.
				playerRigidbody.MoveRotation (newRotatation);
			}
			#else

			Vector3 turnDir = new Vector3(CrossPlatformInputManager.GetAxisRaw("Mouse X") , 0f , CrossPlatformInputManager.GetAxisRaw("Mouse Y"));

			if (turnDir != Vector3.zero)
			{
			// Create a vector from the player to the point on the floor the raycast from the mouse hit.
			Vector3 playerToMouse = (transform.position + turnDir) - transform.position;

			// Ensure the vector is entirely along the floor plane.
			playerToMouse.y = 0f;

			// Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
			Quaternion newRotatation = Quaternion.LookRotation(playerToMouse);

			// Set the player's rotation to this new rotation.
			playerRigidbody.MoveRotation(newRotatation);
			}
			#endif
		}

		public static Vector3 GetVectorRelativeToObject(Vector3 inputVector, Transform camera)
		{
			Vector3 objectRelativeVector = Vector3.zero;
			if (inputVector != Vector3.zero)
			{	
				Vector3 forward = camera.TransformDirection(Vector3.forward);
				forward.y = 0f;
				forward.Normalize();
				Vector3 right = new Vector3(forward.z, 0.0f, -forward.x);

				Vector3 relativeRight = inputVector.x * right;
				Vector3 relativeForward = inputVector.z * forward;

				objectRelativeVector = relativeRight + relativeForward;			

				if (objectRelativeVector.magnitude > 1f) objectRelativeVector.Normalize();				
			}	
			return objectRelativeVector;	
		}

		void Animating (float h, float v)
		{
			// Create a boolean that is true if either of the input axes is non-zero.
			bool walking = h != 0f || v != 0f;

			// Tell the animator whether or not the player is walking.
			anim.SetBool ("IsWalking", walking);
		}
	}
}