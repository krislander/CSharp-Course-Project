   M S B A M L   `   `   `  ����M  IProjectTemplate_v2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:ProjectTemplate_v2.ViewsProjectTemplate_v2.Views  5      -    'ProjectTemplate_v2.Views.EditFormDialog   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:ProjectTemplate_v2.Views     5      V RMaterialDesignThemes.Wpf, Version=2.5.0.1205, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      $	��508��5
      $	��400��6   $��20q�5      .��5      ��   � 	Resources  5   
    �� 
TargetType %  !MaterialDesignFloatingHintTextBox �� BasedOn �� Property �� Value��%[   '��       7   �� 5      " ��6   8   .�5      ��  � ��$ 18(�6-   5      5      .+�5   
   �� )  #MaterialDesignThemes.Wpf.HintAssist   Hint$
 Name��5      $��0 0 0 13q�69   ��5      �� *Name+ �� ValidatesOnDataErrors .  �� UpdateSourceTrigger$ 	LostFocus=�5   9   �� 5   
    & "MaterialDesignFloatingHintComboBox#��[ 5      $ Link with sensor��5      $��18(�5      $��0 0 0 8q�6!   ��5      �� *Models+ �� SelectedItem 5      �� *
ToLinkWith+��5      �� 5      .R�5      �� ��6    �� *Tag+	 � ToolTip	 65   �� *Description+5      5       � 5!   
   $��0 0 0 13q�5"      y�5!      �� *
OtherVis+.+�5#      �� $	 Min��5$      $��50��6;   $��0 0 15 0q�6F   ��5#      �� *
MinValue+ . $ 	LostFocus=�5$   F   �� 5%      $	 Max��5&      $��50��6;   ��5%      �� *
MaxValue+ . $ 	LostFocus=�5&   ;   5'      �� 5(   
   #��[ 5)      $
��Left=�5*      $��0 0 0 8q�5+      $ Value��5,      $	��100��5-      $��18(�6   
 �� SelectedIndex
 5(      �� *Open+y�63   �� *
DoorWindow+.@�5.      �� $��Closed��6   �� 5/      $
��Open��6   50      � 51   
   $��0 0 0 13q�6   .+�52      �� $ Latitude��53      $��0 0 15 0q�6@   $	��100��6R   ��52      �� *
Latitude+ . $ 	LostFocus=�53   R   �� 54      $ 	Longitude��55      $	��100��6A   ��54      �� *	Longitude+ . $ 	LostFocus=�55   A   56      �� 57   
    ( $MaterialDesignFilledTextFieldTextBox#��[ 58      $	��200��59      $��0 5 0 13q�6    �� TextWrapping$
 Wrap=�5:      $��18(�5;      $ Description��5<      $	��Top=�5=      ��57      �� *Description+ . $ 	LostFocus=�5=      � 5>   
   .+�5?      �� $��Cancel��6     MaterialDesignFlatButton#��[ 5@      )  #MaterialDesignThemes.Wpf.DialogHost   CloseDialogCommand#��Z 5A      �� 5B      $��Confirm��6   ��5C      �� *EditCommand+5D      5E      5F      