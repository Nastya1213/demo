using System;
using System.Collections.Generic;

namespace demo.Models;

public partial class ЗаказОбувь
{
    public double? IdЗаказа { get; set; }

    public string? АртикулОбуви { get; set; }

    public double? Количество { get; set; }

    public virtual Заказ? IdЗаказаNavigation { get; set; }

    public virtual Товар? АртикулОбувиNavigation { get; set; }
}
