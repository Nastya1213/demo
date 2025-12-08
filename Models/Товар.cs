using System;
using System.Collections.Generic;

namespace demo.Models;

public partial class Товар
{
    public string Артикул { get; set; } = null!;

    public double? IdНаименованиеТовара { get; set; }

    public string? ЕдиницаИзмерения { get; set; }

    public double? Цена { get; set; }

    public double? IdПоставщик { get; set; }

    public double? IdПроизводитель { get; set; }

    public double? IdКатегорияТовара { get; set; }

    public double? ДействующаяСкидка { get; set; }

    public double? КолВоНаСкладе { get; set; }

    public string? ОписаниеТовара { get; set; }

    public string? Фото { get; set; }

    public virtual КатегорияТовара? IdКатегорияТовараNavigation { get; set; }

    public virtual НаименованиеТовара? IdНаименованиеТовараNavigation { get; set; }

    public virtual Поставщик? IdПоставщикNavigation { get; set; }

    public virtual Производитель? IdПроизводительNavigation { get; set; }
}
