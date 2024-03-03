using System;
using ProjectFox.CoreEngine.Math;

namespace ProjectFox.Windows;

public enum MK : int//what are these for?
{
    LButton = 0x0001,
    RButton = 0x0002,
    Shift = 0x0004,
    Control = 0x0008,
    MButton = 0x0010,
    XButton1 = 0x0020,
    XButton2 = 0x0040,
}

public struct KeyboardMouseState
{
    public Vector mousePosition;
    public int mouseWheel;
    
    //double clicks

    #region Keys
    public bool
        LButton,//mouse buttons?
        RButton,
        Cancel,
        MButton,

        XButton1,
        XButton2,

        Back,
        Tab,

        Clear,
        Return,

        Shift,
        Control,
        Menu,
        Pause,
        Capital,

        Escape,

        Space,
        Prior,
        Next,
        End,
        Home,
        Left,
        Up,
        Right,
        Down,
        Select,
        Print,
        Execute,
        SnapShot,
        Insert,
        Delete,
        Help,

        Zero,
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,

        A,
        B,
        C,
        D,
        E,
        F,
        G,
        H,
        I,
        J,
        K,
        L,
        M,
        N,
        O,
        P,
        Q,
        R,
        S,
        T,
        U,
        V,
        W,
        X,
        Y,
        Z,

        LWin,
        RWin,
        Apps,

        Sleep,

        NumpadZero,
        NumpadOne,
        NumpadTwo,
        NumpadThree,
        NumpadFour,
        NumpadFive,
        NumpadSix,
        NumpadSeven,
        NumpadEight,
        NumpadNine,
        Multiply,
        Add,
        Separator,
        Subtract,
        Decimal,
        Divide,

        F1,
        F2,
        F3,
        F4,
        F5,
        F6,
        F7,
        F8,
        F9,
        F10,
        F11,
        F12,
        F13,
        F14,
        F15,
        F16,
        F17,
        F18,
        F19,
        F20,
        F21,
        F22,
        F23,
        F24,

        NumLock,
        Scroll,

        LShift,
        RShift,
        LControl,
        RControl,
        LMenu,
        RMenu,

        OEM1,
        OEMPlus,
        OEMComma,
        OEMMinus,
        OEMPeriod,
        OEM2,
        OEM3,

        OEM4,
        OEM5,
        OEM6,
        OEM7,
        OEM8;
    #endregion
    
    public void UpdateKey(VK keyCode, bool value)
    {
        switch (keyCode)
        {
            case VK.LButton:
                LButton = value;
                break;
            case VK.RButton:
                RButton = value;
                break;
            case VK.Cancel:
                Cancel = value;
                break;
            case VK.MButton:
                MButton = value;
                break;

            case VK.XButton1:
                XButton1 = value;
                break;
            case VK.XButton2:
                XButton2 = value;
                break;

            case VK.Back:
                Back = value;
                break;
            case VK.Tab:
                Tab = value;
                break;

            case VK.Clear:
                Clear = value;
                break;
            case VK.Return:
                Return = value;
                break;

            case VK.Shift:
                Shift = value;
                break;
            case VK.Control:
                Control = value;
                break;
            case VK.Menu:
                Menu = value;
                break;
            case VK.Pause:
                Pause = value;
                break;
            case VK.Capital:
                Capital = value;
                break;

            case VK.Escape:
                Escape = value;
                break;

            case VK.Space:
                Space = value;
                break;
            case VK.Prior:
                Prior = value;
                break;
            case VK.Next:
                Next = value;
                break;
            case VK.End:
                End = value;
                break;
            case VK.Home:
                Home = value;
                break;
            case VK.Left:
                Left = value;
                break;
            case VK.Up:
                Up = value;
                break;
            case VK.Right:
                Right = value;
                break;
            case VK.Down:
                Down = value;
                break;
            case VK.Select:
                Select = value;
                break;
            case VK.Print:
                Print = value;
                break;
            case VK.Execute:
                Execute = value;
                break;
            case VK.SnapShot:
                SnapShot = value;
                break;
            case VK.Insert:
                Insert = value;
                break;
            case VK.Delete:
                Delete = value;
                break;
            case VK.Help:
                Help = value;
                break;

            case VK.Zero:
                Zero = value;
                break;
            case VK.One:
                One = value;
                break;
            case VK.Two:
                Two = value;
                break;
            case VK.Three:
                Three = value;
                break;
            case VK.Four:
                Four = value;
                break;
            case VK.Five:
                Five = value;
                break;
            case VK.Six:
                Six = value;
                break;
            case VK.Seven:
                Seven = value;
                break;
            case VK.Eight:
                Eight = value;
                break;
            case VK.Nine:
                Nine = value;
                break;

            case VK.A:
                A = value;
                break;
            case VK.B:
                B = value;
                break;
            case VK.C:
                C = value;
                break;
            case VK.D:
                D = value;
                break;
            case VK.E:
                E = value;
                break;
            case VK.F:
                F = value;
                break;
            case VK.G:
                G = value;
                break;
            case VK.H:
                H = value;
                break;
            case VK.I:
                I = value;
                break;
            case VK.J:
                J = value;
                break;
            case VK.K:
                K = value;
                break;
            case VK.L:
                L = value;
                break;
            case VK.M:
                M = value;
                break;
            case VK.N:
                N = value;
                break;
            case VK.O:
                O = value;
                break;
            case VK.P:
                P = value;
                break;
            case VK.Q:
                Q = value;
                break;
            case VK.R:
                R = value;
                break;
            case VK.S:
                S = value;
                break;
            case VK.T:
                T = value;
                break;
            case VK.U:
                U = value;
                break;
            case VK.V:
                V = value;
                break;
            case VK.W:
                W = value;
                break;
            case VK.X:
                X = value;
                break;
            case VK.Y:
                Y = value;
                break;
            case VK.Z:
                Z = value;
                break;

            case VK.LWin:
                LWin = value;
                break;
            case VK.RWin:
                RWin = value;
                break;
            case VK.Apps:
                Apps = value;
                break;

            case VK.Sleep:
                Sleep = value;
                break;

            case VK.NumpadZero:
                NumpadZero = value;
                break;
            case VK.NumpadOne:
                NumpadOne = value;
                break;
            case VK.NumpadTwo:
                NumpadTwo = value;
                break;
            case VK.NumpadThree:
                NumpadThree = value;
                break;
            case VK.NumpadFour:
                NumpadFour = value;
                break;
            case VK.NumpadFive:
                NumpadFive = value;
                break;
            case VK.NumpadSix:
                NumpadSix = value;
                break;
            case VK.NumpadSeven:
                NumpadSeven = value;
                break;
            case VK.NumpadEight:
                NumpadEight = value;
                break;
            case VK.NumpadNine:
                NumpadNine = value;
                break;
            case VK.Multiply:
                Multiply = value;
                break;
            case VK.Add:
                Add = value;
                break;
            case VK.Separator:
                Separator = value;
                break;
            case VK.Subtract:
                Subtract = value;
                break;
            case VK.Decimal:
                Decimal = value;
                break;
            case VK.Divide:
                Divide = value;
                break;

            case VK.F1:
                F1 = value;
                break;
            case VK.F2:
                F2 = value;
                break;
            case VK.F3:
                F3 = value;
                break;
            case VK.F4:
                F4 = value;
                break;
            case VK.F5:
                F5 = value;
                break;
            case VK.F6:
                F6 = value;
                break;
            case VK.F7:
                F7 = value;
                break;
            case VK.F8:
                F8 = value;
                break;
            case VK.F9:
                F9 = value;
                break;
            case VK.F10:
                F10 = value;
                break;
            case VK.F11:
                F11 = value;
                break;
            case VK.F12:
                F12 = value;
                break;
            case VK.F13:
                F13 = value;
                break;
            case VK.F14:
                F14 = value;
                break;
            case VK.F15:
                F15 = value;
                break;
            case VK.F16:
                F16 = value;
                break;
            case VK.F17:
                F17 = value;
                break;
            case VK.F18:
                F18 = value;
                break;
            case VK.F19:
                F19 = value;
                break;
            case VK.F20:
                F20 = value;
                break;
            case VK.F21:
                F21 = value;
                break;
            case VK.F22:
                F22 = value;
                break;
            case VK.F23:
                F23 = value;
                break;
            case VK.F24:
                F24 = value;
                break;

            case VK.NumLock:
                NumLock = value;
                break;
            case VK.Scroll:
                Scroll = value;
                break;

            case VK.LShift:
                LShift = value;
                break;
            case VK.RShift:
                RShift = value;
                break;
            case VK.LControl:
                LControl = value;
                break;
            case VK.RControl:
                RControl = value;
                break;
            case VK.LMenu:
                LMenu = value;
                break;
            case VK.RMenu:
                RMenu = value;
                break;

            case VK.OEM1:
                OEM1 = value;
                break;
            case VK.OEMPlus:
                OEMPlus = value;
                break;
            case VK.OEMComma:
                OEMComma = value;
                break;
            case VK.OEMMinus:
                OEMMinus = value;
                break;
            case VK.OEMPeriod:
                OEMPeriod = value;
                break;
            case VK.OEM2:
                OEM2 = value;
                break;
            case VK.OEM3:
                OEM3 = value;
                break;

            case VK.OEM4:
                OEM4 = value;
                break;
            case VK.OEM5:
                OEM5 = value;
                break;
            case VK.OEM6:
                OEM6 = value;
                break;
            case VK.OEM7:
                OEM7 = value;
                break;
            case VK.OEM8:
                OEM8 = value;
                break;

            default:
                throw new Exception($"Invalid KeyCode '{keyCode}'");//mute key throws exception
        }
    }
}