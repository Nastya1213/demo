using System;
using System.Collections.Generic;

namespace demo.Models;

public partial class ПунктВыдачи
{
    public double Id { get; set; }

    public double? Индекс { get; set; }

    public string? Город { get; set; }

    public string? Улица { get; set; }

    public double? Дом { get; set; }

    public virtual ICollection<Заказ> Заказs { get; set; } = new List<Заказ>();
}
