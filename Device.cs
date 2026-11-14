//Họ và Tên: Trần Thùy Linh
//MSSV: 202418935
using System;

namespace SmartHomeMini
{
    public class SmartLight : IDevice
    {
        private readonly string name;
        public SmartLight(string name) => this.name = name;

        public void TurnOn() => Console.WriteLine($"{name}: ON");
        public void TurnOff() => Console.WriteLine($"{name}: OFF");
    }

    public class SmartFan : IDevice
    {
        private readonly string name;
        public SmartFan(string name) => this.name = name;

        public void TurnOn() => Console.WriteLine($"{name}: ON");
        public void TurnOff() => Console.WriteLine($"{name}: OFF");
    }

    public class SmartDoor : IDevice, ILockable
    {
        private readonly string name;
        public SmartDoor(string name) => this.name = name;

        public void TurnOn() => Console.WriteLine($"{name}: OPENED");
        public void TurnOff() => Console.WriteLine($"{name}: CLOSED");
        public void Lock() => Console.WriteLine($"{name}: LOCKED");
        public void Unlock() => Console.WriteLine($"{name}: UNLOCKED");
    }
}