using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
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

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if (!cursorInputForLook) return;

			// Hold right mouse button to rotate
			if (!Input.GetMouseButton(1))
			{
				SetCursorState(false);
				LookInput(Vector2.zero);
				return;
			}
			else
			{
                    SetCursorState(true);
               }

               if (!fixedHeight)
			{
                    LookInput(value.Get<Vector2>());
               }
			else
			{
				// Ignores pitch
				Vector2 lookVector = new Vector2(value.Get<Vector2>().x, 0f);
                    LookInput(lookVector);
               }

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
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
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

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}