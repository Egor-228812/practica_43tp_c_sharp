using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public class UpperCaseFormat : ITextFormatStrategy
    {
        public string Format(string text) => text.ToUpper();
    }

}
