$(document).ready(function () {
    document.getElementById("emails-table").innerHTML = '<tr> <th> Email </th> <th> Created_at </th> </tr>';
        $.ajax({
            type: "GET",
            url: "api/values?limit=2",
            async: false,
            contentType: "application/json",
            success: function (data, status) {
                $.each(data, function (index) {
                    document.getElementById("emails-table").innerHTML += '<tr> <th>' + data[index].email + '</th> <th>' + data[index].created_at + '</th> </tr>';                   
                });
            }
         });

        $('#emailSubmit').click(function () {
            alert($("input[type=file]")[0].files[1].name);
           
            $.ajax({
                type: "POST",
                url: "api/values/email",
                data: JSON.stringify({
                    subject: $('#emailSubject').val(),
                    textBody: $('#emailBody').val()

                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    alert(data);
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.error);
                }
            });
        });

        $("#pageMinus").click(function () {

            if ($('#page1').text() != '1')  
            {
                decrementPages();
            }
        });

        

        $("#pagePlus").click(function () {
            incrementPages();
        });

        $('#sendEmail').click(function () {
            $('#myModal').show();
        });

        $('#closeModal').click(function () {
            $('#myModal').hide();
        });

     
});

function incrementPages() {
    var a = parseInt($('#page1').text()) + 1;
    $('#page1').text(a);
    a = parseInt($('#page2').text()) + 1;
    $('#page2').text(a);
    a = parseInt($('#page3').text()) + 1;
    $('#page3').text(a);
    a = parseInt($('#page4').text()) + 1;
    $('#page4').text(a);
    a = parseInt($('#page5').text()) + 1;
    $('#page5').text(a);
    a = parseInt($('#page6').text()) + 1;
    $('#page6').text(a);
}

function decrementPages() {
    var a = parseInt($('#page1').text()) - 1;
    $('#page1').text(a);
    a = parseInt($('#page2').text()) - 1;
    $('#page2').text(a);
    a = parseInt($('#page3').text()) - 1;
    $('#page3').text(a);
    a = parseInt($('#page4').text()) - 1;
    $('#page4').text(a);
    a = parseInt($('#page5').text()) - 1;
    $('#page5').text(a);
    a = parseInt($('#page6').text()) - 1;
    $('#page6').text(a);
}

function theFunction(event) {
    $(document).ready(function () {
     document.getElementById("emails-table").innerHTML = '<tr> <th> Email </th> <th> Created_at </th> </tr>';
        $.ajax({
        type: "GET",
        url: "http://localhost:50228/api/values?pageOffset=" + $(event.target).text() + "&limit=2",
        async: false,
        contentType: "application/json",
        success: function (data, status) {
            $.each(data, function (index) {
                document.getElementById("emails-table").innerHTML += '<tr> <th>' + data[index].email + '</th> <th>' + data[index].created_at + '</th> </tr>';
            });
            }
        });
    });
}

window.onclick = function (event) {
    if (event.target == document.getElementById('myModal')) {
        $('#myModal').hide();
    }
}