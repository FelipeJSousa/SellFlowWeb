async function SalvarPermissaoPagina({ permissao, pagina } = { permissao : null, pagina : null }) {
    var _spinner = $("<span>").addClass("spinner-grow spinner-grow-sm me-1")

    $('#btnsalvar').text("")
    $('#btnsalvar').append(_spinner)
    $("<span>", { text: "Salvando" }).insertAfter($(".spinner-grow"));


    await sleep(1000);



    var permissaoPagina = [];
    if (permissao != null) {
        $('.checkpagina:checked').each(ele => permissaoPagina.push({idpagina: ele, idPermissao: permissao}))
    }
    if (pagina != null) {
        $('.checkpermissao:checked').each(ele => permissaoPagina.push({ idpagina: pagina, idPermissao: ele})) //get ele id
    }
    if (permissaoPagina != undefined && permissaoPagina.length > 0) {
        await $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: ApiURL + "/PermissaoPagina",
            headers: {
                origin: WebURL
            },
            dataType: 'json',
            data: JSON.stringify(permissaoPagina),
            success: response => {
                $(".spinner-grow").remove();
                $('#btnSalvarPerfil').text("Salvar");
            },
            failure: response => {
                _error = response.erro;
                $(".spinner-grow").remove();
                $('#tempmessage').text("Não foi possivel salvar.");
                $('#btnSalvarPerfil').text("Salvar");
            }
        });
    }
    else {
        $('#tempmessage').text("Não foi possivel salvar.");
    }
    $(".spinner-grow").remove();
    $('#btnSalvarPerfil').text("Salvar");

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

