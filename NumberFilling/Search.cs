using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;


namespace NumberFilling
{
    class Search
    {
        // 计算指标
        private string expressionStr;
        private string rangeStr;
        private string objectStr;
        // 计算辅助变量
        private char[] travNum;
        private bool[] isNumUsed;
        private double minDiff;
        // 计算结果
        private List<string> optNumStr;

        // 构造函数
        public Search(string expressionStr_, string rangeStr_, string objectStr_)
        {
            // 初始化计算指标
            expressionStr = expressionStr_;
            rangeStr = rangeStr_;
            objectStr = objectStr_;
            //初始化辅助变量
            travNum = new char[rangeStr.Length];
            isNumUsed = new bool[rangeStr.Length];
            minDiff = double.PositiveInfinity;
            // 初始化计算结果
            optNumStr = new List<string>();
            // 开始搜索
            DFSearch(0);
        }

        // 进行深度优先搜索
        public void DFSearch(int depth)
        {
            if (depth == rangeStr.Length) //当搜索得到符合要求的一组排列时
            {
                double curDiff;
                string travNumStr = new string(travNum);
                string result = Calculation(travNumStr);

                // 计算当前结果与目标的差距
                if (string.Equals(objectStr, "max"))
                {
                    // 目标为求最大值时
                    curDiff = -Convert.ToDouble(result);
                }
                else if (string.Equals(objectStr, "min"))
                {
                    // 目标为求最小值时
                    curDiff = Convert.ToDouble(result);
                }
                else
                {
                    // 目标为最接近某个数时
                    curDiff = System.Math.Abs(Convert.ToDouble(result) - Convert.ToDouble(objectStr));
                }

                if (curDiff < minDiff)  // 当新结果更优时，清空List，将新结果添加到List
                {
                    optNumStr.Clear();
                    optNumStr.Add(travNumStr);
                    minDiff = curDiff;
                }
                else if (curDiff == minDiff) // 当新结果一样优时，将新结果添加到List
                {
                    optNumStr.Add(travNumStr);
                }
            }
            else
            {
                for (int i = 0; i < rangeStr.Length; i++)
                {
                    if (!isNumUsed[i])
                    {
                        travNum[depth] = rangeStr[i];
                        isNumUsed[i] = true;
                        DFSearch(depth + 1);
                        isNumUsed[i] = false;
                    }
                }
            }
        }

        // 根据给出的表达式进行计算
        public string Calculation(string travNumStr_)
        {
            char[] tempExpression = expressionStr.ToCharArray();
            string tempExpressionStr;
            string calResult;

            // 将数字的排列组合填到表达式中
            int j = 0;
            for (int i = 0; i < expressionStr.Length; i++)
            {
                if ((expressionStr[i] >= 65 && expressionStr[i] <= 90) || 
                    (expressionStr[i] >= 97 && expressionStr[i] <= 122))
                {
                    tempExpression[i] = travNumStr_[j];
                    j++;
                }
            }
            tempExpressionStr = new string(tempExpression);

            // 计算表达式的值
            object result = new DataTable().Compute(tempExpressionStr, null);
            calResult = Convert.ToString(result);

            return calResult;
        }

        // 返回结果
        public List<string> GetResult()
        {
            return optNumStr;
        }

    }
}
