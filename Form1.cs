using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;

namespace Currencies
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient client = new HttpClient();
        private BankResponse todayData;
        private BankResponse archiveData;

        public Form1()
        {
            InitializeComponent();
            btnLoadToday_Click(null, null);
        }

        private async void btnLoadToday_Click(object sender, EventArgs e)
        {
            string url = "https://www.cbr-xml-daily.ru/daily_json.js";
            try
            {
                string json = await client.GetStringAsync(url);
                todayData = JsonSerializer.Deserialize<BankResponse>(json);
                dgvRates.Rows.Clear();
                foreach (var item in todayData.Valute.Values)
                {
                    dgvRates.Rows.Add(item.Code, item.Name, $"{item.Value:F2} руб.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
        }

        private async void btnLoadArchive_Click(object sender, EventArgs e)
        {
            string year = datePicker.Value.ToString("yyyy");
            string month = datePicker.Value.ToString("MM");
            string day = datePicker.Value.ToString("dd");

            string url = $"https://www.cbr-xml-daily.ru/archive/{year}/{month}/{day}/daily_json.js";
            try
            {
                string json = await client.GetStringAsync(url);
                archiveData = JsonSerializer.Deserialize<BankResponse>(json);
                dgvArchive.Rows.Clear();
                cbSelect.Items.Clear();
                foreach (var item in archiveData.Valute.Values)
                {
                    dgvArchive.Rows.Add(item.Code, item.Name, $"{item.Value:F2} руб.");
                    cbSelect.Items.Add(item);
                }
                cbSelect.DisplayMember = "Name";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            decimal amount;
            if (!decimal.TryParse(txtAmount.Text, out amount))
            {
                MessageBox.Show("Введите корректную сумму цифрами!");
                return;
            }

            if (archiveData == null || cbSelect.SelectedItem == null)
            {
                MessageBox.Show("Сначала выберите дату и нажмите 'Показать архив'!");
                return;
            }

            Currency selectedCur = (Currency)cbSelect.SelectedItem;
            decimal result = amount / (decimal)selectedCur.Value;
            lblResult.Text = $"{result:F2} {selectedCur.Code}";
        }
    }
}
