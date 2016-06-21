
$(document).ready(function() {

try { Sys.Application.add_load(OfficeWebUI.MessageBox.InitPopup); } catch (e) { }
    
});




OfficeWebUI.MessageBox = {

    InitPopup: function() {

        var windowWidth = document.body.offsetWidth; //document.documentElement.clientWidth;
        var windowHeight = document.documentElement.clientHeight;

        var _height = $(".OfficeWebUI_Popup").height();
        var _width = $(".OfficeWebUI_Popup").width();

        //alert(_height);
        //alert(windowWidth);

        $(".OfficeWebUI_Popup").css({
            "top": windowHeight / 2 - _height / 2 - 40,
            "left": windowWidth / 2 - _width / 2
        });


    },

    Open: function(title, src, width, height) {

        var windowWidth = document.body.offsetWidth; //document.documentElement.clientWidth;
        var windowHeight = document.documentElement.clientHeight;

        $(".OfficeWebUI_PopupShadow").show();

        $(".PopupTitleSpan").html(title);
        $(".OfficeWebUI_PopupFrame").attr("src", src);

        $(".OfficeWebUI_Popup").css({
            "position": "absolute",
            "top": windowHeight / 2 - height / 2 - 40,
            "left": windowWidth / 2 - width / 2
        });

        $(".OfficeWebUI_PopupTitle").css({
            "width": width
        });

        $(".OfficeWebUI_PopupContentContainer").css({
            "height": height,
            "width": width
        });

        $(".OfficeWebUI_Popup").show();

    },

    Close: function() {
        $(".OfficeWebUI_PopupFrame").attr("src", "about:blank");
        $(".OfficeWebUI_PopupShadow").hide();
        $(".OfficeWebUI_Popup").hide();
    },


    About: function() {
        OfficeWebUI.About();
    }

};
