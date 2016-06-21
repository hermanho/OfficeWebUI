

$(document).ready(function() {

    try { Sys.Application.add_load(OfficeWebUI.Combobox.Init); } catch (e) { }

    OfficeWebUI.Combobox.Init();

});

OfficeWebUI.Combobox = {


    Init: function() {


        $(".OfficeWebUI_Combobox_Item").click(function() {
            var _text = $(this).attr("InternalText");
            var _value = $(this).attr("InternalValue");

            var _objRoot = $(this).parents(".OfficeWebUI_Combobox")
            var _objSel = _objRoot.find(".OfficeWebUI_Combobox_Selected");
            _objSel.html(_text);
            _objSel.attr("value", _value);



            var _selTextID = _objRoot.attr("Internal_SelectedTextID");
            var _selValueID = _objRoot.attr("Internal_SelectedValueID");


            document.getElementById(_selTextID).value = _text;
            document.getElementById(_selValueID).value = _value;


            $(this).parent().hide();
            $(".OfficeWebUI_ComboboxActive").removeClass("OfficeWebUI_ComboboxActive");
        });


        $(".OfficeWebUI_Combobox_Selected").click(function() {
            $(this).parent().find(".OfficeWebUI_Combobox_DropDown").toggle();
            $(this).parent().toggleClass("OfficeWebUI_ComboboxActive");
        });


        // Click anywhere on the page
        $(document).bind('click', function(e) {
            var $clicked = $(e.target);

            if (!$clicked.parents().hasClass("OfficeWebUI_Combobox")) {
                $(".OfficeWebUI_Combobox_DropDown").hide();
                $(".OfficeWebUI_ComboboxActive").removeClass("OfficeWebUI_ComboboxActive");
            }

        });


    },


    

    About: function() {
        OfficeWebUI.About();
    }

};
