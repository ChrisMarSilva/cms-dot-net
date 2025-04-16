var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();



// INICIO DO EXEMPLO DO MEDIATOR



var textBox = new TextBox();
var checkBox = new CheckBox();
var button = new Button();

var mediator = new DialogMediator
{
    TextBox = textBox,
    CheckBox = checkBox,
    Button = button
};
textBox.SetMediator(mediator);
checkBox.SetMediator(mediator);
button.SetMediator(mediator);

button.Click();              // -> Desabilitado
textBox.Input("João");       // -> Ainda desabilitado
checkBox.Toggle();           // -> Habilitado
button.Click();              // -> Clique processado!
checkBox.Toggle();           // -> Desabilitado
button.Click();              // -> Desabilitado

// FIM DO EXEMPLO DO MEDIATOR


app.Run();



// INICIO DO EXEMPLO DO MEDIATOR


public interface IMediatR
{
    void Notificar(object sender, string @event);
}

public abstract class Componente
{
    protected IMediatR _mediator;
    public void SetMediator(IMediatR mediator) => _mediator = mediator;
}

public class TextBox : Componente
{
    public string Text { get; private set; } = string.Empty;

    public void Input(string value)
    {
        Text = value;
        Console.WriteLine($"[TextBox] Texto alterado para: \"{Text}\"");

        _mediator.Notificar(this, "TextBoxChanged");
    }
}

public class CheckBox : Componente
{
    public bool IsChecked { get; private set; }

    public void Toggle()
    {
        IsChecked = !IsChecked;
        Console.WriteLine($"[CheckBox] Marcado: {IsChecked}");

        _mediator.Notificar(this, "CheckBoxChanged");
    }
}

public class Button : Componente
{
    public bool IsEnabled { get; set; }

    public void Click()
    {
        if (IsEnabled)
            Console.WriteLine("[Button] Clique processado!");
        else
            Console.WriteLine("[Button] O botão está desabilitado.");

        _mediator.Notificar(this, "ButtonChanged");
    }
}

public class DialogMediator : IMediatR
{
    public TextBox TextBox { get; set; }
    public CheckBox CheckBox { get; set; }
    public Button Button { get; set; }

    public void Notificar(object sender, string @event)
    {
        if (@event != "TextChanged" && @event != "CheckedChanged")
            return;

        var enableButton = !string.IsNullOrWhiteSpace(TextBox.Text) && CheckBox.IsChecked;
        Button.IsEnabled = enableButton;
        Console.WriteLine($"[Mediator] Botão {(enableButton ? "habilitado" : "desabilitado")}");
    }
}


// FIM DO EXEMPLO DO MEDIATOR
