$(document).ready(function () {
    $("#mc-embedded-subscribe").click(function () {
        $.ajax({
            type: "POST",
            url: "api/values",
            data: JSON.stringify({
                email: $("#mce-EMAIL").val()
            }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                alert(data);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.responseText);
            }
        });
    });
});