using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 點餐程式
{
    public partial class Form1 : Form
    {
        OrderListProcessor orderListProcessor;

        public Form1()
        {
            InitializeComponent();
            InitalizeMealItems();

            orderListProcessor = new OrderListProcessor(orderListPanel);
        }

        private void InitalizeMealItems()
        {
            // 假設已經讀取完.csv，得到以下List<string>
            var main = new List<string> { "雞腿飯$95", "排骨飯$80", "咖哩飯$70" };
            var side = new List<string> { "貢丸湯$35", "豬肚湯$50", "豆干$30" };
            var drink = new List<string> { "紅茶$20", "奶茶$30", "柳橙汁$40" };
            var dessert = new List<string> { "蛋糕$55", "泡芙$40", "冰棒$30" };

            mainMealItemPanel.AddMealItems(main, MealCheckBox_CheckedChanged, UpDown_ValueChanged);
            sideMealItemPanel.AddMealItems(side, MealCheckBox_CheckedChanged, UpDown_ValueChanged);
            drinkMealItemPanel.AddMealItems(drink, MealCheckBox_CheckedChanged, UpDown_ValueChanged);
            dessertMealItemPanel.AddMealItems(dessert, MealCheckBox_CheckedChanged, UpDown_ValueChanged);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            int totalPrice = 0;

            foreach (MealItemPanel mealItemPanel in this.GetMealItemPanelList())
            {
                foreach (FlowLayoutPanel mealItem in mealItemPanel.Controls)
                {
                    var checkBox = (CheckBox)mealItem.Controls[0];
                    var valueBox = (NumericUpDown)mealItem.Controls[1];

                    if (checkBox.Checked)
                        totalPrice += int.Parse(checkBox.Text.Split('$')[1]) * (int)valueBox.Value;
                }
            }

            label1.Text = totalPrice.ToString();
        }

        private void MealCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = (CheckBox)sender;
            var upDown = (NumericUpDown)checkBox.Tag;
            var mealItem = (FlowLayoutPanel)checkBox.Parent;

            upDown.ValueChanged -= UpDown_ValueChanged;
            if (checkBox.Checked)
                upDown.Value = 1;
            else
                upDown.Value = 0;
            upDown.ValueChanged += UpDown_ValueChanged;

            orderListProcessor.ProcessOrder(mealItem, (int)upDown.Value);
        }

        private void UpDown_ValueChanged(object sender, EventArgs e)
        {
            var upDown = (NumericUpDown)sender;
            var checkBox = (CheckBox)upDown.Tag;
            var mealItem = (FlowLayoutPanel)checkBox.Parent;

            checkBox.CheckedChanged -= MealCheckBox_CheckedChanged;
            if (upDown.Value == 0)
                checkBox.Checked = false;
            else
                checkBox.Checked = true;
            checkBox.CheckedChanged += MealCheckBox_CheckedChanged;

            orderListProcessor.ProcessOrder(mealItem, (int)upDown.Value);
        }
    }
}
