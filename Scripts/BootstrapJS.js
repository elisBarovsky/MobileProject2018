$(document).ready(function () {
    $(".navbar-header").hide();
});

UserInfo = new Object();

$(document).on('vclick', '#LoginSubmit', function () { // on the pageinit of Product page
    UserInfo.ID = document.getElementById("IDTB").value;
    UserInfo.PS = document.getElementById("pwdTB").value;
    localStorage.setItem("UserID", UserInfo.ID); //saving in localS
    localStorage.setItem("UserPS", UserInfo.PS); //saving in localS
    alert(UserInfo.ID + " " + UserInfo.PS);     ///////*********************** לא לשכוח למחוק בסוף
    Login(UserInfo, renderlogin); 

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


function renderlogin(results) {
    //this is the callBackFunc 
    resutls = $.parseJSON(results.d);

    if (resutls = "openSeqQestion") {
        FillSecurityQ(renderFillSecurityQ);
        $.mobile.changePage("#SequrityQ", { transition: "slide", changeHash: false }); // מעביר עמוד 
    }
    else {
        $.mobile.changePage("#DashbordPage", { transition: "slide", changeHash: false }); // מעביר עמוד 
        $(".navbar-header").show();
    }
    //$('#DynamicList').empty();
    //$.each(resutls, function (i, row) {
    //    dynamicLi = "<li> <a href='#' id=" + row.Id + "><h3>" + row.Name + "</h3><span class='ui-li-count' style='float: right;'>" + row.ProductAmount + "</span></li>";
    //    $('#DynamicList').append(dynamicLi);
    //    $('#DynamicList').listview('refresh');
    //});
}


function renderFillSecurityQ(results) {
    //this is the callBackFunc 
    resutls[] = $.parseJSON(results.d);
    dynamicLy = "<select>";
    $.each(results, function (i, row) {
        dynamicLy += " <option value='" + row +"'>"+row+"</option> ";
    }); 
    dynamicLy += "</select>";

 
    //$('#DynamicList').empty();
    //$.each(resutls, function (i, row) {
    //    dynamicLi = "<li> <a href='#' id=" + row.Id + "><h3>" + row.Name + "</h3><span class='ui-li-count' style='float: right;'>" + row.ProductAmount + "</span></li>";
    //    $('#DynamicList').append(dynamicLi);
    //    $('#DynamicList').listview('refresh');
    //});
}