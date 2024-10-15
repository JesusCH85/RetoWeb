function fileUpload(filename) {
    var inputfile = document.getElementById(filename);
    var files = inputfile.files;
    var data = new FormData();
    for (var i = 0; i != files.lenght; i++) {
        data.append("files", files[i]);
    }
    $.ajax(
        {
            url:"/UploadFiles",
            data: fdata,
            processData: false,
            contentType: false,
            type: "post",
            success: Function(data) 
            {
                alert("Archivo subido exitosamente");
            }

        }
    );
}