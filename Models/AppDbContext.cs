using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace demo.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Заказ> Заказs { get; set; }

    public virtual DbSet<ЗаказОбувь> ЗаказОбувьs { get; set; }

    public virtual DbSet<КатегорияТовара> КатегорияТовараs { get; set; }

    public virtual DbSet<НаименованиеТовара> НаименованиеТовараs { get; set; }

    public virtual DbSet<Пользователь> Пользовательs { get; set; }

    public virtual DbSet<Поставщик> Поставщикs { get; set; }

    public virtual DbSet<Производитель> Производительs { get; set; }

    public virtual DbSet<ПунктВыдачи> ПунктВыдачиs { get; set; }

    public virtual DbSet<РольСотрудника> РольСотрудникаs { get; set; }

    public virtual DbSet<СтатусЗаказа> СтатусЗаказаs { get; set; }

    public virtual DbSet<Товар> Товарs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-9146J70;Database=Shoes_ponomaryova;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Заказ>(entity =>
        {
            entity.HasKey(e => e.IdЗаказа);

            entity.ToTable("Заказ");

            entity.Property(e => e.IdЗаказа).HasColumnName("ID_заказа");
            entity.Property(e => e.IdКлиента).HasColumnName("ID_клиента");
            entity.Property(e => e.IdПунктаВыдачи).HasColumnName("ID_пункта_выдачи");
            entity.Property(e => e.ДатаДоставки)
                .HasColumnType("datetime")
                .HasColumnName("Дата доставки");
            entity.Property(e => e.ДатаЗаказа)
                .HasColumnType("datetime")
                .HasColumnName("Дата заказа");
            entity.Property(e => e.КодДляПолучения).HasColumnName("Код для получения");
            entity.Property(e => e.СтатусЗаказа).HasColumnName("Статус заказа");

            entity.HasOne(d => d.IdКлиентаNavigation).WithMany(p => p.Заказs)
                .HasForeignKey(d => d.IdКлиента)
                .HasConstraintName("FK_Заказ_Пользователь");

            entity.HasOne(d => d.IdПунктаВыдачиNavigation).WithMany(p => p.Заказs)
                .HasForeignKey(d => d.IdПунктаВыдачи)
                .HasConstraintName("FK_Заказ_Пункт_выдачи");

            entity.HasOne(d => d.СтатусЗаказаNavigation).WithMany(p => p.Заказs)
                .HasForeignKey(d => d.СтатусЗаказа)
                .HasConstraintName("FK_Заказ_Статус_заказа");
        });

        modelBuilder.Entity<ЗаказОбувь>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Заказ_обувь");

            entity.Property(e => e.IdЗаказа).HasColumnName("ID_Заказа");
            entity.Property(e => e.АртикулОбуви)
                .HasMaxLength(255)
                .HasColumnName("Артикул_обуви");

            entity.HasOne(d => d.IdЗаказаNavigation).WithMany()
                .HasForeignKey(d => d.IdЗаказа)
                .HasConstraintName("FK_Заказ_обувь_Заказ");

            entity.HasOne(d => d.АртикулОбувиNavigation).WithMany()
                .HasForeignKey(d => d.АртикулОбуви)
                .HasConstraintName("FK_Заказ_обувь_Товар");
        });

        modelBuilder.Entity<КатегорияТовара>(entity =>
        {
            entity.ToTable("Категория_товара");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.КатегорияТовара1)
                .HasMaxLength(255)
                .HasColumnName("Категория товара");
        });

        modelBuilder.Entity<НаименованиеТовара>(entity =>
        {
            entity.ToTable("Наименование_товара");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.НаименованиеТовара1)
                .HasMaxLength(255)
                .HasColumnName("Наименование товара");
        });

        modelBuilder.Entity<Пользователь>(entity =>
        {
            entity.ToTable("Пользователь");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdРольСотрудника).HasColumnName("ID_Роль сотрудника");
            entity.Property(e => e.Логин).HasMaxLength(255);
            entity.Property(e => e.Пароль).HasMaxLength(255);
            entity.Property(e => e.Фио)
                .HasMaxLength(255)
                .HasColumnName("ФИО");

            entity.HasOne(d => d.IdРольСотрудникаNavigation).WithMany(p => p.Пользовательs)
                .HasForeignKey(d => d.IdРольСотрудника)
                .HasConstraintName("FK_Пользователь_Роль_сотрудника");
        });

        modelBuilder.Entity<Поставщик>(entity =>
        {
            entity.ToTable("Поставщик");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Поставщик1)
                .HasMaxLength(255)
                .HasColumnName("Поставщик");
        });

        modelBuilder.Entity<Производитель>(entity =>
        {
            entity.ToTable("Производитель");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Производитель1)
                .HasMaxLength(255)
                .HasColumnName("Производитель");
        });

        modelBuilder.Entity<ПунктВыдачи>(entity =>
        {
            entity.ToTable("Пункт_выдачи");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Город).HasMaxLength(255);
            entity.Property(e => e.Улица).HasMaxLength(255);
        });

        modelBuilder.Entity<РольСотрудника>(entity =>
        {
            entity.ToTable("Роль_сотрудника");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.РольСотрудника1)
                .HasMaxLength(255)
                .HasColumnName("Роль сотрудника");
        });

        modelBuilder.Entity<СтатусЗаказа>(entity =>
        {
            entity.ToTable("Статус_заказа");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.СтатусЗаказа1)
                .HasMaxLength(255)
                .HasColumnName("Статус заказа");
        });

        modelBuilder.Entity<Товар>(entity =>
        {
            entity.HasKey(e => e.Артикул);

            entity.ToTable("Товар");

            entity.Property(e => e.Артикул).HasMaxLength(255);
            entity.Property(e => e.IdКатегорияТовара).HasColumnName("ID_Категория товара");
            entity.Property(e => e.IdНаименованиеТовара).HasColumnName("ID_Наименование товара");
            entity.Property(e => e.IdПоставщик).HasColumnName("ID_Поставщик");
            entity.Property(e => e.IdПроизводитель).HasColumnName("ID_Производитель");
            entity.Property(e => e.ДействующаяСкидка).HasColumnName("Действующая скидка");
            entity.Property(e => e.ЕдиницаИзмерения)
                .HasMaxLength(255)
                .HasColumnName("Единица измерения");
            entity.Property(e => e.КолВоНаСкладе).HasColumnName("Кол-во на складе");
            entity.Property(e => e.ОписаниеТовара)
                .HasMaxLength(255)
                .HasColumnName("Описание товара");
            entity.Property(e => e.Фото).HasMaxLength(255);

            entity.HasOne(d => d.IdКатегорияТовараNavigation).WithMany(p => p.Товарs)
                .HasForeignKey(d => d.IdКатегорияТовара)
                .HasConstraintName("FK_Товар_Категория_товара");

            entity.HasOne(d => d.IdНаименованиеТовараNavigation).WithMany(p => p.Товарs)
                .HasForeignKey(d => d.IdНаименованиеТовара)
                .HasConstraintName("FK_Товар_Наименование_товара");

            entity.HasOne(d => d.IdПоставщикNavigation).WithMany(p => p.Товарs)
                .HasForeignKey(d => d.IdПоставщик)
                .HasConstraintName("FK_Товар_Поставщик");

            entity.HasOne(d => d.IdПроизводительNavigation).WithMany(p => p.Товарs)
                .HasForeignKey(d => d.IdПроизводитель)
                .HasConstraintName("FK_Товар_Производитель");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
