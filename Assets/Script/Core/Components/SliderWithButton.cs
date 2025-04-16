using R3;
using UnityEngine.UIElements;
[UxmlElement]
public partial class SliderWithButton : SliderInt {
    [UxmlAttribute]
    public string ButtonLabel { 
        get => _button.text;
        set {
            _button.text = value;
        }
    }
    [UxmlAttribute]
    public string Label { 
        get => label;
        set {
            label = value;
        }
    }
    private Subject<int> _onClick = new();
    public Observable<int> OnClick => _onClick;
    private Button _button;
    public SliderWithButton() {
        _button = new Button();
        _button.RegisterCallback<ClickEvent>(_ => _onClick.OnNext(value));
        Add(_button);

        // default for better editing experience 
        label = "Lorem Ipsum";
        _button.text = "Buy";
    }
    public SliderWithButton SetMaxValue(int value) {
        highValue = value;
        return this;
    }
    public SliderWithButton SetMinValue(int value = 0) {
        lowValue = value;
        return this;
    }
    public SliderWithButton SetLabel(string label) {
        this.label = label;
        return this;
    }
    public SliderWithButton SetButtonLabel(string buttonLabel) {
        _button.text = buttonLabel;
        return this;
    }
    public SliderWithButton SetInputDisplay(bool value) {
        showInputField = value;
        return this;
    }
}