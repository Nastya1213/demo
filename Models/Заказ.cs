using System;
using System.Collections.Generic;

namespace demo.Models;

public partial class Заказ
{
    public double IdЗаказа { get; set; }

    public DateTime? ДатаЗаказа { get; set; }

    public DateTime? ДатаДоставки { get; set; }

    public double? IdПунктаВыдачи { get; set; }

    public double? IdКлиента { get; set; }

    public double? КодДляПолучения { get; set; }

    public double? СтатусЗаказа { get; set; }

    public virtual Пользователь? IdКлиентаNavigation { get; set; }

    public virtual ПунктВыдачи? IdПунктаВыдачиNavigation { get; set; }

    public virtual СтатусЗаказа? СтатусЗаказаNavigation { get; set; }
}
