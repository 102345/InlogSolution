﻿$(document).ready(function () {

    $("#optCaminhao").click(SetNroPassageirosCaminhao);

    $("#optOnibus").click(SetNroPassageirosOnibus);

});


function SetNroPassageirosCaminhao() {

    $('#txtNroPassageiros').val(2);
}

function SetNroPassageirosOnibus() {

    $('#txtNroPassageiros').val(42);
}