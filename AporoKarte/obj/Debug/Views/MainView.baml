   M S B A M L   `   `   `  ����E  AAporoKarte, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:AporoKarte.ViewModelsAporoKarte.ViewModels  5   	   'clr-namespace:AporoKarte
AporoKarte  5   	       AporoKarte.Views.MainView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   2vm#clr-namespace:AporoKarte.ViewModels     5   	   *localclr-namespace:AporoKarte     5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   Bei9http://schemas.microsoft.com/expression/2010/interactions  5   	     � Title$6  0あぽろの森　顧客カルテ管理ツール��5	   	   $	��700��6"   $
��1024��6/    � 
ResizeMode$ CanMinimize=�6<   ,   &AporoKarte.CloseWindowAttachedBehavior   Close 5   	   �� *CloseWindow+.��5      � ���	#FFFFF5B9.+�5   
   �� $
��Left=�6   $��36��6/   $��	10,10,0,0q�6;   $
��Wrap=�6N   $	��Top=�6b   $	��986��6z   $��顧客カルテ一覧��6�   $��24(�6�   ���	#FFA8EEAC6�   �� 5   
   $
��Left=�6   $��24��6/   $��	10,66,0,0q�6;   $
��Wrap=�6N   $��検索条件��6b   $	��Top=�6n   $��81��6�   $��16(�6�   ���	#FF272727$
��Bold"�6�   �� 5   
   $
��Left=�6   $��24��6/   $
��Wrap=�6;   $��申込者名��6O   $	��Top=�6[   $��73��6s   $��16(�6~   ���	#FF272727$��31.5,94,0,0q�6�   �� 5   
   $
��Left=�6   $��24��6-   $��104.5,94,0,0q�69    �� TextWrapping$
 Wrap=�6O   $	��Top=�6�   $	��162��6�   ��6c   ��  �� Path$ 
ClientName�6�   �� 5   
   $
��Left=�6   $��24��6/   $
��Wrap=�6;   $��	調整日��6O   $	��Top=�6Z   $��50��6r   $��16(�6}   ���	#FF272727$��286.5,94,0,0q�6�   �� 5   
   $
��Left=�6   $��24��6/   $
��Wrap=�6;   $	��～��6O   $	��Top=�6X   $��19��6p   $��16(�6{   ���	#FF272727$��457.5,94,0,0q�6�   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ adjustDateStart��5      $
��Left=�6\   $	��Top=�6w   $��341.5,93,0,0q�6�   $	��110��6�      SelectedDate 6.   �� $ AdjustDateStart�6�     5   
   -   5        $ adjustDateEnd��5      $
��Left=�6X   $	��Top=�6s   $��481.5,93,0,0q�6�   $	��110��6�    6,   �� $ AdjustDateEnd�6�   �� 5   
   $��検索��6   $��615.5,92,332.5,0q�6   $
��Auto��68   $	��Top=�6F   $�� ��6�   $��14(�6�   ��6^   �� $ SearchListCommand�6�   �� 5   
   $��新規登録��6   $��884,126,34,0q�6    $��24��6]   $��14(�6i   $	��Top=�6w   ��66   �� *RegistKarteCommand+6w   &   System.Windows.Controls.DataGrid  5   
   -   5        $��dataGrid��5      $��31.5,155,34,54q�6N   $��Visible=�6f   $��Visible=�6�      CanUserAddRows .     AutoGenerateColumns .  ��6#   �� $ KarteListData�	   Columns	 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn
   Header$
 Id��6%      
Visibility$ Hidden=�6S      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� $ ClientId�6g     5       $
 申込者名��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 140 6W    .  63   �� $ 
ClientName�6c     5!      $
 紹介者名��6%   $	 140 6\    .  63   �� $ IntroductorName�6h     5"      $
 住所(都道府県)��6%   $	 100 6\    .  67   �� $ AddressPref�6h     5#      $
 住所(市区町村)��6%   $	 100 6\    .  67   �� $ AddressCity�6h     5$      $
 電話番号��6%   $	 100 6P    .  63   �� $	 Tel�6\     5%      $
 	調整日��6%   $ 80 6n    .  62   �� $ 	AdjustYmd� �� StringFormat$ 
yyyy/MM/dd��6y     5&      $
 	確認日��6%   $ 80 6o    .  62   �� $ 
ConfirmYmd�$ 
yyyy/MM/dd��6z     5'      $
 金額��6%   $	 100 6i    .  61   �� $ AdjustAmount�$	 #,0��   ElementStyle 5(      �� 5)       �� 
TargetType" ��6    .�5*      ��  �� Property �@��TextAlignment �� Value$ Right=�6>   5+      5-      4  .System.Windows.Controls.DataGridTemplateColumn  5/      $
 編集��6)   $ 80 65      CellTemplate 51      �� 53      .R�55      �� $��編集��6%   {�. ��6C   �� *$"DataContext.EditKarteDetailCommand+ �� RelativeSource ��  �� AncestorType"  ��6�   �� $ ClientId�57      5:      5>      �� 5A   
   $��終了��6   $��884,623,34,0q�6   $�� ��6U   $��14(�6i   $��24��6w   $	��Top=�6�   ��64   �� *CloseCommand+6�   5C      5D      