
async function GetCep(cep) {
    let _cep = null;
    let _error = '';

    await $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: `https://viacep.com.br/ws/${cep}/json/`,
        
        success: response => {
            console.log('Inner Cep', response);
            _cep = response;
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
        
async function SalvarEndereco() {
    const enderecoObj = {
        ativo: true,
        pessoa: $("#id").val(),
        id: $("#EnderecoId").val(),
        cep: $("#EnderecoCep").val(),
        logradouro: $("#EnderecoLogradouro").val() + ', ' + $("#EnderecoNumero").val(),
        bairro: $("#EnderecoBairro").val(),
        cidade: $("#EnderecoCidade").val(),
        Estado: $("#EnderecoEstado").val()
    };
    await SendEndereco(enderecoObj).then(async response => {
        if (response != null && response != undefined) {
            var listEndereco = await GetEnderecoByPessoa(enderecoObj.pessoa);
            await LimparTabela('tbdata');
            PopulaTabela(EnderecoMap(listEndereco), 'tbdata');
            limparFormEndereco();
            $('#FecharModalEndereco').click();
        }
        else {
            alert('Não foi possível salvar o endereço.');
        }
    }).catch(e => {
        alert('Não foi possível salvar o endereço.');
    });
}

async function GetEnderecoByPessoa(idPessoa) {
    let _listaEnderecos = [];
    let _error = '';

    await $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        headers: { Authorization: 'Bearer ' + sessionStorage.getItem('token') },
        url: "https://localhost:5001/api/Endereco/ObterPorPessoa?idPessoa="+idPessoa,
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

async function SendEndereco(obj) {
    let endereco = [];
    let _error = '';

    await $.ajax({
        type: obj.id > 0 ? "PUT" : "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(obj),
        dataType: "json",
        headers: { Authorization: 'Bearer ' + sessionStorage.getItem('token') },
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

async function EditarEndereco(id) {
    var idPessoa = $('#id').val();
    var listEndereco = await GetEnderecoByPessoa(idPessoa);
    var endereco = listEndereco.find(x => x.id == id);
    $("#EnderecoId").val(endereco.id)
    $("#EnderecoCep").val(endereco.cep)
    $("#EnderecoLogradouro").val(endereco.logradouro.split(',')[0].trim())
    $("#EnderecoNumero").val(endereco.logradouro.split(',')[1].trim())
    $("#EnderecoBairro").val(endereco.bairro)
    $("#EnderecoCidade").val(endereco.cidade)
    $("#EnderecoEstado").val(endereco.estado)
    $("#btnshowModalEndereco").click();
}

async function ExcluirEndereco(id) {
    let endereco = [];
    let _error = '';

    await $.ajax({
        type: "DELETE",
        contentType: "application/json; charset=utf-8",
        url: "https://localhost:5001/api/Endereco?id="+id,
        headers: { Authorization: 'Bearer ' + sessionStorage.getItem('token') },
        success: response => {
            $(`#linktoeditar_${id}`).closest('tr').remove()
        },
        failure: (response) => {
            alert('Não foi possível excluir o endereço.');
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

function EnderecoMap(response) {
    return response.map(x => {
        return {
            id: x.id,
            logradouro: x.logradouro,
            bairro: x.bairro,
            cidade: x.cidade,
            estado: x.estado,
            cep: x.cep
        }
    })
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
                    let _a = $("<a>", { text: '' }).addClass("btn btn-primary");
                    _a.attr('id', `linktoeditar_${item['id']}`);
                    _a.attr('onclick', `EditarEndereco(${item['id']})`);

                    let _i = $("<i>").addClass("bi bi-pencil-square")

                    _a.append(_i);
                    $(`#editar${item['id']}`).append(_a);
                }
                if (actionDelete) {
                    _td = $("<td>", { text: '' });
                    _td.attr('id', `excluir${item['id']}`);
                    $(`#tr${y}`).append(_td);
                    let _a = $("<a>", { text: '' }).addClass("btn btn-secondary");
                    _a.attr('id', `linktoexcluir_${item['id']}`);
                    _a.attr('onclick', `ExcluirEndereco(${item['id']})`);
                    _a.css(["btn", "btn-secondary"]);

                    let _i = $("<i>").addClass("bi bi-trash-fill");

                    _a.append(_i);
                    $(`#excluir${item['id']}`).append(_a);
                }
            }

        }
    });
}

function limparFormEndereco(limparCep = true) {
    if (limparCep) {
        $("#EnderecoCep").val("");
    }
    $("#EnderecoId").val("0");
    $("#EnderecoLogradouro").val("");
    $("#EnderecoBairro").val("");
    $("#EnderecoCidade").val("");
    $("#EnderecoEstado").val("");
}

function capitalizeFirstLetter(string) {
    return string.charAt(0).toUpperCase() + string.slice(1);
}

$('#EnderecoCep').on('blur', async () => {

    var inputCep = $("#EnderecoCep").val();
    await GetCep(inputCep).then(response => {
        if (response != null && response != undefined) {
            $("#EnderecoId").val("0");
            $("#EnderecoCep").val(response.cep);
            $("#EnderecoLogradouro").val(response.logradouro);
            $("#EnderecoBairro").val(response.bairro);
            $("#EnderecoCidade").val(response.localidade);
            $("#EnderecoEstado").val(response.uf);
        }
    }).catch(e => {
        console.log(`error CEP`, e);
        limparFormEndereco(false);
        ValidarCampo($('#EnderecoCep')[0], 'Informe um CEP válido.', { func: Invalidar() });
    });

});
