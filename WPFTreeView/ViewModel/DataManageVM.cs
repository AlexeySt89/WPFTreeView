using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPFTreeView.Data;
using WPFTreeView.View;

namespace WPFTreeView.ViewModel
{
    public class DataManageVM : INotifyPropertyChanged
    {
        private List<Model.Object> _allObjects = DataWorker.GetObjects();

        public List<Model.Object> AllObjects
        {
            get { return _allObjects; }
            set
            {
                _allObjects = value;
                NotifyPropertyChanged("AllObject");
            }
        }

        private List<Model.Attribute> _allAttributes = DataWorker.GetAttributesForObject();

        public List<Model.Attribute> AllAttributes
        {
            get { return _allAttributes; }
            set
            {
                _allAttributes = value;
                NotifyPropertyChanged("AllAttributes");
            }
        }


        public string ObjectType { get; set; }
        public string ObjectProduct { get; set; }

        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }
        public Model.Object AttributeObject { get; set; }

        public Model.Object SelectedObject { get; set; }
        public Model.Attribute SelectedAttribute { get; set; }

        #region SERIALIZE
        private RelayCommand _serialize;
        public RelayCommand Serialize
        {
            get
            {
                return _serialize ?? new RelayCommand(obj =>
                {
                    string resultStr = "Сохранено";
                    DataWorker.SaveToJson(AllObjects);
                    UpdateTreeView();
                    ShowMessageToUser(resultStr);
                    SetNullValuesToProperties();

                });
            }
        }
        #endregion

        #region COMMAND TO ADD
        private RelayCommand _addNewObject;
        public RelayCommand AddNewObject
        {
            get
            {
                return _addNewObject ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (ObjectType == null || ObjectType.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "TypeBlock");
                    }
                    if (ObjectProduct == null || ObjectProduct.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "ProductBlock");
                    }
                    else
                    {
                        resultStr = DataWorker.CreateObject(ObjectType, ObjectProduct);
                        UpdateTreeView();
                        ShowMessageToUser(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                });
            }
        }

        private RelayCommand _addNewAttribute;
        public RelayCommand AddNewAttribute
        {
            get
            {
                return _addNewAttribute ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (AttributeName == null || AttributeName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "NameBlock");
                    }
                    if (AttributeValue == null || AttributeValue.Replace(" ", "").Length == 0)
                    {

                        SetRedBlockControll(wnd, "ValueBlock");
                    }
                    if (AttributeObject == null)
                    {
                        MessageBox.Show("Укажите объект");
                    }
                    else
                    {
                        resultStr = DataWorker.CreateAttribute(AttributeName, AttributeValue, AttributeObject);
                        UpdateTreeView();
                        ShowMessageToUser(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                });
            }
        }

        #endregion

        #region COMMAND TO DELETE
        private RelayCommand _deleteObject;
        public RelayCommand DeleteObject
        {
            get
            {
                return _deleteObject ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (SelectedObject == null)
                    {
                        MessageBox.Show("Укажите объект");
                    }
                    else
                    {
                        resultStr = DataWorker.DeleteObject(SelectedObject);
                        UpdateTreeView();
                        ShowMessageToUser(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                });
            }
        }

        private RelayCommand _deleteAttribute;
        public RelayCommand DeleteAttribute
        {
            get
            {
                return _deleteAttribute ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (SelectedAttribute == null)
                    {
                        MessageBox.Show("Укажите атрибут");
                    }
                    else
                    {
                        resultStr = DataWorker.DeleteAttribute(SelectedAttribute);
                        UpdateTreeView();
                        ShowMessageToUser(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                });
            }
        }
        #endregion

        #region COMMANDS TO OPEN WINDOWS
        private RelayCommand _openAddNewObjectWindow;
        public RelayCommand OpenAddNewObjectWindow
        {
            get
            {
                return _openAddNewObjectWindow ?? new RelayCommand(obj =>
                {
                    OpenAddObjectWindowMethod();
                });
            }
        }

        private RelayCommand _openAddNewAttributeWindow;
        public RelayCommand OpenAddNewAttributeWindow
        {
            get
            {
                return _openAddNewAttributeWindow ?? new RelayCommand(obj =>
                {
                    OpenAddAttributeWindowMethod();
                });
            }
        }

        private RelayCommand _openDeleteObjectWindow;
        public RelayCommand OpenDeleteObjectWindow
        {
            get
            {
                return _openDeleteObjectWindow ?? new RelayCommand(obj =>
                {
                    OpenDeleteObjectWindowMethod();
                });
            }
        }
        private RelayCommand _openDeleteAttributeWindow;
        public RelayCommand OpenDeleteAttributeWindow
        {
            get
            {
                return _openDeleteAttributeWindow ?? new RelayCommand(obj =>
                {
                    OpenDeleteAttributeWindowMethod();
                });
            }
        }

        #endregion

        #region METHODS TO OPEN WINDOW
        private void OpenAddObjectWindowMethod()
        {
            AddNewObjectWindow newObjectWindow = new AddNewObjectWindow();
            SetCenterPositionAndOpen(newObjectWindow);
        }

        private void OpenAddAttributeWindowMethod()
        {
            AddNewAttributeWindow newAttributeWindow = new AddNewAttributeWindow();
            SetCenterPositionAndOpen(newAttributeWindow);
        }

        private void OpenDeleteObjectWindowMethod()
        {
            DeleteObjectWindow deleteObjectWindow = new DeleteObjectWindow();
            SetCenterPositionAndOpen(deleteObjectWindow);
        }

        private void OpenDeleteAttributeWindowMethod()
        {
            DeleteAttributeWindow deleteAttributeWindow = new DeleteAttributeWindow();
            SetCenterPositionAndOpen(deleteAttributeWindow);
        }

        private void SetCenterPositionAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }
        #endregion

        #region UPDATE VIEWS
        private void UpdateTreeView()
        {
            AllObjects = DataWorker.GetObjects();
            MainWindow.AllObjects.ItemsSource = null;
            MainWindow.AllObjects.Items.Clear();
            MainWindow.AllObjects.ItemsSource = AllObjects;
            MainWindow.AllObjects.Items.Refresh();

        }

        private void SetNullValuesToProperties()
        {
            AttributeName = null;
            AttributeValue = null;
            AttributeObject = null;
            ObjectType = null;
            ObjectProduct = null;
            SelectedObject = null;
        }
        #endregion

        private void SetRedBlockControll(Window wnd, string blockName)
        {
            Control block = wnd.FindName(blockName) as Control;
            block.BorderBrush = System.Windows.Media.Brushes.Red;
        }

        private void ShowMessageToUser(string message)
        {
            MessageView messageView = new MessageView(message);
            SetCenterPositionAndOpen(messageView);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
       => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
