namespace ProjectFox.GameEngine.Input;

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