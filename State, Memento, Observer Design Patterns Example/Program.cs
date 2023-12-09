#region State


// Diagram in the "Diagrams" folder

using System.Net.Http.Headers;
using System.Resources;
using System.Xml;

interface IState
{
    void DoSomething(MyClass myClass);
}


class State_1 : IState
{
    public void DoSomething(MyClass myClass)
    {
        myClass.state = new State_2();
    }
}

internal class State_2 : IState
{
    public void DoSomething(MyClass myClass)
    {
        myClass.state = new State_1();
    }
}



class MyClass
{
    public IState  state{ get; set; }

    public MyClass(IState State)
    {
        this.state = State;
    }


    public void ShowState()
    {
        Console.WriteLine(state.ToString());
    }



}


#endregion


#region Memento

class Document
{
    public string? Text{ get; set; }


   public Memento SaveOldText() => new Memento(Text);

    public void ReturnOldText(Memento OldText) =>Text = OldText.Text;
   

}


class Memento
{
    public string? Text{ get; set; }

    public Memento(string? Text)
    {
        this.Text = Text;
    }
}

#endregion


#region Observer

interface IObserver
{
    void Uptade(string message);
}

interface ISubject
{
    void RegsiterObserver(IObserver observer);
    void NotifyObserver(string message);
}


class Twitch_Viewer : IObserver
{

    public string? Nickname{ get; set; }

    public Twitch_Viewer(string? Nickname)
    {
        this.Nickname = Nickname;
    }
    public void Uptade(string message)
    {
        Console.WriteLine($"Hey {Nickname} you have new message from streamer:\n         {message}");
    }
}

class Twitch_Streamer : ISubject
{

    public List<IObserver> viewvers { get; set; } = new();
    public void NotifyObserver(string message)
    {
        foreach(IObserver observer in viewvers)
            observer.Uptade(message);
    }

    public void RegsiterObserver(IObserver observer)=>viewvers.Add(observer);
}


#endregion