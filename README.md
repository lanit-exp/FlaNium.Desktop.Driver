# FlaNium Desktop Driver

Реализация Selenium Remote WebDriver для автоматизации тестирования приложений Windows на платформах WinFroms и WPF.


Для реализации полного функционала драйвера требуется библиотека: https://github.com/lanit-exp/FlaNium.WinAPI (только Selenium Java)

## Capabilities:

* app - абсолютный путь до тестируемого приложения;
* args - аргументы запуска тестируемого приложения;
* debugConnectToRunningApp - подключение к экземпляру ранее запущенного приложения (true/false - по умолчанию);
* innerPort - установка порта (по умолчанию 9998);
* launchDelay - задержка после запуска приложения в мс (по умолчанию 0).
