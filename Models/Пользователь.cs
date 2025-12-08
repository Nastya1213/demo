using System;
using System.Collections.Generic;

namespace demo.Models;

public partial class Пользователь
{
    public double Id { get; set; }

    public double? IdРольСотрудника { get; set; }

    public string? Фио { get; set; }

    public string? Логин { get; set; }

    public string? Пароль { get; set; }

    public virtual РольСотрудника? IdРольСотрудникаNavigation { get; set; }

    public virtual ICollection<Заказ> Заказs { get; set; } = new List<Заказ>();
}
