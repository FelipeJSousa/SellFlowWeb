async function SalvarPagina(permissao = null, pagina = null) {
    var permissaoPagina = [];
    if (permissao != null) {
        $('.checkpagina:checked').each(ele => permissaoPagina.push({idpagina: ele, idPermissao: permissao}))
    }
    if (pagina != null) {
        $('.checkpermissao:checked').each(ele => permissaoPagina.push({ idpagina: pagina, idPermissao: ele}))
    }
    if (permissao.length > 0) {
        $.ajax()
    }
}
