$("#showoldupload").uploadFile({
    url: "upload.php",
    dragDrop: true,
    fileName: "myfile",
    returnType: "json",
    showDelete: true,
    showDownload: true,
    statusBarWidth: 600,
    dragdropWidth: 600,
    maxFileSize: 200 * 1024,
    showPreview: true,
    previewHeight: "100px",
    previewWidth: "100px",

    onLoad: function (obj) {
        $.ajax({
            cache: false,
            url: "load.php",
            dataType: "json",
            success: function (data) {
                for (var i = 0; i < data.length; i++) {
                    obj.createProgress(data[i]["name"], data[i]["path"], data[i]["size"]);
                }
            }
        });
    },
    deleteCallback: function (data, pd) {
        for (var i = 0; i < data.length; i++) {
            $.post("delete.php", { op: "delete", name: data[i] },
                function (resp, textStatus, jqXHR) {
                    //Show Message	
                    alert("File Deleted");
                });
        }
        pd.statusbar.hide(); //You choice.

    },
    downloadCallback: function (filename, pd) {
        location.href = "download.php?filename=" + filename;
    }
}); 