
async function GetListSituacao() {
    let _listaSituacao = [];
    let _error = '';

    await $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: ApiURL + "/AnuncioSituacao",
        headers: { Authorization: 'Bearer ' + sessionStorage.getItem('token') },
        success: response => {
            response.dados.forEach(x => _listaSituacao.push(x));
        },
        failure: response => {
            alert("Erro ao carregar os dados: " + response);
        }
    });

    return await new Promise((resolve, reject) => {
        resolve(_listaSituacao);
        reject(_error);
    });
}

function capitalizeFirstLetter(string) {
    return string.charAt(0).toUpperCase() + string.slice(1);
}


