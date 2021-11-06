async function RealizarLogin() {
    let email = $('#email').val(),
        senha = $('#senha').val()

    await Validar(email, senha).then(async resp => {
        sessionStorage.setItem("token", resp.token);
        await Logar(resp).then(response => {
            document.location.href = '/Home/Index';
        });
    });
}

async function Logar(access) {
    let _access = '';
    await $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: WebURL + "/Login/Auth",
        headers: {
            origin: "https://localhost:4001"
        },
        dataType: 'json',
        data: JSON.stringify(access),
        success: response => {
            _access = response;
        },
        failure: response => {
            _error = response.erro;
            $('#message').text('Não foi possível realizar o login.')
        }
    });
    return await new Promise((resolve, reject) => {
        resolve(_access);
        reject(_error);
    });
}

async function Validar(email, senha) {
    let _access = '';
    let _error = '';

    let obj = {
        email: email,
        senha: senha
    }

    await $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: ApiURL + "/Acesso/Login",
        headers: {
            origin: "https://localhost:4001"
        },
        dataType: 'json',
        data: JSON.stringify(obj),
        success: response => {
            _access = response.dados;
        },
        failure: response => {
            _error = response.erro;
            $('#message').text('Não foi possível realizar o login.')
        }
    });

    return await new Promise((resolve, reject) => {
        resolve(_access);
        reject(_error);
    });
}
