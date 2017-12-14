
using System;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using LedCSharp;
using System.Threading;

namespace BabyPress2
{
    public partial class Form1 : Form
    {
        private IKeyboardMouseEvents globalKeyboardHook;
        private Keys ActiveKey { get; set; }
        private bool IsActive { get; set; }
        private Random random = new Random();

        public Form1()
        {
            InitializeComponent();
            IsActive = false;
        }

        private void GlobalHook_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            if (IsActive && (ActiveKey == e.KeyCode))
            {
                ShowSuccess();
            }
        }

        private void ActivateKey()
        {
            keyboardNames name = keyboardNames.PAUSE_BREAK;
            while(name == keyboardNames.PAUSE_BREAK)
            {
                try
                {
                    Array keysValues = Enum.GetValues(typeof(Keys));
                    ActiveKey = (Keys)keysValues.GetValue(random.Next(keysValues.Length));
                    name = ToLogitechKeyboardName(ActiveKey);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    continue;
                }
            }
            LogitechGSDK.LogiLedSetLighting(0, 0, 0);
            LogitechGSDK.LogiLedSetLightingForKeyWithKeyName(name, 100, 0, 0);
        }

        private void ShowSuccess()
        {
            System.Media.SystemSounds.Asterisk.Play();
            for (int index = 100; index > 0; index -= 2)
            {
                LogitechGSDK.LogiLedSetLighting(0, index, 0);
                Thread.Sleep(10);
            }
            ActivateKey();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(IsActive)
            {
                globalKeyboardHook.KeyDown -= GlobalHook_KeyDown;
                globalKeyboardHook.Dispose();

                LogitechGSDK.LogiLedStopEffects();
                LogitechGSDK.LogiLedRestoreLighting();
                
                (sender as Button).Text = "Start";
                IsActive = false;
            }
            else
            {
                globalKeyboardHook = Hook.GlobalEvents();
                globalKeyboardHook.KeyDown += GlobalHook_KeyDown;
                
                (sender as Button).Text = "Stop";

                LogitechGSDK.LogiLedInit();
                Thread.Sleep(100); //init needs some time... this shouldn't block the UI-thread, but it's the best solution for now...
                LogitechGSDK.LogiLedSaveCurrentLighting();

                ActivateKey();
                IsActive = true;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Clean up
            if (IsActive)
            {
                globalKeyboardHook.KeyDown -= GlobalHook_KeyDown;
                globalKeyboardHook.Dispose();
            }
        }
        
        private keyboardNames ToLogitechKeyboardName(Keys activeKey)
        {
            switch (activeKey)
            {
                case Keys.PageUp:
                    return keyboardNames.PAGE_UP;
                case Keys.PageDown:
                    return keyboardNames.PAGE_DOWN;
                case Keys.End:
                    return keyboardNames.END;
                case Keys.Home:
                    return keyboardNames.HOME;
                case Keys.Left:
                    return keyboardNames.ARROW_LEFT;
                case Keys.Up:
                    return keyboardNames.ARROW_UP;
                case Keys.Right:
                    return keyboardNames.ARROW_RIGHT;
                case Keys.Down:
                    return keyboardNames.ARROW_DOWN;
                case Keys.Insert:
                    return keyboardNames.INSERT;
                case Keys.A:
                    return keyboardNames.A;
                case Keys.B:
                    return keyboardNames.B;
                case Keys.C:
                    return keyboardNames.C;
                case Keys.D:
                    return keyboardNames.D;
                case Keys.E:
                    return keyboardNames.E;
                case Keys.F:
                    return keyboardNames.F;
                case Keys.G:
                    return keyboardNames.G;
                case Keys.H:
                    return keyboardNames.H;
                case Keys.I:
                    return keyboardNames.I;
                case Keys.J:
                    return keyboardNames.J;
                case Keys.K:
                    return keyboardNames.K;
                case Keys.L:
                    return keyboardNames.L;
                case Keys.M:
                    return keyboardNames.M;
                case Keys.N:
                    return keyboardNames.N;
                case Keys.O:
                    return keyboardNames.O;
                case Keys.P:
                    return keyboardNames.P;
                case Keys.Q:
                    return keyboardNames.Q;
                case Keys.R:
                    return keyboardNames.R;
                case Keys.S:
                    return keyboardNames.S;
                case Keys.T:
                    return keyboardNames.T;
                case Keys.U:
                    return keyboardNames.U;
                case Keys.V:
                    return keyboardNames.V;
                case Keys.W:
                    return keyboardNames.W;
                case Keys.X:
                    return keyboardNames.X;
                case Keys.F1:
                    return keyboardNames.F1;
                case Keys.F2:
                    return keyboardNames.F2;
                case Keys.F3:
                    return keyboardNames.F3;
                case Keys.F4:
                    return keyboardNames.F4;
                case Keys.F5:
                    return keyboardNames.F5;
                case Keys.F6:
                    return keyboardNames.F6;
                case Keys.F7:
                    return keyboardNames.F7;
                case Keys.F8:
                    return keyboardNames.F8;
                case Keys.F9:
                    return keyboardNames.F9;
                case Keys.F10:
                    return keyboardNames.F10;
                case Keys.F11:
                    return keyboardNames.F11;
                case Keys.F12:
                    return keyboardNames.F12;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
