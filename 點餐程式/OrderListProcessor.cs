using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 點餐程式
{
    internal class OrderListProcessor
    {
        OrderListPanel orderListPanel;  // 明細

        public OrderListProcessor(OrderListPanel orderListPanel)
        {
            this.orderListPanel = orderListPanel;  // 明細(由Form1傳入)
        }

        public void ProcessOrder(FlowLayoutPanel mealItem, int quantity)
        {
            if (mealItem == null) return;

            if (mealItem.Tag == null)
            {
                GenerateOrder(mealItem);
                return;
            }

            if (quantity == 0)
                RemoveOrder(mealItem);
            else
                ModifyOrder(mealItem);
        }

        private void GenerateOrder(FlowLayoutPanel mealItem)
        {
            var checkBox = (CheckBox)mealItem.Controls[0];
            var upDown = (NumericUpDown)mealItem.Controls[1];

            // 取得餐點名稱、價錢、數量
            string[] s = checkBox.Text.Split('$');
            string mealName = s[0];
            int price = int.Parse(s[1]);
            int quantity = (int)upDown.Value;

            // 在明細(orderListPanel)上新增一筆資料
            var order = new FlowLayoutPanel { Size = new Size(orderListPanel.Size.Width, 20) };
            var labelSize = new Size(50, order.Size.Height);
            order.Controls.Add(new Label { Text = mealName, Size = labelSize });
            order.Controls.Add(new Label { Text = price.ToString(), Size = labelSize });
            order.Controls.Add(new Label { Text = quantity.ToString(), Size = labelSize });
            order.Controls.Add(new Label { Text = (price * quantity).ToString(), Size = labelSize });
            orderListPanel.Controls.Add(order);

            // 將明細上的這一筆資料也藏在mealItem裡面
            mealItem.Tag = order;
        }

        private void ModifyOrder(FlowLayoutPanel mealItem)
        {
            var checkBox = (CheckBox)mealItem.Controls[0];
            var upDown = (NumericUpDown)mealItem.Controls[1];

            // 取得餐點名稱、價錢、數量
            string[] s = checkBox.Text.Split('$');
            int price = int.Parse(s[1]);
            int quantity = (int)upDown.Value;

            // 修改明細(orderListPanel)上的資料
            var order = (FlowLayoutPanel)mealItem.Tag;
            order.Controls[2].Text = quantity.ToString();
            order.Controls[3].Text = (quantity * price).ToString();
        }

        private void RemoveOrder(FlowLayoutPanel mealItem)
        {
            var order = (FlowLayoutPanel)mealItem.Tag;

            orderListPanel.Controls.Remove(order);
            mealItem.Tag = null;
        }
    }
}
