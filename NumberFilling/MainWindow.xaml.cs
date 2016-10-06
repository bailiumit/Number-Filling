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

namespace NumberFilling
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Search SearchObject;

        public MainWindow()
        {
            InitializeComponent();

            // 计算结果
            List<string> resultList = SearchResult();

            // 显示结果
            DisplayResult(resultList);
        }

        public List<string> SearchResult()
        {
            // 获取数据
            string expressionStr = "(X+X*X)/X";
            string rangeStr = "1234";
            string objectStr = "max";

            // 搜索结果
            SearchObject = new Search(expressionStr, rangeStr, objectStr);
            List<string> resultList = SearchObject.GetResult();

            return resultList;
        }

        public void DisplayResult(List<string> resultList)
        {
            string resultStr = "";
            for (int i = 0; i < resultList.Count(); i++)
            {
                resultStr = resultStr + " " + resultList[i];
            }
            result_label.Content = resultStr;
        }
    }
}
