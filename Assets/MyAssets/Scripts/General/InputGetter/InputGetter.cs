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
        public bool Main_IsSubmitHold { get; private set; } = false;
        public bool Main_IsCancel { get; private set; } = false;
        public bool Main_IsLeft { get; private set; } = false;
        public bool Main_IsRight { get; private set; } = false;
        public bool Main_IsDown { get; private set; } = false;
        public bool Main_IsUp { get; private set; } = false;
        public bool Main_IsLeftHold { get; private set; } = false;
        public bool Main_IsRightHold { get; private set; } = false;
        public bool Main_IsDownHold { get; private set; } = false;
        public bool Main_IsUpHold { get; private set; } = false;
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
                _inputs.Main.SubmitHold.performed += Main_OnSubmitDown;
                _inputs.Main.SubmitHold.canceled += Main_OnSubmitUp;
                _inputs.Main.Cancel.performed += Main_OnCancel;
                _inputs.Main.Left.performed += Main_OnLeft;
                _inputs.Main.Right.performed += Main_OnRight;
                _inputs.Main.Down.performed += Main_OnDown;
                _inputs.Main.Up.performed += Main_OnUp;
                _inputs.Main.LeftHold.performed += Main_OnLeftDown;
                _inputs.Main.RightHold.performed += Main_OnRightDown;
                _inputs.Main.DownHold.performed += Main_OnDownDown;
                _inputs.Main.UpHold.performed += Main_OnUpDown;
                _inputs.Main.LeftHold.canceled += Main_OnLeftUp;
                _inputs.Main.RightHold.canceled += Main_OnRightUp;
                _inputs.Main.DownHold.canceled += Main_OnDownUp;
                _inputs.Main.UpHold.canceled += Main_OnUpUp;
                _inputs.Main.Pause.performed += Main_OnPause;
            }
            else
            {
                _inputs.Main.Submit.performed -= Main_OnSubmit;
                _inputs.Main.SubmitHold.performed -= Main_OnSubmitDown;
                _inputs.Main.SubmitHold.canceled -= Main_OnSubmitUp;
                _inputs.Main.Cancel.performed -= Main_OnCancel;
                _inputs.Main.Left.performed -= Main_OnLeft;
                _inputs.Main.Right.performed -= Main_OnRight;
                _inputs.Main.Down.performed -= Main_OnDown;
                _inputs.Main.Up.performed -= Main_OnUp;
                _inputs.Main.LeftHold.performed -= Main_OnLeftDown;
                _inputs.Main.RightHold.performed -= Main_OnRightDown;
                _inputs.Main.DownHold.performed -= Main_OnDownDown;
                _inputs.Main.UpHold.performed -= Main_OnUpDown;
                _inputs.Main.LeftHold.canceled -= Main_OnLeftUp;
                _inputs.Main.RightHold.canceled -= Main_OnRightUp;
                _inputs.Main.DownHold.canceled -= Main_OnDownUp;
                _inputs.Main.UpHold.canceled -= Main_OnUpUp;
                _inputs.Main.Pause.performed -= Main_OnPause;
            }
        }
        #endregion

        #region
        void Main_OnSubmit(InputAction.CallbackContext context) { Main_IsSubmit = true; }
        void Main_OnSubmitDown(InputAction.CallbackContext context) { Main_IsSubmitHold = true; }
        void Main_OnSubmitUp(InputAction.CallbackContext context) { Main_IsSubmitHold = false; }
        void Main_OnCancel(InputAction.CallbackContext context) { Main_IsCancel = true; }
        void Main_OnLeft(InputAction.CallbackContext context) { Main_IsLeft = true; }
        void Main_OnRight(InputAction.CallbackContext context) { Main_IsRight = true; }
        void Main_OnDown(InputAction.CallbackContext context) { Main_IsDown = true; }
        void Main_OnUp(InputAction.CallbackContext context) { Main_IsUp = true; }
        void Main_OnLeftDown(InputAction.CallbackContext context) { Main_IsLeftHold = true; }
        void Main_OnRightDown(InputAction.CallbackContext context) { Main_IsRightHold = true; }
        void Main_OnDownDown(InputAction.CallbackContext context) { Main_IsDownHold = true; }
        void Main_OnUpDown(InputAction.CallbackContext context) { Main_IsUpHold = true; }
        void Main_OnLeftUp(InputAction.CallbackContext context) { Main_IsLeftHold = false; }
        void Main_OnRightUp(InputAction.CallbackContext context) { Main_IsRightHold = false; }
        void Main_OnDownUp(InputAction.CallbackContext context) { Main_IsDownHold = false; }
        void Main_OnUpUp(InputAction.CallbackContext context) { Main_IsUpHold = false; }
        void Main_OnPause(InputAction.CallbackContext context) { Main_IsPause = true; }
        #endregion
    }
}