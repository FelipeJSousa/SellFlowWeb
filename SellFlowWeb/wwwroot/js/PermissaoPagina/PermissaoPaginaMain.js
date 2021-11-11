async function SalvarPermissaoPagina({ permissao, pagina } = { permissao : null, pagina : null }) {
    var _spinner = $("<span>").addClass("spinner-grow spinner-grow-sm me-1")

    $('#btnsalvar').text("")
    $('#btnsalvar').append(_spinner)
    $("<span>", { text: "Salvando" }).insertAfter($(".spinner-grow"));

    await sleep(1000);

    if (permissao != null) {
        var permissaoObj = {
            id: $('#id').val(),
            nome: $('#nome').val()
        }
        var resp = await SalvarPermissao(permissaoObj);
        if (resp.id != undefined) {
            $('#id').val(resp.id);
            permissao = resp.id;
            var permissaoPagina = { permissao: [] };
            $('.checkpagina:checked').each((i, ele) => permissaoPagina.permissao.push({ pagina: ele.id.split('_')[1], permissao: permissao }))
            if (permissaoPagina.permissao.length == 0) {
                document.location.href = '/Permissao/Index';
            }
            resp = await PostPermissaoPagina(permissaoPagina);
            if (resp) {
                document.location.href = '/Permissao/Index';
            }
            else {
                $('#tempmessage').text("Não foi possivel salvar.")
                $(".spinner-grow").remove();
                $('#btnsalvar').text("Salvar");
            }
        }
        else {
            $('#tempmessage').text("Não foi possivel salvar.")
            $(".spinner-grow").remove();
            $('#btnsalvar').text("Salvar");
        }
    }
    else if (pagina != null) {
        var paginaObj = {
            id: $('#id').val(),
            nome: $('#nome').val(),
            caminho: $('#caminho').val()
        }
        var resp = await SalvarPagina(paginaObj);
        if (resp.id != undefined) {
            $('#id').val(resp.id);
            pagina = resp.id;
            var permissaoPagina = { pagina: [] };
            $('.checkpermissao:checked').each((i, ele) => permissaoPagina.pagina.push({ pagina: pagina, permissao: ele.id.split('_')[1] })) //get ele id
            if (permissaoPagina.pagina.length == 0) {
                document.location.href = '/Pagina/Index';
            }
            resp = await PostPermissaoPagina(permissaoPagina);
            if (resp) {
                document.location.href = '/Pagina/Index';
            }
            else {
                $('#tempmessage').text("Não foi possivel salvar.")
                $(".spinner-grow").remove();
                $('#btnsalvar').text("Salvar");
            }
        }
        else {
            $('#tempmessage').text("Não foi possivel salvar.")
            $(".spinner-grow").remove();
            $('#btnsalvar').text("Salvar");
        }
    }
    else {
        $('#tempmessage').text("Não foi possivel salvar.")
        $(".spinner-grow").remove();
        $('#btnsalvar').text("Salvar");
    }
}

async function SalvarPermissao(obj) {
    var _error = ""
    var resp = ""
    if (obj != undefined) {
        await $.ajax({
            type: obj.id > 0 ? "PUT" : "POST",
            contentType: "application/json; charset=utf-8",
            url: ApiURL + "/Permissao",
            headers: {
                origin: WebURL
            },
            dataType: 'json',
            data: JSON.stringify(obj),
            success: response => {
                resp = response.dados;
            },
            failure: response => {
                resp = false;
                _error = response.erro;
            }
        });
    }
    else {
        _error = "Não foi possível salvar a permissão.\n"
    }

    return await new Promise((resolve, reject) => {
        resolve(resp);
        reject(_error);
    });
}

async function SalvarPagina(obj) {
    var _error = ""
    var resp = ""
    if (obj != undefined) {
        await $.ajax({
            type: obj.id > 0 ? "PUT" : "POST",
            contentType: "application/json; charset=utf-8",
            url: ApiURL + "/Pagina",
            headers: {
                origin: WebURL
            },
            dataType: 'json',
            data: JSON.stringify(obj),
            success: response => {
                resp = response.dados;
            },
            failure: response => {
                resp = false;
                _error = response.erro;
            }
        });
    }
    else {
        _error = "Não foi possível salvar a permissão.\n"
    }

    return await new Promise((resolve, reject) => {
        resolve(resp);
        reject(_error);
    });
}

async function PostPermissaoPagina(obj) {
    var _error = ""
    var resp = false
    if (obj != undefined) {
        await $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: ApiURL + "/PermissaoPagina",
            headers: {
                origin: WebURL
            },
            dataType: 'json',
            data: JSON.stringify(obj),
            success: response => {
                resp = true;
            },
            failure: response => {
                resp = false;
                _error = response.erro;
            }
        });
    }
    else {
        _error = "Permissao Pagina incorretos."
    }

    return await new Promise((resolve, reject) => {
        resolve(resp);
        reject(_error);
    });
}

async function CarregarPermissaoPagina({ permissao, pagina } = { permissao : null, pagina : null })
{
    if (permissao != null) {
        var paginas = await GetPaginas(permissao);

        paginas.forEach(ele => $(`#paginaId_${ele}`).prop('checked', true))

    }
    if (pagina != null) {
        var permissoes = await GetPermissoes(pagina);

        permissoes.forEach(ele => $(`#permissaoId_${ele}`).prop('checked', true))

    }
}

async function GetPermissoes(idPagina) {
    var paginas = [];
    var _error = "";
    await $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: ApiURL + "/PermissaoPagina/Pagina/" + idPagina,
        headers: {
            origin: WebURL
        },
        success: response => {
            paginas = response.dados;
        },
        failure: response => {
            _error = response.erro;
            $('#message').text('Não foi possível obter as páginas.')
        }
    });

    return await new Promise((resolve, reject) => {
        resolve(paginas);
        reject(_error);
    });
}

async function GetPaginas(idPermissao) {
    var permissoes = [];
    var _error = "";
    await $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: ApiURL + "/PermissaoPagina/Permissao/" + idPermissao,
        headers: {
            origin: WebURL
        },
        success: response => {
            permissoes = response.dados;
        },
        failure: response => {
            _error = response.erro;
            $('#message').text('Não foi possível obter as páginas.')
        }
    });

    return await new Promise((resolve, reject) => {
        resolve(permissoes);
        reject(_error);
    });
}

