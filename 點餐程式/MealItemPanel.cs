using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 點餐程式
{
    internal class MealItemPanel : FlowLayoutPanel
    {
        public void AddMealItems(List<string> list, EventHandler checkBoxClicked,
            EventHandler upDownClicked)
        {
            foreach (var s in list)
            {
                var mealItem = new FlowLayoutPanel { Size = new Size(this.Size.Width, 36) };

                var upDown = new NumericUpDown { Value = 0, Size = new Size(50, 36) };
                upDown.ValueChanged += upDownClicked;


                var checkBox = new CheckBox();
                checkBox.CheckedChanged += checkBoxClicked;
                checkBox.Text = s;

                mealItem.Controls.Add(checkBox);
                mealItem.Controls.Add(upDown);
                this.Controls.Add(mealItem);

                // 藏資料
                upDown.Tag = checkBox;
                checkBox.Tag = upDown;
            }
        }
    }
}
