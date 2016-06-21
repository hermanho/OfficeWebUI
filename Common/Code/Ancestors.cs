using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;

namespace OfficeWebUI.Common
{
    public class ControlAncestor : Control
    {
        //[Browsable(false)]
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //public override bool Enabled
        //{
        //    get
        //    {
        //        return base.Enabled;
        //    }
        //    set
        //    {
        //        base.Enabled = value;
        //    }
        //}

        //public override void RenderBeginTag(System.Web.UI.HtmlTextWriter writer)
        //{
        //    //base.RenderBeginTag(writer);
        //}

        //public override void RenderEndTag(System.Web.UI.HtmlTextWriter writer)
        //{
        //    //base.RenderEndTag(writer);
        //}

    }

    public interface IOfficeWebUIWebControl
    {
    
    }

    public class ControlContainer : INamingContainer
    {
    
    }
}
