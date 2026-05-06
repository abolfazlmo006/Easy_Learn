// init signalR
var connection = new signalR.HubConnectionBuilder()
    .withUrl("/commentHub")
    .build();

connection.on("ResponseCreateComment", function (successful, message, errors) {
    if (!successful) {
        if (errors != null) {
            for (var i = 0; i < document.getElementById("errors").childNodes.length; i++) {
                document.getElementById("errors").removeChild(document.getElementById("errors").childNodes[i]);
            }
            for (var i = 0; i < errors.length; i++) {
                var li = document.createElement("li");
                li.textContent = errors[i];
                document.getElementById("errors").appendChild(li);
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
        document.getElementById("CreateCommentVM_Private").checked = false;
        document.getElementById("CreateCommentVM_Title").value = "";
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
}
);

connection.on("ResponseUpdateComment", function (successful, message, errors) {
    if (!successful) {
        if (errors != null) {
            for (var i = 0; i < document.getElementById("errorsEdit").childNodes.length; i++) {
                document.getElementById("errorsEdit").removeChild(document.getElementById("errorsEdit").childNodes[i]);
            }
            for (var i = 0; i < errors.length; i++) {
                var li = document.createElement("li");
                li.textContent = errors[i];
                document.getElementById("errorsEdit").appendChild(li);
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
        document.getElementById("CreateForm").style.display = "block";
        document.getElementById("EditForm").style.display = "none";
        document.getElementById("commentId").value = "";
        document.getElementById("UpdateCommentVM_Title").value = "";
        document.getElementById("CreateCommentVM_Title").focus();

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
        }).then((result) => {
            if (result.isConfirmed) {
                location.reload();
            }
        });
    }
});

connection.on("ResponseEditComment", function (title, Id) {
    document.getElementById("CreateForm").style.display = "none";
    document.getElementById("EditForm").style.display = "block";
    document.getElementById("commentId").value = Id;
    document.getElementById("UpdateCommentVM_Title").value = title;
    document.getElementById("UpdateCommentVM_Title").focus();

});

connection.on("ResponseDeleteComment", function () {
    Swal.fire({
        title: "حذف شد!",
        text: "نظر شما با موفقیت حذف شد.",
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
    }).then((result) => {
        if (result.isConfirmed) {
            location.reload();
        }
    });
});

connection.on("ResponseAddToCart", function (successful, message) {
    const Toast = Swal.mixin({
        toast: true,
        position: "bottom-end",
        showConfirmButton: false,
        timer: 3000,
        timerProgressBar: true,
        didOpen: (toast) => {
            toast.onmouseenter = Swal.stopTimer;
            toast.onmouseleave = Swal.resumeTimer;
        }
    });

    if (!successful) {
        Toast.fire({
            icon: "error",
            title: message,
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
    } else {
        Toast.fire({
            icon: "success",
            title: message,
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
});

connection.start();

function create(event, token, id) {
    let private = document.getElementById("CreateCommentVM_Private").checked;
    let title = document.getElementById("CreateCommentVM_Title").value;
    if (title.trim() == "") {
        document.getElementById("titleError").textContent = "این فیلد الزامی است";
    } else {
        document.getElementById("titleError").textContent = "";
        event.preventDefault();
        connection.invoke("CreateComment", token, title, private, id);
    }
}

function update(event, token) {
    let title = document.getElementById("UpdateCommentVM_Title").value;
    let commentId = Number(document.getElementById("commentId").value);
    if (title.trim() == "") {
        document.getElementById("titleErrorEdit").textContent = "این فیلد الزامی است";
    } else {
        document.getElementById("titleErrorEdit").textContent = "";
        event.preventDefault();
        connection.invoke("UpdateComment", token, title, commentId);
    }
}

function edit(event, token, commentId) {
    event.preventDefault();
    connection.invoke("EditComment", token, commentId);
}

function cancelEdit(event) {
    event.preventDefault();
    document.getElementById("CreateForm").style.display = "block";
    document.getElementById("EditForm").style.display = "none";
    document.getElementById("commentId").value = "";
    document.getElementById("UpdateCommentVM_Title").value = "";
    document.getElementById("CreateCommentVM_Title").focus();
}

function Delete(event, token, commentId) {
    Swal.fire({
        title: "مطمئن هستید؟",
        text: "آیا می خواهید این نظر حذف شود؟",
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
            connection.invoke("DeleteComment", token, Number(commentId));
        }
    });
}


function AddToCart(event, token, courseId) {
    event.preventDefault();
    connection.invoke("AddToCart", token, Number(courseId));
}