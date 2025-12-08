using demo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly AppDbContext _context = new AppDbContext();
        private ICollectionView _view;
        public MainWindow()
        {
            InitializeComponent();
            LoadProducts();
            LoadSuppliers();
        }

        private void LoadProducts()
        {
            var products = _context.Товарs.Include(p => p.IdНаименованиеТовараNavigation)
                .Include(p => p.IdПоставщикNavigation)
                .Include(p => p.IdКатегорияТовараNavigation)
                .Include(p => p.IdПроизводительNavigation).ToList();
            ProductList.ItemsSource = products;
            System.Diagnostics.Debug.WriteLine($"Загружено товаров: {products.Count}");
            _view = CollectionViewSource.GetDefaultView(ProductList.ItemsSource);
            _view.Filter = FilterProducts;

        }
        private void LoadSuppliers()
        {
            var suppliers = _context.Поставщикs.ToList(); // или как называется ваш DbSet

            foreach (var s in suppliers)
            {
                SupplierFilter.Items.Add(new ComboBoxItem
                {
                    Content = s.Поставщик1,
                    Tag = s.Id // s.Id — double, и это нормально
                });
            }
        }

        private bool FilterProducts(object item)
        {
            if (item is not Товар product)
                return false;

            // Поиск (работает)
            string searchText = SearchBox?.Text?.Trim();
            if (!string.IsNullOrEmpty(searchText))
            {
                string name = product.IdНаименованиеТовараNavigation?.НаименованиеТовара1 ?? "";
                if (!name.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                    return false;
            }

            // Фильтр по поставщику — ИСПРАВЛЕНО: double вместо int
            var selected = SupplierFilter.SelectedItem as ComboBoxItem;
            if (selected?.Tag is double selectedSupplierId)
            {
                // Сравниваем double? с double
                if (product.IdПоставщик != selectedSupplierId)
                    return false;
            }

            return true;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _view?.Refresh();
        }

        private void SupplierFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _view?.Refresh();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var win = new ProductEditWindow(); // окно из предыдущего ответа
            if (win.ShowDialog() == true && win.Save())
            {
                _context.Товарs.Add(win.Product);
                _context.SaveChanges();
                LoadProducts(); // перезагрузить список
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductList.SelectedItem is not Товар selected)
            {
                MessageBox.Show("Выберите товар для редактирования.");
                return;
            }

            var win = new ProductEditWindow(selected);
            if (win.ShowDialog() == true && win.Save())
            {
                _context.SaveChanges(); // изменения уже в объекте selected
                LoadProducts();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductList.SelectedItem is not Товар selected)
            {
                MessageBox.Show("Выберите товар для удаления.");
                return;
            }

            var result = MessageBox.Show(
                $"Удалить товар '{selected.Артикул}'?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _context.Товарs.Remove(selected);
                _context.SaveChanges();
                LoadProducts();
            }
        }
    }

}

