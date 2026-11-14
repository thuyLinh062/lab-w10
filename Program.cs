//Họ và Tên: Trần Thùy Linh
//MSSV: 202418935
using System;

namespace SmartHomeMini
{
    internal class Program
    {
        private static int passed = 0;
        private static int failed = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("===== SMART HOME MINI - TESTING =====\n");

            RunTest("T1 - Factory tao dung cac loai thiet bi", TestT1);
            RunTest("T2 - Factory tu choi loai thiet bi khong hop le", TestT2);
            RunTest("T3 - Singleton chi co mot instance", TestT3);
            RunTest("T4 - Facade GoToWork()", TestT4);
            RunTest("T5 - Facade GoToSleep()", TestT5);

            Console.WriteLine("\n===== KET QUA KIEM THU =====");
            Console.WriteLine($"Passed: {passed}");
            Console.WriteLine($"Failed: {failed}");
            Console.WriteLine($"Total : {passed + failed}");
        }

        // Ham chay va ghi nhan ket qua tung test
        static void RunTest(string name, Action test)
        {
            try
            {
                test();
                passed++;
                Console.WriteLine($"[PASS] {name}");
            }
            catch (Exception ex)
            {
                failed++;
                Console.WriteLine($"[FAIL] {name}");
                Console.WriteLine($"       {ex.Message}");
            }
        }

        // T1: Kiem tra Factory
        static void TestT1()
        {
            IDevice light =
                DeviceFactory.Create("LiGhT", "Living room light");

            IDevice fan =
                DeviceFactory.Create(" FAN ", "Living room fan");

            IDevice door =
                DeviceFactory.Create("Door", "Main door");

            if (light is not SmartLight)
                throw new Exception("Factory tao sai SmartLight.");

            if (fan is not SmartFan)
                throw new Exception("Factory tao sai SmartFan.");

            if (door is not SmartDoor)
                throw new Exception("Factory tao sai SmartDoor.");
        }

        // T2: Kiem tra loai khong hop le
        static void TestT2()
        {
            bool exceptionThrown = false;

            try
            {
                DeviceFactory.Create("camera", "Security camera");
            }
            catch (ArgumentException)
            {
                exceptionThrown = true;
            }

            if (!exceptionThrown)
                throw new Exception(
                    "Factory khong phat sinh ArgumentException.");
        }

        // T3: Kiem tra Singleton
        static void TestT3()
        {
            var c1 = HomeConfiguration.Instance;
            var c2 = HomeConfiguration.Instance;

            if (!ReferenceEquals(c1, c2))
                throw new Exception(
                    "Hai bien khong tham chieu cung mot doi tuong.");

            // Kiem tra du lieu cau hinh duoc chia se
            int oldLevel = c1.NightLightLevel;

            try
            {
                c1.NightLightLevel = 45;

                if (c2.NightLightLevel != 45)
                    throw new Exception(
                        "Cau hinh khong duoc chia se.");
            }
            finally
            {
                c1.NightLightLevel = oldLevel;
            }
        }

        // Ham lay noi dung Console de kiem tra
        static string CaptureOutput(Action action)
        {
            TextWriter original = Console.Out;

            using StringWriter writer = new StringWriter();

            try
            {
                Console.SetOut(writer);
                action();
                return writer.ToString();
            }
            finally
            {
                Console.SetOut(original);
            }
        }

        // T4: Kiem tra che do di lam
        static void TestT4()
        {
            IDevice light =
                DeviceFactory.Create("light", "Living room light");

            IDevice fan =
                DeviceFactory.Create("fan", "Living room fan");

            SmartDoor door =
                (SmartDoor)DeviceFactory.Create("door", "Main door");

            SmartHomeFacade facade =
                new SmartHomeFacade(light, fan, door);

            string output = CaptureOutput(() =>
            {
                facade.GoToWork();
            });

            if (!output.Contains("Living room light: OFF"))
                throw new Exception("Den chua tat.");

            if (!output.Contains("Living room fan: OFF"))
                throw new Exception("Quat chua tat.");

            if (!output.Contains("Main door: LOCKED"))
                throw new Exception("Cua chua khoa.");
        }

        // T5: Kiem tra che do di ngu
        static void TestT5()
        {
            IDevice light =
                DeviceFactory.Create("light", "Bedroom light");

            IDevice fan =
                DeviceFactory.Create("fan", "Bedroom fan");

            SmartDoor door =
                (SmartDoor)DeviceFactory.Create("door", "Main door");

            SmartHomeFacade facade =
                new SmartHomeFacade(light, fan, door);

            var config = HomeConfiguration.Instance;
            int oldLevel = config.NightLightLevel;

            try
            {
                config.NightLightLevel = 45;

                string output = CaptureOutput(() =>
                {
                    facade.GoToSleep();
                });

                if (!output.Contains("Bedroom fan: OFF"))
                    throw new Exception("Quat chua tat.");

                if (!output.Contains("Main door: LOCKED"))
                    throw new Exception("Cua chua khoa.");

                if (!output.Contains("Bedroom light: ON"))
                    throw new Exception("Den ngu chua bat.");

                if (!output.Contains("Night light level: 45%"))
                    throw new Exception(
                        "Muc sang ban dem khong chinh xac.");
            }
            finally
            {
                config.NightLightLevel = oldLevel;
            }
        }
    }
}