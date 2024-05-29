using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 點餐程式_v2
{
    public partial class Form1 : Form
    {
        readonly OrderProcessor orderProcessor;
        public static StrategyListModel strategyListModel;

        public Form1()
        {
            InitializeComponent();
            InitalizeMealItems();

            orderProcessor = new OrderProcessor();

            // 物件轉字串 => 序列化
            // 字串轉物件 => 反序列化
            string datas = ConfigurationManager.AppSettings["Strategy"];
            strategyListModel = JsonConvert.DeserializeObject<StrategyListModel>(datas);

            IndicationUnit.OrderChanged += UpdateOrder;
            IndicationUnit.DiscountModeChanged += UpdateDiscountMode;
        }

        private void InitalizeMealItems()
        {
            // 假設已經讀取完.csv，得到以下List<string>
            var main = new List<string> { "雞腿飯$95", "排骨飯$80", "咖哩飯$70" };
            var side = new List<string> { "貢丸湯$35", "豬肚湯$50", "豆干$30" };
            var drink = new List<string> { "紅茶$20", "奶茶$30", "柳橙汁$40" };
            var dessert = new List<string> { "蛋糕$55", "泡芙$40", "冰棒$30" };

            flowLayoutPanel1.AddMealItem(main, MealCheckBox_CheckedChanged, UpDown_ValueChanged);
            flowLayoutPanel2.AddMealItem(side, MealCheckBox_CheckedChanged, UpDown_ValueChanged);
            flowLayoutPanel3.AddMealItem(drink, MealCheckBox_CheckedChanged, UpDown_ValueChanged);
            flowLayoutPanel4.AddMealItem(dessert, MealCheckBox_CheckedChanged, UpDown_ValueChanged);
        }

        private void UpdateOrder(object sender, OrderListModel orderListModel)
        {
            // 更新明細表
            flowLayoutPanel5.Controls.Clear();
            foreach (OrderModel order in orderListModel.orderList)
            {
                var oneLineOrder = new FlowLayoutPanel { Size = new Size(flowLayoutPanel5.Size.Width, 20) };
                var labelSize = new Size(50, oneLineOrder.Size.Height);
                oneLineOrder.Controls.Add(new Label { Text = order.Name, Size = labelSize });
                oneLineOrder.Controls.Add(new Label { Text = order.Price.ToString(), Size = labelSize });
                oneLineOrder.Controls.Add(new Label { Text = order.TotalQuantity.ToString(), Size = labelSize });
                oneLineOrder.Controls.Add(new Label { Text = order.SubTotal.ToString(), Size = labelSize });
                flowLayoutPanel5.Controls.Add(oneLineOrder);
            }

            // 更新總價
            label1.Text = orderListModel.totalPrice.ToString();
        }

        private void UpdateDiscountMode(object sender, string discountMode)
        {
            discountComboBox.SelectedItem = discountMode;
        }

        private void MealCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = (CheckBox)sender;
            var upDown = (NumericUpDown)((Tag)checkBox.Tag).userInputBox;
            var mealItem = (FlowLayoutPanel)checkBox.Parent;

            upDown.Value = (checkBox.Checked ? 1 : 0);

            orderProcessor.Order(PackOrderModel(mealItem));
        }

        private void UpDown_ValueChanged(object sender, EventArgs e)
        {
            var upDown = (NumericUpDown)sender;
            var checkBox = (CheckBox)((Tag)upDown.Tag).userInputBox;
            var mealItem = (FlowLayoutPanel)checkBox.Parent;

            checkBox.Checked = (upDown.Value != 0);

            orderProcessor.Order(PackOrderModel(mealItem));
        }


        private OrderModel PackOrderModel(FlowLayoutPanel mealItem)
        {
            var checkBox = (CheckBox)mealItem.Controls[0];
            var upDown = (NumericUpDown)mealItem.Controls[1];

            // 取得餐點名稱、價錢、數量
            string[] s = checkBox.Text.Split('$');
            string mealName = s[0];
            int price = int.Parse(s[1]);
            int quantity = (int)upDown.Value;

            return new OrderModel
            {
                Name = mealName,
                Price = price,
                PaidQuantity = quantity,
                FreeQuantity = 0,
            };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (var strategy in strategyListModel.StrategyList)
            {
                discountComboBox.Items.Add(strategy.Name);
            }
        }

        private void DiscountComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            orderProcessor.ApplyDiscountMode(discountComboBox.Text);
        }
    }
}
