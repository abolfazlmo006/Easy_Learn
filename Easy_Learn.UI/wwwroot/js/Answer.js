var connection = new signalR.HubConnectionBuilder()
    .withUrl("/answersHub")
    .build();


connection.on("ResponseDeleteAnswer", function () {
    Swal.fire({
        title: "حذف شد!",
        text: "پاسخ شما با موفقیت حذف شد.",
        icon: "success",
        customClass: {
            popup: 'vazir',
            header: 'vazir',
            title: 'vazir',
            content: 'vazir',
            confirmButton: 'vazir',
            cancelButton: 'vazir',
            footer: 'vazir'
        }
    });
});

connection.on("ResponseAllDeleteAnswer", function (id) {
    var answer = document.getElementById("answers");

    answer.removeChild(document.getElementsByClassName("answer-" + id)[0]);
})

connection.on("ResponseCreateAnswer", function (successful, message, errors) {
    if (!successful) {
        if (errors != null) {
            for (var i = 0; i < document.getElementById("answerError").childNodes.length; i++) {
                document.getElementById("answerError").removeChild(document.getElementById("answerError").childNodes[i]);
            }
            for (var i = 0; i < errors.length; i++) {
                var li = document.createElement("li");
                li.textContent = errors[i];
                document.getElementById("answerError").appendChild(li);
            }
        } else {
            Swal.fire({
                icon: "error",
                text: message,
                customClass: {
                    popup: 'vazir',
                    header: 'vazir',
                    title: 'vazir',
                    content: 'vazir',
                    confirmButton: 'vazir',
                    cancelButton: 'vazir',
                    footer: 'vazir'
                }
            });
        }
    } else {
        document.getElementById("Description").value = "";
        Swal.fire({
            icon: "success",
            text: message,
            customClass: {
                popup: 'vazir',
                header: 'vazir',
                title: 'vazir',
                content: 'vazir',
                confirmButton: 'vazir',
                cancelButton: 'vazir',
                footer: 'vazir'
            }
        });
    }

})

connection.on("ResponseAllCreateAnswer", function (description, full_name, id, date) {

    var clone = document.getElementsByClassName("exam")[0].cloneNode(true);

    document.getElementById("answers").appendChild(clone);

    var answer = document.getElementsByClassName("exam")[1];

    answer.classList.remove("exam");
    answer.classList.add("answer-" + id);
    answer.classList.remove("d-none");

    document.getElementsByClassName("exam-description")[1].textContent = description;

    document.getElementsByClassName("exam-date")[1].textContent = date;

    document.getElementsByClassName("exam-name")[1].textContent = full_name;

    document.getElementsByClassName("exam-name")[1].classList.remove("exam-name");

    document.getElementsByClassName("exam-description")[1].classList.remove("exam-description");

    document.getElementsByClassName("exam-date")[1].classList.remove("exam-date");
})

connection.start()

function CreateAnswer(event, token, questionId) {
    event.preventDefault();
    var text = document.getElementById("Description").value;
    if (text.trim() == "") {
        document.getElementById("answerError").textContent = "فیلد الزامی است";
    } else {
        connection.invoke("CreateAnswer", token, text, questionId);
    }
}

function DeleteAnswer(event, token, answerId) {
    Swal.fire({
        title: "مطمئن هستید؟",
        text: "آیا می خواهید این پاسخ حذف شود؟",
        icon: "warning",
        showCancelButton: true,
        cancelButtonText: "خیر",
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "بله، حذف شود",
        customClass: {
            popup: 'vazir',
            header: 'vazir',
            title: 'vazir',
            content: 'vazir',
            confirmButton: 'vazir',
            cancelButton: 'vazir',
            footer: 'vazir'
        }
    }).then((result) => {
        if (result.isConfirmed) {
            event.preventDefault();
            connection.invoke("DeleteAnswer", token, answerId);
        }
    });
}