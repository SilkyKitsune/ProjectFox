namespace ProjectFox.Windows;

/// <summary> Window Messages constants from Winuser </summary>
public enum WM : uint
{
    Null =    0x0000,
    Create =  0x0001,
    Destroy = 0x0002,
    Move =    0x0003,
    Size =    0x0005,

    Activate = 0x0006,

    SetFocus =      0x0007,
    KillFocus =     0x0008,
    Enable =        0x000A,
    SetRedraw =     0x000B,
    SetText =       0x000C,
    GetText =       0x000D,
    GetTextLength = 0x000E,
    Paint =         0x000F,
    Close =         0x0010,

    QueryEndSession = 0x0011,
    QueryOpen =       0x0013,
    EndSession =      0x0016,

    Quit =           0x0012,
    EraseBkgnd =     0x0014,
    SysColorChange = 0x0015,
    ShowWindow =     0x0018,
    WinIniChange =   0x001A,

    SettingChange = WinIniChange,

    DevModeChange = 0x001B,
    ActivateApp =   0x001C,
    FontChange =    0x001D,
    TimeChange =    0x001E,
    CancelMode =    0x001F,
    SetCursor =     0x0020,
    MouseActivate = 0x0021,
    ChildActivate = 0x0022,
    QueueSync =     0x0023,

    GetMinMaxInfo = 0x0024,

    PaintIcon =      0x0026,
    IconEraseBkgnd = 0x0027,
    NextDLGCTL =     0x0028,
    SpoolerStatus =  0x002A,
    DrawItem =       0x002B,
    MeasureItem =    0x002C,
    DeleteItem =     0x002D,
    VKeyToItem =     0x002E,
    CharToItem =     0x002F,
    SetFont =        0x0030,
    GetFont =        0x0031,
    SetHotkey =      0x0032,
    GetHotkey =      0x0033,
    QueryDragIcon =  0x0037,
    CompareItem =    0x0039,

    GetObject = 0x003D,

    Compacting =        0x0041,
    CommNotify =        0x0044,
    WindowPosChanging = 0x0046,
    WindowPosChanged =  0x0047,

    Power = 0x0048,

    CopyData =      0x004A,
    CancelJournal = 0x004B,

    Notify =                 0x004E,
    InputLangChangeRequest = 0x0050,
    InputLangChange =        0x0051,
    TCard =                  0x0052,
    Help =                   0x0053,
    UserChanged =            0x0054,
    NotifyFormat =           0x0055,

    ContextMenu =   0x007B,
    StyleChanging = 0x007C,
    StyleChanged =  0x007D,
    DisplayChange = 0x007E,
    GetIcon =       0x007F,
    SetIcon =       0x0080,

    NCCreate =   0x0081,
    NCDestroy =  0x0082,
    NCCalcSize = 0x0083,
    NCHitTest =  0x0084,
    NCPaint =    0x0085,
    NCActivate = 0x0086,
    GetDLGCode = 0x0087,

    SyncPaint =  0x0088,

    NCMouseMove =     0x00A0,
    NCLButtonDown =   0x00A1,
    NCLButtonUp =     0x00A2,
    NCLButtonDblClk = 0x00A3,
    NCRButtonDown =   0x00A4,
    NCRButtonUp =     0x00A5,
    NCRButtonDblClk = 0x00A6,
    NCMButtonDown =   0x00A7,
    NCMButtonUp =     0x00A8,
    NCMButtonDblClk = 0x00A9,

    NCXButtonDown =   0x00AB,
    NCXButtonUp =     0x00AC,
    NCXButtonDblClk = 0x00AD,

    InputDeviceChange = 0x00FE,

    Input = 0x00FF,

    //KeyFirst =    0x0100,//---|---same
    KeyDown =     0x0100,//---|
    KeyUp =       0x0101,
    Char =        0x0102,
    DeadChar =    0x0103,
    SysKeyDown =  0x0104,
    SysKeyUp =    0x0105,
    SysChar =     0x0106,
    SysDeadChar = 0x0107,

    UniChar =     0x0109,//---|---same
    KeyLast = UniChar,//    0x0109,//---|

    IMEStartComposition = 0x010D,
    IMEEndComposition =   0x010E,
    IMEComposition =      0x010F,//---|---same
    IMEKeylast = IMEComposition,//         0x010F,//---|

    InitDialog =      0x0110,
    Command =         0x0111,
    SysCommand =      0x0112,
    Timer =           0x0113,
    HScroll =         0x0114,
    VScroll =         0x0115,
    InitMenu =        0x0116,
    InitMenuPopup =   0x0117,

    Gesture =         0x0119,
    GestureNotify =   0x011A,

    MenuSelect =      0x011F,
    MenuChar =        0x0120,
    EnterIdle =       0x0121,

    MenuButtonUp =    0x0122,
    MenuDrag =        0x0123,
    MenuGetObject =   0x0124,
    UnInitMenuPopup = 0x0125,
    MenuCommand =     0x0126,

    ChangeUIState = 0x0127,
    UpdateUIState = 0x0128,
    QueryUIState =  0x0129,

    CTLColorMsgBox =    0x0132,
    CTLColorEdit =      0x0133,
    CTLColorListBox =   0x0134,
    CTLColorBtn =       0x0135,
    CTLColorDlg =       0x0136,
    CTLColorScrollBar = 0x0137,
    CTLColorStatic =    0x0138,

    //MouseFirst =    0x0200,//---|---same
    MouseMove =     0x0200,//---|
    LButtonDown =   0x0201,
    LButtonUp =     0x0202,
    LButtonDblClk = 0x0203,
    RButtonDown =   0x0204,
    RButtonUp =     0x0205,
    RButtonDblClk = 0x0206,
    MButtonDown =   0x0207,
    MButtonUp =     0x0208,
    MButtonDblClk = 0x0209,

    MouseWheel =    0x020A,

    XButtonDown =   0x020B,
    XButtonUp =     0x020C,
    XButtonDblClk = 0x020D,

    MouseHWheel =   0x020E,
    //same----------|
    MouseLast = 0x020E,//this one is different depending on windows version?

    ParentNotify =  0x0210,
    EnterMenuLoop = 0x0211,
    ExitMenuLoop =  0x0212,

    NextMenu =       0x0213,
    Sizing =         0x0214,
    CaptureChanged = 0x0215,
    Moving =         0x0216,

    PowerBroadcast = 0x0218,

    DeviceChange = 0x0219,

    MDICreate =      0x0220,
    MDIDestroy =     0x0221,
    MDIActivate =    0x0222,
    MDIRestore =     0x0223,
    MDINext =        0x0224,
    MDIMaximize =    0x0225,
    MDITile =        0x0226,
    MDICascade =     0x0227,
    MDIIconArrange = 0x0228,
    MDIGetActive =   0x0229,

    MDISetMenu =     0x0230,
    EnterSizeMove =  0x0231,
    ExitSizeMove =   0x0232,
    DropFiles =      0x0233,
    MDIRefreshMenu = 0x0234,
    
    PointerDeviceChange =     0x0238,
    PointerDeviceInRange =    0x0239,
    PointerDeviceOutOfRange = 0x023A,

    Touch = 0x0240,
    
    NCPointerUpdate =       0x0241,
    NCPointerDown =         0x0242,
    NCPointerUp =           0x0243,
    PointerUpdate =         0x0245,
    PointerDown =           0x0246,
    PointerUp =             0x0247,
    PointerEnter =          0x0249,
    PointerLeave =          0x024A,
    PointerActivate =       0x024B,
    PointerCaptureChanged = 0x024C,
    TouchHitTesting =       0x024D,
    PointerWheel =          0x024E,
    PointerHWheel =         0x024F,
    PointerRoutedTo =       0x0251,
    PointerRoutedAway =     0x0252,
    PointerRoutedReleased = 0x0253,

    IMESetContext =      0x0281,
    IMENotify =          0x0282,
    IMEControl =         0x0283,
    IMECompositionFull = 0x0284,
    IMESelect =          0x0285,
    IMEChar =            0x0286,

    IMERequest =         0x0288,

    IMEKeyDown =         0x0290,
    IMEKeyUp =           0x0291,

    MouseHover = 0x02A1,
    MouseLeave = 0x02A3,

    NCMouseHover = 0x02A0,
    NCMouseLeave = 0x02A2,

    WTSSessionChange = 0x02B1,

    TabletFirst = 0x02C0,
    TabletLast =  0x02DF,

    DPIChanged =             0x02E0,

    DPIChangedBeforeParent = 0x02E2,
    DPIChangedAfterParent =  0x02E3,
    GetDPIScaledSize =       0x02E4,

    Cut =               0x0300,
    Copy =              0x0301,
    Paste =             0x0302,
    Clear =             0x0303,
    Undo =              0x0304,
    RenderFormat =      0x0305,
    RenderAllFormats =  0x0306,
    DestroyClipboard =  0x0307,
    DrawClipboard =     0x0308,
    PaintClipboard =    0x0309,
    VScrollClipboard =  0x030A,
    SizeClipboard =     0x030B,
    AskCBFormatName =   0x030C,
    ChangeCBChain =     0x030D,
    HScrollClipboard =  0x030E,
    QueryNewPalette =   0x030F,
    PaletteIsChanging = 0x0310,
    PaletteChanged =    0x0311,
    Hotkey =            0x0312,

    Print =       0x0317,
    PrintClient = 0x0318,

    AppCommand = 0x0319,

    ThemeChanged = 0x031A,

    ClipboardUpdate = 0x031D,

    DWMCompositionChanged =       0x031E,
    DWMNCRenderingChanged =       0x031F,
    DWMColorizationColorChanged = 0x0320,
    DWMWindowMaximizedChange =    0x0321,

    DWMSendIconicThumbnail =         0x0323,
    DWMSendIconicLivePreviewBitmap = 0x0326,

    GetTitlBarInfoEX = 0x033F,

    HandheldFirst = 0x0358,
    HandheldLast =  0x035F,

    AFXFirst = 0x0360,
    AFXLast =  0x037F,

    PenWinFirst = 0x0380,
    PenWinLast =  0x038F,

    App = 0x8000,

    User = 0x0400
}