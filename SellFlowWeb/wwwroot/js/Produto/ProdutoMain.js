
async function GetListProduto() {
    let _listaProdutos = [];
    let _error = '';

    await $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        data: "{}",
        dataType: "json",
        url: "https://localhost:5001/api/Produto",
        success: response => {
            console.log(response);
            response.dados.forEach(x => _listaProdutos.push(x));
        },
        failure: response => {
            alert("Erro ao carregar os dados: " + response);
        }
    });

    return await new Promise((resolve, reject) => {
        resolve(_listaProdutos);
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
        url: "https://localhost:5001/api/Categoria",
        success: response => {
            console.log(response);
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

function GeraTabela(list, tagHeader, tagData, actionEdit = true, actionDelete = true) {
    Object.getOwnPropertyNames(list[0])
        .forEach(prop => {
            let _th = $("<th>", { text: capitalizeFirstLetter(prop) });
            _th.attr('scope', 'col');
            $(`#${tagHeader}`).append(_th);
        })
    if (actionEdit) {
        let _th = $("<th>", { text: 'Editar' });
        _th.attr('scope', 'col');
        $(`#${tagHeader}`).append(_th);
    }
    if (actionDelete) {
        let _th = $("<th>", { text: 'Excluir' });
        _th.attr('scope', 'col');
        $(`#${tagHeader}`).append(_th);
    }

    list.forEach((item, y) => {
        console.log(`produto-item ${y + 1}`, item);
        let _tr = $("<tr>");
        _tr.attr('id', `tr${y}`);
        $(`#${tagData}`).append(_tr);

        for (const prop in item) {
            let _td = "";
            if (item[prop] === 'id') {
                _td = $("<th>", { text: item[prop] });
                _tr.attr('id', `tr${y}`);
            }
            else {
                let data = item[prop] == true ? 'Sim' : item[prop] == false ? 'Não' : item[prop] ?? '-';
                _td = $("<td>", { text: data });
            }
            $(`#tr${y}`).append(_td);

            if (prop == Object.keys(item)[Object.keys(item).length - 1]) {
                if (actionEdit) {
                    _td = $("<td>", { text: '' });
                    _td.attr('id', `editar${item['id']}`);
                    $(`#tr${y}`).append(_td);
                    let _a = $("<a>", { text: 'Editar' });
                    _a.attr('id', `linktoeditar${item['id']}`);
                    _a.attr('href', `http://localhost:4001/Produto/Editar/${item['id']}`);
                    $(`#editar${item['id']}`).append(_a);
                }
                if (actionDelete) {
                    _td = $("<td>", { text: '' });
                    _td.attr('id', `excluir${item['id']}`);
                    $(`#tr${y}`).append(_td);
                    let _a = $("<a>", { text: 'Excluir' });
                    _a.attr('id', `linktoeditar${item['id']}`);
                    _a.attr('href', `http://localhost:4001/Produto/Excluir/${item['id']}`);
                    $(`#excluir${item['id']}`).append(_a);
                }
            }

        }
    });
}

function capitalizeFirstLetter(string) {
    return string.charAt(0).toUpperCase() + string.slice(1);
}


