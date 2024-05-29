using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 點餐程式_v2
{
    public static class Extension
    {
        public static void AddMealItem(this FlowLayoutPanel panel, List<string> list,
            EventHandler checkBoxClicked, EventHandler upDownClicked)
        {
            foreach (var s in list)
            {
                var mealItem = new FlowLayoutPanel { Size = new Size(panel.Size.Width, 36) };

                var upDown = new NumericUpDown { Value = 0, Size = new Size(50, 36) };
                upDown.ValueChanged += upDownClicked;

                var checkBox = new CheckBox();
                checkBox.CheckedChanged += checkBoxClicked;
                checkBox.Text = s;

                mealItem.Controls.Add(checkBox);
                mealItem.Controls.Add(upDown);
                panel.Controls.Add(mealItem);

                // 藏資料
                upDown.Tag = new Tag { userInputBox = checkBox };
                checkBox.Tag = new Tag { userInputBox = upDown };
            }
        }

        public static List<T> Clone<T>(this List<T> list)
            where T : ICloneable
        {
            return list.Select(x => (T)x.Clone()).ToList();
        }
    }
}