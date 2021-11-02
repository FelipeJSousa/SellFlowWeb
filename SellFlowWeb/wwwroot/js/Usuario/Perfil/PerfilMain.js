async function SalvarPerfil() {

    var _spinner = $("<span>").addClass("spinner-grow spinner-grow-sm me-1")

    $('#btnSalvarPerfil').text("")
    $('#btnSalvarPerfil').append(_spinner)
    $("<span>", { text: "Salvando" }).insertAfter($(".spinner-grow"));


    await sleep(1000);

    const objPessoa = {
        ativo: true,
        id: $("#id").val(),
        nome: $("#nome").val(),
        sobrenome: $("#sobrenome").val(),
        cpf: $("#cpf").val(),
        usuario: $("#usuarioObj_id").val(),
        dataNascimento: $("#dataNascimento").val()
    };

    const objUsuario = {
        ativo: true,
        id: $("#usuarioObj_id").val(),
        senha: $("#usuarioObj_senha").val() === "*****" ? null : $("#usuarioObj_senha").val(),
        email: $("#usuarioObj_email").val(),
        permissao: $("#usuarioObj_permissao").val()
    }

    await SavePessoa(objPessoa).then(async responsePessoa => {
        if (responsePessoa != null && responsePessoa != undefined) {

            await SaveUsuario(objUsuario).then(async response => {
                if (response == null || response == undefined) {
                    alert('Não foi possível salvar o perfil.');
                }
            }).catch(e => {
                alert('Não foi possível salvar o perfil.');
            });

            $(".spinner-grow").remove();
            $('#btnSalvarPerfil').text("Salvar");

        }
        else {
            alert('Não foi possível salvar o perfil.');
        }
    }).catch(e => {
        alert('Não foi possível salvar o perfil.');
    });
}

async function SavePessoa(obj) {
    let pessoa = [];
    let _error = '';

    await $.ajax({
        type: "PUT",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(obj),
        dataType: "json",
        headers: { Authorization: 'Bearer ' + sessionStorage.getItem('token') },
        url: "https://localhost:5001/api/Pessoa",
        success: response => {
            pessoa = response.dados;
        },
        failure: (response) => {
            _error = response;
        }
    });

    return await new Promise((resolve, reject) => {
        resolve(pessoa);
        reject(_error);
    });
}

async function SaveUsuario(obj) {
    let pessoa = [];
    let _error = '';

    await $.ajax({
        type: "PUT",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(obj),
        dataType: "json",
        headers: { Authorization: 'Bearer ' + sessionStorage.getItem('token') },
        url: "https://localhost:5001/api/Usuario",
        success: response => {
            pessoa = response.dados;
        },
        failure: (response) => {
            _error = response;
        }
    });

    return await new Promise((resolve, reject) => {
        resolve(pessoa);
        reject(_error);
    });
}
