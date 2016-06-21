using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.Design.WebControls;
using System.ComponentModel.Design;
using OfficeWebUI.Ribbon;

namespace OfficeWebUI.ListView
{
    internal class ListView_Designer : CompositeControlDesigner
    {
        private DesignerActionListCollection _actionLists = null;

        /*
        
         * hummm... What you read here is... DRAFT ! 
         * The designer is not ready at all !
         
        */

        public override string GetDesignTimeHtml()
        {
            String lReturn = String.Empty;
            OfficeRibbon lControl = (Component as OfficeRibbon);

            return "<div style='height:60px; padding:5px; border:1px solid #C0C0C0; font-family:Verdana; font-size:8pt;'><b>Ribbon</b> [" + lControl.ID + "]</div>";
        }

        // Do not allow direct resizing of the control
        public override bool AllowResize
        {
            get { return false; }
        }

        // Return a custom ActionList collection
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (_actionLists == null)
                {
                    _actionLists = new DesignerActionListCollection();
                    _actionLists.AddRange(base.ActionLists);

                    // Draft for future...
                    //_actionLists.Add(new ActionList(this));
                }
                return _actionLists;
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

                AboutOfficeWebUI AboutForm = new AboutOfficeWebUI();
                AboutForm.ShowDialog();


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
