   M S B A M L   `   `   `  ����c  _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  5	   	    Freed.Api.Monitor"   Freed.Api.Monitor.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5	   	   -   5           NoResize_window#��[  5   	     � 
ResizeMode$  NoResize=�5   	    � Title$ 
MainWindow��5
   	   $	��780��6   $
��1200��6)    � WindowStartupLocation$ CenterScreen=�5   	   .   (System.Windows.Interactivity.Interaction   Triggers 5      /   )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Loaded��6   .   (System.Windows.Interactivity.TriggerBase   Actions. 5      6   0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *DoInit+5       � 	Resources 5        CloseBut �� 
TargetType	 ��Name
 ��Name �� Triggers P� Property P� Value �� 
TargetName �� Property �� Value  HideBut 
 BigBut  MenuBut  ButtonTemplate1 �� CornerRadius J� Effect3  -System.Windows.Media.Effects.DropShadowEffect   Color   
BlurRadius   ShadowDepth��%�  &	       &	 ?    &	 �    &	 �    &	 �    �� 5   
   " ��6+   .R�5      �� $	 closebor��6   $��25��6'   $��25��62   ._�5      �� $
 closeImg��6   $��/Images/关闭(2).png��6*   5       5      P� 5       �@J�IsMouseOver . . �5      �� $ closeImg��6    � ��$ /Images/关闭 (1).png��6F   5      5      �� 5   
   " ��6*   .R�5      �� $	 closebor��6   $��25��6'   $��25��62   ._�5       �� $
 closeImg��6   $��/Images/最小化(2).png��6*   5!       5#      P� 5$       �@J�IsMouseOver . . �5%      �� $ closeImg��6    � ��$ /Images/最小化 (1).png��6F   5&      5(      �� 5*   
   " ��6)   .R�5+      �� $	 Bigbor��6   $��25��6%   $��25��60   ._�5,      �� $
 BigImg��6   $��/Images/放大(2).png��6(   5-       5/      P� 50       �@J�IsMouseOver . . �51      �� $ BigImg��6    � ��$ /Images/放大 (1).png��6D   52      54      �� 56   
   " ��6*   .R�57      �� $	 Bigbor��6   $��25��6%   $��25��60   ._�58      �� $
 MenuImg��6   $��/Images/Menu.png��6)   59       5;      P� 5<       �@J�IsMouseOver . . �5=      �� $ MenuImg��6    � ��$ /Images/Menu (1).png��6E   5>      5@      �� 5B   
   " ��62   .R�5C      �� $��border1��6   
�������$��1q�68   $ 20��6L   ���#8DC44C$��50��6s   $	��180��6    5E        5F      $ Black��6'   $ 10Q�65   $ 8Q�6E   6W   ._�5H      � .+�5J      �� #��z��6'   $��Center=�6R   $��Center=�6m   6�   5K      5L       5N      P� 5O       �@J�IsMouseOver . . �5P      �� $ border1��6    � ��
 � ���6I   5Q      5S      5T      ��5U      �� 5V   
    �� ImageSource$ /Images/bg.png��6   .��5X      � -   5         �Name$ wb��5X       � RowDefinitions 5Y   
   �� 5Z      $��50 �6   �� 5[      $��* �6   �� 5\      $��30 �6   .+�5`   
   �� $��0��6   
���   �$��0,1,0,1q�62   ._�5a      �  � ColumnDefinitions 5b      �� 5c      $	��350 �6'   �� 5d      $��* �6'   .+�5f      �� $��0��6   ._�5g      �  5h      �� 5i      $��50 �6/   �� 5j      $��* �6/   .+�5l      �� $��Images\logo.png��6    �� 5m      $��1��6$   $��25(�64   $��Center=�6B   
�������$��微软雅黑，幼圆)�6p   $
��Bold"�6�   WMS接口请求监控6�   5n      5o      �� 5q      $��1��6   $��Right=�6)   $��Center=�6E   ._�5r      � .+�5s      �� #��[ 6!   $��50��6F   $��50��6R    � ToolTip$ 菜单��6]   ��6j   �� *ClickMenuCommand+�� 5t      -   5        #��[ 5t   !   $��50��6W   $��50��6c   $ 	最大化��6n   �� 5u      -   5        #��[ 5u   !   $��50��6W   $��50��6c   $ 	最小化��6n   �� 5v      -   5        #��[ 5v   !   $��50��6Y   $��50��6e   $ 关闭��6p   5w      5x      5y      5z      �� 5}   
   $��1��6   ._�5~      �  5      �� 5�      $
��Auto �6'   �� 5�      $��* �6'   .+�5�      �� $��0��6   y�6)   �� *LeftMenuVisibility+._�5�      A�  A� Rows$ 8��6"    A� Columns$ 1��6+   
�������$��Center=�6K   .+�5�      ��   ImageButton2#��[ 6!   $��0��6p   $��50��6�   $��50��6�   $ 	主菜单��6�   ��6G   �� *LeftMeunClickCommand+6�   �� 5�      #��[ 6!   $��1��6p   $��50��6�   $��50��6�   $ 接口请求监控主页��6�   ��6G   �� *LeftMeunClickCommand+6�   �� 5�      #��[ 6!   $��2��6p   $��50��6�   $��50��6�   $ 接口日志监控��6�   ��6G   �� *LeftMeunClickCommand+6�   �� 5�      #��[ 6!   $��3��6p   $��50��6�   $��50��6�   $ 接口请求统计��6�   ��6G   �� *LeftMeunClickCommand+6�   �� 5�      #��[ 6!   $��4��6p   $��50��6�   $��50��6�   $ 账套接口统计��6�   ��6G   �� *LeftMeunClickCommand+6�   �� 5�      #��[ 6!   $��50��6l   $��50��6w   $ 
主菜单5��6�   ��6G   �� *ClickMenuCommand+6�   �� 5�      #��[ 6!   $��50��6l   $��50��6w   $ 
主菜单6��6�   ��6G   �� *ClickMenuCommand+6�   �� 5�      #��[ 6!   $��50��6l   $��50��6w   $ 
主菜单7��6�   ��6G   �� *ClickMenuCommand+6�   5�      5�       � 5�      -   5          �Name$ 	frameMaim��5�   (   $��1��6     � NavigationUIVisibility$ Hidden=�6;     � JournalOwnership$ UsesParentJournal=�6[      � Source  6�   �� *
MainTraget+5�      5�      �� 5�   
   $��2��6   
���   �$��0,1,0,1q�62   ._�5�      �  5�      �� 5�      $	��120 �6'   �� 5�      $��* �6'   �� 5�      $	��200 �6'   .+�5�      �� $��0��6   ._�5�      � $��Center=�6    $��Center=�6=   ! � Orientation$! 
Horizontal=�6X   .+�5�      �� 
�������当前版本号：6?   �� 5�      $��0,0,0,0q�6>   
����  ���6$   �� *
Versions+6O   5�      5�      �� 5�      $��2��6   ._�5�      � $��Center=�6    $��Center=�6=   $! 
Horizontal=�6X   .+�5�      �� 
���������6$   �� *	NowTime+6=   �� 5�      $��10,0,0,0q�6=   
����  ���6$   �� *	NowWeek+6O   5�      5�      5�      5�      5�      5�      