   M S B A M L   `   `   `  ����L  HFreed.Api.Monitor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Freed.Api.Monitor.ResourceFreed.Api.Monitor.Resource  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   DataGridRow.Height  DataGrid.BorderThickness  u� Bottom u� Left u� Right
 u� Top   ColumnHeader.BorderThickness  DataGridRow.BorderThickness  DataGrid.BorderBrush �� Color  ColumnHeader.Background  ColumnHeader.BorderBrush  DataGridRow.BorderBrush ! DataGridRow.Normal.Background !	 DataGridRow.Normal.Foreground $
  DataGridRow.MouseOver.Background $  DataGridRow.MouseOver.Foreground # DataGridRow.Selected.Background # DataGridRow.Selected.Foreground&    System.Windows.Controls.DataGrid �� 
TargetType �� Property �� Value=  7System.Windows.Controls.Primitives.DataGridColumnHeader �� 
TargetType	 ��Name
 J� SnapsToDevicePixels � UseLayoutRounding)  #System.Windows.Controls.DataGridRow?  9System.Windows.Controls.Primitives.SelectiveScrollingGrid � ColumnDefinitions � RowDefinitions?  9System.Windows.Controls.Primitives.DataGridCellsPresenter   
ItemsPanelA  ;System.Windows.Controls.Primitives.DataGridDetailsPresenter   DetailsVisibility$   SelectiveScrollingOrientation;  5System.Windows.Controls.SelectiveScrollingOrientation   Vertical �� ConverterParameter#    RowDetailsScrollingConverter �� 	Converter �� RelativeSource �� AncestorType:  4System.Windows.Controls.Primitives.DataGridRowHeader7  1System.Windows.Controls.DataGridHeadersVisibility
   Row!    HeadersVisibilityConverter �� Triggers P� Property P� Value*	  $System.Windows.Controls.DataGridCell �� Triggers�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      2local(clr-namespace:Freed.Api.Monitor.Resource  5      .sys&clr-namespace:System;assembly=mscorlib  5      %  &	        &	      &	 \     &	 �     &	 �     &	     &	 "    &	 C    &	 d    &		 �    &	
 �    &	 �    &	 �    &	 	    '   *    7  7  7  7   '      7  7  '  
    7  7  7  7  7   7  7 
 7  7  '	  �    X�5      33Q�60   u� 5      $  1Q�61   $ 1Q�6<   $ 1Q�6E   $ 1Q�6O   u� 5
      $  0Q�65   $ 0Q�6@   $ 1Q�6I   $ 0Q�6S   u� 5      $  0Q�64   $ 0Q�6?   $ 0Q�6H   $ 1Q�6R   �� 5      $ #E9ECF1��63   �� 5      $ #F6F7FB��66   �� 5      $ #E9ECF1��67   �� 5      $ #E9ECF1��66   �� 5      $ #FFFFFF��6<   �� 5      $ #000000��6<   �� 5      $ #F6F7FB��6?   �� 5      $ #000000��6?   �� 5      $ #F6F7FB��6>   �� 5!      $ #000000��6>   �� 5#      "   6   .�5%   
   ��  �@  RowHeaderWidth$ 0Q�6+   �� 5&   
    � ��8   6,   �� 5'   
    �@  HeadersVisibility$ Column=�6.   �� 5(   
    � ��8  6'   �� 5)   
    � ��8  6(   �� 5*   
     �@  HorizontalGridLinesBrush �	#00E9ECF165   �� 5+   
    �@  VerticalGridLinesBrush �	#00E9ECF163   �� 5,   
    �@�UseLayoutRounding . 6.   �� 5-   
    �@J�SnapsToDevicePixels . 60   �� 5.   
    �@  AutoGenerateColumns .  60   �� 5/   
    �@  CanUserAddRows .  6+   �� 50   
    �@  CanUserReorderColumns .  62   �� 51   
    �@  CanUserResizeColumns .  61   �� 52   
    �@  CanUserResizeRows .  6.   �� 53   
    �@  CanUserSortColumns .  6/   �� 54   
    �@  GridLinesVisibility$
 None=�60   �� 55   
    �@  
IsReadOnly . 6'   �� 56   
    �@  	RowHeight8  6&   �� 57   
    �@  SelectionMode$ Single=�6*   58      �� 5:      "  6   .�5;   
   ��  �@J�SnapsToDevicePixels . 60   �� 5<   
    � �� �#0000006'   �� 5=   
    � ��$ 12(�6%   �� 5>   
    �@�Cursor$
 Hand��6#   �� 5?   
    � ��$ 28��6#   �� 5@   
    � �� 5A      �� 5B      "  6"   .R�5C      �� $	 BackgroundBorder��6   $
��Auto��67   $
��-1,0q�6D   8��  8�� 
 .  . ._�5G      �� $	��5,0q�6+   $��Center=�68   $��Center=�6U   5H      5I      5K      5L      �� 5N      "  6   .�5O   
   ��  � ��8   6'   �� 5P   
    � ��8  6'   �� 5Q   
    �@J�SnapsToDevicePixels . 60   �� 5R   
    �@�UseLayoutRounding . 6.   �� 5S   
    �@=�ErrorTemplate 65   Z� �� 5T   
    � ��8  6,   �� 5U   
    � ��8  6(   �� 5V   
    �@ ValidationErrorTemplate 5W      �� 5X      .R�5Y      �� $��0,0,0,0q�6    $��Center=�61   
���  ��$��!��6]   5Z      5\      �� 5]   
    � �� 5^      �� 5_      "  6"   .R�5`      �� $	 
DGR_Border��6   $��0,0,0,-1q�61   #��z��5a      #��z��5b      #��z��5c      
 .  . ._�5e         5f      �� 5g   "   $
��Auto �63   �� 5h   "   $��* �63    5j      �� 5k   "   $��* �60   8�� 6;   �� 5l   "   $
��Auto �60   .+�5n        $��1��65   #��z 5o   5   #
 z
 5p   5     5q      $��1��67   $��1��6D   #y�z 5v   7    5r   7   �� *AreRowDetailsFrozen+# Z # Z  �� "   5v   7     5w      $��2��60   $ Vertical=�6A   y�5x   0   �� *HeadersVisibility+# Z # Z  �� "   5|      5}       5~      P� 5       �@J�IsMouseOver . . �5�      �� 8   � ��6;   �� 5�      8   � ��6;   5�      P� 5�       �@ 
IsSelected . . �5�      �� 8   � ��6;   �� 5�      8   � ��6;   5�      5�      5�      5�      �� 5�      " 	 6   .�5�   
   ��  � �� 5�      �� 5�      " 	 6"   .R�5�      �� $	 Bg��6   
������  . ._�5�      �� $��Center=�6+   $��Center=�6H   5�      5�      5�       5�   
   P� 5�       �@	 
IsSelected . . �5�      ��  � ��
 ���� 6/   �� 5�       � �� �#0000006/   5�      5�      5�      