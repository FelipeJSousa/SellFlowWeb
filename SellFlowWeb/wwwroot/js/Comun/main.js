function Submit(formId) {
    $(`#${formId}`).find('.form-control').each((i, ele) => $(`#${ele.id}`).blur());
    $(`#${formId}`).find('.form-select').each((i, ele) => ele.click());
    if ($('.item-error').length > 0) {
        $('#tempmessage').text('Preencha todos os campos antes de continuar.')
    }
    else {
        $(`#${formId}`).submit();
    }
}

function ValidarCampo(field, error, { func, itemclass, labelclass} = { func : null, itemclass : "item-error", labelclass : "label-error" }) {
    var validation = false;
    func = func == undefined ? null : func;
    itemclass = itemclass == undefined ? "item-error" : itemclass;
    labelclass = labelclass == undefined ? "label-error" : labelclass;

    if (func != null) {
        validation = func;
    }
    else {
        if (field.tagName === 'PASSWORD') {
            var value = field.value.replace(" ", "");
            validation = value == '*****' ? false : field.value.length > 0;
        }
        else if (field.tagName === 'INPUT' || field.tagName === 'TEXTAREA' || field.tagName === 'EMAIL' || field.tagName === 'DATE') {
            var value = field.value.replace(" ", "");
            validation = value.length > 0;
        }
        else if (field.tagName === 'SELECT') {
            validation = field.selectedIndex != 0;
        }
    }

    Validar(field, validation, error, itemclass, labelclass);
}

function Invalidar() {
    return false;
}

function Validar(field, validation, error, itemclass, labelclass) {
    if (validation) {
        field.classList.remove(itemclass)
        if (field.nextElementSibling?.className != null) {
            if (field.nextElementSibling?.className?.includes(labelclass)) {
                field.nextElementSibling.remove();
            }
        }
    }
    else {
        var alreadyError = field.nextElementSibling?.className?.includes(labelclass);
        if (field.nextElementSibling == undefined || !alreadyError) {
            var ele = document.createElement('span');
            ele.innerText = error;
            ele.classList.add(labelclass);
            field.parentNode.appendChild(ele);
            field.classList.add(itemclass);
        }
    }
}


function ValidarCPF(cpf) {
    cpf = cpf.replace(/[^\d]+/g, '');
    if (cpf == '') return false;

    if (cpf.length != 11 ||
        cpf == "00000000000" ||
        cpf == "11111111111" ||
        cpf == "22222222222" ||
        cpf == "33333333333" ||
        cpf == "44444444444" ||
        cpf == "55555555555" ||
        cpf == "66666666666" ||
        cpf == "77777777777" ||
        cpf == "88888888888" ||
        cpf == "99999999999")
        return false;

    add = 0;

    for (i = 0; i < 9; i++) {
        add += parseInt(cpf.charAt(i)) * (10 - i);
    }
    rev = 11 - (add % 11);
    if (rev == 10 || rev == 11) {
        rev = 0;
    }

    if (rev != parseInt(cpf.charAt(9))) {
        return false;
    }

    add = 0;
    for (i = 0; i < 10; i++) {
        add += parseInt(cpf.charAt(i)) * (11 - i);
    }
    rev = 11 - (add % 11);
    if (rev == 10 || rev == 11) {
        rev = 0;
    }
    if (rev != parseInt(cpf.charAt(10))) {
        return false;
    }

    return true;
}