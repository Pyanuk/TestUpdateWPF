# Как создать релиз для автообновления

## Вариант 1: Простой (без Squirrel installer)

1. **Создай ZIP архив:**
   - Зайди в папку `publish`
   - Выдели все файлы
   - Создай ZIP архив: `TestAutoUpdate-1.0.0.zip`

2. **Загрузи на GitHub:**
   - Иди на https://github.com/Pyanuk/TestUpdateWPF/releases
   - Нажми "Create a new release"
   - Tag version: `v1.0.0`
   - Release title: `Version 1.0.0`
   - Загрузи `TestAutoUpdate-1.0.0.zip`
   - Нажми "Publish release"

3. **Измени код для работы с ZIP:**
   Нужно будет немного изменить логику обновления для работы с ZIP вместо Squirrel пакетов.

## Вариант 2: С Squirrel (рекомендуется)

### Установка Squirrel CLI:

```powershell
# Скачай Squirrel вручную:
# https://github.com/clowd/Clowd.Squirrel/releases/latest
# Распакуй squirrel.exe в папку проекта
```

### Создание релиза:

```powershell
# После того как скачал squirrel.exe:
.\squirrel.exe pack --packId TestAutoUpdate --packVersion 1.0.0 --packDirectory .\publish --releaseDir .\Releases
```

### Загрузка на GitHub:

1. Иди на https://github.com/Pyanuk/TestUpdateWPF/releases
2. Create new release с тегом `v1.0.0`
3. Загрузи ВСЕ файлы из папки `Releases`:
   - `TestAutoUpdate-1.0.0-full.nupkg`
   - `RELEASES`
   - `TestAutoUpdate-Setup.exe`

## Текущий статус:

✅ Проект собран в папке `publish`
✅ Код обновления настроен на https://github.com/Pyanuk/TestUpdateWPF/releases
⏳ Нужно создать первый релиз на GitHub

## Для следующих версий:

1. Измени версию в `TestAutoUpdate.csproj`
2. Запусти `dotnet publish TestAutoUpdate\TestAutoUpdate.csproj -c Release -r win-x64 --self-contained true -o .\publish`
3. Создай релиз с новой версией
4. Приложение автоматически обновится при следующем запуске!
