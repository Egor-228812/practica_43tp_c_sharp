using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public class TitleCaseFormat : ITextFormatStrategy
    {
        public string Format(string text) => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text.ToLower());
    }

}
