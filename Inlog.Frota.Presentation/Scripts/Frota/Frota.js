$(".incluirVeiculo").click(function () {
    $("#modIncluirVeiculo").load("/Frota/ExibirJanelaIncluiVeiculo", function () {
        $("#modIncluirVeiculo").modal();
    })
});


$(".editarVeiculo").click(function () {
    var id = $(this).attr("data-id");
    $("#modAtualizarVeiculo").load("/Frota/ExibirJanelaEditaVeiculo/" + id, function () {
        $("#modAtualizarVeiculo").modal();
    })
});


$(".excluirVeiculo").click(function () {
    var id = $(this).attr("data-id");
    $("#modExcluirVeiculo").load("/Frota/ExibirJanelaExcluiVeiculo/" + id, function () {
        $("#modExcluirVeiculo").modal();
    })
});


$(".pesquisarVeiculo").click(function () {
    $("#modPesquisarVeiculo").load("/Frota/ExibirJanelaPesquisaVeiculo", function () {
        $("#modPesquisarVeiculo").modal();
    })
});