$("#btnLogar").click(function (e) {
    e.preventDefault();

    if (validarPreenchimento()) {
        requisicaoAjax(rootUrl + "Login/Logar", $("form").serializeArray(),
            function () {
                window.location.href = rootUrl + "Emprestimo";
            });
    }

});