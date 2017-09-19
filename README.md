# Unity-Winform-Solution
Unity winform solution

Q：When Open a new window (select file) Unity will be Automatic minimization, is boring. how can we fixed it?
A:you can use this solution can fixed it!

Q:Can I fixed it in Unity setting?
A:yes! 
Q：How?
A:File - BuildSettings - PlayerSettings - Resolution and Presentation - Visible In Background -- just check it on.
Q:Why not use this method?
A:Use "Unity-Winform-Solution" is better way to controll window layer,set foreground,fullscreen and without boarder.

WindowMod.cs can set the window foreground or backbround, set fullscreen and without boarder.

ToolControlTaskBar.cs is a static class, you can use this to set the TaskBar to hide or show any where,just use : ToolcontrolTaskBar.ShowTaskBar(); or use ToolControlTaskBar.HideTaskBar();

WindowOpenfile.cs is a sample for open file window.


CH:
WindowMod.cs 这个脚本用来控制窗口的置顶、全屏和边框设置。
ToolControlTaskBar.cs 用来控制任务栏的显示和隐藏。
WindowOpenfile.cs 这是一个打开选取窗口的样例。


Any question can contact me mark@u3dc.com


