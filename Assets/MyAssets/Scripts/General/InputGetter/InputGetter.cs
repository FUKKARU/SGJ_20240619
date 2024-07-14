using UnityEngine;
using UnityEngine.InputSystem;

namespace IA
{
    public class InputGetter : MonoBehaviour
    {
        #region
        IA _inputs;

        public static InputGetter Instance { get; set; } = null;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            _inputs = new IA();

            Link(true);
        }

        void OnDestroy()
        {
            Link(false);

            _inputs.Dispose();
        }

        void OnEnable()
        {
            _inputs.Enable();
        }

        void OnDisable()
        {
            _inputs.Disable();
        }
        #endregion

        #region
        public bool Main_IsSubmit { get; private set; } = false;
        public bool Main_IsCancel { get; private set; } = false;
        public bool Main_IsLeft { get; private set; } = false;
        public bool Main_IsRight { get; private set; } = false;
        public bool Main_IsDown { get; private set; } = false;
        public bool Main_IsUp { get; private set; } = false;
        public bool Main_IsPause { get; private set; } = false;
        #endregion

        #region
        void LateUpdate()
        {
            if (Main_IsSubmit) Main_IsSubmit = false;
            if (Main_IsCancel) Main_IsCancel = false;
            if (Main_IsLeft) Main_IsLeft = false;
            if (Main_IsRight) Main_IsRight = false;
            if (Main_IsDown) Main_IsDown = false;
            if (Main_IsUp) Main_IsUp = false;
            if (Main_IsPause) Main_IsPause = false;
        }
        #endregion

        #region
        void Link(bool isLink)
        {
            if (isLink)
            {
                _inputs.Main.Submit.performed += Main_OnSubmit;
                _inputs.Main.Cancel.performed += Main_OnCancel;
                _inputs.Main.Left.performed += Main_OnLeft;
                _inputs.Main.Right.performed += Main_OnRight;
                _inputs.Main.Down.performed += Main_OnDown;
                _inputs.Main.Up.performed += Main_OnUp;
                _inputs.Main.Pause.performed += Main_OnPause;
            }
            else
            {
                _inputs.Main.Submit.performed -= Main_OnSubmit;
                _inputs.Main.Cancel.performed -= Main_OnCancel;
                _inputs.Main.Left.performed -= Main_OnLeft;
                _inputs.Main.Right.performed -= Main_OnRight;
                _inputs.Main.Down.performed -= Main_OnDown;
                _inputs.Main.Up.performed -= Main_OnUp;
                _inputs.Main.Pause.performed -= Main_OnPause;
            }
        }
        #endregion

        #region
        void Main_OnSubmit(InputAction.CallbackContext context) { Main_IsSubmit = true; }
        void Main_OnCancel(InputAction.CallbackContext context) { Main_IsCancel = true; }
        void Main_OnLeft(InputAction.CallbackContext context) { Main_IsLeft = true; }
        void Main_OnRight(InputAction.CallbackContext context) { Main_IsRight = true; }
        void Main_OnDown(InputAction.CallbackContext context) { Main_IsDown = true; }
        void Main_OnUp(InputAction.CallbackContext context) { Main_IsUp = true; }
        void Main_OnPause(InputAction.CallbackContext context) { Main_IsPause = true; }
        #endregion
    }
}