using System;
using System.Windows;
using System.Windows.Controls;

namespace TestAppOne
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string firstNumber = ""; // Левый операнд
        string operationSymbol = ""; // Знак операции
        string secondNumber = ""; // Правый операнд
        string result = ""; // Результат операции

        public MainWindow()
        {
            InitializeComponent();

            foreach (UIElement element in MyRootElement.Children)
            {
                if (element is Button)
                {
                    ((Button)element).Click += WhenButtonClick;
                }
            }
        }


        private void WhenButtonClick(object sender, RoutedEventArgs e)
        {
            if (result != "")
            {
                textField.Text = "";
                firstNumber = "";
                secondNumber = "";
                operationSymbol = "";
                result = "";
            }

            // Получаем текст кнопки
            string textFromButton = (string)((Button)e.OriginalSource).Content;

            int possibleNumber;

            // Пытаемся преобразовать его в число
            bool checkIsItNumber = Int32.TryParse(textFromButton, out possibleNumber);

            // Если текст - это число
            if (checkIsItNumber == true)
            {
                // Если операция не задана
                if (operationSymbol == "")
                {
                    // Добавляем его в текстовое поле
                    textField.Text = textField.Text + textFromButton;

                    // Добавляем к левому операнду
                    firstNumber += possibleNumber;
                }
                else
                {
                    // Добавляем его в текстовое поле
                    textField.Text = textField.Text + textFromButton;

                    // Иначе к правому операнду
                    secondNumber += possibleNumber;
                }
            }
            // Если было введено не число
            else
            {
                // Если равно, то выводим результат операции
                if (textFromButton == "=")
                {
                    if (firstNumber != "" & secondNumber != "")
                    {
                        // Добавляем его в текстовое поле
                        textField.Text = textField.Text + textFromButton;
                        textField.Text += Calculation();
                    } else
                    {
                        MessageBox.Show("Введите операцию правильно");
                    }
                }
                // Очищаем поле и переменные
                else if (textFromButton == "CLEAR")
                {
                    textField.Text = "";
                    firstNumber = "";
                    secondNumber = "";
                    operationSymbol = "";
                }
                // Получаем операцию
                else
                {
                    if (firstNumber != "")
                    {
                        if (operationSymbol != "")
                        {
                            if (secondNumber == "")
                            {
                                MessageBox.Show("В операции возможен только один знак");
                                return;
                            } else
                            {
                                MessageBox.Show("Калькулятор принимает только два числа");
                                return;
                            }
                        }
                        operationSymbol = textFromButton;
                        // Добавляем его в текстовое поле
                        textField.Text = textField.Text + textFromButton;
                    } else 
                    {
                        MessageBox.Show("Введите первое число");
                    }
                }
            }
        }
        

        private string Calculation()
        {
            int num1 = Int32.Parse(firstNumber);
            int num2 = Int32.Parse(secondNumber);
            // И выполняем операцию
            switch (operationSymbol)
            {
                case "+":
                    result = (num1 + num2).ToString();
                    break;
                case "-":
                    result = (num1 - num2).ToString();
                    break;
                case "*":
                    result = (num1 * num2).ToString();
                    break;
                case "/":
                    if (secondNumber == "0")
                    {
                        result = "На ноль делить нельзя";
                    }
                    else
                    {
                        result = (num1 / num2).ToString();
                    }
                    break;
            }
            return result;
        }

    }
}
