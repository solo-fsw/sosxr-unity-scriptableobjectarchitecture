using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif


namespace ScriptableObjectArchitecture.Examples
{
    public class KeyboardMover : MonoBehaviour
    {
        [SerializeField]
        private FloatReference m_moveSpeed = default;

        private Vector2 _moveInput;


        private void Awake()
        {
            #if ENABLE_INPUT_SYSTEM
            Debug.Log("Using new input system");
            #else
            Debug.Log("Using old input system");
            #endif
        }


        private void Update()
        {
            #if ENABLE_INPUT_SYSTEM
            UseNewInputSystem();
            #else
            UseOldInputSystem();
            #endif
        }


        /// <summary>
        ///     SOSXR: This is really not how I'm supposed to use the Keyboard input system, but I'm using it here to quickly get
        ///     some input.
        /// </summary>
        private void UseNewInputSystem()
        {
            var keyboard = Keyboard.current;

            if (keyboard == null)
            {
                Debug.LogWarning("Keyboard not found!");

                return;
            }

            _moveInput = keyboard.wKey.isPressed ? new Vector2(0, 1) : _moveInput;
            _moveInput = keyboard.sKey.isPressed ? new Vector2(0, -1) : _moveInput;
            _moveInput = keyboard.aKey.isPressed ? new Vector2(-1, 0) : _moveInput;
            _moveInput = keyboard.dKey.isPressed ? new Vector2(1, 0) : _moveInput;

            if (keyboard.aKey.value == 0 && keyboard.dKey.value == 0)
            {
                _moveInput.x = 0;
            }

            if (keyboard.wKey.value == 0 && keyboard.sKey.value == 0)
            {
                _moveInput.y = 0;
            }

            var moveDirection = new Vector3(_moveInput.x, _moveInput.y, 0);

            transform.position += moveDirection * m_moveSpeed.Value * Time.deltaTime;
        }


        private void UseOldInputSystem()
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                transform.position += Vector3.up * m_moveSpeed.Value;
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                transform.position += Vector3.down * m_moveSpeed.Value;
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                transform.position += Vector3.right * m_moveSpeed.Value;
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                transform.position += Vector3.left * m_moveSpeed.Value;
            }
        }
    }
}