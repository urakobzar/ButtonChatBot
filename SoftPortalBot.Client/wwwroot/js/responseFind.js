// Файл отвечает за поиск ответа по интересующей теме.

// Название программного обеспечения.
var applicationTitle = "";

// Диалоговое окно собеседника с фразой "Я смог вам помочь?".
var dialog = document.querySelector('dialog');

// Кнопка "Назад".
var backButton = document.getElementById("backButton");

/**
 * Начать диалог с виртуальным собеседником.
 * */
function DialogBegin() {

    // Сообщение от виртуального собеседника.
    $("#botMessage").html('Выберите по какому ПО вам требуется помощь.');

    // Сместить сообщение, чтобы оно было по центру.
    document.getElementById("botMessageContainer").classList.toggle("botMessageContainer");

    // Убрать кнопку "Начать".
    $("#beginButton").empty();

    // Отобразить выпадающий список приложений с поиском.
    $("#searchInput").css({ "display": "block" });

    // Отобразить блок с кнопками.
    $("#buttons").css({ "display": "block" });
}

/**
 * Отобразить кнопки ответов базы знаний, выбрав приложение среди кнопок.
 * @param {any} appName Название программного обеспечения.
 * @param {any} appId ID программного обеспечения.
 */
function ShowKnowledgeBaseButtons(appName, appId) {
    var stringUrl = `api/Chat/${appId}`;
    $.ajax({
        method: "POST",
        url: stringUrl,
        async: false,
        data: {
            id: appId
        },
        success: function (data) {
            CreateKnowledgeBaseButtons(appName, data);
            applicationTitle = appName;
        }
    });
}

/**
 * Вернуться обратно к списку приложений.
 * */
function BackToAppList() {

    // Сообщение от виртуального собеседника.
    $("#botMessage").html('Выберите по какому ПО вам требуется помощь.');

    // Отобразить кнопку "Нет нужного ПО".
    $("#noAppButton").css({ "display": "block" });

    // Скрыть кнопку "Нет ответа на мой вопрос".
    $("#noResponseButton").css({ "display": "none" });

    // Очистить кнопки с ответами.
    $("#knowledgeBaseButtons").empty();

    // Скрыть блок с кнопкой "Назад".
    $("#backButtonContainer").css({ "display": "none" });

    // Отобразить выпадающий список приложений с поиском.
    $("#searchInput").css({ "display": "block" });

    // Отобразить кнопки с названием приложений.
    $("#appButtons").css({ "display": "block" });
}

// Создать выпадающий список приложений с поиском.
$(document).ready(function () {
    $('.js-select2').select2({
        placeholder: "Выберите приложение",
        maximumSelectionLength: 2,
        language: "ru"
    });
});

/**
 * Поиск приложения по нажатию кнопки "Искать".
 * */
function SearchApp() {
    var object = $(".select2-selection__rendered");

    // Проверка, что в выпадающем списке с поиском выбрано приложение.
    if (object[0].title == "Выберите приложение") {
        return;
    }
    ShowKnowledgeBaseButtonsByList(object[0].title);
}

/**
 * Отобразить кнопки ответов базы знаний, выбрав приложение из выпадающего списка.
 * @param {any} appName Название программного обеспечения.
 * */
function ShowKnowledgeBaseButtonsByList(appName) {
    var stringUrl = `api/ChatByList/'${appName}'`;

    console.log(stringUrl);
    $.ajax({
        method: "POST",
        url: stringUrl,
        async: false,
        data: {
            name: appName
        },
        success: function (data) {
            CreateKnowledgeBaseButtons(appName, data);
            applicationTitle = appName;
        }
    });
}

/**
 * Создать кнопки ответов базы знаний.
 * @param {any} appName Название программного обеспечения.
 * @param {any} data Ответы базы знаний по данному программному обеспечению.
 * */
function CreateKnowledgeBaseButtons(appName, data) {
    for (let i = 0; i < Object.keys(data).length; i++) {
        $("#knowledgeBaseButtons").append('<button type="button" class="btn btn-dark btn-lg"' +
            'style = "width: 175px; height: 80px; margin: 10px" onclick="ShowBotResponse(' + data[i].id + ')">' +
            data[i].name + '</button>');
    }

    // Сообщение от виртуального собеседника.
    $("#botMessage").html(`Вы выбрали <b>ПО ${appName}</b>. Выберите интересующую вас тему.`);

    // Отобразить кнопку "Нет ответа на мой вопрос".
    $("#noResponseButton").css({ "display": "block" });

    // Скрыть кнопку "Нет нужного ПО".
    $("#noAppButton").css({ "display": "none" });

    // Отобразить блок с кнопкой "Назад".
    $("#backButtonContainer").css({ "display": "block" });

    // Скрыть выпадающий список приложений с поиском.
    $("#searchInput").css({ "display": "none" });

    // Скрыть кнопки с названием приложений.
    $("#appButtons").css({ "display": "none" });
}

/**
 * Вернуться назад к странице с кнопками ответов.
 * */
function BackToKnowledgeBase() {

    // Изменить метод, который вызывается при нажатии кнопки "Назад".
    backButton.setAttribute('onclick', 'BackToAppList()');

    // Отобразить диалог с виртуальным собеседником.
    $("#botAdvise").css({ "display": "block" });

    // Скрыть ответ виртуального собеседника после диалога.
    $("#botResponse").css({ "display": "none" });
    CloseDialogWindow();
}

/**
 * Показать содержание ответа виртуального собеседника.
 * @param {number} responseId ID-ответа из базы данных.
 * */
function ShowBotResponse(responseId) {
    var stringUrl = `api/Response/${responseId}`;
    $.ajax({
        method: "POST",
        url: stringUrl,
        async: false,
        data: {
            id: responseId
        },
        success: function (data) {

            // Название ответа по интересующей теме.
            $("#responseTitle").html(data[0].name);

            // Содержание ответа по интересующей теме.
            $("#responseContent").html(data[0].content);

            // Изменить метод, который вызывается при нажатии кнопки "Назад".
            backButton.setAttribute('onclick', 'BackToKnowledgeBase()');

            // Скрыть диалог с виртуальным собеседником.
            $("#botAdvise").css({ "display": "none" });

            // Отобразить ответ виртуального собеседника после диалога.
            $("#botResponse").css({ "display": "block" });

            // Отобразить маленькое диалоговое окно.
            dialog.show();

            // Сообщение диалогового окна.
            $("#dialogMessage").html('Я вам помог?');

            // Отобразить кнопку "Да".
            $("#successButton").css({ "display": "block" });

            // Отобразить кнопку "Нет".
            $("#failureButton").css({ "display": "block" });

            // Скрыть кнопку "Попробовать искать снова".
            $("#againButton").css({ "display": "none" });

            // Скрыть кнопку "Создать заявку".
            $("#requestButton").css({ "display": "none" });
        }
    });
}

/**
 * Вывод в диалоговом окне сообщения об успешной помощи бота. 
 * */
function ShowSuccessMessage() {
    $("#dialogMessage").html('Ура!');

    // Скрыть кнопку "Да".
    $("#successButton").css({ "display": "none" });

    // Скрыть кнопку "Нет".
    $("#failureButton").css({ "display": "none" });
}

/**
 * Вывод в диалоговом окне сообщения об неуспешной помощи бота. 
 * */
function ShowFailureMessage() {
    $("#dialogMessage").html('Какие следующие шаги?');

    // Скрыть кнопку "Да".
    $("#successButton").css({ "display": "none" });

    // Скрыть кнопку "Нет".
    $("#failureButton").css({ "display": "none" });

    // Отобразить кнопку "Попробовать искать снова".
    $("#againButton").css({ "display": "block" });

    // Отобразить кнопку "Создать заявку".
    $("#requestButton").css({ "display": "block" });
}

// Событие при нажатии на крестик диалогового окна.
$(document).ready(function () {
    document.querySelector('#closeDialog').onclick = function () {
        dialog.close();
    }
});

/**
 * Закрыть диалоговое окно.
 * */
function CloseDialogWindow() {
    dialog.close();
}

/**
 * Начать поиск информации сначала.
 * */
function ShowAgain() {
    CloseDialogWindow();
    BackToKnowledgeBase();
    BackToAppList();
}
