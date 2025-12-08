using System;
using System.Collections.Generic;

namespace demo.Models;

public partial class СтатусЗаказа
{
    public double Id { get; set; }

    public string? СтатусЗаказа1 { get; set; }

    public virtual ICollection<Заказ> Заказs { get; set; } = new List<Заказ>();
}
