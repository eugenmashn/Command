using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Commad
{
    class Program
    {
        static void Main(string[] args)
        {
            Pult pult = new Pult();
            Tv tv = new Tv();
            pult.SetCommand(new TVOnCommand(tv));
            pult.PressButton();
            pult.PressUndo();

            Console.Read();
            Console.WriteLine("Hello World!");
        }
    }
    interface ICommand
    {
        void Execute();
        void Undo();
    }
    class Tv //Reciver
    {
        public void On()
        {
            Console.WriteLine("Телевізор вімкнений");
        }
        public void Off()
        {
            Console.WriteLine("Телевізор вімкений");
        }
    }
    class TVOnCommand : ICommand
    {
        Tv tv;
        public TVOnCommand(Tv tvSet)
        {
            tv = tvSet;
        }
        public void Execute()
        {
            tv.On();
        }
        public void Undo()
        {
            tv.Off();
        }
    }
    class Microwave
    {
        public void StartСooking(int time)
        {
            Console.WriteLine("Підігріваєм їжу");
            Task.Delay(time).GetAwaiter().GetResult();
        }
        public void StopCooking()
        {
            Console.WriteLine("Їжа підігріта!");
        }
    }
    class MicrowaveCommand : ICommand
    {
        Microwave microwave;
        int time;
        public MicrowaveCommand(Microwave m, int t)
        {
            microwave = m;
            time = t;
        }
        public void Execute()
        {
            microwave.StartСooking(time);
            microwave.StopCooking();
        }

        public void Undo()
        {
            microwave.StopCooking();
        }
    }
    class NoCommand : ICommand
    {
        public void Execute()
        {
        }
        public void Undo()
        {
        }
    }

    class MultiPult
    {
        ICommand[] buttons;
        Stack<ICommand> commandsHistory;
        public MultiPult()
        {
            buttons = new ICommand[2];
            for(int i = 0; i < buttons.Length; i++)
            {
                buttons[i] = new NoCommand();
            }
            commandsHistory = new Stack<ICommand>();
        }
        public void SetCommand(int number , ICommand com)
        {
            buttons[number] = com;
        }
        public void PressButton(int number)
        {
            buttons[number].Execute();
            commandsHistory.Push(buttons[number]);
        }
        public void PressUndoButton()
        {
            if (commandsHistory.Count > 0)
            {
                ICommand undoCommand = commandsHistory.Pop();
                undoCommand.Undo();
            }
        }
    }

    class Pult //invoker
    {
        ICommand command;
        public Pult()
        {
            command = new NoCommand();
        }
        public void SetCommand(ICommand com)
        {
            command = com;
        }
        public void PressButton()
        {
            command.Execute();
        }
        public void PressUndo()
        {
            command.Undo();

        }
    }

}
