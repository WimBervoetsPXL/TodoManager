using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TodoManager.Application.Services;
using TodoManager.Domain.Models;

namespace TodoManager.WPF
{
    /// <summary>
    /// Interaction logic for TodoListWindow.xaml
    /// </summary>
    public partial class TodoListWindow : Window
    {
        private readonly TodoService _todoService;

        public TodoListWindow()
        {
            InitializeComponent();

            _todoService = new TodoService();
            RefreshTodos();
        }

        private void RefreshTodos()
        {
            todosListBox.Items.Clear();

            List<TodoItem> todos = _todoService.GetTodos();

            foreach (TodoItem todo in todos)
            {
                todosListBox.Items.Add(todo); // relies on TodoItem.ToString()
            }

            ClearDetails();
        }

        private void ClearDetails()
        {
            titleTextBlock.Text = "";
            dueDateTextBlock.Text = "";
            completedTextBlock.Text = "";
            completedAtTextBlock.Text = "";
            descriptionTextBlock.Text = "";
        }

        private void TodosListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // TODO: geselecteerde todo ophalen
            // TODO: call ClearDetails & return indien null
            // TODO: details tonen in TitleTextBlock, DueDateTextBlock, DescriptionTextBox, CompletedTextBlock, CompletedAtTextBlock

            //1: TodoItem selectedTodo = (TodoItem)todosListBox.SelectedItem;
            //2: TodoItem selectedTodo = todosListBox.SelectedItem as TodoItem;
            if (todosListBox.SelectedItem is TodoItem selectedTodo)
            {
                titleTextBlock.Text = selectedTodo.Title;
                dueDateTextBlock.Text = selectedTodo.DueDate.ToLongDateString();
                completedTextBlock.Text = selectedTodo.IsCompleted ? "Done" : "Open";
                completedAtTextBlock.Text = selectedTodo.CompletedAt?.ToLongDateString() ?? "";
                descriptionTextBlock.Text = selectedTodo.Description;
            }
            else
            { 
                ClearDetails();
                return;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddTodoWindow addWindow = new AddTodoWindow();
            addWindow.Owner = this;

            bool? result = addWindow.ShowDialog();
            if (result != true)
            {
                return;
            }

            // TODO: try-catch
            try
            {
                _todoService.AddTodo(addWindow.TodoTitle, addWindow.TodoDescription, addWindow.TodoDueDate);
                RefreshTodos();
            }
            catch (ArgumentException ae)
            {
                MessageBox.Show(ae.Message, "Foutieve data");
            }
            catch (InvalidOperationException ioe)
            {
                MessageBox.Show(ioe.Message, "Ongeldige actie");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Onverwachte fout");
            }
        }

        private void CompleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
