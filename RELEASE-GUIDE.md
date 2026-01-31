# Автоматический релиз - Инструкция

## Как создать новый релиз автоматически

Теперь все делается одной командой! 🚀

### Способ 1: Автоматический (рекомендуется)

Просто запусти скрипт с новой версией:

```powershell
.\make-release.ps1 -Version 1.0.2
```

Скрипт автоматически:
1. ✅ Обновит версию в `.csproj`
2. ✅ Закоммитит изменения
3. ✅ Создаст и запушит тег `v1.0.2`
4. ✅ GitHub Actions соберет проект
5. ✅ Создаст Squirrel пакеты
6. ✅ Опубликует релиз на GitHub

### Способ 2: Вручную

```powershell
# 1. Измени версию в TestAutoUpdate.csproj
# 2. Закоммить и запушить
git add .
git commit -m "Bump version to 1.0.2"
git push

# 3. Создать тег
git tag v1.0.2
git push origin v1.0.2
```

GitHub Actions сделает все остальное!

## Как это работает

1. При пуше тега `v*.*.*` запускается GitHub Actions
2. Actions собирает проект для Windows x64
3. Скачивает Squirrel Tools
4. Создает релиз-пакеты
5. Автоматически публикует Release на GitHub со всеми файлами

## Проверка статуса

Следи за процессом сборки:
https://github.com/Pyanuk/TestUpdateWPF/actions

## Что будет в релизе

- `RELEASES` - файл с информацией о версиях
- `TestAutoUpdate-X.X.X-full.nupkg` - полный пакет
- `TestAutoUpdate-X.X.X-delta.nupkg` - дельта-обновление (если есть предыдущая версия)
- `TestAutoUpdateSetup.exe` - установщик

## Пример использования

```powershell
# Создать версию 1.0.2
.\make-release.ps1 -Version 1.0.2

# Создать версию 2.0.0
.\make-release.ps1 -Version 2.0.0
```

Готово! Больше не нужно вручную создавать релизы! 🎉
