$(function () {
    tabela.carregar();
});

$("#btnSalvar").click(function (e) {
    e.preventDefault();

    if (validarPreenchimento()) {
        requisicaoAjax(rootUrl + 'Amigo/Salvar', $('form').serializeArray(),
            function () {
                limparCampos();
                $('#btnCancelar').addClass('invisivel');
                tabela.carregar();
            });
    }

});

$('#btnCancelar').click(function (e) {
    e.preventDefault();
    limparCampos();
    $(this).addClass('invisivel');
});

$('table#tblAmigos').on('click', '.js-editar', function (e) {

    if (confirm(MSG_09)) {

        var $colunasDataTable = $(this).parent().parent().find('td');

        $('#btnCancelar').removeClass('invisivel');
        $('#hdnCodigo').val($(this).attr('id'));
        $('#txtNome').val($($colunasDataTable[0]).text());
        $('#txtApelido').val($($colunasDataTable[1]).text());

        subirScroll();
    }
});

$('table#tblAmigos').on('click', '.js-excluir', function (e) {

    if (confirm(MSG_08)) {

        $('#hdnCodigo').val($(this).attr('id'));

        requisicaoAjax(rootUrl + 'Amigo/Excluir', $('form').serializeArray(),
             function () {
                 tabela.carregar();
             });
    }
});

var tabela = {
    carregar: function () {
        datatableBase.criarTabela({
            idTabela: 'tblAmigos',
            controller: 'Amigo',
            action: 'Listar',
            order: [[0, 'desc']],
            fnAdicionarDados: function (data) {
            },
            columns: [
                { mData: 'Nome', sTitle: 'Nome', sWidth: '40%', orderable: true },
                { mData: 'Apelido', sTitle: 'Apelido', sWidth: '40%', orderable: true },
                {
                    mData: null, sTitle: 'Ação', sWidth: '20%', orderable: false, mRender: function (valor, tipo, objeto) {

                        var retorno = '<a id=' + objeto.Codigo + ' class="js-editar" href="javascript:void(0);" ><img class="iconeDataTables" src="' + rootUrl + 'Content/Imagens/editar.png" alt="Editar Amigo" title="Editar Amigo" ></a>';
                        retorno += '<a id=' + objeto.Codigo + ' class="js-excluir" href="javascript:void(0);" ><img class="iconeDataTables" src="' + rootUrl + 'Content/Imagens/excluir.png" alt="Excluir Amigo" title="Excluir Amigo" ></a>';

                        return retorno;
                    }
                },
            ],

        });
    },
}