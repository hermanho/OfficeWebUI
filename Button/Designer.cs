using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.Design.WebControls;

namespace OfficeWebUI.Button
{
    internal class Button_Designer : CompositeControlDesigner
    {
        public override string GetDesignTimeHtml()
        {
            String lReturn = String.Empty;
            OfficeButton lControl = (Component as OfficeButton);


            return "<div style='width:80px; height:90px; display:inline-block; padding:5px; border:1px solid #777; font-family:Verdana; font-size:8pt; color:#444;'><b>Button</b><br>" + lControl.Text + "</div>";
        }

        // Do not allow direct resizing of the control
        public override bool AllowResize
        {
            get { return false; }
        }

    }
}
