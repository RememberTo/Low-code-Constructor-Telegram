   M S B A M L   `   `   `  ????U  QChatbotConstructorTelegram, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:ChatbotConstructorTelegram.View.Window&ChatbotConstructorTelegram.View.Window  5   	   ]3clr-namespace:ChatbotConstructorTelegram.ViewModels%ChatbotConstructorTelegram.ViewModels  5   	   B    <ChatbotConstructorTelegram.View.Window.CreationProjectWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Flocal4clr-namespace:ChatbotConstructorTelegram.View.Window     5   	   Bvm3clr-namespace:ChatbotConstructorTelegram.ViewModels     5   	   _ [MaterialDesignThemes.Wpf, Version=4.6.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     ? Title$   ??5
   	   ???	#FF17293A   MaterialDesignBody#???   5   	   $??Regular"?5   	   $??15(?5   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=?5   	      TextRenderingMode$
 Auto=?5   	   $	??500??6.   $	??800??6;    ? 
ResizeMode$ NoResize=?6G    ? WindowStartupLocation$ CenterScreen=?6]    ? DataContext 5      D  >ChatbotConstructorTelegram.ViewModels.CreationProjectViewModel  5   
   .??5      ?  ? RowDefinitions 5   
   ?? 5      ?? 5      $??55.04 ?6   .+?5   
   ?? $??0??6    ?? Orientation$ Vertical=?6"   .+?5      ?? $??60??6   .+?5      ?? $??Center=?6   $??Center=?69   
???????$??24(?5   4   $??
0,30,300,0q?6B   $??DejaVu Sans Light)?5       !Создание проекта6M   5!      ?? 5"      $??0,10,0,0q?6   .+?5#      ?? $
??Left=?69   $??Center=?6T   ???	#FFB23D3D$??16(?5$   3   $??	50,15,0,0q?6A   $??DejaVu Sans Light)?5%      ??5#      ?? *TextWarning+y?5%   ;   ?? *TextVisibilityWarning+6j   5&      ?? 5'      $??55??6   $ 
Horizontal=?6%   $??0,20,0,0q?6>   .+?5(      ?? -   5         ??Name$ TextBox_NameProject??5(      )  #MaterialDesignThemes.Wpf.HintAssist	   Hint$%	 Название проекта??5)      
 ? Cursor$
 Arrow??6L   $??7,3,0,0q?5+      $??DejaVu Sans Light)?6,      
Foreground ?#7F93A9???	#FF1D3348$	??500??5-   1   $??	35,5,15,5q?6=   
??????? % !MaterialDesignFloatingHintTextBox#???  5/       ?? 
CaretBrush
 ????? ?? SelectionBrush ?	#FF528EBB
?????? ??5*      ?? *TextNameProject+50   5   52      ?? 53      $??55??6   $ 
Horizontal=?6%   $??0,20,0,0q?6>   .+?55      ?? -   5        $ TextBox_Path??57      $	 Расположение??55      $??	35,5,15,5q?6H   $
 Arrow??57   0   $??7,3,0,0q?58      $??DejaVu Sans Light)?6,    ?#7F93A9???	#FF1D3348$	??500??5:   1   
???????#???  5<      
 ????? ?	#FF528EBB
?????? $??0,0,0,0q?5=   O   ??57   ?   ?? *
TextPath+.??5>      5?      ?? 5@        MaterialDesignFlatButton#??[ 6   $??45??6L   $

 Hand??5A      $??	0,10,0,10q?6(   ???	#FF03A9F4???	#00DDDDDD???	#FF03A9F4$??1,1,1,1q?5C      ??64   ?? *ChoseFolder+.??6T   ...6Y   5D      ?? 5E      $??55??6   $ 
Horizontal=?6%   $??0,20,0,0q?6>   .+?5F      ?? -   5        $ TextBox_Token??5F      $	 
Токен??5G      $
 Arrow??6A   $??7,3,0,0q?5I      $??DejaVu Sans Light)?6,    ?#7F93A9???	#FF1D3348$	??500??5K   1   $??	35,5,15,5q?6=   
???????#???  5M      
 ????? ?	#FF528EBB
?????? ??5H      ?? *	TextToken+5N   5   5P      5Q      ?? 5R   
   $??1??6   $ 
Horizontal=?6#   $??RightToLeft=?6<   ???	#FF17293A$??0,0,0,10q?6o   .+?5S      ?? -   5        $??Отмена??5S      $??30??6&   $	??100??6O   $??	15,0,15,0q?6[   ???	#DD000000???	#FF0072A66?   ?? 5T      -   5        $??Создать??5T      $??30??6'   $	??100??5U   3   ???	#DD000000??5T   3   ?? *CreateProject+5U   X   5V      5X      5Y      