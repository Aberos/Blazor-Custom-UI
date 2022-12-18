using BlazorUI.Base;
using Microsoft.AspNetCore.Components;
using System.Text.RegularExpressions;

namespace BlazorUI.Components
{
    public partial class InputCPF: InputComponentBase<string>
    {
        public string FormattedValue
        {
            get => _formattedValue;
            set
            {
                if (_rawValue != value)
                {
                    OnInput(value);
                    _formattedValue = FormatCpf(_rawValue);
                }
            }
        }

        private string _formattedValue;
        private string _rawValue;
        private string _previousValue;

        protected override void OnParametersSet()
        {
            // Atualiza o valor formatado quando o valor raw é alterado
            if (Value != _previousValue)
            {
                _previousValue = Value;
                _rawValue = Value;
                _formattedValue = FormatCpf(_rawValue);
            }
        }

        private void OnInput(string value)
        {
            // Atualiza o valor raw quando o valor formatado é alterado
            _rawValue = UnformatCpf(value);
            OnChange(_rawValue);
        }

        private string FormatCpf(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            value = ValidateValue(value) ;

            string pattern;
            string replacement;
            if (value.Length <= 3)
            {
                pattern = @"(\d{"+ value.Length + "})";
                replacement = @"$1";
            } else if (value.Length <= 6) {
                pattern = @"(\d{3})(\d{"+ (value.Length - 3) + "})";
                replacement = @"$1.$2";
            }
            else if (value.Length <= 9)
            {
                pattern = @"(\d{3})(\d{3})(\d{"+ (value.Length - 6) + "})";
                replacement = @"$1.$2.$3";
            }
            else
            {
                pattern = @"(\d{3})(\d{3})(\d{3})(\d{"+ (value.Length - 9) + "})";
                replacement = @"$1.$2.$3-$4";
            }
            
            return Regex.Replace(value, pattern, replacement);
        }

        private string UnformatCpf(string value)
        {
            var output = Regex.Replace(value, @"[^\d]", string.Empty);
            return ValidateValue(output);
        }

        private string ValidateValue(string value)
        {
            if(string.IsNullOrEmpty(value))
                return value;

            if (value.Length < 11)
                return value;

            return value.Substring(0, 11);
        }
    }
}
