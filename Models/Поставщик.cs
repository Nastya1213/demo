using System;
using System.Collections.Generic;

namespace demo.Models;

public partial class Поставщик
{
    public double Id { get; set; }

    public string? Поставщик1 { get; set; }

    public virtual ICollection<Товар> Товарs { get; set; } = new List<Товар>();
}
