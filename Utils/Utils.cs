using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace OfficeWebUI
{
    class Utils
    {
        public static void AddStylesheetResource(Control aControl, string aStyleSheetRessourceName)
        {
            string includeTemplate = "<link rel='stylesheet' text='text/css' href='{0}' />";

            string includeLocation = aControl.Page.ClientScript.GetWebResourceUrl(aControl.GetType(), aStyleSheetRessourceName);
            string lInclude = String.Format(includeTemplate, includeLocation);
            

            if ((aControl != null) && (aControl.Page != null) && (aControl.Page.Header != null))
                if (!isInclude(aControl.Page.Header, lInclude))
                {
                    LiteralControl include = new LiteralControl(String.Format(includeTemplate, includeLocation));
                    ((System.Web.UI.HtmlControls.HtmlHead)aControl.Page.Header).Controls.Add(include);
                }
        }

        public static void AddStylesheetFile(Control aControl, string aStyleSheetName)
        {
            string includeTemplate = "<link rel='stylesheet' text='text/css' href='{0}' />";
            string lInclude = String.Format(includeTemplate, aStyleSheetName);

            if ((aControl != null) && (aControl.Page != null) && (aControl.Page.Header != null))
                if (!isInclude(aControl.Page.Header, lInclude))
                {
                    LiteralControl include = new LiteralControl(String.Format(includeTemplate, aStyleSheetName));
                    ((System.Web.UI.HtmlControls.HtmlHead)aControl.Page.Header).Controls.Add(include);
                }
        }

        internal static bool isInclude(Control aControl, String aRessouce)
        {
            if (aControl != null)
                foreach (Control lControl in aControl.Controls)
                {
                    if (lControl as LiteralControl != null)
                    {
                        LiteralControl lTempControl = (LiteralControl)lControl;
                        if (aRessouce == lTempControl.Text)
                            return true;
                    }
                }
            return false;
        }
    }
}
