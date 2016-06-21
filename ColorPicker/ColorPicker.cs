using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeWebUI.Common;
using OfficeWebUI.Button;
using System.Web.UI.HtmlControls;

namespace OfficeWebUI
{
    [ToolboxData("<{0}:OfficeColorPicker runat=server></{0}:OfficeColorPicker>")]
    public class OfficeColorPicker : ControlAncestor, INamingContainer
    {

        #region Private

        private Panel _Panel;
        private Table _Table1;
        private String _ClientClick = String.Empty;

        private List<String>[] _colors = new List<String>[6];


        #endregion

        #region Public

        public event EventHandler SelectedColorChanged;

        public String ClientClick
        {
            get { return this._ClientClick; }
            set { this._ClientClick = value; }
        }

        #endregion

        private void InitColors()
        {
            _colors[0] = new List<String>();
            _colors[0].Add("#ffffff");
            _colors[0].Add("#000000");
            _colors[0].Add("#eeece1");
            _colors[0].Add("#1f497d");
            _colors[0].Add("#4f81bd");
            _colors[0].Add("#c0504d");
            _colors[0].Add("#9bbb59");
            _colors[0].Add("#8064a2");
            _colors[0].Add("#4bacc6");
            _colors[0].Add("#f79646");

            _colors[1] = new List<String>();
            _colors[1].Add("#f2f2f2");
            _colors[1].Add("#7f7f7f");
            _colors[1].Add("#ddd9c3");
            _colors[1].Add("#c6d9f0");
            _colors[1].Add("#dbe5f1");
            _colors[1].Add("#f2dcdb");
            _colors[1].Add("#ebf1dd");
            _colors[1].Add("#e5e0ec");
            _colors[1].Add("#dbeef3");
            _colors[1].Add("#fdeada");
            
            _colors[2] = new List<String>();
            _colors[2].Add("#d8d8d8");
            _colors[2].Add("#595959");
            _colors[2].Add("#c4bd97");
            _colors[2].Add("#8db3e2");
            _colors[2].Add("#b8cce4");
            _colors[2].Add("#e5b9b7");
            _colors[2].Add("#d7e3bc");
            _colors[2].Add("#ccc1d9");
            _colors[2].Add("#b7dde8");
            _colors[2].Add("#fbd5b5");

            _colors[3] = new List<String>();
            _colors[3].Add("#bfbfbf");
            _colors[3].Add("#3f3f3f");
            _colors[3].Add("#938953");
            _colors[3].Add("#548dd4");
            _colors[3].Add("#95b3d7");
            _colors[3].Add("#d99694");
            _colors[3].Add("#c3d69b");
            _colors[3].Add("#b2a2c7");
            _colors[3].Add("#92cddc");
            _colors[3].Add("#fac08f");

            _colors[4] = new List<String>();
            _colors[4].Add("#a5a5a5");
            _colors[4].Add("#262626");
            _colors[4].Add("#494429");
            _colors[4].Add("#17365d");
            _colors[4].Add("#366092");
            _colors[4].Add("#953734");
            _colors[4].Add("#76923c");
            _colors[4].Add("#5f497a");
            _colors[4].Add("#31849b");
            _colors[4].Add("#e36c09");

            _colors[5] = new List<String>();
            _colors[5].Add("#7f7f7f");
            _colors[5].Add("#0c0c0c");
            _colors[5].Add("#1d1b10");
            _colors[5].Add("#0f243e");
            _colors[5].Add("#244061");
            _colors[5].Add("#632423");
            _colors[5].Add("#4f6128");
            _colors[5].Add("#3f3151");
            _colors[5].Add("#205867");
            _colors[5].Add("#984806");

        }

        protected override void OnInit(EventArgs e)
        {
            if (!HttpContext.Current.Items.Contains("OfficeWebUI_Manager"))
                throw new Exception("You must include an OfficeWebUIManager on your page to use OfficeWebUI components");

            ScriptManager _scriptManager = ScriptManager.GetCurrent(Page);
            if (_scriptManager != null)
                _scriptManager.Scripts.Add(new ScriptReference("OfficeWebUI.Resources.Common.Javascript.ColorPicker.js", this.GetType().Assembly.GetName().Name));
            else
                Page.ClientScript.RegisterClientScriptResource(this.GetType(), "OfficeWebUI.Resources.Common.Javascript.ColorPicker.js");

            InitColors();

            this._Panel = new Panel();
            this.Controls.Add(this._Panel);

            this._Table1 = new Table();
            this._Table1.CellSpacing = 0;
            this._Table1.CellPadding = 0;
            this._Panel.Controls.Add(_Table1);

            foreach (List<String> lLine in _colors)
            {
                TableRow lRow = new TableRow();
                this._Table1.Controls.Add(lRow);

                foreach (String lColor in lLine)
                {
                    TableCell lCell = new TableCell();
                    lCell.CssClass = "RibbonColorPicker_Cell";
                    lRow.Controls.Add(lCell);

                    LinkButton lLink = new LinkButton();
                    lLink.CssClass = "RibbonColorPicker_Cell_a";
                    lLink.Click += new EventHandler(lLink_Click);
                    lCell.Controls.Add(lLink);

                    Label lSpan1 = new Label();
                    lSpan1.CssClass = "RibbonColorPicker_Cellspan";
                    lSpan1.Style.Add("background", lColor);
                    lLink.Controls.Add(lSpan1);

                    Label lSpan2 = new Label();
                    lSpan2.CssClass = "RibbonColorPicker_Cellinternalspan";
                    lLink.Controls.Add(lSpan2);
                }
            }

            base.OnInit(e);
        }

        void lLink_Click(object sender, EventArgs e)
        {
            if (this.SelectedColorChanged != null)
            {
                this.SelectedColorChanged(this, new EventArgs());
            }
        }


        
        
    }
}
