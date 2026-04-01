using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Lab8_Var34
{
    public partial class MainWindow : Window
    {
        LinkedList<string> friends = new LinkedList<string>();

        public MainWindow()
        {
            InitializeComponent();
        }

        void UpdateView()
        {
            DataBox.Items.Clear();
            foreach (var f in friends) DataBox.Items.Add(f);
        }

        void Log(string msg)
        {
            LogBox.Text += $"> {msg}\n";
            LogBox.ScrollToEnd();
        }

        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            string input = InputBox.Text.Trim();
            if (string.IsNullOrEmpty(input)) return;

            try
            {
                var parts = input.Split(new[] { ' ' }, 2);
                string cmd = parts[0].ToLower();

                if (cmd == "add" && parts.Length > 1)
                {
                    friends.AddLast(parts[1]);
                    Log($"Добавлен друг: {parts[1]}");
                }
                else if (cmd == "vip" && parts.Length > 1)
                {
                    friends.AddFirst(parts[1]);
                    Log($"Добавлен VIP: {parts[1]}");
                }
                else if (cmd == "pop_first")
                {
                    if (friends.Count == 0) throw new InvalidOperationException();
                    string val = friends.First.Value;
                    friends.RemoveFirst();
                    Log($"Удален первый: {val}");
                }
                else if (cmd == "pop_last")
                {
                    if (friends.Count == 0) throw new InvalidOperationException();
                    string val = friends.Last.Value;
                    friends.RemoveLast();
                    Log($"Удален последний: {val}");
                }
                else if (cmd == "clear")
                {
                    friends.Clear();
                    Log("Список очищен");
                }
                else
                {
                    Log("Ошибка: Неизвестная команда");
                }
            }
            catch (InvalidOperationException)
            {
                Log("Ошибка: Список пуст");
            }
            catch (Exception ex)
            {
                Log($"Ошибка: {ex.Message}");
            }

            UpdateView();
            InputBox.Clear();
        }
    }
}