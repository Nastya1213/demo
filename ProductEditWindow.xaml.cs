using demo.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace demo
{
    /// <summary>
    /// Логика взаимодействия для ProductEditWindow.xaml
    /// </summary>
    public partial class ProductEditWindow : Window
    {
        private readonly AppDbContext _context = new();
        public Товар Product { get; private set; }
        private string? _imagePath;

        // Для добавления
        public ProductEditWindow()
        {
            InitializeComponent();
            Product = new Товар();
            LoadReferences();
        }

        // Для редактирования
        public ProductEditWindow(Товар existing) : this()
        {
            Product = existing;
            ArtikulBox.Text = Product.Артикул;
            PriceBox.Text = Product.Цена?.ToString();
            if (Product.IdПоставщик.HasValue)
                SupplierBox.SelectedItem = SupplierBox.Items.Cast<Поставщик>()
                    .FirstOrDefault(s => s.Id == Product.IdПоставщик.Value);
            if (Product.IdКатегорияТовара.HasValue)
                CategoryBox.SelectedItem = CategoryBox.Items.Cast<КатегорияТовара>()
                    .FirstOrDefault(c => c.Id == Product.IdКатегорияТовара.Value);
            if (!string.IsNullOrEmpty(Product.Фото))
            {
                try { PreviewImage.Source = new BitmapImage(new Uri(Product.Фото)); }
                catch { /* игнор */ }
            }
        }

        private void LoadReferences()
        {
            var suppliers = _context.Поставщикs.ToList();
            var categories = _context.КатегорияТовараs.ToList();
            SupplierBox.ItemsSource = suppliers;
            CategoryBox.ItemsSource = categories;
        }

        private void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Изображения|*.png;*.jpg;*.jpeg;*.bmp;*.gif"
            };
            if (dialog.ShowDialog() == true)
            {
                _imagePath = dialog.FileName;
                PreviewImage.Source = new BitmapImage(new Uri(_imagePath));
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            _context.Dispose();
            base.OnClosed(e);
        }

        // Метод, который вызывается, когда нажали "Сохранить"
        public bool Save()
        {
            if (string.IsNullOrWhiteSpace(ArtikulBox.Text))
            {
                MessageBox.Show("Введите артикул");
                return false;
            }

            Product.Артикул = ArtikulBox.Text.Trim();
            Product.Цена = double.TryParse(PriceBox.Text, out var price) ? price : null;

            var supplier = SupplierBox.SelectedItem as Поставщик;
            Product.IdПоставщик = supplier?.Id;

            var category = CategoryBox.SelectedItem as КатегорияТовара;
            Product.IdКатегорияТовара = category?.Id;

            // Сохраняем картинку в папку приложения (просто!)
            if (!string.IsNullOrEmpty(_imagePath))
            {
                try
                {
                    string fileName = System.IO.Path.GetFileName(_imagePath);
                    string destPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", fileName);
                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(destPath)!);
                    File.Copy(_imagePath, destPath, overwrite: true);
                    Product.Фото = destPath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка сохранения фото: {ex.Message}");
                }
            }

            return true;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (Save()) // вызываем ваш метод сохранения данных
            {
                DialogResult = true; // говорит, что окно закрыто с подтверждением
                Close();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; // отмена
            Close();
        }
    }
}
