   M S B A M L   `   `   `  ����L  HFreed.Api.Monitor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:Freed.Api.Monitor.UserControlTmp Freed.Api.Monitor.UserControlTmp  5      <    6Freed.Api.Monitor.UserControlTmp.SocketDataGridControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      @local.clr-namespace:Freed.Api.Monitor.UserControlTmp     5      -   5          � 	Resources  5	         ColumnHeaderStyleC �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader �� Property �� Value �� Triggers P� Property P� Value��%�   &	        �� 5
   
   "  6+   .�5      ��  � ��$ Center=�6;    5      P� 5       �@J�IsMouseOver . . �5      ��  �@�ToolTip$ Click to sort.��60   5      5      5      .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   .+�5   
   �� $��0��6   ._�5      &   System.Windows.Controls.DataGrid  -   5          Name$ Scoket_DataGrid��5   %   	 J� Opacity$		 0.5Q�6   
   AutoGenerateColumns
 .     CanUserAddRows .     ColumnHeaderStyle 5      �� 5      "  6   .�5      ��  � ��$ Center=�6G   5      �� 5       � �� �#f5f5f56I   �� 5       � ��
 �   �6G   �� 5       � ��$ 1q�6<   �� 5        � ��
 �����68   5!         Columns 5#      0  *System.Windows.Controls.DataGridTextColumn  5$      ,  &System.Windows.Controls.DataGridColumn   Header$ 接口名称��6m      
Foreground
 ��  �   FontSize$ 15Q�6�   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6*   �� *
ActionName+   ClipboardContentBinding 6I   Z� 6�     5%      $ 接口地址��6l   $ 15Q�6z    6*   �� *
RequestUrl+ 6I   Z� 6z     5&      $ 访问数量��6n   
 �  ��$ 15Q�6�    6*   �� *RequestCount+ 6K   Z� 6�   5)      5*      5+      5,      