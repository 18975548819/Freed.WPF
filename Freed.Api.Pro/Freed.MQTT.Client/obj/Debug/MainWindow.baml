   M S B A M L   `   `   `  ����L  HFreed.MQTT.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Freed.MQTT.ClientFreed.MQTT.Client  5   	   "    Freed.MQTT.Client.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:Freed.MQTT.Client     5   	   -   5          �Name$  
mqttClient��5   5    � Title$ 
MainWindow��6	   $	��450��6   $	��800��6)   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   
���   �$��0,1,0,1q�62   ._�5      � $��Center=�6   .+�5      �� 服务器IP：6$   �� 5      -   5         ��Name$ txtIp��5   &   $	��100��6   .��65   	127.0.0.16@   �� 5      服务器端口：6$   �� 5      -   5        $ txtPort��5   &   $	��100��6   .��67   123456>   �� 5      -   5        .��5   +   连接6/   �� 5      -   5        .��5   +   断开6/   5      5      �� 5   
   $��1��6   
���   �$��0,0,0,1q�62   ._�5      �  � ColumnDefinitions 5      �� 5      $
��Auto �6'   �� 5      $
��463* �6'   �� 5      $
��181* �6'   �� 5       $
��Auto �6'   .+�5"      �� -   5         ��Name$ comboTopics��5"   +   $��0��6    �� DisplayMemberPath$ Topic��6�    �� SelectedValuePath$ Topic��6�   $	��120��6�   ��6@   ��  �� Path$ 	AllTopics�	 �� Mode$	 TwoWay=�
 �� ElementName$
 
mqttClient��6�   �� 5#      -   5        $ 
txtContent��5#   >   $��1��6   $��2��6*   6T   �� 5$      -   5         ��Name$ 
btnPublish��5$   )   $��3��6   .��6V   发布6Z   5%      5&      �� 5(   
   $��2��6   ._�5)      �  5*      �� 5+      $	��300 �6'   �� 5,      $��* �6'   .+�5.      �� $��0��6   ._�5/      �� $��订阅��6   .��50      �  51      �� 52   "   $��* �60   6=   �� 53   "   $
��Auto �60   6@   .+�55      &   System.Windows.Controls.DataGrid  -	   5          Name$ dgSub��55   4   $��0��6'      AutoGenerateColumns .     CanUserAddRows .  ��6_   �� $ 	AllTopics�$	 TwoWay=�$
 
mqttClient��   Columns 56   "   4  .System.Windows.Controls.DataGridCheckBoxColumn  57   &   ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ 1* 6\   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6=   �� *
IsSelected+6i   0  *System.Windows.Controls.DataGridTextColumn  58   &      Header$ 主题��6S   $ 2* 6_    69   �� *Topic+6l     59   &   $ 描述��6V   $ 2* 6b    69   �� *
Describe+6o   5;      � 5<      $��1��6(   $��Center=�65   $��Center=�6P   .+�5=   "   �� -
   5        $ btnSave��5=   )   .��6P   保存6T   5>      5?      5@      5A      �� 5C      $��1��6   ._�5D      �� $	��Log��6   .��5E      �� -   5         ��Name$ richMsg��5E   &   " �� VerticalScrollBarVisibility$
 Auto=�68    J� Opacity$	 0.5Q�6[   . �5F      /�  /� 
LineHeight$ 1��6+   6<    � ContextMenu 5G      �� 5H   "   .@�5I   &   m� -   5         m�Name$ 	menuClear��5I   /   $��清空内容��6Z   6j   5J   #   5L      5M      5N      5O      5P      5Q      5R      