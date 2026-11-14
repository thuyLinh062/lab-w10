//Họ và Tên: Trần Thùy Linh
//MSSV: 202418935
using System;

namespace SmartHomeMini
{
    public interface IDevice
    {
        void TurnOn();
        void TurnOff();
    }

    public interface ILockable
    {
        void Lock();
        void Unlock();
    }
}