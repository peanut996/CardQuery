   M S B A M L   `   `   `  ����D  @CardQuery, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:CardQuery	CardQuery  5   	       CardQuery.InitWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:CardQuery     5   	     � Title$  Waiting ��5   	   $	��180��6   $	��320��6'    � WindowStartupLocation$ Manual=�63   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $
��141* �6   �� 5      $	��19* �6   �� 5      $	��21* �6   �� 5      $
��131* �6   .+�5   
   �� $��Center=�6   $
��Left=�62   $��Initializing...��6T   $	��Top=�6n   $��66��6�   $��24(�6�   $��Century Gothic)�6�   $
��Bold"�6�   $��107,46,45,0q�6�   $��4��6�   �� 5   
   
������ $��Center=�6*   $��Center=�6L    J� RenderTransformOrigin$ 0.5,0.5&�5      $��65,56,24,37q�6?   z�5      V� 5      .~�5      ��  �� Angle$ 0Q�6'   5       � Triggers 5      :� 5       :� RoutedEvent$ FrameworkElement.Loaded��6   .[�5      �� .��5      �� .��5      W�  W� From$ 0X�6.   	 W� To$	 360X�67   	 g� RepeatBehavior$	 Forever��6@   
 g� Duration$
 0:0:1D�6Y    �� TargetProperty$< 6(RenderTransform).(TransformGroup.Children)[0].(Angle)�5   .   5      5      5       .��5"      �� $��32��6   $��tide_logo_128px.png��6    $
��Fill=�6=   $��32��6L   5#      5%      5&      