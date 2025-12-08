using System;
using System.Collections.Generic;

namespace demo.Models;

public partial class РольСотрудника
{
    public double Id { get; set; }

    public string? РольСотрудника1 { get; set; }

    public virtual ICollection<Пользователь> Пользовательs { get; set; } = new List<Пользователь>();
}
