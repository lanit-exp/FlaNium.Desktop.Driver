# FlaNium Desktop Driver

Реализация Selenium Remote WebDriver для автоматизации тестирования приложений Windows на платформах WinFroms и WPF.


> Для реализации полного функционала драйвера требуется библиотека: https://github.com/lanit-exp/FlaNium.WinAPI (только Selenium Java)

Command Line Options:

    --log-path : путь до файла логов драйвера

    -p, --port : порт серверного процесса драйвера

    --allowed-ips : Список IP-адресов, с которых разрешены соединения. Локальные адреса доступны по умолчанию. (разделитель адресов - ',' )
   
    -v, --verbose : подробный вывод  логов в консоль

    -s', --silent : отключение всех логов


Capabilities:

    {
        "capabilities": {
            "firstMatch": [
                {
                    "flanium:capabilities": {
                        "app": "src/main/resources/apps/Application.exe", // - Путь до тестируемого приложения.
                        "args": "", // - Аргументы командной строки используемые при запуске приложения.
                        "connectToRunningApp": true, // - Подключение к ранее запущенному процессу приложения.
                        "launchDelay": 2000, // - Статическое ожидание на запуск приложения.
                        "processFindTimeOut": 2000, // - Таймаут поиска процесса из параметра processName.
                        "processName": "Application", // - Имя процесса к которому следует подключиться после запуска приложения.
                        "injectionActivate": true, // - Использование инжекта.
                        "injectionDllType": "DELPHI", // - Выбор библиотеки для инжекта.
                        "responseTimeout": 30000 // - Тайм-аут ответа драйвера.
                    }
                }
            ]
        }
    }

___
___

>   Более подробная информация по работе с драйвером, а так же все возможности описаны здесь: https://github.com/lanit-exp/FlaNium.WinAPI