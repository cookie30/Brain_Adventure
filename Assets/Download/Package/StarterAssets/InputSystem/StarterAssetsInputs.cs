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

		//動畫控制器
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
		
		//只要應用程式處於專注狀態
		private void OnApplicationFocus(bool hasFocus)
		{
			//設定游標狀態(true)
			SetCursorState(cursorLocked);			
		}

		public void SetCursorState(bool newState)
		{
			//假如遊戲不是處於暫停狀態
			if (!PauseMenu.GameisPause)
			{
				//則讓游標可以到處移動(不鎖定游標)
                Cursor.lockState = CursorLockMode.None;
            }
			else
			{
				//否則禁止游標移動(鎖定游標)
				//三元運算子(變數?真:假)：若布林變數為真則執行冒號前面的程式碼，或為假則執行冒號後面的程式碼
                Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
				
            }
            //print(Cursor.lockState);
        }
	}
	
}