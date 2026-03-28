using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
public class TextFormatter
{
    private ITextFormatStrategy strategy;

    public void SetStrategy(ITextFormatStrategy strategy)
    {
        this.strategy = strategy;
    }

    public string FormatText(string text)
    {
        if (strategy == null)
            throw new InvalidOperationException("Strategy not set.");
        return strategy.Format(text);
    }
}

}
