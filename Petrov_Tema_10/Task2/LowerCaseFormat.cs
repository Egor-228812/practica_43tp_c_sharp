using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
public class LowerCaseFormat : ITextFormatStrategy
{
    public string Format(string text) => text.ToLower();
}

}
