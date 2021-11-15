
async function Search(text) {
    var categoria = $('#categoria').val() != 0 ? $('#categoria').val() : null;
    if (text.length > 1 && $('#search').val().replaceAll(' ', '')) {

        var _spinner = $("<span>").addClass("spinner-grow spinner-grow-sm me-1")
        $('.spinner').append(_spinner)
        var anuncios = await GetAnuncio(text, categoria);
        $('.spinner').empty();

        $('#anunciosGrid').empty();
        FillCards(anuncios);
    }
    else if (!$('#search').val()) {
        var _spinner = $("<span>").addClass("spinner-grow spinner-grow-sm me-1")
        $('.spinner').append(_spinner)
        if (categoria == null) {
            var anuncios = await GetAnuncioAll();
        }
        else {
            var anuncios = await GetAnuncio(null, categoria);
        }
        $('.spinner').empty();

        $('#anunciosGrid').empty();
        FillCards(anuncios);
    }
}


function FillCards(obj) {

    obj.forEach((item, index) => {

        var main = $('<div>').addClass('col');

        var content = $("<div>").addClass("card h-100");

        var img = $("<img>").addClass("card-img-top imgCard");

        img.attr('src', item.produtoObj.imagemDestaque ?? "");

        content.append(img);
        var promo = false;
        if (parseFloat(item.percentPromocao.replace('%')) > 0)
        {
            promo = true;
            var desconto = $("<span>").addClass("badge rounded-pill bg-success mx-2");
            desconto.text(`Desconto ${item.percentPromocao}`);

            content.append(desconto);
        }

        var body = $("<div>").addClass("card-body");

        var title = $("<h4>").addClass("card-title");

        title.text(item.nome);

        body.append(title);

        if (promo) {
            var valorDesconto = $("<h5>").addClass("card-text text-success");
            valorDesconto.text(`${item.valor.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' })} - `);

            var valorAntigo = $("<s>").addClass("card-text text-secondary");
            valorAntigo.text(item.produtoObj.valor.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }))

            valorDesconto.append(valorAntigo);

            body.append(valorDesconto);
        }
        else {

            var valor = $("<h5>").addClass("card-text");
            valor.text(item.produtoObj.valor.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }))

            body.append(valor);
        }

        var produto = $("<h6>").addClass("card-text");
        produto.text(item.produtoObj.nome);

        body.append(produto);

        var vendedor = $("<p>").addClass("card-text");
        vendedor.text(`Vendedor - ${item.vendedor}`)

        body.append(vendedor);

        var btnVisualizar = $("<a>").addClass("btn btn-primary");
        btnVisualizar.text(`Visualizar`);
        btnVisualizar.attr('href', `${WebURL}/Auncio/Visualizar/${item.id}`)

        body.append(btnVisualizar);

        content.append(body);

        var footerDia = $("<small>").addClass("text-muted");

        var parts = item.dataCriacao.split('-');
        var dateCriacao = new Date(parts[0], parts[1] - 1, parts[2].slice(0, 2));
        var dateHoje = new Date();
        var diffMs = (dateHoje.getTime() - dateCriacao.getTime());
        var diffDays = Math.floor(diffMs / 86400000);
        if (diffDays >= 1) {
            footerDia.text(`Postado a ${diffDays} dias.`);
        }
        else {
            var diffMins = Math.round(((diffMs % 86400000) % 3600000) / 60000);
            footerDia.text(`Postado a ${diffMins} minutos.`);
        }

        var footer = $("<div>").addClass("card-footer");

        footer.append(footerDia);

        content.append(body);

        content.append(footer);

        main.append(content);

        $('#anunciosGrid').append(main);

    });
}




async function GetAnuncioAll(idSituacao = 2) {
    var anuncios = [];
    var _error = "";

    await $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: ApiURL + "/Anuncio/Situacao?idSituacao=" + idSituacao,
        headers: {
            origin: WebURL
        },
        success: response => {
            anuncios = response.dados;
        },
        failure: response => {
            _error = response.erro;
            $('#message').text('Não foi possível obter os anúncios.')
        }
    });

    return await new Promise((resolve, reject) => {
        resolve(anuncios);
        reject(_error);
    });

}


async function GetAnuncio(text = null, categoria = null) {
    var anuncios = [];
    var _error = "";
    var params = text != null ? `busca=${text}&` : "";
        params += categoria != null ? `categoria=${categoria}` : "";
    if (text != null || categoria != null) {
        await $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: ApiURL + "/Anuncio?" + params,
            headers: {
                origin: WebURL
            },
            success: response => {
                anuncios = response.dados;
            },
            failure: response => {
                _error = response.erro;
                $('#message').text('Não foi possível obter os anúncios.')
            }
        });

    }

    return await new Promise((resolve, reject) => {
        resolve(anuncios);
        reject(_error);
    });

}


async function GetListCategoria() {
    let _listaCategoria = [];
    let _error = '';

    await $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        data: "{}",
        dataType: "json",
        headers: { Authorization: 'Bearer ' + sessionStorage.getItem('token') },
        url: ApiURL + "/Categoria",
        success: response => {
            response.dados.forEach(x => _listaCategoria.push(x));
        },
        failure: (response) => {
            _error = response;
        }
    });

    return await new Promise((resolve, reject) => {
        resolve(_listaCategoria);
        reject(_error);
    });
}