using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.Design.WebControls;
using System.Web.UI.Design;
using System.ComponentModel.Design;
using System.Web.UI;
using System.ComponentModel;

namespace OfficeWebUI.Popup
{
    internal class OfficePopup_Designer : CompositeControlDesigner
    {
        private DesignerActionListCollection _actionLists = null;
        private OfficePopup _Control;

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            _Control = (Component as OfficePopup);
        }

        public override string GetDesignTimeHtml(DesignerRegionCollection regions)
        {
            BuildRegions(regions);
            return BuildDesignTimeHtml();
            //return "<div style='height:200px; padding:5px; border:1px solid #C0C0C0; font-family:Verdana; font-size:8pt;'><b>OfficeWebUI:Workspace</b> [" + _Control.ID + "]</div>";
        }

        public override bool AllowResize
        {
            get { return true; }
        }

        protected virtual void BuildRegions(DesignerRegionCollection regions)
        {
            EditableDesignerRegion edr0 = new EditableDesignerRegion(this, "Content", false);
            regions.Add(edr0);

            regions[0].Highlight = true;
        }

        protected virtual String BuildDesignTimeHtml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(BuildBeginDesignTimeHtml());
            sb.Append(BuildEndDesignTimeHtml());

            return sb.ToString();
        }

        protected virtual String BuildBeginDesignTimeHtml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div style='height:" + _Control.Height + "; width:" + _Control.Width + "; border:1px solid #C0C0C0; font-family:Verdana; font-size:8pt;'>");
            sb.Append("<div style=\"padding:5px; background:#C0C0C0\">Office Web.UI:Popup [" + _Control.Title + "]</div>");
            sb.Append("<div style=\"padding:5px;\" " + DesignerRegion.DesignerRegionAttributeName + "='0'>");

            return sb.ToString();
        }

        protected virtual String BuildEndDesignTimeHtml()
        {
            return ("</div></div>");
        }

        public override string GetEditableDesignerRegionContent(EditableDesignerRegion region)
        {
            IDesignerHost host = (IDesignerHost)Component.Site.GetService(typeof(IDesignerHost));

            if (host != null)
            {
                ITemplate template = _Control.Content;

                if (template != null)
                    return ControlPersister.PersistTemplate(template, host);
            }

            return "oops...";
        }

        public override void SetEditableDesignerRegionContent(EditableDesignerRegion region, string content)
        {
            IDesignerHost host = (IDesignerHost)Component.Site.GetService(typeof(IDesignerHost));

            if (host != null)
            {
                ITemplate template = ControlParser.ParseTemplate(host, content);

                if (template != null)
                {
                    _Control.Content = template;
                }
            }
        }

        /**/

    }
}
