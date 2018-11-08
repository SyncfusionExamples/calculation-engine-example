# Calculate any formulas and built-in functions using Calculation Engine.

## About the sample

This example will explain how to parse and compute the formulas and expressions using CalcEngine. Also, you can set and retrive the values in row and column index using ICalcData.

The below code explain how to create a class for set and retrive the values in row and column index using ICalcData.

```c#
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
```
The below code will explain how to set the data value of a specified row and column index.

```c#
CalcData calcData = new CalcData();
CalcEngine engine = new CalcEngine(calcData);

// To set the data value of a specified row and column.
calcData.SetValueRowCol(90, 1, 1);
calcData.SetValueRowCol(50, 1, 2);
```

Now calculate using built-in functions with cell reference of stored values,

```c#
 // Calculate the built in function based on cell reference.
string formula = "SUM(A1, B1)";
string result = engine.ParseAndComputeFormula(formula);
```

Also we can parse and compute the expressions and built-in functions using Calculation engine.

```c#
//Computing Expressions,
string formula1 = "(5+25) *2";
string result1 = engine.ParseAndComputeFormula(formula1);

//Computing In-Built formulas,
string formula2 = "AVG(4,5,6)";
string result2 = engine.ParseAndComputeFormula(formula2);
```

The output of parse and compute expressions and formulas using Calculation Engine,

![Parse and compute expressions and formulas using Calculation Engine](https://blog.syncfusion.com/wp-content/uploads/2018/11/Parse-compute-expressions-formulas-Calculation-Engine.png)