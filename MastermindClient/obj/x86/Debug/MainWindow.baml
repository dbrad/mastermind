   M S B A M L   `   `   `  ����  MastermindClient!    MastermindClient.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6F   -   5          � Title$8  2Code Breaker - David Brad and Bradley Elliott 2011��5   	   $	��525��5       � SizeToContent$ Height=�6*    � WindowStartupLocation$ CenterScreen=�6Z    � 
ResizeMode$ CanMinimize=�6   .��5      � .+�5   
   �� -   5        $��
connectBox��5   N   $��Connect to a game.��6   $��70��6/   $��	50,0,50,0q�6;   $	��Top=�6`   .��5	      � .+�5
      �� -   5        $��	lblServer��5
   b   $��Server��6   $��24��6)   $
��Left=�65   $��2,12,0,0q�6P   $	��Top=�6s   �� 5      -   5        $��	txtServer��5   :   $��24��6   $��
50,12,83,0q�6&   $	��Top=�6K   �� 5      -   5        $��	btnServer��5   I   $��Connect��6   $��24��6+   $��0,12,2,0q�67   $	��Top=�6Z   $��Right=�6�   $��75��6�   �� 5      -   5        $��lblName��5   `   $
��Name��6   $��24��6'   $
��Left=�63   $��2,12,0,0q�6N   $	��Top=�6o   $y�Hidden=�6�   �� 5      -   5        $��txtName��5   :   $��24��6   $��
50,12,83,0q�6&   $	��Top=�6I   $y�Hidden=�6a   �� 5      -   5        $��btnJoin��5   F   $
��Join��6   $��24��6(   $��0,12,2,0q�64   $	��Top=�6U   $��Right=�6�   $��75��6�   $y�Hidden=�6�   5      5      �� 5   
   -	   5        $��	btnSubmit��5   \   $��Submit��6   $��50��6"   $
��Left=�6.   $��	50,10,0,0q�6I   $	��Top=�6m   $��50��6�   $y�Hidden=�6�   6�   � 5   
   -
   5        $��
gridAnswer��5   :   $��	50,0,50,0q�6   $	��Top=�6"   .+�5      �� -   5        $��	lblResult��5   g   $�� ��6   $��44(�6   $��70��6-   $��Center=�69   $��0,0,0,0q�6V   $��Center=�6x   5      � 5   
   -   5        $��gridPegs��5   $   $��50,98,50,10q�6   5      �� 5   
   -   5        $��lblTurn��5   c   $��Player's Turn:��6   $��28��6)   $
��Left=�65   $��	50,70,0,0q�6P   $	��Top=�6r   $y�Hidden=�6�   �� 5   
   -   5        $��	lblPlayer��5   V   $�� ��6   $��28��6   $
��Left=�6'   $��
136,70,0,0q�6B   $	��Top=�6g   $y�Hidden=�6   5      5      