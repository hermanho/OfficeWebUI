using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Web.UI.Design.WebControls;

namespace OfficeWebUI.MessageBox
{
    internal class MessageBox_Designer : CompositeControlDesigner
    {
        public override string GetDesignTimeHtml()
        {
            String lReturn = String.Empty;
            OfficeMessageBox lControl = (Component as OfficeMessageBox);

            String lStringButtons = lControl.ButtonsType.ToString();

            return "<div style='width:250px; height:100px; display:inline-block; padding:5px; border:1px solid #777; font-family:Verdana; font-size:8pt; color:#444;'><b>MessageBox</b><br>" + lControl.Text + "<br/>" + lStringButtons + "</div>";
        }

        // Do not allow direct resizing of the control
        public override bool AllowResize
        {
            get { return false; }
        }

    }
}
