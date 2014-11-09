$(document).ready(function(){

    $(".expandOnFocus").focus(function() {
        $(this).animate({height: "+=60"});
    });

    $(".expandOnFocus").blur(function() {
        $(this).animate({height: "-=60"});
    });

});