namespace ProjectFox.Windows;

/// <summary> Virtual Keycodes </summary>
public enum VK : byte
{
    /// <summary> Left Mouse Button </summary>
    LButton = 0x01,
    /// <summary> Right Mouse Button </summary>
    RButton = 0x02,
    Cancel =  0x03,//
    /// <summary> Middle Mouse Button </summary>
    MButton = 0x04,

    XButton1 = 0x05,//X1MouseButton
    XButton2 = 0x06,//X2MouseButton

    /// <summary> Backspace Key </summary>
    Back = 0x08,
    /// <summary> Tab Key </summary>
    Tab =  0x09,

    /// <summary> Clear Key </summary>
    Clear =  0x0C,
    /// <summary> Enter Key </summary>
    Return = 0x0D,

    /// <summary> Shift Key </summary>
    Shift =   0x10,
    /// <summary> Ctrl Key </summary>
    Control = 0x11,
    /// <summary> Alt Key </summary>
    Menu =    0x12,
    /// <summary> Pause/Break Key </summary>
    Pause =   0x13,
    /// <summary> Caps Lock Key </summary>
    Capital = 0x14,

    /// <summary> Escape Key </summary>
    Escape = 0x1B,

    /// <summary> Space Key </summary>
    Space =    0x20,
    /// <summary> Page Up Key </summary>
    Prior =    0x21,
    /// <summary> Page Down Key </summary>
    Next =     0x22,
    /// <summary> End Key </summary>
    End =      0x23,
    /// <summary> Home Key </summary>
    Home =     0x24,
    /// <summary> Left Arrow Key </summary>
    Left =     0x25,
    /// <summary> Up Arrow Key </summary>
    Up =       0x26,
    /// <summary> Right Arrow Key </summary>
    Right =    0x27,
    /// <summary> Down Arrow Key </summary>
    Down =     0x28,
    Select =   0x29,//
    Print =    0x2A,//
    Execute =  0x2B,//
    /// <summary> Print Screen/System Request Key </summary>
    SnapShot = 0x2C,
    /// <summary> Insert Key </summary>
    Insert =   0x2D,
    /// <summary> Delete Key </summary>
    Delete =   0x2E,
    Help =     0x2F,//

    /// <summary> Zero/Right Paranthesis Key </summary>
    Zero =  0x30,
    /// <summary> One/Exclamation Mark Key </summary>
    One =   0x31,
    /// <summary> Two/At Symbol Key </summary>
    Two =   0x32,
    /// <summary> Three/Number Sign (aka Pound/Hash) Key </summary>
    Three = 0x33,
    /// <summary> Four/Dollar Sign Key </summary>
    Four =  0x34,
    /// <summary> Five/Percent Symbol Key </summary>
    Five =  0x35,
    /// <summary> Six/Caret Key </summary>
    Six =   0x36,
    /// <summary> Seven/Ampersand Key </summary>
    Seven = 0x37,
    /// <summary> Eight/Asterisk Key </summary>
    Eight = 0x38,
    /// <summary> Nine/Left Paranthesis Key </summary>
    Nine =  0x39,

    ///
    A = 0x41,
    ///
    B = 0x42,
    ///
    C = 0x43,
    ///
    D = 0x44,
    ///
    E = 0x45,
    ///
    F = 0x46,
    ///
    G = 0x47,
    ///
    H = 0x48,
    ///
    I = 0x49,
    ///
    J = 0x4A,
    ///
    K = 0x4B,
    ///
    L = 0x4C,
    ///
    M = 0x4D,
    ///
    N = 0x4E,
    ///
    O = 0x4F,
    ///
    P = 0x50,
    ///
    Q = 0x51,
    ///
    R = 0x52,
    ///
    S = 0x53,
    ///
    T = 0x54,
    ///
    U = 0x55,
    ///
    V = 0x56,
    ///
    W = 0x57,
    ///
    X = 0x58,
    ///
    Y = 0x59,
    ///
    Z = 0x5A,

    /// <summary> Left Windows Key </summary>
    LWin = 0x5B,
    /// <summary> Right Windows Key </summary>
    RWin = 0x5C,
    /// <summary> Apps Key </summary>
    Apps = 0x5D,

    Sleep = 0x5F,//

    ///
    NumpadZero =  0x60,
    ///
    NumpadOne =   0x61,
    ///
    NumpadTwo =   0x62,
    ///
    NumpadThree = 0x63,
    ///
    NumpadFour =  0x64,
    ///
    NumpadFive =  0x65,
    ///
    NumpadSix =   0x66,
    ///
    NumpadSeven = 0x67,
    ///
    NumpadEight = 0x68,
    ///
    NumpadNine =  0x69,
    ///
    Multiply =    0x6A,
    ///
    Add =         0x6B,
    ///
    Separator =   0x6C,
    ///
    Subtract =    0x6D,
    ///
    Decimal =     0x6E,
    ///
    Divide =      0x6F,

    ///
    F1 =  0x70,
    ///
    F2 =  0x71,
    ///
    F3 =  0x72,
    ///
    F4 =  0x73,
    ///
    F5 =  0x74,
    ///
    F6 =  0x75,
    ///
    F7 =  0x76,
    ///
    F8 =  0x77,
    ///
    F9 =  0x78,
    ///
    F10 = 0x79,
    ///
    F11 = 0x7A,
    ///
    F12 = 0x7B,
    ///
    F13 = 0x7C,
    ///
    F14 = 0x7D,
    ///
    F15 = 0x7E,
    ///
    F16 = 0x7F,
    ///
    F17 = 0x80,
    ///
    F18 = 0x81,
    ///
    F19 = 0x82,
    ///
    F20 = 0x83,
    ///
    F21 = 0x84,
    ///
    F22 = 0x85,
    ///
    F23 = 0x86,
    ///
    F24 = 0x87,

    /// <summary> Numpad Lock Key </summary>
    NumLock = 0x90,
    /// <summary> Scroll Lock Key </summary>
    Scroll =  0x91,

    /// <summary> Left Shift Key </summary>
    LShift =   0xA0,
    /// <summary> Right Shift Key </summary>
    RShift =   0xA1,
    /// <summary> Left Ctrl Key </summary>
    LControl = 0xA2,
    /// <summary> Right Ctrl Key </summary>
    RControl = 0xA3,
    /// <summary> Left Menu Key </summary>
    LMenu =    0xA4,
    /// <summary> Right Menu Key </summary>
    RMenu =    0xA5,

    /// <summary> OEM1 Key (Semicolon/Colon for US) </summary>
    OEM1 =      0xBA,
    /// <summary> OEM Plus Key (Equals/Plus) </summary>
    OEMPlus =   0xBB,
    /// <summary> OEM Comma Key (Comma/Less Than) </summary>
    OEMComma =  0xBC,
    /// <summary> OEM Key (Minus/Underscore) </summary>
    OEMMinus =  0xBD,
    /// <summary> OEM Key (Period/Greater Than) </summary>
    OEMPeriod = 0xBE,
    /// <summary> OEM2 Key (Forward Slash/Question Mark for US) </summary>
    OEM2 =      0xBF,
    /// <summary> OEM3 Key (Back Tick/Tilde for US) </summary>
    OEM3 =      0xC0,

    /// <summary> OEM4 Key (Left Bracket/Left Body for US) </summary>
    OEM4 = 0xDB,
    /// <summary> OE5M Key (Back Slash/Divider for US) </summary>
    OEM5 = 0xDC,
    /// <summary> OEM6 Key (Right Bracket/Right Body for US) </summary>
    OEM6 = 0xDD,
    /// <summary> OEM7 Key (Apostrophe/Quotation Mark for US) </summary>
    OEM7 = 0xDE,
    /// <summary> OEM8 Key </summary>
    OEM8 = 0xDF
}