async function GetCep(cep) {
    let _cep = null;
    let _error = '';

    await $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: `https://viacep.com.br/ws/${cep}/json/`,
        success: response => {
            cep = response;
        },
        failure: (response) => {
            _error = response;
        }
    });

    return await new Promise((resolve, reject) => {
        resolve(_cep);
        reject(_error);
    });
}

async function GetEnderecoByPessoa(idPessoa) {
    let _listaEnderecos = [];
    let _error = '';

    await $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "https://localhost:5001/api/Endereco/ObterPorPessoa?idPessoa"+idPessoa,
        success: response => {
            response.dados.forEach(x => _listaEnderecos.push(x));
        },
        failure: (response) => {
            _error = response;
        }
    });

    return await new Promise((resolve, reject) => {
        resolve(_listaEnderecos);
        reject(_error);
    });
}

async function PostEndereco(Endereco) {
    let endereco = [];
    let _error = '';

    await $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: Endereco,
        dataType: "json",
        url: "https://localhost:5001/api/Endereco",
        success: response => {
            endereco = response.dados;
        },
        failure: (response) => {
            _error = response;
        }
    });

    return await new Promise((resolve, reject) => {
        resolve(endereco);
        reject(_error);
    });
}

async function LimparTabela(table) {
    $(`#${table} tr`).remove();
}


function PopulaTabela(list, tagData = null, actionEdit = true, actionDelete = true) {

    list.forEach((item, y) => {
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
                    _a.attr('href', `https://localhost:4001/Produto/Editar/${item['id']}`);
                    $(`#editar${item['id']}`).append(_a);
                }
                if (actionDelete) {
                    _td = $("<td>", { text: '' });
                    _td.attr('id', `excluir${item['id']}`);
                    $(`#tr${y}`).append(_td);
                    let _a = $("<a>", { text: 'Excluir' });
                    _a.attr('id', `linktoeditar${item['id']}`);
                    _a.attr('href', `https://localhost:4001/Produto/Excluir/${item['id']}`);
                    $(`#excluir${item['id']}`).append(_a);
                }
            }

        }
    });
}


function GeraTabela(list, tagHeader, tagData = null, actionEdit = true, actionDelete = true) {
    if (tagData === null) {
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
    }
    else
    {
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
                        _a.attr('href', `https://localhost:4001/Produto/Editar/${item['id']}`);
                        $(`#editar${item['id']}`).append(_a);
                    }
                    if (actionDelete) {
                        _td = $("<td>", { text: '' });
                        _td.attr('id', `excluir${item['id']}`);
                        $(`#tr${y}`).append(_td);
                        let _a = $("<a>", { text: 'Excluir' });
                        _a.attr('id', `linktoeditar${item['id']}`);
                        _a.attr('href', `https://localhost:4001/Produto/Excluir/${item['id']}`);
                        $(`#excluir${item['id']}`).append(_a);
                    }
                }

            }
        });
    }
}

function capitalizeFirstLetter(string) {
    return string.charAt(0).toUpperCase() + string.slice(1);
}


