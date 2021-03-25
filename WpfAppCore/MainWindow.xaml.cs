using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Tls;
using Renci.SshNet.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAppCore.common;
using WpfAppCore.context;
using WpfAppCore.model;

namespace WpfAppCore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RealtyContext db;
        public MainWindow()
        {
            InitializeComponent();

            db = new RealtyContext();

            db.Lands.Load();
            landGrid.ItemsSource = db.Lands.Local.ToBindingList();

            db.Apartments.Load();
            apartmentGrid.ItemsSource = db.Apartments.Local.ToBindingList();

            db.Houses.Load();
            houseGrid.ItemsSource = db.Houses.Local.ToBindingList();
            
            
        }

        private string canInsertNewLand(Land land)
        {
            if (land.latitude > 90 || land.latitude < -90)
            {
                return "Широта принимает значение в диапозоне от -90 до +90";
            }
            if (land.longitude > 180 || land.longitude < -180)
            {
                return "Долгота принимает значение в диапозоне от -180 до +180";
            }
            if(land.square < 0)
            {
                return "Площадь не может быть отрицательной";
            }
            return null;
        }

        private void SaveLandButton_Click(object sender, RoutedEventArgs e)
        {

            if (landGrid.Items.Count > 0)
            {
                for (int i = 0; i < landGrid.Items.Count; i++)
                {
                    Land land = landGrid.Items[i] as Land;
                    if (land != null)
                    {
                        string error = canInsertNewLand(land);
                        if (error != null)
                        {
                            MessageBox.Show("Строка: " + i + " " + error);
                            return;
                        }
                        else
                        {
                            db.SaveChanges();
                        }
                    }
                }

            }
        }

        private void DeleteLandButton_Click(object sender, RoutedEventArgs e)
        {
            if (landGrid.SelectedItems.Count > 0) {
                for(var i =0;i< landGrid.SelectedItems.Count;i++) {
                    if (landGrid.SelectedItems[i] != null && landGrid.SelectedItems[i] is Land) {
                        db.Lands.Remove((Land)landGrid.SelectedItems[i]);
                    }
                }
                db.SaveChanges();
            }
        }

        private void landGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "id")
                e.Cancel = true;
        }

        private string canInsertNewApartment(Apartment apartment)
        {
            if(apartment.latitude > 90 || apartment.latitude < -90)
            {
                return "Широта принимает значение в диапозоне от -90 до +90";
            }
            if(apartment.longitude > 180 || apartment.longitude < -180)
            {
                return "Долгота принимает значение в диапозоне от -180 до +180";
            }
            if (apartment.square < 0)
            {
                return "Площадь не может быть отрицательной";
            }

            if(Convert.ToInt32(apartment.houseNumber) < 0)
            {
                return "Номер дома не может быть отрицательым";
            }
            if (Convert.ToInt32(apartment.apartmentNumber) < 0)
            {
                return "Номер квартиры не может быть отрицательым";
            }
            if(apartment.roomsAmount <= 0)
            {
                return "Количество комнат не может быть отрицательным или равняться нулю";
            }
            return null;
        }

        private void SaveApartmentButton_Click(object sender, RoutedEventArgs e)
        {
            if(apartmentGrid.Items.Count > 0)
            {
                for(int i = 0; i < apartmentGrid.Items.Count; i++)
                {
                    Apartment apartment = apartmentGrid.Items[i] as Apartment;
                    if(apartment != null)
                    {
                        string error = canInsertNewApartment(apartment);
                        if(error != null)
                        {
                            MessageBox.Show("Строка: " + i + " " + error);
                            return;
                        }
                        else
                        {
                            db.SaveChanges();
                        }
                    }
                }
                    
            }
        }

        private void DeleteApartmentButton_Click(object sender, RoutedEventArgs e)
        {
            if(apartmentGrid.SelectedItems.Count > 0)
            {
                for(var i = 0; i < apartmentGrid.SelectedItems.Count; i++)
                {
                    if(apartmentGrid.SelectedItems[i] != null && apartmentGrid.SelectedItems[i] is Apartment)
                    {
                        db.Apartments.Remove((Apartment)apartmentGrid.SelectedItems[i]);
                    }
                }
            }
        }

        private void apartmentGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "id")
                e.Cancel = true;
        }

        private string canInsertNewHouse(House house)
        {
            if (house.latitude > 90 || house.latitude < -90)
            {
                return "Широта принимает значение в диапозоне от -90 до +90";
            }
            if (house.longitude > 180 || house.longitude < -180)
            {
                return "Долгота принимает значение в диапозоне от -180 до +180";
            }
            if (house.square < 0)
            {
                return "Площадь не может быть отрицательной";
            }

            if (Convert.ToInt32(house.houseNumber) < 0)
            {
                return "Номер дома не может быть отрицательым";
            }
            if (Convert.ToInt32(house.apartmentNumber) < 0)
            {
                return "Номер квартиры не может быть отрицательым";
            }
            if (house.roomsAmount <= 0)
            {
                return "Количество комнат не может быть отрицательным или равняться нулю";
            }
            if(house.floorsAmount <= 0) 
            {
                return "Количество этажей не може быть отрицательным или равняться нулю";
            }
            return null;
        }

        private void SaveHouseButton_Click(object sender, RoutedEventArgs e)
        {
            if (houseGrid.Items.Count > 0)
            {
                for (int i = 0; i < houseGrid.Items.Count; i++)
                {
                    House house = houseGrid.Items[i] as House;
                    if (house != null)
                    {
                        string error = canInsertNewHouse(house);
                        if (error != null)
                        {
                            MessageBox.Show("Строка: " + i + " " + error);
                            return;
                        }
                        else
                        {
                            db.SaveChanges();
                        }
                    }
                }

            }
        }

        private void DeleteHouseButton_Click(object sender, RoutedEventArgs e)
        {
            if (houseGrid.SelectedItems.Count > 0)
            {
                for (var i = 0; i < houseGrid.SelectedItems.Count; i++)
                {
                    if (houseGrid.SelectedItems[i] != null && houseGrid.SelectedItems[i] is House)
                    {
                        db.Houses.Remove((House)houseGrid.SelectedItems[i]);
                    }
                }
            }
        }
        private void houseGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "id")
                e.Cancel = true;
        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            db.Dispose();
        }

        private void SearchByAdress_Click(object sender, RoutedEventArgs e)
        {
            BindingList<IRealty> sr = new BindingList<IRealty>();
            searchGrid.ItemsSource = sr;
            sr.Clear();
            ArrayList result = new ArrayList();
            ArrayList all = new ArrayList();
            all.AddRange(db.Lands.Local.ToList());
            all.AddRange(db.Apartments.Local.ToList());
            all.AddRange(db.Houses.Local.ToList());
            db.Houses.Where(p => p.city == "city");

            string adress = cityText.Text.Trim().ToLower() + streetText.Text.Trim().ToLower() + houseNumberText.Text.Trim().ToLower() + apartmentNumberText.Text.Trim().ToLower();
            //string fioSearch = name.Text.Trim().ToLower() + lastName.Text.Trim().ToLower() + patronymic.Text.Trim().ToLower();
            foreach (var item in all)
            {
                if (item != null)
                {
                    if (item is IRealty)
                    {
                        string[] data = Levi.GetLeviData(cityText.Text, streetText.Text, houseNumberText.Text, apartmentNumberText.Text, (IRealty)item);
                        if (Levi.LevenshteinDistance(data[0], data[1]) <= 3)
                        {

                            sr.Add(item as IRealty);
                        }
                    }

                }
            }
        }
    }
}
