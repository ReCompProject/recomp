$(function () {
    $("#cities").change(function () {
        $.ajax({
            url: "http://recomp.az/ajax/GetRegions/" + $(this).val(),
            type: "POST",
            dataType: "json",
            success: function (res) {
                $("#Regions").empty();
                $("#Regions1").empty();
                if (res.status == 200) {
                    var result = res.response;
                    for (var i = 0; i < result.length; i++) {
                        $("#Regions").append('<option value="' + result[i].Id + '" >' + result[i].Name + '</option>');
                        $("#Regions1").append('<option value="' + result[i].Id + '" >' + result[i].Name + '</option>');

                    }
                }
                
            }
        })
    })

    $("#cities1").change(function () {
        $.ajax({
            url: "http://recomp.az/ajax/GetRegions/" + $(this).val(),
            type: "POST",
            dataType: "json",
            success: function (res) {
                $("#Regions").empty();
                $("#Regions1").empty();
                if (res.status == 200) {
                    var result = res.response;
                    for (var i = 0; i < result.length; i++) {
                        $("#Regions").append('<option value="' + result[i].Id + '" >' + result[i].Name + '</option>');
                        $("#Regions1").append('<option value="' + result[i].Id + '" >' + result[i].Name + '</option>');

                    }
                };

            }
        })
    })
})