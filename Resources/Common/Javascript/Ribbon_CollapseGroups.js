

$(document).ready(function() {

    $(window).resize(function() {
        rtime = new Date();
        if (timeout === false) {
            timeout = true;
            setTimeout(resizeend, delta);
        }
    });

    $(".RibbonGroupCollapsedContainerTable").click(function() {
        OfficeRibbonResizer_ResetLayout();
        $(this).addClass("RibbonGroupCollapsedContainerTableActive");
        $(this).parent().find(".RibbonGroupCollapsedContainerDropDown").show();
    });


    // Click anywhere on the page
    $(document).bind('click', function(e) {
        var $clicked = $(e.target);

        if ((!$clicked.parents().hasClass("RibbonGroupCollapsedContainerTable")) && (!$clicked.parents().hasClass("RibbonGroupCollapsedContainerDropDown"))) {
            OfficeRibbonResizer_ResetLayout();
        }

    });


    OfficeRibbonResizer();

});


var _TotalContainerWidth = 0;
var _TotalElementsWidth = 0;
var _ActiveTab;


var rtime = new Date(1, 1, 2000, 12, 00, 00);
var timeout = false;
var delta = 0;

function resizeend() {
    if (new Date() - rtime < delta) {
        setTimeout(resizeend, delta);
    } else {
        timeout = false;
        OfficeRibbonResizer();
    }
}


function OfficeRibbonResizer_ResetLayout() {
    $(".RibbonGroupCollapsedContainerDropDown").hide();
    $(".RibbonGroupCollapsedContainerTableActive").removeClass("RibbonGroupCollapsedContainerTableActive");
}


function OfficeRibbonResizer() {

    _TotalContainerWidth = $(".RibbonTabsContainer").width();

    $(".RibbonGroupCollapsedContainer").hide();
    $(".RibbonGroupContent").show();


    $("DIV.RibbonGroupContent").each(function(idx) {

        var _element = $(this);
        var _elementID = $(this).attr("id");

        var _AssociatedElement = $("DIV[AssociatedElement=" + _elementID + "]");
        var _DropDownContainer = _AssociatedElement.find(".RibbonGroupCollapsedContainerDropDown");

        if (_DropDownContainer.find("table").length > 0)
            _DropDownContainer.find("table").first().appendTo(_element);


    });

    
    $(".RibbonTabContent").each(function(idx) {

        if ($(this).css("display") != "none") {
            _ActiveTab = $(this);
        }

    });

    

    _TotalElementsWidth = 0;
    _ActiveTab.find("DIV.RibbonGroupCollapsedContainer, DIV.RibbonGroupContent").each(function(idx) {

        var _element = $(this);
        if (_element.css("display") != "none")
            _TotalElementsWidth = _TotalElementsWidth + _element.width();
    
    });

    //alert("total : " + _TotalContainerWidth);
    //while (_TotalElementsWidth > (_TotalContainerWidth - 100)) {
    while ((_TotalElementsWidth > 0) && (_TotalElementsWidth > (_TotalContainerWidth - 100))) {
        HideElement();
        RacalculateWidth();
        //alert("elements : " + _TotalElementsWidth);
    }
    

}

function RacalculateWidth() {

    _TotalElementsWidth = 0;
    _ActiveTab.find("DIV.RibbonGroupCollapsedContainer, DIV.RibbonGroupContent").each(function(idx) {

        var _element = $(this);
        if (_element.css("display") != "none")
            _TotalElementsWidth = _TotalElementsWidth + _element.width();
    
    });
}


function HideElement() {

    $(_ActiveTab.find(".RibbonGroupContent").get().reverse()).each(function() {

        var _element = $(this);
        if ((_element.attr("objType") == "Element") && (_element.css("display") != "none")) {

            _element.hide();

            var _elementWidth = _element.width();
            var _AssociatedElement = $("DIV[AssociatedElement=" + _element.attr("id") + "]");
            var _DropDownContainer = _AssociatedElement.find(".RibbonGroupCollapsedContainerDropDown");

            if (_element.find("table").length > 0)
                _element.find("table").first().appendTo(_DropDownContainer);

            _AssociatedElement.show();
            _AssociatedElement.find(".RibbonGroupCollapsedContainerDropDown").css("width", _elementWidth);
            _DropDownContainer.hide();
            return false;
        }

    });

}


