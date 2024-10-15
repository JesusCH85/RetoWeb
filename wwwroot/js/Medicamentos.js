   $(function () {
        GetMedicamentos();
});
    function GetMedicamentos() {

        alert('Test');
    $.ajax({
        url: 'https://localhost:7069/api/Medicamentos/listaMedicamentos',
    type: 'get',
    datatype: 'json',
    async: false,
    contentType: 'application/json;charset=utf-8',
    success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                var object = '';
    object += '<tr>';
        object += '<td class="text-center" colspan="8">'
            + 'No existen medicamentos.' + '</td>';
        object += '</tr>';
    $('#tblBody').html(object);
            }
    else {
                var object = '';
    $.each(response, function (index, item) {
        object += '<tr>';
    object += '<td class="text-center">' + item.nombre + '</td>';
    object += '<td>' + item.concentracion + '</td>';
    object += '<td class="text-center">' + item.idformafarmaceutica + '</td>';
    object += '<td class="text-center">' + item.precio + '</td>';
    object += '<td class="text-center">' + item.stock + '</td>';
    object += '<td class="text-center">' + item.presentacion + '</td>';
    object += '<td class="text-center">' + item.bhabilitado + '</td>';
    object += '<td class="text-center"> <a href="#" class="btn btn-outline-warning btn-sm" onclick="Edit(' + item.idMedicamento + ')">Edit</a> <a href="#" class="btn btn-outline-danger btn-sm" onclick="Delete(' + item.idMedicamento + ')">Delete</a></td>';
    object += '</tr>';
                });
$('#tblBody').html(object);
            }
        },
error: function () {
    alert('Unable to read the data.');
}
    });
}
