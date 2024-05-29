using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.Control;

namespace 點餐程式
{
    internal static class Extension
    {
        //public static void AddMealItem(this FlowLayoutPanel panel, List<string> list,
        //    EventHandler checkBoxClicked, EventHandler upDownClicked)
        //{
        //    foreach (var s in list)
        //    {
        //        var mealItem = new FlowLayoutPanel { Size = new Size(panel.Size.Width, 36) };

        //        var upDown = new NumericUpDown { Value = 0, Size = new Size(50, 36) };
        //        upDown.ValueChanged += upDownClicked;

        //        var checkBox = new CheckBox();
        //        checkBox.CheckedChanged += checkBoxClicked;
        //        checkBox.Text = s;

        //        mealItem.Controls.Add(checkBox);
        //        mealItem.Controls.Add(upDown);
        //        panel.Controls.Add(mealItem);

        //        // 藏資料
        //        upDown.Tag = checkBox;
        //        checkBox.Tag = upDown;
        //    }
        //}

        public static List<MealItemPanel> GetMealItemPanelList(this Form form)
        {
            var mealItemPanelList = new List<MealItemPanel>();

            foreach (var item in form.Controls)
            {
                if (item is MealItemPanel mealItemPanel)
                    mealItemPanelList.Add(mealItemPanel);

            }

            return mealItemPanelList;
        }

        public static List<FlowLayoutPanel> GetPanelList(this Form form)
        {
            var panelList = new List<FlowLayoutPanel>();

            foreach (var item in form.Controls)
            {
                if (item is FlowLayoutPanel panel)
                    panelList.Add(panel);
            }

            return panelList;
        }
    }
}