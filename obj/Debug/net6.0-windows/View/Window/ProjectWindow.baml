   M S B A M L   `   `   `  ????U  QChatbotConstructorTelegram, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:ChatbotConstructorTelegram.View.Window&ChatbotConstructorTelegram.View.Window  5   	   ]3clr-namespace:ChatbotConstructorTelegram.ViewModels%ChatbotConstructorTelegram.ViewModels  5   	   Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466J6clr-namespace:FontAwesome.WPF;assembly=FontAwesome.WPFFontAwesome.WPF 5	   	   :    4ChatbotConstructorTelegram.View.Window.ProjectWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Flocal4clr-namespace:ChatbotConstructorTelegram.View.Window     5   	   Bvm3clr-namespace:ChatbotConstructorTelegram.ViewModels     5   	   X TFontAwesome5.Net, Version=2.1.11.0, Culture=neutral, PublicKeyToken=9cfaf01297a008f8/fa5%http://schemas.fontawesome.com/icons/  5   	   ?fa6clr-namespace:FontAwesome.WPF;assembly=FontAwesome.WPF  5	   	   ???	#FF17293A   MaterialDesignBody#???   5   	   $??Regular"?5   	   $??15(?5   	   &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=?5   	      TextRenderingMode$
 Auto=?5   	     MaterialDesignFont#???  5   	    ? WindowStartupLocation$ CenterScreen=?6;    ? WindowState$ 	Maximized=?5   	   $	??600??6!   $	??900??61    ? Title 5   	   ?? *Title+ ? DataContext 5      B  <ChatbotConstructorTelegram.ViewModels.ProjectWindowViewModel  5   
   .??5      ]? $
??True??6   .+?5   
   ?? $??24??6   $??Bottom=?6    ???	#FF263A50.@?5      ??  ?? Orientation$ 
Horizontal=?6   .+?5        FontAwesome5.ImageAwesome     Icon$ Brands_Telegram=?6#   $??16??6:      
Foreground
 ?????6`   ?? 5       #???  64   
???????$??10,1,0,0q?5!   4   ??5       ?? *Status+5!   H   ?? 5"      6   5#      5$      ? 5&   
   $	??Top=?6   $??40??6$   ???	#FF213556	 ? ColumnDefinitions	 5'      ?? 5(      $??41 ?6#   ?? 5)      $
??859* ?6#   .+?5+      ? .+?5,      "  FontAwesome.WPF.ImageAwesome  
   Icon$
 Spinner=?6"      Spin$
 True??61      SpinDuration$ 7Q?6=      
Foreground
 ??? ?$??30??6`   6n   5-      ?? 5.      $ 
Horizontal=?6   $??1??62   .+?5/      ?? $	??119??6   $??24??6%   $??10,0,0,0q?61   $??0q?6C     MaterialDesignFlatButton#???  50      #???  51      $??13(?53   >   ???	#FFBACED7 ? Cursor$
 Hand??54   0   ??52      ?? *ChangeTokenCommand+??53      ?? *TextChangeToken+55      56      57      ? 59   
   	 5:      ?? 5;      $??1* ?6#   ?? 5<      $??7* ?6#   ?? 5=      $??3* ?6#   .+?5@      ? $??0??6   
???   ?$??5,5,6,5q?66   5B      ?? 5D      $??0??6   $??3??6+   
???iii?$??0,5,0,5q?6J   $??Right=?6[   ? 5F      $??1??6   
???????$??3,5,3,5q?6=   5H      ? 5J      $??2??6   
????? ?$??6,5,5,5q?65   5L      ?? 5N      $??2??6   $??3??6+   
???iii?$??0,5,0,5q?6J   $
??Left=?6[   5O      5Q      5R      