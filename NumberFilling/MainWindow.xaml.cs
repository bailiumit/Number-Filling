using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;


namespace NumberFilling
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Search SearchObject;
        string objectStr = "max";

        public MainWindow()
        {
            InitializeComponent();
            nearest_TextBox.IsEnabled = false;
            // 初始化ComboBox
            Dictionary<int, string> objective_Dictionary = new Dictionary<int, string>()
            {
                {1,"Max"},
                {2,"Min"},
                {3,"Nearest to"}
            };
            objective_ComboBox.ItemsSource = objective_Dictionary;
            objective_ComboBox.SelectedValuePath = "Key";
            objective_ComboBox.DisplayMemberPath = "Value";
            objective_ComboBox.SelectedIndex = 0;
        }

        private void search_Button_Click(object sender, RoutedEventArgs e)
        {
            // 获取字符串
            string expressionStr = expression_TextBox.Text;
            string rangeStr = number_TextBox.Text;

            int count = expressionStr.Replace("*", "").Replace("X", "*").Split('*').Length - 1;
            if (count > rangeStr.Length)   //变量数目大于数字范围
            {
                MessageBox.Show("The number of X should be the same as the length of number");
            }
            else
            {
                // 计算结果
                SearchObject = new Search(expressionStr, rangeStr, objectStr);
                List<string> resultList = SearchObject.GetResult();
                // 显示结果
                DisplayResult(resultList);
            }
        }

        public void DisplayResult(List<string> resultList)
        {
            string resultStr = "";
            char[] refinedExpression;
            string refinedExpressionStr;

            for (int i = 0; i < resultList.Count(); i++)
            {
                refinedExpression = expression_TextBox.Text.ToCharArray();
                int k = 0;
                for (int j = 0; j < refinedExpression.Length; j++)
                {
                    if ((refinedExpression[j] >= 65 && refinedExpression[j] <= 90) ||
                        (refinedExpression[j] >= 97 && refinedExpression[j] <= 122))
                    {
                        refinedExpression[j] = resultList[i][k];
                        k++;
                    }
                }
                refinedExpressionStr = new string(refinedExpression);

                if (i == 0)
                {
                    resultStr = resultStr + refinedExpressionStr;
                }
                else
                {
                    resultStr = resultStr + Environment.NewLine + refinedExpressionStr;
                }
            }
            result_TextBox.Text = resultStr;
        }

        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;

            switch (cb.SelectedIndex)
            {
                case 0:
                    nearest_TextBox.Clear();
                    nearest_TextBox.IsEnabled = false;
                    objectStr = "max";
                    break;
                case 1:
                    nearest_TextBox.Clear();
                    nearest_TextBox.IsEnabled = false;
                    objectStr = "min";
                    break;
                case 2:
                    nearest_TextBox.IsEnabled = true;
                    break;
            }
        }

        private void nearest_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            objectStr = nearest_TextBox.Text;
        }

        private void expression_TextBox_MouseEnter(object sender, MouseEventArgs e)
        {
            // expression_TextBox.Clear();
        }

        private void number_TextBox_MouseEnter(object sender, MouseEventArgs e)
        {
            // number_TextBox.Clear();
        }

        private void expression_TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void number_TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9]+");
            e.Handled = re.IsMatch(e.Text);
        }

        private void nearest_TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9]+");
            e.Handled = re.IsMatch(e.Text);
        }
    }
}
