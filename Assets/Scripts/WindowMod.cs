using System;

using System.Collections;

using System.Runtime.InteropServices;

using System.Diagnostics;

using UnityEngine;
/// <summary>
/// GAME窗口的置顶和隐藏，本类需要挂载在monobehavior的任意物件上运行启用
/// 原理：通过winform的api找到当前的unity窗口名字，IntPtr hWnd = FindWindow(null,"U3DC");这里的U3DC时你打包uniy发布后的窗口名字，需要注意，找到这个窗口才能控制它置顶然后设置无边框，编辑器下无作用，打包后才能起效。
/// </summary>
public class WindowMod : MonoBehaviour
{

    [DllImport("user32.dll")]
    static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hPos, int x, int y, int cx, int cy, uint nflags);

    [DllImport("User32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Auto)]
    private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("User32.dll", EntryPoint = "SetWindowLong")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    [DllImport("User32.dll", EntryPoint = "GetWindowLong")]
    private static extern int GetWindowLong(IntPtr hWnd, int dwNewLong);

    [DllImport("User32.dll", EntryPoint = "MoveWindow")]
    private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int width, int height, bool repaint);

    [DllImport("user32.dll", EntryPoint = "ShowWindow", CharSet = CharSet.Auto)]
    public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

    [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
    public static extern int SendMessage(IntPtr hwnd, int msg, IntPtr wP, IntPtr IP);

    [DllImport("user32.dll", EntryPoint = "SetParent", CharSet = CharSet.Auto)]
    public static extern IntPtr SetParent(IntPtr hChild, IntPtr hParent);

    [DllImport("user32.dll", EntryPoint = "GetParent", CharSet = CharSet.Auto)]
    public static extern IntPtr GetParent(IntPtr hChild);

    [DllImport("User32.dll", EntryPoint = "GetSystemMetrics")]
    public static extern IntPtr GetSystemMetrics(int nIndex);

    [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
    public static extern bool SetForegroundWindow(IntPtr hWnd);//设置此窗体为活动窗体

    public enum appStyle
    {
        FullScreen = 0,
        WindowedFullScreen = 1,
        Windowed = 2,
        WindowedWithoutBorder = 3,
    }
    public appStyle AppWindowStyle = appStyle.WindowedFullScreen;

    public enum zDepth
    {
        Normal = 0,
        Top = 1,
        TopMost = 2,
    }
    public zDepth ScreenDepth = zDepth.Normal;
    public int windowLeft = 0;
    public int windowTop = 0;
    private int windowWidth = Screen.width;
    private int windowHeight = Screen.height;
    const uint SWP_SHOWWINDOW = 0x0040;
    const int GWL_STYLE = -16;
    const int WS_BORDER = 1;
    private Rect screenPosition;
    private const int GWL_EXSTYLE = (-20);
    private const int WS_CAPTION = 0xC00000;
    private const int WS_POPUP = 0x800000;
    IntPtr HWND_TOP = new IntPtr(0);
    IntPtr HWND_TOPMOST = new IntPtr(-1);
    IntPtr HWND_NORMAL = new IntPtr(-2);
    private const int SM_CXSCREEN = 0x00000000;
    private const int SM_CYSCREEN = 0x00000001;
    int Xscreen;
    int Yscreen;
    //add 2015.4.21
    public bool StartAuto = false;
    public enum ScreenDirection
    {
        defaultDirection,
        horizontal,
        vertical,
    }
    public ScreenDirection CurDirection = ScreenDirection.defaultDirection;
    void Awake()
    {
        Xscreen = (int)GetSystemMetrics(SM_CXSCREEN);
        Yscreen = (int)GetSystemMetrics(SM_CYSCREEN);

        if (!StartAuto)
        {
            if (Xscreen > Yscreen)
            {
                windowWidth = 1920;
                windowHeight = 1080;
                // Global.CurDictiion = Global.EnumDiction.Horizontal;
            }
            else
            {
                windowWidth = 1080;
                windowHeight = 1920;
                //Global.CurDictiion = Global.EnumDiction.Vertical;
            }
        }
        else
        {
            if (CurDirection == ScreenDirection.horizontal)
            {
                windowWidth = 1920;
                windowHeight = 1080;
                // Global.CurDictiion = Global.EnumDiction.Horizontal;
            }
            else if (CurDirection == ScreenDirection.vertical)
            {
                windowWidth = 1080;
                windowHeight = 1920;
                //Global.CurDictiion = Global.EnumDiction.Vertical;
            }
        }
        if ((int)AppWindowStyle == 0)
        {
            Screen.SetResolution(Xscreen, Yscreen, true);
        }
        if ((int)AppWindowStyle == 1)
        {
            //Screen.SetResolution(Xscreen - 1, Yscreen - 1, false);
            //screenPosition = new Rect(0, 0, Xscreen - 1, Yscreen - 1);

            Screen.SetResolution(windowWidth, windowHeight, false);
            screenPosition = new Rect(0, 0, windowWidth, windowHeight);
        }
        if ((int)AppWindowStyle == 2)
        {
            Screen.SetResolution(windowWidth, windowWidth, false);
        }
        if ((int)AppWindowStyle == 3)
        {
            Screen.SetResolution(windowWidth, windowWidth, false);
            screenPosition = new Rect(windowLeft, windowTop, windowWidth, windowWidth);
        }
    }

    void Start()
    {
        InvokeRepeating("LaunchProjectile", 1, 0.5f);
    }

    void LaunchProjectile()
    {
        print("hello");
    }

    int i = 0;
    void Update()
    {
        IntPtr hWnd = FindWindow(null, "U3DC");  //U3DC 这个是我unity 打包发布后的 窗口名字。这里注意设置。</span><br>
        //if(hWnd != IntPtr.Zero)
        //{
        //    System.IO.Directory.CreateDirectory("d:\\ttt");
        //}
        SetForegroundWindow(hWnd);

        if (i < 30)
        {
            if ((int)AppWindowStyle == 1)
            {
                SetWindowLong(hWnd, -16, 369164288);
                if ((int)ScreenDepth == 0)
                    SetWindowPos(hWnd, HWND_NORMAL, (int)screenPosition.x, (int)screenPosition.y, (int)screenPosition.width, (int)screenPosition.height, SWP_SHOWWINDOW);
                if ((int)ScreenDepth == 1)
                    SetWindowPos(hWnd, HWND_TOP, (int)screenPosition.x, (int)screenPosition.y, (int)screenPosition.width, (int)screenPosition.height, SWP_SHOWWINDOW);
                if ((int)ScreenDepth == 2)
                    SetWindowPos(hWnd, HWND_TOPMOST, (int)screenPosition.x, (int)screenPosition.y, (int)screenPosition.width, (int)screenPosition.height, SWP_SHOWWINDOW);
                //ShowWindow(GetForegroundWindow(), 3);
            }

            if ((int)AppWindowStyle == 2)
            {
                if ((int)ScreenDepth == 0)
                {
                    SetWindowPos(hWnd, HWND_NORMAL, 0, 0, 0, 0, 0x0001 | 0x0002);
                    SetWindowPos(hWnd, HWND_NORMAL, 0, 0, 0, 0, 0x0001 | 0x0002 | 0x0020);
                }
                if ((int)ScreenDepth == 1)
                {
                    SetWindowPos(hWnd, HWND_TOP, 0, 0, 0, 0, 0x0001 | 0x0002);
                    SetWindowPos(hWnd, HWND_TOP, 0, 0, 0, 0, 0x0001 | 0x0002 | 0x0020);
                }
                if ((int)ScreenDepth == 2)
                {
                    SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, 0x0001 | 0x0002);
                    SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, 0x0001 | 0x0002 | 0x0020);
                }
            }
            if ((int)AppWindowStyle == 3)
            {
                SetWindowLong(hWnd, -16, 369164288);
                if ((int)ScreenDepth == 0)
                    SetWindowPos(hWnd, HWND_NORMAL, (int)screenPosition.x, (int)screenPosition.y, (int)screenPosition.width, (int)screenPosition.height, SWP_SHOWWINDOW);
                if ((int)ScreenDepth == 1)
                    SetWindowPos(hWnd, HWND_TOP, (int)screenPosition.x, (int)screenPosition.y, (int)screenPosition.width, (int)screenPosition.height, SWP_SHOWWINDOW);
                if ((int)ScreenDepth == 2)
                    SetWindowPos(hWnd, HWND_TOPMOST, (int)screenPosition.x, (int)screenPosition.y, (int)screenPosition.width, (int)screenPosition.height, SWP_SHOWWINDOW);
            }
        }
        i++;

    }
}