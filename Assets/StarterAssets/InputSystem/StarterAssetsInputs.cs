using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	// TODO: Clean up unused controls
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public float yaw;
		public bool jump;
		public bool sprint;
		public bool fixedHeight;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = false;
		public bool cursorInputForLook = false;
		public bool rotationWithQE = true;
		public bool cameraLocked = true;

		public bool controlsLocked = false;

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			if (controlsLocked)
			{
				MoveInput(Vector2.zero);
				return;
			}
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			// No camera movement allowed
			if (!cursorInputForLook) return;

               // Hold camera unlock trigger (Currently 'Q') to rotate
               if (cameraLocked)
			{
				LookInput(Vector2.zero);
				return;
			}
               
               // Rotation in pitch and yaw. Orbits the player
               if (!fixedHeight)
			{
                    LookInput(value.Get<Vector2>());
               }
               // Keeps the camera at a fixed height and allows yaw rotation only
               else
               {
				Vector2 lookVector = new Vector2(value.Get<Vector2>().x, 0f);
                    LookInput(lookVector);
               }

		}

		// frees cursor when locked
		// Note: In webGl When mapped to mouse button, a keyboard key must also be pressed to trigger cursor lock
		// When mapped to keyboard key, there is a slight delay before cursor becomes locked
		public void OnCameraLock(InputValue value)
		{
			CameraLockInput(!value.isPressed);
          }

		public void OnRotate(InputValue value)
		{
               if (rotationWithQE)
               {
                    YawInput(value.Get<float>());
               }
          }

		public void OnJump(InputValue value)
		{
			//JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			//SprintInput(value.isPressed);
		}
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void CameraLockInput(bool newCameraLockState)
		{
			//cameraLocked = newCameraLockState;
			//SetCursorState(!cameraLocked);
          }


          public void YawInput(float newYaw)
		{
			yaw = newYaw;
			look = new Vector2(yaw, 0f);
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

/*		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}*/

		public void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}