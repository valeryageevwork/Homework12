using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using static Homework11.Generator;

namespace Homework11
{
    public partial class MainWindow : Window
    {
        private List<Consultant> DataBaseConsultants;
        private List<Manager> DataBaseManagers;
        private List<List<bool>> flags = new List<List<bool>>()
        {
            new List<bool>() { false },
            new List<bool>() { false , false, false }
        };

        public MainWindow()
        {
            InitializeComponent();

            DataBaseConsultants = new List<Consultant>();
            DataBaseManagers = new List<Manager>();

            for (int i = 0; i < 10; i++)
            {
                Client client = ClientGenerate();

                DataBaseConsultants.Add(new Consultant(client.Name, client.Surname,
                                                       client.PhoneNumber, client.PassportSeries,
                                                       client.PassportNumber,
                                                       new DataOfRecord("All", "Beginning", "None")));

                DataBaseManagers.Add(new Manager(client.Name, client.Surname,
                                                 client.PhoneNumber, client.PassportSeries,
                                                 client.PassportNumber,
                                                 new DataOfRecord("All", "Beginning", "None")));
            }

            ConsultantListBox.ItemsSource = DataBaseConsultants;
            ManagerListBox.ItemsSource = DataBaseManagers;
        }

        private void PhoneNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            flags[0][0] = false;

            if (ButtonChangePhoneNumberClient != null)
                ButtonChangePhoneNumberClient.IsEnabled = false;

            if (PhoneNumberTextBox.Text != "") 
            {
                if (PhoneNumberTextBox.Text.First() == '+')
                {
                    if (PhoneNumberTextBox.Text.Length > 1)
                    {
                        if (PhoneNumberTextBox.Text.Length < 14)
                        {
                            if (!long.TryParse(PhoneNumberTextBox.Text.Remove(0, 1), out long num))
                                PhoneNumberTextBox.Clear();

                            if(PhoneNumberTextBox.Text.Length == 3)
                            {
                                if(!((PhoneNumberTextBox.Text[1] == '4') && (PhoneNumberTextBox.Text[2] == '4')))
                                    PhoneNumberTextBox.Clear();
                            }

                            if (PhoneNumberTextBox.Text.Length == 13)
                            {
                                flags[0][0] = true;
                                ButtonChangePhoneNumberClient.IsEnabled = flags[0][0];
                            }
                        }
                        else
                            PhoneNumberTextBox.Clear();
                    }
                }
                else
                    PhoneNumberTextBox.Clear();
            }
        }

        private void ChangePhoneNumberClientButton_Click(object sender, RoutedEventArgs e)
        {
            int index = ConsultantListBox.SelectedIndex;
            Consultant consultant = (Consultant) ConsultantListBox.SelectedItem;

            if (consultant != null)
            {
                Consultant new_consultant = new Consultant(consultant.Name, consultant.Surname,
                                                           PhoneNumberTextBox.Text, consultant.PassportSeries,
                                                           consultant.PassportNumber,
                                                           new DataOfRecord("PhoneNumber", "Changing", "Consultant"));
                DataBaseConsultants[index] = new_consultant;

                Manager manager = new Manager(consultant.Name, consultant.Surname,
                                              PhoneNumberTextBox.Text, consultant.PassportSeries,
                                              consultant.PassportNumber,
                                              new DataOfRecord("PhoneNumber", "Changing", "Consultant"));
                DataBaseManagers[index] = manager;

                ConsultantListBox.Items.Refresh();
                ManagerListBox.Items.Refresh();
            }
        }

        private void AddDataButton_Click(object sender, RoutedEventArgs e)
        {
            Manager manager = new Manager(NameTextBox.Text, SurnameTextBox.Text, M_PhoneNumberTextBox.Text,
                                          PassportSeriesTextBox.Text, PassportNumberTextBox.Text,
                                          new DataOfRecord("All", "Adding", "Manager"));
            DataBaseManagers.Add(manager);
            DataBaseConsultants.Add(manager);

            ManagerListBox.Items.Refresh();
            ConsultantListBox.Items.Refresh();
        }

        private void ConsultantListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonChangePhoneNumberClient.IsEnabled = flags[0][0];

            if (PhoneNumberTextBox.Text == "")
                ButtonChangePhoneNumberClient.IsEnabled = true;
        }

        private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ChangeNameButton.IsEnabled = false;
            IsEnabledButtonAddData();
        }

        private void SurnameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ChangeSurnameButton.IsEnabled = false;
            IsEnabledButtonAddData();
        }

        private void MPhoneNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            flags[1][0] = false;

            if(ButtonAddData != null)
                ButtonAddData.IsEnabled = false;

            if (M_PhoneNumberTextBox.Text.Length == 13)
            {
                Regex regex = new Regex(@"^(\+44\d{10,10})$");
                if (regex.IsMatch(M_PhoneNumberTextBox.Text))
                {
                    flags[1][0] = true;
                }
                else
                    M_PhoneNumberTextBox.Clear();
            }
            IsEnabledButtonAddData();
        }

        private void PassportNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            flags[1][1] = false;

            if (ButtonAddData != null)
                ButtonAddData.IsEnabled = false;

            if (PassportNumberTextBox.Text.Length == 4)
            {
                Regex regex = new Regex(@"^(\d{4,4})$");
                if (regex.IsMatch(PassportNumberTextBox.Text))
                {
                    flags[1][1] = true;
                }
                else
                    PassportNumberTextBox.Clear();
            }
            IsEnabledButtonAddData();
        }

        private void PassportSeriesTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            flags[1][2] = false;

            if (ButtonAddData != null)
                ButtonAddData.IsEnabled = false;

            if (PassportSeriesTextBox.Text.Length == 6)
            {
                Regex regex = new Regex(@"^(\d{6,6})$");
                if (regex.IsMatch(PassportSeriesTextBox.Text))
                {
                    flags[1][2] = true;
                }
                else
                    PassportSeriesTextBox.Clear();
            }
            IsEnabledButtonAddData();
        }

        private void IsEnabledButtonAddData()
        {
            if (flags[1][0] && flags[1][1] && flags[1][2] &&
                NameTextBox.Text != "" && SurnameTextBox.Text != "")
                ButtonAddData.IsEnabled = true;
            else
                ButtonAddData.IsEnabled = false;
        }

        private void ManagerListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(NameTextBox.Text != "") 
                ChangeNameButton.IsEnabled = true;
            if(SurnameTextBox.Text != "") 
                ChangeSurnameButton.IsEnabled = true;

            ButtonDeleteData.IsEnabled = true;
            ChangeMPhoneNumberButton.IsEnabled = flags[1][0];
            ChangePassportNumberButton.IsEnabled = flags[1][1];
            ChangePassportSeriesButton.IsEnabled = flags[1][2];
        }

        private void ChangeFieldsManager(Manager new_manager)
        {
            int index = ManagerListBox.SelectedIndex;
            DataBaseManagers[index] = new_manager;
            DataBaseConsultants[index] = new_manager;

            ManagerListBox.Items.Refresh();
            ConsultantListBox.Items.Refresh();
        }

        private void ChangeNameButton_Click(object sender, RoutedEventArgs e)
        {
            Manager manager = ManagerListBox.SelectedItem as Manager;
            if (manager != null) 
                ChangeFieldsManager(new Manager(NameTextBox.Text, manager.Surname, manager.PhoneNumber,
                                                manager.PassportNumber, manager.PassportSeries,
                                                new DataOfRecord("Name", "Changing", "Manager")));
        }

        private void ChangeSurnameButton_Click(object sender, RoutedEventArgs e)
        {
            Manager manager = ManagerListBox.SelectedItem as Manager;
            if (manager != null)
                ChangeFieldsManager(new Manager(manager.Name, SurnameTextBox.Text, manager.PhoneNumber,
                                                manager.PassportNumber, manager.PassportSeries,
                                                new DataOfRecord("Surname", "Changing", "Manager")));
        } 

        private void ChangeMPhoneNumberButton_Click(object sender, RoutedEventArgs e)
        {
            Manager manager = ManagerListBox.SelectedItem as Manager;
            if (manager != null)
                ChangeFieldsManager(new Manager(manager.Name, manager.Surname, M_PhoneNumberTextBox.Text,
                                                manager.PassportNumber, manager.PassportSeries,
                                                new DataOfRecord("PhoneNumber", "Changing", "Manager")));
        }

        private void ChangePassportNumberButton_Click(object sender, RoutedEventArgs e)
        {
            
            Manager manager = ManagerListBox.SelectedItem as Manager;
            if (manager != null)
                ChangeFieldsManager(new Manager(manager.Name, manager.Surname, manager.PhoneNumber,
                                                PassportNumberTextBox.Text, manager.PassportSeries,
                                                new DataOfRecord("PassportNumber", "Changing", "Manager")));
        }

        private void ChangePassportSeriesButton_Click(object sender, RoutedEventArgs e)
        {
            Manager manager = ManagerListBox.SelectedItem as Manager;
            if (manager != null)
                ChangeFieldsManager(new Manager(manager.Name, manager.Surname, manager.PhoneNumber,
                                                manager.PassportNumber, PassportSeriesTextBox.Text,
                                                new DataOfRecord("PassportSeries", "Changing", "Manager")));
        }

        private void ButtonDeleteData_Click(object sender, RoutedEventArgs e)
        {
            int index = ManagerListBox.SelectedIndex;

            DataBaseManagers.RemoveAt(index);
            DataBaseConsultants.RemoveAt(index);

            ManagerListBox.Items.Refresh();
            ConsultantListBox.Items.Refresh();

            ButtonDeleteData.IsEnabled = false;
        }

        private void ButtonSortBySurname_Click(object sender, RoutedEventArgs e)
        {
            DataBaseManagers.Sort(Manager.SortedBy());
            ManagerListBox.Items.Refresh();

            DataBaseConsultants.Sort(Consultant.SortedBy());
            ConsultantListBox.Items.Refresh();
        }
    }
}
