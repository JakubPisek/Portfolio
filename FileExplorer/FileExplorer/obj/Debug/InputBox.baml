   M S B A M L   `   `   `  ����G  CFileExplorer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:FileExplorerFileExplorer  5   	       FileExplorer.InputBox   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:FileExplorer     5   	     � Title$  Placeholder��5   	    � WindowStartupLocation$ CenterScreen=�5	   	   $	��130��5
   	   $	��300��6   $	��130��5   	   $	��300��6    � 
ResizeMode$ NoResize=�5   	   .��5      �  � ShowGridLines .   � RowDefinitions 5   
   �� 5      $��2* �6   �� 5      $��3* �6   �� 5      $��2* �6    � ColumnDefinitions 5   
   �� 5      $
��0.5* �6   �� 5      $��6* �6   �� 5      $��2* �6   �� 5      $
��2.5* �6   �� 5      $
��0.5* �6   .+�5!   
   �� -   5        $��Headline_TextBlock��5!      $��Placeholder��6.   $��0��5"      $��1��6!   $��3��61   $��Center=�6E   $��Stretch=�6`   �� 5&   
   -   5        $��Input_TextBox��5&      $�� ��6'    �� TextWrapping$
 Wrap=�6/   $��14(�6C   $��1��5'      $��1��6   $��3��6/   $��Center=�6C   $��Stretch=�6^   $��20��5(      $��20��6   �� 53   
   -   5        $��	Ok_Button��53      $��Ok��6#   $��2��54      $��3��6   $	��Top=�6.   $��Right=�6F   $��20��55      $��50��6   $��20��6(   $��50��67   6E   58      59      