using Syncfusion.Calculate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICalcDataExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // This class is derived from ICalcData interface.
            CalcData calcData = new CalcData();
            CalcEngine engine = new CalcEngine(calcData);

            // To set the data value of a specified row and column.
            calcData.SetValueRowCol(90, 1, 1);
            calcData.SetValueRowCol(50, 1, 2);
            
            // Calculate the built in function based on cell reference.
            string formula = "SUM(A1, B1)";
            string result = engine.ParseAndComputeFormula(formula);
			
			//Computing Expressions,
			string formula1 = "(5+25) *2";
			string result1 = engine.ParseAndComputeFormula(formula1);

			//Computing In-Built formulas,
			string formula2 = "AVG(4,5,6)";
			string result2 = engine.ParseAndComputeFormula(formula2);

            Console.WriteLine("The calculated result of formula (SUM(A1, B1)) : " + result);
            Console.WriteLine("The calculated result of formula ((5+25) *2) : " + result1);
            Console.WriteLine("The calculated result of formula (AVG(4,5,6)) : " + result2);
            Console.ReadKey();
        }
    }

    public class CalcData : ICalcData
    {
        public event ValueChangedEventHandler ValueChanged;

        Dictionary<string, object> values = new Dictionary<string, object>();
        public object GetValueRowCol(int row, int col)
        {
            object value = null;
            var key = RangeInfo.GetAlphaLabel(col) + row;
            this.values.TryGetValue(key, out value);
            return value;
        }

        public void SetValueRowCol(object value, int row, int col)
        {
            var key = RangeInfo.GetAlphaLabel(col) + row;
            if (!values.ContainsKey(key))
                values.Add(key, value);
            else if (values.ContainsKey(key) && values[key] != value)
                values[key] = value;
        }

        public void WireParentObject()
        {
        }

        private void OnValueChanged(int row, int col, string value)
        {
            if (ValueChanged != null)
                ValueChanged(this, new ValueChangedEventArgs(row, col, value));
        }
    }
}
