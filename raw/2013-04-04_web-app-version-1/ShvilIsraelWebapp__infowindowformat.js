function getGeneralItemHTMLDescription(general_item){
var informationString = 
	
'<div id="content" style="direction: rtl; ">'+

'<b>' + general_item.title + '</b>' + 
'<hr style="background:#0EC4C1; border:0; height:2px" />'+
general_item.text + 
'</div>';

return informationString;
}

function getAngelHTMLDescription(angel){

var informationString = 
	
'<div id="content" style="direction: rtl; ">'+

'<b>' + 
angel.city + ', ' + angel.name + '</b>' + 
'<hr style="background:#0EC4C1; border:0; height:2px" />'+

'<img alt="info" src="http://icons.iconarchive.com/icons/kyo-tux/delikate/16/Info-icon.png" style="width: 16px; height: 16px;" />' +
'&nbsp;' + angel.information +

'<br>'+
'<img alt="phone" src="http://icons.iconarchive.com/icons/wwalczyszyn/android-style-honeycomb/16/Phone-icon.png" style="width: 16px; height: 16px;" />' +
'&nbsp;' + angel.phone1;

if  (angel.phone2){
informationString += 
'<br>'+
'<img alt="phone" src="http://icons.iconarchive.com/icons/wwalczyszyn/android-style-honeycomb/16/Phone-icon.png" style="width: 16px; height: 16px;" />' +
'&nbsp;' + angel.phone2;
}

if (angel.religious){
informationString += 
'<br>'+
'<img alt="yes" src="http://icons.iconarchive.com/icons/icojam/blue-bits/16/symbol-check-icon.png" style="width: 16px; height: 16px;" />&nbsp;דתי';
}

if (angel.passport){
informationString += 
'<br>'+
'<img alt="yes" src="http://icons.iconarchive.com/icons/icojam/blue-bits/16/symbol-check-icon.png" style="width: 16px; height: 16px;" />&nbsp;דרכון';
}

if (angel.stamp){
informationString += 
'<br>'+
'<img alt="yes" src="http://icons.iconarchive.com/icons/icojam/blue-bits/16/symbol-check-icon.png" style="width: 16px; height: 16px;" />&nbsp;חותמת';
}

informationString += 
'</div>';

return informationString;
	}