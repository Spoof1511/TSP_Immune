   M S B A M L   `   `   `  ����E  ATSP Immune, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:TSP_Immune
TSP_Immune  5   	       TSP_Immune.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:TSP_Immune     5   	     � Title$  
TSP Solver��5   	    � SizeToContent$ WidthAndHeight=�6   .��5	      ]� .+�5
   
   o� $	��Top=�6   .@�5      m� $��Times New Roman)�6   $��14(�64   $
��File��6C   .@�5      m� -   5        $
��Open��5      6>   m� 5      -   5        $
��Save��5      6>   m� 5      -   5        $��Close��5      6@   5      5      � 5   
    � ColumnDefinitions 5      �� 5      $	��139 �6#   �� 5      $��5 �6#   �� 5      .+�5      �� $��0��6   $��5,5,0,5q�6)   .+�5      �� $��Times New Roman)�6   $��Italic$�69   $��14(�6L   Кол-во городов6j   �� 5      -   5         ��Name$ CitiesCountTextBox��5      $��Times New Roman)�66   $��Italic$�6S   $��14(�6f   .��6t   206x   �� 5      $��Times New Roman)�6   $��Italic$�69   $��14(�6L   Кол-во нейронов6k   �� 5      -   5        $ NeuronsCountTextBox��5      $��Times New Roman)�67   $��Italic$�6T   $��14(�6g   .��6u   606y   �� 5      $��Times New Roman)�6   $��Italic$�69   $��14(�6L    Итераций за такт6l   �� 5       -   5        $ IterationsCountTextBox��5       $��Times New Roman)�6:   $��Italic$�6W   $��14(�6j   .��6x   106|   �� 5!      -   5         ��Name$ Studing_Coef��5!      $��Times New Roman)�62   $��14(�6O   $��Italic$�6]   " Коэф. обучаемости6�   �� 5"      -   5        $ Stud_Coef_Textbox��5"      $��Times New Roman)�65   $��14(�6R   $��Italic$�6`   .��6t   26w   �� 5#      -	   5        $ Neightbourhood_Koef��5#      $��Times New Roman)�69   $��14(�6V   $��Italic$�6d   &$Коэф. соседства н-ов6�   �� 5$      -
   5        $! Neightbourhood_Koef_Textbox��5$      $��Times New Roman)�6?   $��14(�6\   $��Italic$�6j   .��6}   0,256�   �� 5%      -   5        $��Times New Roman)�5%   8   $��Italic$�6U   $��14(�6h   .��6v   Generate Cities6�   �� 5&      -   5         ��Name$ StartButton��5&      $��Times New Roman)�6F   $��Italic$�6c   $��14(�6v   $��Start��6�   6�   �� 5'      -   5        $��Times New Roman)�5'   4   $��Italic$�6Q   $��14(�6d   $
��Stop��6r   �� 5(      -   5         ��Name$
 Iter��5(      $��Times New Roman)�6&   $��14(�6C   $��Italic$�6Q   .��6d   Кол-во итераций:6v   �� 5)      -   5        $ IterationCount��5)      $��Times New Roman)�60   $��14(�6M   $��Italic$�6[   $��0��6n   6|   �� 5*      -   5        $ Length��5*      $��Times New Roman)�6(   $��14(�6E   $��Italic$�6S   .��6f   $"Вес получ-го цикла:6{   �� 5+      -   5        $ Length_label��5+      $��Times New Roman)�6.   $��14(�6K   $��Italic$�6Y   .��6l   06o   �� 5,      -   5        $ 	ZoomLabel��5,      $��Times New Roman)�6+   $��14(�6H   $��Italic$�6V   .��6i   !Масштабирование:6{   �� 5-      -   5        $��
ZoomSlider��5-      $��80��6+    � Minimum$ 1Q�66    � Maximum$ 10Q�6B   	 � Value$	 1Q�6O   �� 5.      $��4,0,0,0q�6   $��Center=�6-   ��6H   �� 
 �� ElementName$
 
ZoomSlider�� �� Path$ Value� �� StringFormat$ F1��5/      �� 50      $��2��6   $
��1200��6%   $	��650��62   
���   �$��1q�6S   �� 51      -   5        $��TrackScrollViewer��51   +   $��2��6   $
��Auto=�6D   $
��Auto=�6i   $��1��6�   $��10q�6�   .��52      �� -   5         ��Name$ Canvas��52      $
��1200��6*   $	��650��67    � LayoutTransform 53      �� 54       �� ScaleX 6)   �� $
 
ZoomSlider��$ Value� �� ScaleY 55   !   �� $
 
ZoomSlider��$ Value�57      58      �� 59      -   5        $ 	ProgTitle��59      $��Times New Roman)�6'   $��14(�6D   $��Italic$�6R   $S��MSOM using ideas  
from immune 
system to solve
Travelling Salesman
Problem.��6f   $��9,423,-1,86q�6�   5:      5;      5<      