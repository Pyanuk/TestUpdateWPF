using System.Configuration;
using System.Data;
using System.Windows;
using Squirrel;

namespace TestAutoUpdate
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Обработка установки/обновления Squirrel
            SquirrelAwareApp.HandleEvents(
                onInitialInstall: OnAppInstall,
                onAppUninstall: OnAppUninstall,
                onEveryRun: OnAppRun);

            // Проверка обновлений при запуске
            await CheckForUpdates();
        }

        private async Task CheckForUpdates()
        {
            try
            {
                // Замени на свой GitHub репозиторий
                var updateUrl = "https://github.com/YOUR_USERNAME/YOUR_REPO/releases";

                using var mgr = new UpdateManager(updateUrl);
                
                var updateInfo = await mgr.CheckForUpdate();
                
                if (updateInfo.ReleasesToApply.Count > 0)
                {
                    var result = MessageBox.Show(
                        $"Доступна новая версия {updateInfo.FutureReleaseEntry.Version}. Установить?",
                        "Обновление",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        await mgr.UpdateApp();
                        MessageBox.Show("Обновление установлено. Перезапустите приложение.", "Готово");
                        Current.Shutdown();
                    }
                }
            }
            catch (Exception ex)
            {
                // Игнорируем ошибки обновления, чтобы приложение запустилось
                System.Diagnostics.Debug.WriteLine($"Update check failed: {ex.Message}");
            }
        }

        private static void OnAppInstall(SemanticVersion version, IAppTools tools)
        {
            tools.CreateShortcutForThisExe(ShortcutLocation.StartMenu | ShortcutLocation.Desktop);
        }

        private static void OnAppUninstall(SemanticVersion version, IAppTools tools)
        {
            tools.RemoveShortcutForThisExe(ShortcutLocation.StartMenu | ShortcutLocation.Desktop);
        }

        private static void OnAppRun(SemanticVersion version, IAppTools tools, bool firstRun)
        {
            tools.SetProcessAppUserModelId();
        }
    }

}
