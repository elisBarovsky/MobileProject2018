﻿$(document).ready(function () {
  


$('#slide-nav.navbar-inverse').after($('<div class="inverse" id="navbar-height-col"></div>'));
$('#slide-nav.navbar-default').after($('<div id="navbar-height-col"></div>'));

var slidewidth = '20%';
var navbarneg = '-' + slidewidth;

if ($(window).width() < 767) {
    $('#navbar-height-col').css("width", slidewidth);
    $('#navbar-height-col').css("right", navbarneg);
    $('#slide-nav #slidemenu').css("width", slidewidth);
    $('#slide-nav #slidemenu').css("right", navbarneg);
}

$("#slide-nav").on("click", '.navbar-toggle', function (e) {

    // slider is active
    var selected = $(this).hasClass('slide-active');

    // set slidemenu width
    $('#slidemenu').stop().animate({
        right: selected ? navbarneg : '0px'
    });

    // set navbar width
    $('#navbar-height-col').stop().animate({
        right: selected ? navbarneg : '0px'
    });

    // set content let
    $('#page-content').stop().animate({
        right: selected ? '0px' : slidewidth
    });

    // set navbar left
    $('.navbar-header').stop().animate({
        right: selected ? '0px' : slidewidth
    });

    $(this).toggleClass('slide-active', !selected);
    $('#slidemenu').toggleClass('slide-active');

    $('#page-content, .navbar, body, .navbar-header').toggleClass('slide-active');
});

var selected = '#slidemenu, #page-content, body, .navbar, .navbar-header';

$(window).on("resize", function () {
    if ($(window).width() > 767 && $('.navbar-toggle').is(':hidden')) {
        $(selected).removeClass('slide-active');
    }
});
});

    $(document).on('vclick', '#CHBottom', function () {
  //  alert("כפתור נלחץ");
    $.mobile.changePage("#DashbordPage", { transition: "slide", changeHash: false }); // מעביר עמוד 
}); 

$(document).on('vclick', '#CHBottom1', function () {
    //  alert("כפתור נלחץ");
    $.mobile.changePage("#DiklaPage", { transition: "slide", changeHash: false }); // מעביר עמוד 
});