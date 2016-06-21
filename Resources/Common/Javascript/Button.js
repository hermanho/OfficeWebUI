
$(document).ready(function() {


    $(".ButtonWithMenu").click(function() {
        OfficeWebUI.Button.CloseAllMenus();
        $(this).addClass("ButtonMenuActive");
        $(this).find(".ButtonDropDownMenu").show();
    });


    // Click anywhere on the page
    $(document).bind('click', function(e) {
        var $clicked = $(e.target);

        if (!$clicked.parents().hasClass("ButtonWithMenu")) {
            OfficeWebUI.Button.CloseAllMenus();
        }

    });
    
    
});



OfficeWebUI.Button = {

    CloseAllMenus: function() {
        $(".ButtonWithMenu").removeClass("ButtonMenuActive");
        $(".ButtonDropDownMenu").hide();
    },
    

    About: function() {
        OfficeWebUI.About();
    }

};
