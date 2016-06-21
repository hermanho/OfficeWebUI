


// The Ribbon Namespace
OfficeWebUI.Ribbon = {

    // Get DOM object properties
    _gO: function(obj, coord) {
        obj = document.getElementById(obj);
        var val = obj["offset" + coord];
        while ((obj = obj.offsetParent) != null) {
            val += obj["offset" + coord];
        }
        return val;
    },

    // Save last tab state
    SaveLastTab: function(tabId) {
        if (typeof OfficeWebUI_Ribbon_LastTab != "undefined")
            document.getElementById(OfficeWebUI_Ribbon_LastTab).value = tabId;
    },

    // Save last Backstage Page state
    SaveLastBackstagePage: function(pageId) {
        if (typeof OfficeWebUI_Ribbon_LastBackstagePage != "undefined")
            document.getElementById(OfficeWebUI_Ribbon_LastBackstagePage).value = pageId;
    },

    // Get last Backstage page
    GetLastBackstagePage: function() {
        if (typeof OfficeWebUI_Ribbon_LastBackstagePage != "undefined")
            return document.getElementById(OfficeWebUI_Ribbon_LastBackstagePage).value;
    },

    // Get last tab
    GetLastTab: function() {
        if (typeof OfficeWebUI_Ribbon_LastTab != "undefined")
            return document.getElementById(OfficeWebUI_Ribbon_LastTab).value;
    },

    // Get associated Context for current Tab
    GetCurrentTabContext: function(TabID) {
        return $(".RibbonTab[TabID=" + OfficeWebUI.Ribbon.GetLastTab() + "]").parent().attr("ContextID");
    },

    // Hide all Tabs
    HideAllTabs: function() {
        $(".RibbonTab").removeClass("RibbonTabActive");
        $(".RibbonTabContent").css("display", "none");
    },

    // Init the Ribbon
    Init: function() {

        if (OfficeWebUI.Ribbon.GetLastTab() != "") {
            //alert(OfficeWebUI.Ribbon.GetLastTab());
            var tempid = $(".RibbonTab[TabID=" + OfficeWebUI.Ribbon.GetLastTab() + "]").attr("AssociatedTab");
            $(".RibbonTab[TabID=" + OfficeWebUI.Ribbon.GetLastTab() + "]").addClass("RibbonTabActive");
            $("div[AssociatedTab=" + tempid + "]").css("display", "block");

            OfficeWebUI.Ribbon.ShowContext(OfficeWebUI.Ribbon.GetCurrentTabContext(OfficeWebUI.Ribbon.GetLastTab()));

        } else {
            var tempid = $(".RibbonTab:first").attr("AssociatedTab");
            $(".RibbonTab:first").addClass("RibbonTabActive");
            $("div[AssociatedTab=" + tempid + "]").css("display", "block");
        }

        //if (OfficeWebUI.Ribbon.GetLastBackstagePage() != "") {
        //alert("last page = " + OfficeWebUI.Ribbon.GetLastBackstagePage());
        //}
    },

    // Show a given context
    ShowContext: function(contextId) {
        $("div[ContextID=" + contextId + "]").css("display", "block");
    },

    // Hide all contexts
    HideAllContexts: function() {
        $("div[ContextID]").css("display", "none");
        OfficeWebUI.Ribbon.HideAllTabs();
        OfficeWebUI.Ribbon.Init();
    },

    HideTab: function(tabId) {
        $("div[tabId=" + tabId + "]").hide();
    },

    // Hide a given context
    HideContext: function(contextId) {
        OfficeWebUI.Ribbon.HideAllTabs();
        OfficeWebUI.Ribbon.Init();
        $("div[ContextID=" + contextId + "]").css("display", "none");
    },

    // Show a given Tab
    ShowTab: function(tabId) {
        OfficeWebUI.Ribbon.HideBackstage();
        OfficeWebUI.Ribbon.HideAllTabs();
        $("div[TabID=" + tabId + "]").addClass("RibbonTabActive");
        $("div[TabContentID=" + tabId + "]").css("display", "block");

        OfficeWebUI.Ribbon.SaveLastTab(tabId);
    },

    ShowFirstTab: function() {
        OfficeWebUI.Ribbon.HideBackstage();
        OfficeWebUI.Ribbon.HideAllTabs();
        
        var tempid = $(".RibbonTab:first").attr("AssociatedTab");
        $(".RibbonTab:first").addClass("RibbonTabActive");
        $("div[AssociatedTab=" + tempid + "]").css("display", "block");
    },

    // Toggle Off the Ribbon
    ToggleOffRibbon: function() {
        OfficeWebUI.Ribbon.HideAllTabs();
        $(".RibbonToggleButtonUp").attr("class", "RibbonToggleButtonDown");
        $(".RibbonTabsContainer").css("display", "none");
    },

    // Toggle On the Ribbon
    ToggleOnRibbon: function(sForce) {
        $(".RibbonToggleButtonDown").attr("class", "RibbonToggleButtonUp");
        $(".RibbonTabsContainer").css("display", "block");
        if (sForce)
            OfficeWebUI.Ribbon.Init();
    },

    // Auto toggle the Ribbon
    ToggleRibbon: function() {
        if ($(".RibbonTabsContainer").css("display") == "block") {
            OfficeWebUI.Ribbon.ToggleOffRibbon();
        } else {
            OfficeWebUI.Ribbon.ToggleOnRibbon(true);
        }

        $('.RibbonTabsContainer').trigger('Toggled');
    },

    // Hide the Backstage
    HideBackstage: function() {
        $('.RibbonBackstageContainer').css("display", "none");
    },

    // Toggle Backstage 
    ToggleBackstage: function() {
        if ($('.RibbonBackstageContainer').css("display") == "none") {
            OfficeWebUI.Ribbon.HideAllTabs();
            var offset = $('.RibbonContextsContainer').offset();
            $('.RibbonBackstageContainer').height($(window).height() - 52 - offset.top);
            $('.RibbonBackstageContainer').width($(".RibbonContextsContainer").width() - 2);
            $('.RibbonBackstageContainer').css("display", "block");
            OfficeWebUI.Ribbon.InitBackstage();
        } else {
            $('.RibbonBackstageContainer').css("display", "none");
            OfficeWebUI.Ribbon.Init();
        }
    },

    HideAllBackstagePages: function() {
        $(".BackstagePageTitle").each(function(index) {
            $(this).removeClass("BackstagePageActive");
            $(this).css("background-color", "transparent");
            $(this).css("border-bottom", "1px solid transparent");
            $(this).css("border-top", "1px solid transparent");
        });

        //$(".BackstagePageTitle").removeClass("BackstagePageActive");
        //$(".BackstagePageTitle").css("background-color", "transparent");
        //$(".BackstagePageTitle").css("border-bottom", "1px solid transparent");
        //$(".BackstagePageTitle").css("border-top", "1px solid transparent");
        $(".BackstagePageContent").css("display", "none");
    },

    // Init Backstage view
    InitBackstage: function() {
        $(".BackstagePageTitle:first").click();
        /*
        var tempid = $(".BackstagePageTitle:first").attr("AssociatedPage");
        $(".BackstagePageTitle:first").addClass("BackstagePageActive");
        $("div[AssociatedPage=" + tempid + "]").css("display", "block");
        */
    },

    // Close all menus (Items with menu)
    CloseAllMenus: function() {
        $(".RibbonItemWithMenu").removeClass("RibbonMenuActive");
        $(".RibbonDropDownMenu").hide();
    },

    // Close all galleries
    CloseAllGalleries: function() {
        $(".RibbonGalleryActive").removeClass("RibbonGalleryActive");
        $(".RibbonGallerySubMenu").hide();
    },

    // Hide Application Menu
    HideMenu: function() {
        $('.RibbonApplicationMenuContainer').css("display", "none");
    },

    // Toggle Application Menu
    ToggleMenu: function() {
        $('.RibbonApplicationMenuContainer').toggle();
        /*
        if ($('.RibbonApplicationMenuContainer').css("display") == "none") {
        $('.RibbonApplicationMenuContainer').css("display", "block");
        } else {
        $('.RibbonApplicationMenuContainer').css("display", "none");
        }
        */
    },

    // Check an item
    CheckItem: function(itemId) {
        $("div[ItemID=" + itemId + "]").addClass("Checked");
    },

    // Uncheck an item
    UnCheckItem: function(itemId) {
        $("div[ItemID=" + itemId + "]").removeClass("Checked");
    },

    // Toggle checked state
    ToggleCheck: function(itemId) {
        $("div[ItemID=" + itemId + "]").toggleClass("Checked");
    },


    // Show a Gallery
    OpenGallery: function(src, trg) {
        var src1 = document.getElementById(src);
        document.getElementById(trg).style.display = (document.getElementById(trg).style.display == "none") ? "block" : "none";
        document.getElementById(trg).style.left = OfficeWebUI.Ribbon._gO(src, "Left") + "px";
        document.getElementById(trg).style.top = (OfficeWebUI.Ribbon._gO(src, "Top") + src1.offsetHeight - 1) + "px";
    },

    // Show a menu
    ShowMenu: function(src, trg) {
        document.getElementById(trg).style.display = (document.getElementById(trg).style.display == "none") ? "block" : "none";
        document.getElementById(trg).style.left = OfficeWebUI.Ribbon._gO(src.id, "Left") + "px";
        document.getElementById(trg).style.top = (OfficeWebUI.Ribbon._gO(src.id, "Top") + src.offsetHeight) + "px";
    },

    /*
    // Keep the menu opened
    KeepMenu: function(trg) {
    document.getElementById(trg).style.display = "";
    document.getElementById(trg).getAttribute("MenuOpener").className = "RibbonSmallItem_Over";
    },

    // Hide a menu
    HideMenu: function(trg) {
    document.getElementById(trg).style.display = "none";
    },

    // Register the "File Menu"
    RegisterFileMenu: function(text, container, menu, color) {
    try {
    if (color == null) {
    color = "green";
    }
    var divTag = document.createElement("div");
    divTag.id = "SBRibbon_FileMenu";
    divTag.className = "Ribbon_FileMenu_" + color; // Change this classname to customize the File Menu, See CSS file
    divTag.innerHTML = text;
    divTag.setAttribute("onclick", "SBRibbon.ShowMenu(this, '" + menu + "_FileMenu')");
    document.body.appendChild(divTag);

            var temp_bar = document.getElementById(container + "_header");
    temp_bar.insertBefore(divTag, temp_bar.firstChild);

            document.getElementById("SBRibbon_FileMenu").appendChild(document.getElementById(menu + "_FileMenu"));

        } catch (e) { alert(e); }
    },
    */
    About: function() {
        OfficeWebUI.About();
    }

};



// Start when document is ready
$(document).ready(function () {

    // Click on ribbon tabs
    $(".RibbonTab").click(function () {
        OfficeWebUI.Ribbon.HideBackstage();
        OfficeWebUI.Ribbon.HideAllTabs();
        OfficeWebUI.Ribbon.ToggleOnRibbon(false);
        var tempid = $(this).attr("AssociatedTab");
        var tabid = $(this).attr("TabID");
        $("div[AssociatedTab=" + tempid + "]").css("display", "block");
        $(this).addClass("RibbonTabActive");
        OfficeWebUI.Ribbon.SaveLastTab(tabid);
    });

    // Click on Backstage Pages title
    $(".BackstagePageTitle").click(function () {
        OfficeWebUI.Ribbon.HideAllBackstagePages();
        var tempid = $(this).attr("AssociatedPage");
        $("div[AssociatedPage=" + tempid + "]").css("display", "block");
        $(this).addClass("BackstagePageActive");

        $(this).css("background-color", OfficeWebUI_Ribbon_AppMenuColor);
        $(this).css("border-top", "1px solid " + OfficeWebUI_Ribbon_AppMenuColor);
        $(this).css("border-bottom", "1px solid " + OfficeWebUI_Ribbon_AppMenuColor);
        //OfficeWebUI.Ribbon.SaveLastBackstagePage(tempid);
    });

    // Click on Items with menu
    $(".RibbonItemWithMenu").click(function () {
        OfficeWebUI.Ribbon.CloseAllMenus();
        $(this).addClass("RibbonMenuActive");
        $(this).find(".RibbonDropDownMenu").show();
    });

    // Click on Items with Gallery
    $(".RibbonGalleryDropDownArrow").click(function () {
        OfficeWebUI.Ribbon.CloseAllGalleries();
        $(this).addClass("RibbonGalleryActive");
        $(this).parent().find(".RibbonGallerySubMenu").show();
        $(this).parent().find(".RibbonGallerySubMenu").css("margin-top", "65px");
    });

    // Click on Items with Gallery
    $(".RibbonItemWithTooltip").hover(function () {
        var tooltiptext = $(this).attr("tooltip");
        var offset = $(this).offset();
        $(".RibbonTooltipContainer").show();
        $(".RibbonTooltipContainer").css("left", offset.left);
        $(".RibbonTooltipContainer").html(tooltiptext);
    }, function () {
        $(".RibbonTooltipContainer").hide();
    });

    // Click anywhere on the page
    $(document).bind('click', function (e) {
        var $clicked = $(e.target);

        if ((!$clicked.parent().hasClass("RibbonApplicationMenuContainer"))
           && (!$clicked.hasClass("ApplicationButton"))
           ) {
            $(".RibbonApplicationMenuContainer").hide();
        }

        if (!$clicked.parents().hasClass("RibbonItemWithMenu")) {
            OfficeWebUI.Ribbon.CloseAllMenus();
        }

        if (!$clicked.parents().hasClass("RibbonGalleryDropDownArrow")) {
            OfficeWebUI.Ribbon.CloseAllGalleries();
        }

    });

    // Mouse over Backstage Pages title    
    $(".BackstagePageTitle").hover(
      function () {
          if ($(this).hasClass("BackstagePageActive")) {

          } else {
              $(this).css("background-color", OfficeWebUI_Ribbon_AppMenuColor);
              $(this).css("border-top", "1px solid " + OfficeWebUI_Ribbon_AppMenuColor);
              $(this).css("border-bottom", "1px solid " + OfficeWebUI_Ribbon_AppMenuColor);
          }
      },
      function () {
          if ($(this).hasClass("BackstagePageActive")) {
          } else {
              $(this).css("border-top", "1px solid transparent");
              $(this).css("border-bottom", "1px solid transparent");
              $(this).css("background-color", "transparent");
          }
      }
    );


    // Init Ribbon
    OfficeWebUI.Ribbon.HideAllTabs();
    OfficeWebUI.Ribbon.Init();
});
