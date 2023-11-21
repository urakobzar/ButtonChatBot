// Файл отвечает за формирование заявки пользователя.

$(document).ready(function () {

    /**
     * Показывает текущий шаг оформления заявки.
     */
    var status = 0;

    /**
     * Предыдущее выбранного программное обеспечение.
     */
    var previousTitle = "";

    /**
     * Верхнее сообщение виртуального собеседника.
     */
    var upperMessage = document.getElementsByClassName("upperMessage");

    /**
     * Нижнее сообщение виртуального собеседника.
     */
    var lowerMessage = document.getElementsByClassName("lowerMessage");

    /**
     * ID причины обращения "Установка".
     */
    var indexInstall = document.getElementsByClassName("installReason")[0].value;

    /**
     * Причина обращения.
     */
    var reasonValue;

    /**
     * Название программного обеспечения.
     */
    var applicationName =
        document.querySelector('#searchFormRequest').querySelectorAll('.select2-selection__rendered');

    /**
     * Список приложений.
     */
    var selectOptions =
        document.querySelector('#searchFormRequest').querySelectorAll('.select2-hidden-accessible');

    /**
     * Элементы списка приложений.
     */
    var options = selectOptions[0].options;

    /**
     * ID приложения для заявки.
     */
    var applicationIdPost = document.getElementById("applicationId");

    /**
     * ID причины обращения для заявки.
     */
    var requestReasonIdPost = document.getElementById("requestReasonId");

    /**
     * Описание проблемы пользователя для заявки.
     */
    var problemDescriptionPost = document.getElementById("problemDescription");

    /**
     * Номер компьютера пользователя для заявки.
     */
    var computerNumberPost = document.getElementById("computerNumber");

    /**
     * Модальное окно подтверждения создания заявки.
     */
    var myModal = new bootstrap.Modal(document.getElementById('AddModal'),
        {
            keyboard: false
        });

    // Проверка на наличие символов помимо цифр при вводе номера компьютера.
    $("#requestComputerNumber").on('input',
        function (e) {
            this.value = this.value.replace(/[^0-9\.]/g, '');
        });

    /**
     * Загрузка файлов на сервер.
     * */
    function uploadFiles() {

        /**
         * Элемент веб-страницы для прикрепления файлов.
         */
        var input = document.getElementById("files");

        /**
         * Прикрепленные файлы.
         */
        var files = input.files;

        /**
         * Переменная для загрузки файлов на сервер.
         */
        var formData = new FormData();

        for (var i = 0; i != files.length; i++) {
            formData.append("files", files[i]);
        }

        $.ajax(
            {
                url: "/Home/Index",
                data: formData,
                async: false,
                processData: false,
                contentType: false,
                type: "POST"
            }
        );
    }

    /**
     * Отобразить окно формы создания заявки.
     */
    function showRequestForm() {
        // Скрыть диалог с виртуальным собеседником.
        $("#botAdvise").css({ "display": "none" });

        // Скрыть ответ виртуального собеседника после диалога.
        $("#botResponse").css({ "display": "none" });

        // Отобразить форму создания заявки.
        $("#requestForm").css({ "display": "block" });

        // Скрыть блок с кнопкой "Назад".
        $("#backButtonContainer").css({ "display": "none" });

        // Отобразить блок с кнопкой "На главную".
        $("#backHomeContainer").css({ "display": "block" });

        status = 1;
    }

    /**
     * Отобразить сообщение об успешном формировании заявки.
     */
    function showSuccessRequestFormation() {
        $('.upperMessage').css('backgroundColor', 'dodgerblue');
        $('.lowerMessage').css('backgroundColor', 'dodgerblue');

        // Скрыть верхнее сообщение виртуального собеседника.
        $("#upperContainerInfoMessage").css({ "display": "none" });

        // Отобразить верхнее итоговое сообщение виртуального собеседника.
        $("#upperFinalInfoMessage").css({ "display": "block" });

        // Скрыть нижнее сообщение виртуального собеседника.
        $("#lowerContainerInfoMessage").css({ "display": "none" });

        // Отобразить нижнее итоговое сообщение виртуального собеседника.
        $("#lowerFinalInfoMessage").css({ "display": "block" });

        // Скрыть кнопку "Назад".
        $("#backwardButton").css({ "display": "none" });

        // Скрыть кнопку "Создать".
        $("#confirmButton").css({ "display": "none" });

        // Скрыть данные пользователя.
        $("#userDescription").css({ "display": "none" });
    }

    // Событие при нажатии на кнопку "Нет ответа на мой вопрос".
    $("#noResponseButton").click(function () {
        showRequestForm();

        // Программное обеспечение в выпадающем списке с поиском.

        applicationName[0].title = applicationTitle;
        applicationName[0].innerText = applicationTitle;

        for (let i = 1; i < options.length; i++) {
            if (options[i].innerText == applicationTitle) {

                // Выбор программного обеспечения, которое пользователь указал до оформления заявки.
                selectOptions[0].selectedIndex = i;
                break;
            }
        }
    });

    // Событие при нажатии на кнопку "Нет нужного ПО".
    $("#noAppButton").click(function () {
        showRequestForm();
    });

    // Событие при нажатии на кнопку "Создать заявку".
    $("#requestButton").click(function () {
        CloseDialogWindow();
        showRequestForm();
    });

    // Событие при нажатии на кнопку "Далее".
    $("#forwardButton").click(function () {

        // Нажатие кнопки на шаге "Шаг 1. Выберите приложение.".
        if (status == 1) {

            // Если пользователь не выбрал приложение.
            if (applicationName[0].title == "Выберите приложение") {

                // Сообщение об ошибке.
                $("#errorMessage").html(`Выберите приложение!`);
                return;
            }

            // Фиксация последнего выбранного приложения.
            if (previousTitle == "") {
                previousTitle = applicationName[0].title;
            }

            // Очистка всех последующих шагов при смене приложения.
            if (previousTitle != applicationName[0].title) {
                var radioButton = document.getElementsByName("options-outlined");
                for (let i = 0; i < radioButton.length; i++) {
                    radioButton[i].checked = false;
                }
                $('#description').val('');
                $('#requestComputerNumber').val('');
                $('#files').val('');
                previousTitle = applicationName[0].title;
            }
            $("#errorMessage").html("");
            $("#reasonButtons").css({ "display": "block" });
            $("#searchFormRequest").css({ "display": "none" });
            $("#backwardButton").css({ "display": "block" });
            $("#lowerMessage").css({ "display": "none" });
            $("#upperInfoMessage").html(`Шаг 2. Выберите тип обращения.`);
            for (let i = 0; i < upperMessage.length; i++) {
                upperMessage[i].style.backgroundColor = "coral";
            }

            // Присвоение ID приложения для оформления заявки.
            applicationIdPost.value = selectOptions[0].selectedIndex;
            status = 2;
            return;
        }

        // Нажатие кнопки на шаге "Шаг 2. Выберите причину обращения.".
        if (status == 2) {
            reasonValue = $('input[name="options-outlined"]:checked').val();
            if (reasonValue == undefined) {
                $("#errorMessage").html(`Выберите причину обращения!`);
                return;
            }
            $("#errorMessage").html("");

            // Скрыть кнопки причины обращения.
            $("#reasonButtons").css({ "display": "none" });

            // Отобразить нижнее сообщение виртуального собеседника.
            $("#lowerMessage").css({ "display": "flex" });

            $("#upperInfoMessage").html(`Шаг 3. Опишите проблему и прикрепите файлы, если необходимо.`);
            $("#lowerInfoMessage").html(`При заявке на установку укажите нужную версию ПО.`);
            for (let i = 0; i < lowerMessage.length; i++) {
                lowerMessage[i].style.backgroundColor = "darkgray";
            }

            // Отобразить нижнее сообщение виртуального собеседника.
            $("#userDescription").css({ "display": "block" });

            // Присвоение ID причины обращения для оформления заявки.
            requestReasonIdPost.value = reasonValue;
            if (reasonValue != indexInstall) {

                // Скрыть кнопку "Далее".
                $("#forwardButton").css({ "display": "none" });

                // Отобразить кнопку "Создать".
                $("#confirmButton").css({ "display": "block" });
            }
            status = 3;
            return;
        }
        // Нажатие кнопки на шаге "Шаг 3. Опишите свою проблему.".
        if (status == 3) {
            if ($("#description").val() == "") {
                $("#errorMessage").html(`Опишите свою проблему!`);
                return;
            }
            $("#errorMessage").html("");

            // Присвоение текста проблемы для оформления заявки.
            problemDescriptionPost.value = $("#description").val();
            if (reasonValue == indexInstall) {
                $("#upperInfoMessage").html(`Шаг 4. Введите номер вашего компьютера.`);

                // Скрыть нижнее сообщение виртуального собеседника.
                $("#lowerMessage").css({ "display": "none" });

                // Скрыть описание проблемы пользователя.
                $("#description").css({ "display": "none" });

                // Скрыть поле для прикрепления вложений пользователя.
                $("#attachments").css({ "display": "none" });

                // Отобразить поле ввода номера компьютера.
                $("#requestComputerNumber").css({ "display": "block" });

                // Скрыть кнопку "Далее".
                $("#forwardButton").css({ "display": "none" });

                // Отобразить кнопку "Создать".
                $("#confirmButton").css({ "display": "block" });
                status = 4;
            } else {
                showSuccessRequestFormation();
            }
            return;
        }
    });

    // Событие при нажатии кнопки "Назад".
    $("#backwardButton").click(function () {

        // Нажатие кнопки на шаге "Шаг 2. Выберите причину обращения.".
        if (status == 2) {
            $("#errorMessage").html("");
            $("#reasonButtons").css({ "display": "none" });
            $("#searchFormRequest").css({ "display": "block" });
            $("#backwardButton").css({ "display": "none" });
            $("#lowerMessage").css({ "display": "flex" });
            $("#upperInfoMessage").html(`Для подачи заявки необходимо заполнить некоторую информацию.`);
            for (var i = 0; i < upperMessage.length; i++) {
                upperMessage[i].style.backgroundColor = "dodgerblue";
            }
            status = 1;
        }

        // Нажатие кнопки на шаге "Шаг 3. Опишите свою проблему.".
        if (status == 3) {
            $("#errorMessage").html("");
            $("#reasonButtons").css({ "display": "block" });
            $("#userDescription").css({ "display": "none" });
            $("#lowerMessage").css({ "display": "none" });
            $("#upperInfoMessage").html(`Шаг 2. Выберите тип обращения.`);
            $("#lowerInfoMessage").html(`Шаг 1. Выберите ПО.`);
            for (let i = 0; i < lowerMessage.length; i++) {
                lowerMessage[i].style.backgroundColor = "coral";
            }
            if (reasonValue != indexInstall) {
                $("#forwardButton").css({ "display": "block" });
                $("#confirmButton").css({ "display": "none" });
            }
            status = 2;
        }

        // Нажатие кнопки на шаге "Шаг 4. Введите номер компьютера.".
        if (status == 4) {
            $("#upperInfoMessage").html(`Шаг 3. Опишите проблему и прикрепите файлы, если необходимо.`);
            $("#lowerMessage").css({ "display": "flex" });
            $("#description").css({ "display": "block" });
            $("#attachments").css({ "display": "block" });
            $("#requestComputerNumber").css({ "display": "none" });
            status = 3;
        }
    });


    // Событие при нажатии кнопки "Создать".
    $("#confirmButton").click(function () {

        // Нажатие кнопки на шаге "Шаг 3. Опишите свою проблему.".
        if (status == 3) {
            if ($("#description").val() == "") {
                $("#errorMessage").html(`Опишите свою проблему!`);
                return;
            }

            // Присвоение текста проблемы для оформления заявки.
            problemDescriptionPost.value = $("#description").val();
        }

        // Нажатие кнопки на шаге "Шаг 4. Введите номер компьютера.".
        if (status == 4) {
            if ($("#requestComputerNumber").val() == "") {
                $("#errorMessage").html(`Введите номер компьютера!`);
                return;
            } else if ($("#requestComputerNumber").val().length < 4) {
                $("#errorMessage").html(`Номер компьютера должен состоять из 4 цифр!`);
                return;
            }

            // Присвоение номера компьютера для оформления заявки.
            computerNumberPost.value = $("#requestComputerNumber").val();
        }
        $("#errorMessage").html("");
        myModal.show();
        return;
    });

    // Событие при нажатии кнопки "Да" в модальном окне подтверждения.
    $("#createRequestButton").click(function () {
        myModal.hide();

        // Последний шаг был "Шаг 3. Опишите свою проблему.".
        if (status == 3) {
            showSuccessRequestFormation();
        }

        // Последний шаг был "Шаг 4. Введите номер компьютера.".
        if (status == 4) {
            $("#lowerMessage").css({ "display": "flex" });
            showSuccessRequestFormation();
        }
        var requestForm = $("#saveForm").serialize();
        uploadFiles();
        $.ajax({
            type: "POST",
            url: "/Home/Create",
            async: false,
            data: requestForm
        });
    });
});
