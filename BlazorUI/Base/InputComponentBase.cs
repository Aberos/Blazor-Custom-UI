using Microsoft.AspNetCore.Components;

namespace BlazorUI.Base
{
    public class InputComponentBase<T> : ComponentBase
    {
        [Parameter] public T Value { get; set; }
        [Parameter] public EventCallback<T> ValueChanged { get; set; }

        protected void OnChange(T value)
        {
            Value = value;
            ValueChanged.InvokeAsync(value);
        }
    }
}
