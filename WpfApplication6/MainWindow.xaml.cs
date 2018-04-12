using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Win10Calc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Wstępna deklaracja kontenerów na liczby używane do działań
        decimal a = 0;
        decimal b = 0;

        // Kontener na znak działania
        string sign = "";

        // Flagi do zarządzania zdarzeniami
        bool clr = true;
        bool isFirstOperation = true;
        bool isSignSet = false;
        public MainWindow()
        {
            InitializeComponent();
            txtResult.Text = "0";

        }

        // Główna funkcja licząca. Przyjmuje 3 argumenty:
        // _a - pierwsza zmienna działania
        // _b - druga zmienna działania
        // _sign - znak działania
        // Przykład: MakeEquasion(2, 3, "+") => 2+3 => zwróci decimal o wartości 5

        public decimal MakeEquasion(decimal _a, decimal _b, string _sign)
        {
            decimal result = 0;

            // W zależności od znaku inne działanie
            switch (_sign)
            {
                case "+":
                    result = _a + _b;
                    break;
                case "-":
                    result = _a - _b;
                    break;
                case "*":
                    result = _a * _b;
                    break;
                case "/":
                    if (_a != 0 || _b != 0)
                        result = _a / _b;
                    break;
                default:
                    break;
            }

            return result;
        }

        // Zdarzenie kliknięcia cyfry
        private void btnDigitClick(object sender, RoutedEventArgs e)
        {
            var symbol = (sender as Button).Content.ToString();

            // Jeżeli flaga clr == true, skasuj obecny stan textboxa
            if (clr)
                txtResult.Text = "";

            // Jeżeli jest to nowe działanie, wyzeruj obecne zmienne
            if (isFirstOperation)
            {
                a = 0;
                b = 0;
            }

            // Ustaw nowe stany dla flag i dodawaj klikniętą liczbę do textboxa 
            clr = false;
            isSignSet = false;
            txtResult.Text += symbol.ToString();
        }

        // Zdarzenie kliknięcia znaku równania
        private void btnSignClick(object sender, RoutedEventArgs e)
        {
            // Jeżeli NIE był jeszcze kliknięty znak
            if (!isSignSet)
            {
                clr = true;
                isSignSet = true;

                // Jeżeli pierwsza operacja
                if (isFirstOperation)
                {
                    // Przypisz obecną zawartość textboxa do zmiennej a
                    a = decimal.Parse(txtResult.Text);
                    // Oznacz że nie jest to już pierwsze działanie
                    isFirstOperation = false;
                }
                // Jeżeli NIE pierwsza operacja
                else
                {
                    // Przypisz obecną zawartość textboxa do zmiennej b
                    b = decimal.Parse(txtResult.Text);
                    // Wykonaj poprzednie działanie i wyświetl
                    txtResult.Text = MakeEquasion(a, b, sign).ToString();
                    // Przypisz jego wynik zmiennej a
                    a = decimal.Parse(txtResult.Text);
                }
            }
            
            sign = (sender as Button).Content.ToString();
        }

        // Zdarzenie kliknięcia przecinka
        private void btnComaClick(object sender, RoutedEventArgs e)
        {
            clr = false;

            // Sprawdź czy istnieje znak przecinka w textboxie
            if (!txtResult.Text.Contains(","))
                // Jeżeli nie, to dodaj
                txtResult.Text += ",";
        }

        // Zdarzenie kliknięcia czyszczenia
        private void btnErase_Click(object sender, RoutedEventArgs e)
        {
            // Przywracamy stan początkowy wszystkich zmiennych i flag
            a = 0;
            b = 0;
            txtResult.Text = "0";
            sign = "";
            clr = true;
            isSignSet = false;
            isFirstOperation = true;
        }

        // Zdarzenie kliknięcia znaku równości
        private void btnEq_Click(object sender, RoutedEventArgs e)
        {
            // Jeżeli ostatnio NIE był kliknięty znak działania
            if (!isSignSet)
            {
                // Jeżeli NIE jest to nowe działanie
                if (!isFirstOperation)
                {
                    b = decimal.Parse(txtResult.Text);
                }
                
                txtResult.Text = MakeEquasion(a, b, sign).ToString();
                a = decimal.Parse(txtResult.Text);
            }
            // Jeżeli ostatnio był kliknięty znak działania
            else
            {
                b = decimal.Parse(txtResult.Text);
                txtResult.Text = MakeEquasion(a, b, sign).ToString();
            }
            
            isFirstOperation = true;
            isSignSet = false;
            clr = true;
        }
    }
}
