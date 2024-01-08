namespace ProjectFox.Windows;

/// <summary> Window Styles constants from Winuser </summary>
public enum WS : uint
{
    /// <summary> The window is an overlapped window. An overlapped window has a title bar and a border. Same as the WS_TILED style. (0b0000_0000___0000_0000___0000_0000___0000_0000) </summary>
    Overlapped =   0x00000000u,

    /// <summary> The window is a pop-up window. This style cannot be used with the WS_CHILD style. (0b1000_0000___0000_0000___0000_0000___0000_0000) </summary>
    PopUp =        0x80000000u,

    /// <summary> The window is a child window. A window with this style cannot have a menu bar. This style cannot be used with the WS_POPUP style. (0b0100_0000___0000_0000___0000_0000___0000_0000) </summary>
    Child =        0x40000000u,

    /// <summary> The window is initially minimized. Same as the WS_ICONIC style. (0b0010_0000___0000_0000___0000_0000___0000_0000) </summary>
    Minimize =     0x20000000u,

    /// <summary> The window is initially visible. This style can be turned on and off by using the ShowWindow or SetWindowPos function. (0b0001_0000___0000_0000___0000_0000___0000_0000) </summary>
    Visible =      0x10000000u,

    /// <summary> The window is initially disabled. A disabled window cannot receive input from the user. To change this after a window has been created, use the EnableWindow function. (0b0000_1000___0000_0000___0000_0000___0000_0000) </summary>
    Disabled =     0x08000000u,

    /// <summary> Clips child windows relative to each other; that is, when a particular child window receives a WM_PAINT message, the WS_CLIPSIBLINGS style clips all other overlapping child windows out of the region of the child window to be updated. If WS_CLIPSIBLINGS is not specified and child windows overlap, it is possible, when drawing within the client area of a child window, to draw within the client area of a neighboring child window. (0b0000_0100___0000_0000___0000_0000___0000_0000) </summary>
    ClipSiblings = 0x04000000u,

    /// <summary> Excludes the area occupied by child windows when drawing occurs within the parent window. This style is used when creating the parent window. (0b0000_0010___0000_0000___0000_0000___0000_0000) </summary>
    ClipChildren = 0x02000000u,

    /// <summary> The window is initially maximized (0b0000_0001___0000_0000___0000_0000___0000_0000) </summary>
    Maximize =     0x01000000u,

    /// <summary> The window has a title bar (includes the WS_BORDER style) (0b0000_0000___1100_0000___0000_0000___0000_0000) </summary>
    Caption =      0x00C00000u,

    /// <summary> The window has a thin-line border (0b0000_0000___1000_0000___0000_0000___0000_0000) </summary>
    Border =       0x00800000u,

    /// <summary> The window has a border of a style typically used with dialog boxes. A window with this style cannot have a title bar. (0b0000_0000___0100_0000___0000_0000___0000_0000) </summary>
    DLGFrame =     0x00400000u,

    /// <summary> The window has a vertical scroll bar (0b0000_0000___0010_0000___0000_0000___0000_0000) </summary>
    VScroll =      0x00200000u,

    /// <summary> The window has a horizontal scroll bar (0b0000_0000___0001_0000___0000_0000___0000_0000) </summary>
    HScroll =      0x00100000u,

    /// <summary> The window has a window menu on its title bar. The WS_CAPTION style must also be specified. (0b0000_0000___0000_1000___0000_0000___0000_0000) </summary>
    SysMenu =      0x00080000u,

    /// <summary> The window has a sizing border. Same as the WS_SIZEBOX style. (0b0000_0000___0000_0100___0000_0000___0000_0000) </summary>
    ThickFrame =   0x00040000u,

    /// <summary> The window is the first control of a group of controls. The group consists of this first control and all controls defined after it, up to the next control with the WS_GROUP style. The first control in each group usually has the WS_TABSTOP style so that the user can move from group to group. The user can subsequently change the keyboard focus from one control in the group to the next control in the group by using the direction keys. You can turn this style on and off to change dialog box navigation. To change this style after a window has been created, use the SetWindowLong function. (0b0000_0000___0000_0010___0000_0000___0000_0000) </summary>
    Group =        0x00020000u,

    /// <summary> The window is a control that can receive the keyboard focus when the user presses the TAB key. Pressing the TAB key changes the keyboard focus to the next control with the WS_TABSTOP style. You can turn this style on and off to change dialog box navigation. To change this style after a window has been created, use the SetWindowLong function. For user-created windows and modeless dialogs to work with tab stops, alter the message loop to call the IsDialogMessage function. (0b0000_0000___0000_0001___0000_0000___0000_0000) </summary>
    TabStop =      0x00010000u,

    /// <summary> The window has a minimize button. Cannot be combined with the WS_EX_CONTEXTHELP style. The WS_SYSMENU style must also be specified. (0b0000_0000___0000_0010___0000_0000___0000_0000) </summary>
    MinimizeBox =  0x00020000u,
    //should these be set to group/tabstop?
    /// <summary> The window has a maximize button. Cannot be combined with the WS_EX_CONTEXTHELP style. The WS_SYSMENU style must also be specified. (0b0000_0000___0000_0001___0000_0000___0000_0000) </summary>
    MaximizeBox =  0x00010000u,

    /// <summary> The window is an overlapped window. An overlapped window has a title bar and a border. Same as the WS_OVERLAPPED style. (0b0000_0000___0000_0000___0000_0000___0000_0000) </summary>
    Tiled = Overlapped,

    /// <summary> The window is initially minimized (Same as the WS_MINIMIZE style) (0b0010_0000___0000_0000___0000_0000___0000_0000) </summary>
    Iconic = Minimize,

    /// <summary> The window has a sizing border. Same as the WS_THICKFRAME style. (0b0000_0000___0000_0100___0000_0000___0000_0000) </summary>
    SizeBox = ThickFrame,

    /// <summary> The window is an overlapped window. Same as the WS_OVERLAPPEDWINDOW style. (0b0000_0000___0000_0000___0000_0000___0000_0000) </summary>
    TiledWindow = OverlappedWindow,

    /// <summary> The window is an overlapped window. Same as the WS_TILEDWINDOW style. (0x00CF0000) (0b0000_0000___1100_1111___0000_0000___0000_0000) </summary>
    OverlappedWindow = Overlapped | Caption | SysMenu | ThickFrame | MinimizeBox | MaximizeBox,

    /// <summary> The window is a pop-up window. The WS_CAPTION and WS_POPUPWINDOW styles must be combined to make the window menu visible. (0x80880000) (0b1000_0000___1000_1000___0000_0000___0000_0000) </summary>
    PopUpWindow = PopUp | Border | SysMenu,

    /// <summary> Same as the WS_CHILD style (0b0100_0000___0000_0000___0000_0000___0000_0000) </summary>
    ChildWindow = Child,
}