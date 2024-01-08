using ProjectFox.CoreEngine.Math;
using ProjectFox.GameEngine.Visuals;

namespace ProjectFox.GameEngine.Input;

/*public interface IRawInputDevice //remove
{
    public abstract void Process();
}*/

public abstract class InputDevice
{
    public InputDevice(int digitalButtonsLength, int analogButtonsLength, int directionalPadsLength, int sticksLength, int cursorsLength)
    {
        digitalButtons = digitalButtonsLength > 0 ? new DigitalButton[digitalButtonsLength] : null;
        analogButtons = analogButtonsLength > 0 ? new AnalogButton[analogButtonsLength] : null;
        directionalPads = directionalPadsLength > 0 ? new DirectionalPad[directionalPadsLength] : null;
        sticks = sticksLength > 0 ? new Stick[sticksLength] : null;
        cursors = cursorsLength > 0 ? new Cursor[cursorsLength] : null;
    }

    protected internal readonly DigitalButton[] digitalButtons;
    protected internal readonly AnalogButton[] analogButtons;
    protected internal readonly DirectionalPad[] directionalPads;
    protected internal readonly Stick[] sticks;
    protected internal readonly Cursor[] cursors;

    //connected?

    public void UpdateValues(bool[] digitalButtonValues, byte[] analogButtonValues, Vector.Direction[] directionalPadValues, Vector[] analogStickValues, Vector[] cursorValues)
    {
        if (digitalButtons != null && digitalButtonValues != null)
            for (int i = 0, l = Math.Min(digitalButtons.Length, digitalButtonValues.Length); i < l; i++)
            {
                DigitalButton button = digitalButtons[i];
                bool value = digitalButtonValues[i];

                button.changed = value != button.value;
                button.value = value;
            }

        if (analogButtons != null && analogButtonValues != null)
            for (int i = 0, l = Math.Min(analogButtons.Length, analogButtonValues.Length); i < l; i++)
                analogButtons[i].value = analogButtonValues[i];

        if (directionalPads != null && directionalPadValues != null)
            for (int i = 0, l = Math.Min(directionalPads.Length, directionalPadValues.Length); i < l; i++)
            {
                //Vector.Direction dPad = directionalPads[i];
                Vector.Direction value = directionalPadValues[i];
                //
            }

        if (sticks != null && analogStickValues != null)
            for (int i = 0, l = Math.Min(sticks.Length, analogStickValues.Length); i < l; i++)
                sticks[i].position = analogStickValues[i].Clamp(-127, 127);

        if (cursors != null && cursorValues != null)
            for (int i = 0, l = Math.Min(cursors.Length, cursorValues.Length); i < l; i++)
            {
                Cursor cursor = cursors[i];
                Vector value = cursorValues[i];

                if (cursor.nativePos.Equals(value)) cursor.posChanged = false;
                else
                {
                    cursor.nativePos = value;//clamp to window size?

                    float scale = Screen.Scale;//is the member private?
                    int scaleInt = (int)scale;
                    Vector scaled = Screen.OneToOne ?//is the member private?
                    new(value.x / scaleInt, value.y / scaleInt) :
                    new((int)(value.x / scale), (int)(value.y / scale));

                    scaled = scaled.Clamp(default, Screen.size);

                    if (cursor.scaledPos.Equals(scaled))
                    {
                        cursor.posChanged = false;
                        return;
                    }

                    cursor.scaledPos = scaled;
                    cursor.posChanged = true;
                }
            }
    }
}

public class KeyboardMouseDevice : InputDevice
{
    public KeyboardMouseDevice()
        : base(111, 0, 0, 1, 1)
    {
        cursors[0] = mouse = new();
        sticks[0] = mouseWheel = new();

        #region Keys
        digitalButtons[0] = LeftMouse = new();
        digitalButtons[1] = RightMouse = new();
        digitalButtons[2] = MiddleMouse = new();
        digitalButtons[3] = X1Mouse = new();
        digitalButtons[4] = X2Mouse = new();

        digitalButtons[5] = Escape = new();

        digitalButtons[6] = Enter = new();
        digitalButtons[7] = Space = new();
        digitalButtons[8] = Tab = new();
        digitalButtons[9] = Insert = new();
        digitalButtons[10] = Backspace = new();
        digitalButtons[11] = Delete = new();

        digitalButtons[12] = Shift = new();
        digitalButtons[13] = LeftShift = new();
        digitalButtons[14] = RightShift = new();

        digitalButtons[15] = Ctrl = new();
        digitalButtons[16] = LeftCtrl = new();
        digitalButtons[17] = RightCtrl = new();

        digitalButtons[18] = Alt = new();
        digitalButtons[19] = LeftAlt = new();
        digitalButtons[20] = RightAlt = new();

        digitalButtons[21] = LeftWindows = new();
        digitalButtons[22] = RightWindows = new();

        digitalButtons[23] = Apps = new();
        digitalButtons[24] = PrintScreen = new();

        digitalButtons[25] = CapsLock = new();
        digitalButtons[26] = ScrollLock = new();

        digitalButtons[27] = Home = new();
        digitalButtons[28] = End = new();
        digitalButtons[29] = PageUp = new();
        digitalButtons[30] = PageDown = new();

        digitalButtons[31] = Left = new();
        digitalButtons[32] = Right = new();
        digitalButtons[33] = Up = new();
        digitalButtons[34] = Down = new();

        digitalButtons[35] = F1 = new();
        digitalButtons[36] = F2 = new();
        digitalButtons[37] = F3 = new();
        digitalButtons[38] = F4 = new();
        digitalButtons[39] = F5 = new();
        digitalButtons[40] = F6 = new();
        digitalButtons[41] = F7 = new();
        digitalButtons[42] = F8 = new();
        digitalButtons[43] = F9 = new();
        digitalButtons[44] = F10 = new();
        digitalButtons[45] = F11 = new();
        digitalButtons[46] = F12 = new();

        digitalButtons[47] = Zero = new();
        digitalButtons[48] = One = new();
        digitalButtons[49] = Two = new();
        digitalButtons[50] = Three = new();
        digitalButtons[51] = Four = new();
        digitalButtons[52] = Five = new();
        digitalButtons[53] = Six = new();
        digitalButtons[54] = Seven = new();
        digitalButtons[55] = Eight = new();
        digitalButtons[56] = Nine = new();

        digitalButtons[57] = A = new();
        digitalButtons[58] = B = new();
        digitalButtons[59] = C = new();
        digitalButtons[60] = D = new();
        digitalButtons[61] = E = new();
        digitalButtons[62] = F = new();
        digitalButtons[63] = G = new();
        digitalButtons[64] = H = new();
        digitalButtons[65] = I = new();
        digitalButtons[66] = J = new();
        digitalButtons[67] = K = new();
        digitalButtons[68] = L = new();
        digitalButtons[69] = M = new();
        digitalButtons[70] = N = new();
        digitalButtons[71] = O = new();
        digitalButtons[72] = P = new();
        digitalButtons[73] = Q = new();
        digitalButtons[74] = R = new();
        digitalButtons[75] = S = new();
        digitalButtons[76] = T = new();
        digitalButtons[77] = U = new();
        digitalButtons[78] = V = new();
        digitalButtons[79] = W = new();
        digitalButtons[80] = X = new();
        digitalButtons[81] = Y = new();
        digitalButtons[82] = Z = new();

        digitalButtons[83] = Plus = new();
        digitalButtons[84] = Minus = new();
        digitalButtons[85] = LeftBracket = new();
        digitalButtons[86] = RightBracket = new();
        digitalButtons[87] = Semicolon = new();
        digitalButtons[88] = Apostrophe = new();
        digitalButtons[89] = Comma = new();
        digitalButtons[90] = Period = new();
        digitalButtons[91] = ForwardSlash = new();
        digitalButtons[92] = BackSlash = new();
        digitalButtons[93] = Backtick = new();

        digitalButtons[94] = NumLock = new();
        digitalButtons[95] = NumpadZero = new();
        digitalButtons[96] = NumpadOne = new();
        digitalButtons[97] = NumpadTwo = new();
        digitalButtons[98] = NumpadThree = new();
        digitalButtons[99] = NumpadFour = new();
        digitalButtons[100] = NumpadFive = new();
        digitalButtons[101] = NumpadSix = new();
        digitalButtons[102] = NumpadSeven = new();
        digitalButtons[103] = NumpadEight = new();
        digitalButtons[104] = NumpadNine = new();
        digitalButtons[105] = NumpadAdd = new();
        digitalButtons[106] = NumpadSubtract = new();
        digitalButtons[107] = NumpadMultiply = new();
        digitalButtons[108] = NumpadDivide = new();
        digitalButtons[109] = NumpadDecimal = new();
        digitalButtons[110] = NumpadSeparator = new();
        #endregion
    }

    //double clicks?

    public readonly Cursor mouse;
    public readonly Stick mouseWheel;

    #region Keys
    public readonly DigitalButton
        LeftMouse, RightMouse, MiddleMouse, X1Mouse, X2Mouse,

        Escape,

        Enter, Space, Tab, Insert, Backspace, Delete,

        Shift, LeftShift, RightShift,
        Ctrl, LeftCtrl, RightCtrl,
        Alt, LeftAlt, RightAlt,
        LeftWindows, RightWindows,
        Apps,//rename? menu?
        PrintScreen,

        CapsLock, ScrollLock,

        Home, End, PageUp, PageDown,
        Left, Right, Up, Down,

        F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12,

        Zero, One, Two, Three, Four, Five, Six, Seven, Eight, Nine,

        A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z,

        Plus/*equals?*/, Minus,//hyphen/dash?
        LeftBracket, RightBracket,
        Semicolon, Apostrophe,
        Comma, Period,
        ForwardSlash, BackSlash,
        Backtick,

        NumLock,
        NumpadZero, NumpadOne, NumpadTwo, NumpadThree, NumpadFour,
        NumpadFive, NumpadSix, NumpadSeven, NumpadEight, NumpadNine,
        NumpadAdd, NumpadSubtract, NumpadMultiply, NumpadDivide,
        NumpadDecimal, NumpadSeparator;
    #endregion
}

public class GamepadInputDevice : InputDevice
{
    public GamepadInputDevice() : base(10, 2, 1, 2, 0)
    {
        //check that button numbers match
        cross = a = b0 = digitalButtons[0] = new();
        circle = b = b1 = digitalButtons[1] = new();
        square = x = b2 = digitalButtons[2] = new();
        triangle = y = b3 = digitalButtons[3] = new();
        start = b4 = digitalButtons[4] = new();
        select = back = b5 = digitalButtons[5] = new();
        l1 = leftShoulder = b6 = digitalButtons[6] = new();
        r1 = rightShoulder = b7 = digitalButtons[7] = new();
        l3 = leftStickButton = b8 = digitalButtons[8] = new();
        r3 = rightStickButton = b9 = digitalButtons[9] = new();

        dPad = directionalPads[0] = new();

        l2 = leftTrigger = analogButtons[0] = new();
        r2 = rightTrigger = analogButtons[1] = new();

        leftStick = sticks[0] = new();
        rightStick = sticks[1] = new();
    }

    public readonly DigitalButton b0, b1, b2, b3, b4, b5, b6, b7, b8, b9,
        a, b, x, y, start, back, leftShoulder, rightShoulder, leftStickButton, rightStickButton,
        cross, circle, square, triangle, select, l1, r1, l3, r3;
    public readonly DirectionalPad dPad;
    public readonly AnalogButton leftTrigger, rightTrigger, l2, r2;
    public readonly Stick leftStick, rightStick;
}

//Debug input type with all keyboard keys gamepad inputs and mouse, was this going to go in debug?

public class NESInputDevice : InputDevice
{
    public struct RawNESInputDevice// : IRawInputDevice //remove
    {
        public bool a, b, start, select;
        public Vector.Direction dPad;

        public void Process() { }
    }

    public NESInputDevice() : base(4, 0, 1, 0, 0)
    {
        a = digitalButtons[0] = new();
        b = digitalButtons[1] = new();
        start = digitalButtons[2] = new();
        select = digitalButtons[3] = new();

        dPad = directionalPads[0] = new();
    }

    public readonly DigitalButton a, b, start, select;
    public readonly DirectionalPad dPad;
}