
document.getElementById("btnLogin").onclick = function () {
    validateInputs();
}

function validateInputs() {
    const inputUser = document.getElementById("user");
    const spanUser = document.getElementById("span-login-user");
    let isUserValid = false;

    const inputPassword = document.getElementById("password");
    const spanPassword = document.getElementById("span-login-password");
    let isPasswordValid = false;

    if (inputUser.value == "") {
        inputUser.style.borderColor = "#dc3545"
        spanUser.classList.replace("d-none", "d-block");
    }
    else {
        inputUser.style.borderColor = "#ced4da"
        spanUser.classList.replace("d-block", "d-none");
        isUserValid = true;
    }

    if (inputPassword.value == "") {
        inputPassword.style.borderColor = "#dc3545"
        spanPassword.classList.replace("d-none", "d-block");
    }
    else {
        inputPassword.style.borderColor = "#ced4da"
        spanPassword.classList.replace("d-block", "d-none");
        isPasswordValid = true;
    }

    if (isUserValid && isPasswordValid) {
        login(inputUser.value, inputPassword.value)
    }
}

function login(user, password) {

    const payload = {
        username: user,
        password: password
    };

    $.ajax({
        url: "/json/client/login",
        type: "POST",
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify(payload),
        success: function (response) {
            const data = response.result;

            if (data == undefined) {
                document.getElementById("msgError").innerText = "Falha ao realizar o login1."
                return;
            }

            if (data.status == "success") {
                const idClient = btoa(data.loginClient.clientId)
                sessionStorage.setItem("idClient", idClient);
                window.location.href = `/index`;
            }
            else
                document.getElementById("msgError").innerText = "Usuário ou Senha incorretos."
        },
        error: function () {
            document.getElementById("msgError").innerText = "Falha ao realizar o login."
        }
    });

}