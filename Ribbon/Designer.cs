using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.Design.WebControls;
using System.ComponentModel.Design;
using System.Web.UI.Design;
using System.ComponentModel;
using System.Web.UI;

namespace OfficeWebUI.Ribbon
{

    internal class OfficeRibbon_Designer : CompositeControlDesigner
    {
        private DesignerActionListCollection _actionLists = null;
        private OfficeRibbon _Control;
        private List<RibbonTab> _ListTabs = new List<RibbonTab>();

        public Int32 CurrentTab = 0;
        public override bool AllowResize { get { return false; } }
        
        


        public string GetDesignTimeHtml0(DesignerRegionCollection regions)
        {
            return "<div style='height:60px; padding:5px; border:1px solid #C0C0C0; font-family:Verdana; font-size:8pt;'><b>OfficeWebUI:Ribbon</b> [" + _Control.ID + "]</div>";
        }

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            _Control = (OfficeRibbon)component;

            foreach (RibbonContext lContext in _Control.Contexts)
            {
                foreach (RibbonTab lTab in lContext.Tabs)
                {
                    _ListTabs.Add(lTab);
                }
            }
        }


        protected override void OnClick(DesignerRegionMouseEventArgs e)
        {
            if (e.Region == null) return;

            String lStr = e.Region.Name.Split('_')[1];
            int regionIndex = Int32.Parse(lStr);

            //System.Windows.Forms.MessageBox.Show(e.Region.DisplayName);

            CurrentTab = regionIndex;
            UpdateDesignTimeHtml();

            /*
            if (e.Region.Name == "Header0" && _currentView != 0)
            {
                _currentView = 0;
                UpdateDesignTimeHtml();
            }

            if (e.Region.Name == "Header1" && _currentView != 1)
            {
                _currentView = 1;
                UpdateDesignTimeHtml();
            }

            if (e.Region.Name == "Header2" && _currentView != 2)
            {
                _currentView = 2;
                UpdateDesignTimeHtml();
            }*/
        }

        public override String GetDesignTimeHtml(DesignerRegionCollection regions)
        {
            BuildRegions(regions);
            return BuildDesignTimeHtml();
        }

        protected virtual void BuildRegions(DesignerRegionCollection regions)
        {
            //regions.Add(new DesignerRegion(this, "Header0"));
            //regions.Add(new DesignerRegion(this, "Header1"));

            //int i = 0;
            //foreach (RibbonTab lTab in _ListTabs)
            //{
                
            //    i++;
            //}

            for (int i = 0; i < _ListTabs.Count; i++)
            {
                regions.Add(new DesignerRegion(this, "Tab_" + i.ToString()));
            }

            // If the current view is for all, we need another editable region
            //EditableDesignerRegion edr0 = new EditableDesignerRegion(this, "Content" + _currentView, false);
            //regions.Add(edr0);

            // Set the highlight, depending upon the selected region
            //if (_currentView == 0 || _currentView == 1 || _currentView == 2)
            regions[CurrentTab].Highlight = true;
        }

        protected virtual string BuildDesignTimeHtml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(BuildBeginDesignTimeHtml());
            sb.Append(BuildContentDesignTimeHtml());
            //sb.Append(BuildEndDesignTimeHtml());

            return sb.ToString();
        }

        protected virtual String BuildBeginDesignTimeHtml()
        {
            // Create the table layout
            StringBuilder sb = new StringBuilder();
            //sb.Append("<table ");

            // Styles that we'll use to render for the design-surface
            //sb.Append("height='200' width='100%'>");


            sb.Append("<div style=\"width:100%\">");
            int i = 0;
            foreach (RibbonTab lTab in _ListTabs)
            {
                sb.Append("<div style=\"float:left; height:20px; padding:3px; font-size:8pt; font-family:Verdana;\" " + DesignerRegion.DesignerRegionAttributeName + "='" + i.ToString() + "'>" + _ListTabs[i].Text + "</div>");
                i++;
            }
            sb.Append("<div style=\"clear:both\"></div></div>");

            //// Generate the title or caption bar
            //sb.Append("<tr height='25px' align='center' " +
            //    "style='font-family:tahoma;font-size:10pt;font-weight:bold;'>" +
            //    "<td style='width:50%' " + DesignerRegion.DesignerRegionAttributeName +
            //    "='0'>");
            //sb.Append("Page-View 1</td>");
            //sb.Append("<td style='width:50%' " +
            //    DesignerRegion.DesignerRegionAttributeName + "='1'>");
            
            //sb.Append("</tr>");

            return sb.ToString();
        }

        protected virtual String BuildEndDesignTimeHtml()
        {
            return ("</table>");
        }

        protected virtual String BuildContentDesignTimeHtml()
        {
            StringBuilder sb = new StringBuilder();
            //sb.Append("<tr><td colspan='2' style='");
            //sb.Append("background-color:red;'");


            sb.Append("<div style=\"border:1px solid #C0C0C0\">");
            //sb.Append("okkkk " + CurrentTab.ToString() + "");
            //sb.Append("contenu de " + _ListTabs[CurrentTab].Text);

            foreach (RibbonGroup lGroup in _ListTabs[CurrentTab].Groups)
            {
                sb.Append("<div style=\"float:left;\">");

                    sb.Append("<table>");
                    sb.Append("<tr><td style=\"height:80px; border-right:1px solid #C0C0C0\" valign=top>");

                    foreach (GroupZone lZone in lGroup.Zones)
                    {
                        sb.Append("<div style=\"float:left;\">");

                        foreach (Control lCtr in lZone.Content)
                        {
                            sb.Append("<div style=\"font-size:8pt; font-family:Verdana; padding:2px; background:red; margin:1px;\">CTRL</div>");
                        }

                        sb.Append("</div>");
                    }

                    sb.Append("<div style=\"clear:both\"></div></td></tr>");
                    sb.Append("<tr><td style=\"font-size:8pt; font-family:Verdana; text-align:center\">" + lGroup.Text + "</td></tr>");
                    sb.Append("</table>");

                    sb.Append("<div style=\"clear:both\"></div></div>");
            }

            //sb.Append(DesignerRegion.DesignerRegionAttributeName + "='9'>ok " + _currentView.ToString() + "</td></tr>");
            sb.Append("<div style=\"clear:both\"></div></div>");

            return sb.ToString();
        }

    

        public override string GetEditableDesignerRegionContent
            (EditableDesignerRegion region)
        {
            IDesignerHost host =
                (IDesignerHost)Component.Site.GetService(typeof(IDesignerHost));

            

            if (host != null)
            {
                //ITemplate template = new OfficeRibbon_Designer_Inside();


                //if (region.Name == "Content1")
                //    template = _Control.View2;

                //if (template != null)
                //    return ControlPersister.PersistTemplate(template, host);
            }
            return "oop" + CurrentTab.ToString();
            
        }

        public override void SetEditableDesignerRegionContent
             (EditableDesignerRegion region, string content)
        {
            int regionIndex = Int32.Parse(region.Name.Substring(7));

            if (content == null)
            {
                //if (regionIndex == 0)
                //    _Control.View1 = null;
                //else if (regionIndex == 1)
                //    _Control.View2 = null;
                //return;
            }

            IDesignerHost host =
                (IDesignerHost)Component.Site.GetService(typeof(IDesignerHost));

            if (host != null)
            {
                //ITemplate template = ControlParser.ParseTemplate(host, content);

                //if (template != null)
                //{
                //    if (regionIndex == 0)
                //        myControl.View1 = template;
                //    else if (regionIndex == 1)
                //        myControl.View2 = template;
                //}
            }
        }


        /**/

        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (_actionLists == null)
                {
                    _actionLists = new DesignerActionListCollection();
                    _actionLists.AddRange(base.ActionLists);

                    // Draft for future...
                    _actionLists.Add(new ActionList(this));
                }
                return _actionLists;
            }
        }

        public void AddButton()
        {
            //RootDesigner.AddControlToDocument(new RibbonContext() { Text = "coool" }, _Control, ControlLocation.LastChild);

            try
            {
                RootDesigner.AddControlToDocument(new OfficeButton(), _Control.Contexts[0].Tabs[0].Groups[0].Zones[0], ControlLocation.LastChild);
                
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message.ToString());
            }
            
        }

        public class ActionList : DesignerActionList
        {
            private OfficeRibbon_Designer _parent;
            private DesignerActionItemCollection _items;

            // Constructor
            public ActionList(OfficeRibbon_Designer parent)
                : base(parent.Component)
            {
                _parent = parent;
            }

            // Create the ActionItem collection and add one command
            public override DesignerActionItemCollection GetSortedActionItems()
            {
                if (_items == null)
                {
                    _items = new DesignerActionItemCollection();
                    _items.Add(new DesignerActionHeaderItem("Office Web UI"));
                    _items.Add(new DesignerActionMethodItem(this, "ToggleLargeText", "Test for future...", "Office Web UI"));

                }
                return _items;

            }



            // ActionList command to change the text size
            private void ToggleLargeText()
            {
                // Get a reference to the parent designer's associated control
                OfficeRibbon ctl = (OfficeRibbon)_parent.Component;
                
                //AboutOfficeWebUI AboutForm = new AboutOfficeWebUI();
                //AboutForm.ShowDialog();

                //_parent.AddButton();

                System.Windows.Forms.MessageBox.Show(_parent.CurrentTab.ToString());

                /*
                // Get a reference to the control's LargeText property
                PropertyDescriptor propDesc = TypeDescriptor.GetProperties(ctl)["ApplicationMenuText"];
                              

                // Get the current value of the property
                String v = (String)propDesc.GetValue(ctl);

                // Toggle the property value
                propDesc.SetValue(ctl, "hello world");
                */
            }
        }
    }
}
