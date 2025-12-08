using System;
using System.Collections.Generic;

namespace demo.Models;

public partial class Производитель
{
    public double Id { get; set; }

    public string? Производитель1 { get; set; }

    public virtual ICollection<Товар> Товарs { get; set; } = new List<Товар>();
}
