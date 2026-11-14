//Họ và Tên: Trần Thùy Linh
//MSSV: 202418935
using System;

namespace SmartHomeMini
{
    // --- Factory Pattern ---
    public static class DeviceFactory
    {
        public static IDevice Create(string type, string name)
        {
            return type.Trim().ToLowerInvariant() switch
            {
                "light" => new SmartLight(name),
                "fan" => new SmartFan(name),
                "door" => new SmartDoor(name),
                _ => throw new ArgumentException($"Unsupported device type: {type}")
            };
        }
    }

    // --- Singleton Pattern ---
    public sealed class HomeConfiguration
    {
        private static readonly Lazy<HomeConfiguration> lazy =
            new(() => new HomeConfiguration());

        public static HomeConfiguration Instance => lazy.Value;

        public string HomeName { get; set; } = "My Home";
        public int NightLightLevel { get; set; } = 30;

        private HomeConfiguration() { }
    }

    // --- Facade Pattern ---
    public sealed class SmartHomeFacade
    {
        private readonly IDevice light;
        private readonly IDevice fan;
        private readonly ILockable door;

        public SmartHomeFacade(IDevice light, IDevice fan, ILockable door)
        {
            this.light = light;
            this.fan = fan;
            this.door = door;
        }

        public void GoToWork()
        {
            light.TurnOff();
            
            fan.TurnOff(); 
            
            door.Lock();
        }

        public void GoToSleep()
        {
            fan.TurnOff();
            door.Lock();
            light.TurnOn();
            int level = HomeConfiguration.Instance.NightLightLevel;
            Console.WriteLine($"Night light level: {level}%");
        }
    }
}