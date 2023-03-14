# FlaNium Desktop Driver

Реализация Selenium Remote WebDriver для автоматизации тестирования приложений Windows на платформах WinFroms и WPF.


> Для реализации полного функционала драйвера требуется библиотека: https://github.com/lanit-exp/FlaNium.WinAPI (только Selenium Java)

Command Line Options:

    --log-path : путь до файла логов драйвера

    -p, --port : порт серверного процесса драйвера

    --url-base : URL префикс
   
    -v, --verbose : подробный вывод  логов в консоль

    -s', --silent : отключение всех логов


Capabilities:

    app - Путь до тестируемого приложения.

    args - Аргументы командной строки используемые при запуске приложения.

    connectToRunningApp - Подключение к ранее запущенному процессу приложения.

    launchDelay - Статическое ожидание на запуск приложения.

    processName - Имя процесса к которому следует подключиться после запуска приложения.
        
    processFindTimeOut - Таймаут поиска процесса из параметра processName.

    injectionActivate - Использование инжекта.

    injectionDllType - Выбор библиотеки для инжекта.

    responseTimeout - Тайм-аут ответа драйвера.

___
___

>   Более подробная информация по работе с драйвером, а так же все возможности описаны здесь: https://github.com/lanit-exp/FlaNium.WinAPI