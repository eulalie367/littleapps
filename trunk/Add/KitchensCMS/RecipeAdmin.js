/// <reference path="jquery-1.4.1-vsdoc.js" />

function pageLoad(sender, args) {

    // Recipe List   

    $('iframe', window.parent.document).css("overflow","scroll");

    $(".gvRecipes").hide();
    $(".gvRecipes").show(500);

    $(".hideOnClick").click(function () {
        $(".gvRecipes").hide(500);
    });

    $('a.edit').fancybox({
        'transitionIn': 'elastic',
        'transitionOut': 'elastic',
        'speedIn': 600,
        'speedOut': 200,
        'overlayShow': true,
        'type': 'iframe',
        'width': 550,
        'height': '90%'
    });

    $('a.preview').fancybox({
        'transitionIn': 'elastic',
        'transitionOut': 'elastic',
        'speedIn': 600,
        'speedOut': 200,
        'overlayShow': true,
        'type': 'iframe',
        'width': '95%',
        'height': '90%'
    });

    // Recipe Detail

    toggleRecipeTabs('General');

};


function toggleRecipeTabs(tabId) {

    $('#General').hide();
    $('#Search').hide();
    $('#Nutrition').hide();
    $('#Additional').hide();
	
    $('#' + tabId).show();

}

doPopup = function (which) {


};

var HERSHEYS = {};

$(document).ready(function () {
	$('li.tab').click(function(){
		$('.tab').removeClass('active');
		$(this).addClass('active');
	});
	$('#General').parent().parent().parent().parent().css({"background-color":"#e3e3e3","border":"1px solid black","margin-right":"10px"})
});