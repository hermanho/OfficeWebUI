
$(document).ready(function() {

    OfficeWebUI.Workspace.ResizeLayout();

    $(".OfficeWebUI_Workspace").resize(function() {
        OfficeWebUI.Workspace.ResizeLayout();
    });

    $(window).resize(function() {
        OfficeWebUI.Workspace.ResizeLayout();
    });

    $(".OfficeWebUI_WorkspaceItem").click(function() {
        OfficeWebUI.Workspace.ClearAllItems();
        var tempid = $(this).attr("id");
        $(this).addClass("OfficeWebUI_WorkspaceItemActive");
        // save last item is in code-behind
    });

    OfficeWebUI.Workspace.RegisterAreaButtonClick();
    OfficeWebUI.Workspace.Init();
});



OfficeWebUI.Workspace = {

    _AjaxLoadSupport: function() {
        OfficeWebUI.Workspace.Init();
        OfficeWebUI.Workspace.ResizeLayout();
        OfficeWebUI.Workspace.RegisterAreaButtonClick();

        //$(".RibbonTabsContainer").bind("Toggled", function() { OfficeWebUI.Workspace.ResizeLayout(); });
    },

    RegisterAreaButtonClick: function() {

        $(".OfficeWebUI_WorkspaceAreaButton").click(function() {
            OfficeWebUI.Workspace.HideAllAreas();
            var tempid = $(this).attr("AssociatedArea");
            var buttonid = $(this).attr("AreaId");
            $("div[AssociatedArea=" + tempid + "]").show();
            $(this).addClass("WorkspaceAreaActive");
            OfficeWebUI.Workspace.SaveLastArea(buttonid);
        });

    },


    HideAllAreas: function() {
        $(".OfficeWebUI_WorkspaceAreaZone").hide();
        $(".WorkspaceAreaActive").removeClass("WorkspaceAreaActive");
    },

    ClearAllItems: function() {

        $(".OfficeWebUI_WorkspaceItemActive").removeClass("OfficeWebUI_WorkspaceItemActive");

    },

    SaveLastItem: function(itemId) {
        if (typeof OfficeWebUI_Workspace_LastItem != "undefined") {
            document.getElementById(OfficeWebUI_Workspace_LastItem).value = itemId;
            //alert(itemId);
        }
    },

    SaveLastArea: function(areaId) {
        if (typeof OfficeWebUI_Workspace_LastArea != "undefined")
            document.getElementById(OfficeWebUI_Workspace_LastArea).value = areaId;
    },

    GetLastItem: function() {
        if (typeof OfficeWebUI_Workspace_LastItem != "undefined")
            return document.getElementById(OfficeWebUI_Workspace_LastItem).value;
    },

    GetLastArea: function() {
        if (typeof OfficeWebUI_Workspace_LastArea != "undefined")
            return document.getElementById(OfficeWebUI_Workspace_LastArea).value;
    },

    ShowArea: function(id) {
        OfficeWebUI.Workspace.HideAllAreas();
        //var areabutton = $("#" + id);
        //var tempid = areabutton.attr("AssociatedArea");
        //var buttonid = areabutton.attr("id");
        $("div[AssociatedArea=" + id + "]").show();
        $("div[AreaId=" + id + "]").addClass("WorkspaceAreaActive");
    },

    Init: function() {
        OfficeWebUI.Workspace.HideAllAreas();
        var tempid = $(".OfficeWebUI_WorkspaceAreaButton:first").attr("AssociatedArea");
        $(".OfficeWebUI_WorkspaceAreaButton:first").addClass("WorkspaceAreaActive");
        $("div[AssociatedArea=" + tempid + "]").show();

        if (OfficeWebUI.Workspace.GetLastItem() != "")
            $("div[WorkspaceInternalID=" + OfficeWebUI.Workspace.GetLastItem() + "]").addClass("OfficeWebUI_WorkspaceItemActive");

        if (OfficeWebUI.Workspace.GetLastArea() != "") {
            OfficeWebUI.Workspace.ShowArea(OfficeWebUI.Workspace.GetLastArea());
        }

        $(".RibbonTabsContainer").bind("Toggled", function() { OfficeWebUI.Workspace.ResizeLayout(); });
    },

    getWindowHeight: function() {
        var windowHeight = 0;
        if (typeof (window.innerHeight) == 'number') {
            windowHeight = window.innerHeight;
        }
        else {
            if (document.documentElement && document.documentElement.clientHeight) {
                windowHeight = document.documentElement.clientHeight;
            }
            else {
                if (document.body && document.body.clientHeight) {
                    windowHeight = document.body.clientHeight;
                }
            }
        }
        return windowHeight;
    },

    ResizeLayout: function() {

        var windowHeight = OfficeWebUI.Workspace.getWindowHeight();

        var _WorkspaceOffset = $(".OfficeWebUI_Workspace").offset();
        var _WorkspaceTop = _WorkspaceOffset.top;
        var _StatusBarHeight = $(".OfficeWebUI_WorkspaceStatusBar").height();

        var _leftPanelHeight = (windowHeight - _WorkspaceTop - 18 - _StatusBarHeight);

        $(".OfficeWebUI_WorkspaceLeftPanel").css("height", _leftPanelHeight);
        $(".OfficeWebUI_WorkspaceContentPanel").css("height", _leftPanelHeight);

        $(".OfficeWebUI_Workspace").css("height", _leftPanelHeight);
        var _AreasOffset = $(".OfficeWebUI_WorkspaceAreaContainer").height();

        $(".OfficeWebUI_WorkspaceNavContainer").css("height", (_leftPanelHeight - _AreasOffset + 5) + "px");


    },

    About: function() {
        OfficeWebUI.About();
    }

};
