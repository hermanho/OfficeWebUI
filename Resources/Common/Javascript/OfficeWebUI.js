
OfficeWebUI = {

    // Get DOM object properties
    _gO: function(obj, coord) {
        obj = document.getElementById(obj);
        var val = obj["offset" + coord];
        while ((obj = obj.offsetParent) != null) {
            val += obj["offset" + coord];
        }
        return val;
    },
    
    About: function() {
        alert("OfficeWebUI, Visual components for ASP.NET.\n http://officewebui.codeplex.com");
    }


};
