   M S B A M L   `   `   `  ����M  IProjectTemplate_v2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:ProjectTemplate_v2.ViewsProjectTemplate_v2.Views  5      ,    &ProjectTemplate_v2.Views.AddFormDialog   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:ProjectTemplate_v2.Views     5      V RMaterialDesignThemes.Wpf, Version=2.5.0.1205, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      $	��400��5
      $	��564��6   $��20q�5      .��5      ��   � 	Resources  5   
    �� 
TargetType %  !MaterialDesignFloatingHintTextBox �� BasedOn �� Property �� Value��%[   '��       7   �� 5      " ��6   8   .�5      ��  � ��$ 18(�6-   5      5      .+�5   
   �� )  #MaterialDesignThemes.Wpf.HintAssist   Hint$
 Name��6   $��0 0 0 13q�69   ��5      �� *Name+ �� ValidatesOnDataErrors . �� 5   
    & "MaterialDesignFloatingHintComboBox#��[ 5      $
 Type��5      $��18(�5      $��0 0 0 13q�6!   ��5      �� *Types+ �� SelectedItem 5      �� *SelectedItem+ . 5   !   � 5    
   $��0 0 0 13q�5!      y�5       �� *Visibility1+.+�5"      �� $	 Min��6   $��70��6;   $��0 0 80 0q�6F   ��5#      �� *
MinValue+ . �� 5$      $	 Max��6   $��70��6;   ��5%      �� *
MaxValue+ . 5&      �� 5)   
   #��[ 5*      $ Value��5+      $	��100��5,      $��18(�6   $
��Left=�5-      y�5)      �� *Visibility2+ �� SelectedIndex 5.      �� *Open+.@�5/      �� $��Closed��6   �� 50      $
��Open��6   51      � 54   
   $��0 0 0 13q�6   .+�55      �� $ Latitude��6   $��0 0 15 0q�6@   $	��100��6R   ��56      �� *
Latitude+ . 	 �� UpdateSourceTrigger$	 	LostFocus=��� 57      $ 	Longitude��6   $	��100��6A   ��58      �� *	Longitude+ . $	 	LostFocus=�59      �� 5<   
   #��[ 5=      $ Link with sensor��5>      $��18(�5?      $��0 0 0 5q�6!   ��5<      �� *Models+{�5@      �� *	IsEnabled+ 5A      �� *
ToLinkWith+ . $	 PropertyChanged=���5B      �� 5C      .R�5D      �� ��6    �� *Tag+
 � ToolTip
 65   �� *Description+5E      5G      d� 5J   
    $  MaterialDesignActionToggleButton#��[ 6   $
 Track Sensor��5K      $��0 10 0 5q�5L       d� 	IsChecked 5M      �� *
Tracking+��5N      '  !MaterialDesignThemes.Wpf.PackIcon  5O      �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.5.0.1205, Culture=neutral, PublicKeyToken=null]]   Kind$ EyePlus=�6*   1  +MaterialDesignThemes.Wpf.ToggleButtonAssist   	OnContent 5Q        5R      $
 Tick=�6*   5T      �� 5W   
    ( $MaterialDesignFilledTextFieldTextBox#��[ 6   $	��150��5X      $��0 5 0 15q�6    �� TextWrapping$
 Wrap=�5Y      $��18(�5Z      $ Description��5[      $	��Top=�5\      ��5]      �� *Description+ . $	 	LostFocus=�� 5`   
   .+�5a      �� $��Cancel��6     MaterialDesignFlatButton#��[ 5b      )  #MaterialDesignThemes.Wpf.DialogHost   CloseDialogCommand#��Z 5c      �� 5d      $��Submit��6   ��5e      �� *SubmitCommand+5f      5g      5h      