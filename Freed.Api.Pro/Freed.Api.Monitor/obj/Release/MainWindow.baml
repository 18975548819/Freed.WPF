   M S B A M L   `   `   `  ���� c  _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity   Freed.Api.Monitor"   Freed.Api.Monitor.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity        NoResize_window#��[    � 
ResizeMode$  NoResize=� � Title$ 
MainWindow��$	��780��$
��1200�� � WindowStartupLocation$ CenterScreen=�.   (System.Windows.Interactivity.Interaction   Triggers /   )System.Windows.Interactivity.EventTrigger     	EventName$ Loaded��.   (System.Windows.Interactivity.TriggerBase   Actions. 6   0System.Windows.Interactivity.InvokeCommandAction     Command �� *DoInit+ � 	Resources   CloseBut �� 
TargetType	 ��Name
 ��Name �� Triggers P� Property P� Value �� 
TargetName �� Property �� Value  HideBut 
 BigBut  ButtonTemplate1 �� CornerRadius J� Effect3  -System.Windows.Media.Effects.DropShadowEffect   Color   
BlurRadius   ShadowDepth��%|  &	       &	 �     &	 �    &	 R    �� " ��.R��� $	 closebor��$��25��$��25��._��� $
 closeImg��$��/Images/关闭(2).png�� P�  �@J�IsMouseOver . . ��� $ closeImg�� � ��$ /Images/关闭 (1).png���� " ��.R��� $	 closebor��$��25��$��25��._��� $
 closeImg��$��/Images/最小化(2).png�� P�  �@J�IsMouseOver . . ��� $ closeImg�� � ��$ /Images/最小化 (1).png���� " ��.R��� $	 Bigbor��$��25��$��25��._��� $
 BigImg��$��/Images/放大(2).png�� P�  �@J�IsMouseOver . . ��� $ BigImg�� � ��$ /Images/放大 (1).png���� " ��.R��� $��border1��
�������$��1q�$ 20�����#8DC44C$��50��$	��180��   $ Black��$ 10Q�$ 8Q�._�� .+��� #��z��$��Center=�$��Center=� P�  �@J�IsMouseOver . . ��� $ border1�� � ��
 � �������  �� ImageSource$ /Images/bg.png��.��� -    �Name$ wb�� � RowDefinitions �� $��50 ��� $��* ��� $��30 �.+��� $��0��
���   �$��0,1,0,1q�._��  � ColumnDefinitions �� $	��350 ��� $��* �.+��� $��0��._��  �� $��50 ��� $��* �.+��� $��Images\logo.png���� $��1��$��25(�$��Center=�
����  �$��微软雅黑，幼圆)�$
��Bold"�WMS接口请求监控�� $��1��$��Right=�$��Center=�._�� .+��� -   #��[ $��50��$��50�� � ToolTip$ 	最大化���� -   #��[ $��50��$��50��$ 	最小化���� -   #��[ $��50��$��50��$ 关闭���� $��1��._� � -     �Name$ 	frameMaim��$��1��  � NavigationUIVisibility$ Hidden=�  � JournalOwnership$ UsesParentJournal=�  � Source �� *
MainTraget+�� $��2��
���   �$��0,1,0,1q�._��  �� $	��120 ��� $��* ��� $	��200 �.+��� $��0��._�� $��Center=�$��Center=� � Orientation$ 
Horizontal=�.+��� 
�������当前版本号：�� $��0,0,0,0q�
����  ����� *
Versions+�� $��2��._�� $��Center=�$��Center=�$ 
Horizontal=�.+��� 
����������� *	NowTime+�� $��10,0,0,0q�
����  ����� *	NowWeek+