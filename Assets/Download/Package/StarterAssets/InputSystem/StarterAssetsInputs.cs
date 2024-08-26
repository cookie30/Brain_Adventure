using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public float weapon;
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;

		[Header("Movement Settings")]
		public bool analogMovement;

		//�ʵe���
        public Animator anim;


        [Header("Mouse Cursor Settings")]
		bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
        public void OnWeapon(InputValue value)
        {
			WeaponInput(value.Get<float>());	
        }
        public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
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

        public void WeaponInput(float wheel)
        {
            weapon = wheel;
			GameObject.Find("Weapon").GetComponent<WeaponManagement>().WeaponChange(weapon);
        }

        public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
            anim = GameObject.Find("Player/1").GetComponent<Animator>();
			anim.SetBool("Walk", true);

			if (move==Vector2.zero)
			{
				anim.SetBool("Walk", false);
			}
        }

        public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}
		
		//�u�n���ε{���B��M�`���A
		private void OnApplicationFocus(bool hasFocus)
		{
			//�]�w��Ъ��A(true)
			SetCursorState(cursorLocked);			
		}

		public void SetCursorState(bool newState)
		{
			//���p�C�����O�B��Ȱ����A
			if (!PauseMenu.GameisPause)
			{
				//�h����Хi�H��B����(����w���)
                Cursor.lockState = CursorLockMode.None;
            }
			else
			{
				//�_�h�T���в���(��w���)
				//�T���B��l(�ܼ�?�u:��)�G�Y���L�ܼƬ��u�h����_���e�����{���X�A�ά����h����_���᭱���{���X
                Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
				
            }
            //print(Cursor.lockState);
        }
	}
	
}