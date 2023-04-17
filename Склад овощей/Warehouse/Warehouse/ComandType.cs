using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse
{
    /// <summary>
    ///  Перечисление для типа команд.
    /// </summary>
    public enum ComandType : uint
    {
        Exit = 0,
        AddContainerToWarehouse = 1,
        RemoveContainerById = 2,
        ShowInformation = 3,

    }
}
