using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeWebUI.Common;
using System.Web.UI.HtmlControls;

namespace OfficeWebUI.Ribbon
{
    [ToolboxData("<{0}:SeparatorItem runat=server></{0}:SeparatorItem>")]
    public class SeparatorItem : WebControl, IOfficeWebUIWebControl
    {
        protected override void OnInit(EventArgs e)
        {
            this.Controls.Add(new HtmlGenericControl("hr"));

            base.OnInit(e);
        }
    }
}
