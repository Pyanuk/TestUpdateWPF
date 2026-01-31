using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Squirrel;
using System.Reflection;

namespace TestAutoUpdate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadVersion();
        }

        private void LoadVersion()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            VersionText.Text = $"Version {version?.Major}.{version?.Minor}.{version?.Build}";
        }

        private async void CheckUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            CheckUpdateButton.IsEnabled = false;
            StatusText.Text = "Проверка обновлений...";

            try
            {
                var updateUrl = "https://github.com/Pyanuk/TestUpdateWPF/releases";

                using var mgr = new UpdateManager(updateUrl);
                var updateInfo = await mgr.CheckForUpdate();

                if (updateInfo.ReleasesToApply.Count > 0)
                {
                    StatusText.Text = $"Найдена новая версия: {updateInfo.FutureReleaseEntry.Version}";
                    
                    var result = MessageBox.Show(
                        $"Доступна новая версия {updateInfo.FutureReleaseEntry.Version}.\n\nУстановить сейчас?",
                        "Обновление доступно",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        StatusText.Text = "Загрузка обновления...";
                        await mgr.UpdateApp();
                        
                        MessageBox.Show(
                            "Обновление установлено!\n\nПриложение будет перезапущено.",
                            "Обновление завершено",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                        
                        Application.Current.Shutdown();
                    }
                    else
                    {
                        StatusText.Text = "Обновление отменено";
                    }
                }
                else
                {
                    StatusText.Text = "У вас установлена последняя версия";
                    MessageBox.Show(
                        "У вас уже установлена последняя версия приложения.",
                        "Обновлений нет",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                StatusText.Text = "Ошибка при проверке обновлений";
                MessageBox.Show(
                    $"Не удалось проверить обновления:\n{ex.Message}",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            finally
            {
                CheckUpdateButton.IsEnabled = true;
            }
        }
    }
}