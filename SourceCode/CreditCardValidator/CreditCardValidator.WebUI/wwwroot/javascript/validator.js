function isNumberKey(evt) {
    var input = document.getElementById("cardNo");
    var charCode = (evt.which) ? evt.which : evt.keyCode;

    var keyChar = String.fromCharCode(charCode);

    if (/\d/.test(keyChar)) {
        var value = input.value.replace(/\s/g, '');
        var formattedValue = '';

        for (var i = 0; i < value.length; i++) {
            if (i > 0 && i % 4 === 0) {
                formattedValue += ' ';
            }
            formattedValue += value[i];
        }

        input.value = formattedValue;

        return true;
    }

    evt.preventDefault();
    return false;
}

function empty() {
    document.getElementById("message").setAttribute("hidden", true);
    document.getElementById("cardNo").value = "";
}

function validate($, document) {
    var creditCardNo = document.getElementById("cardNo").value.replace(/\s+/g, '');

    $.ajax({
        url: 'http://localhost:37059/CreditCardValidator',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(creditCardNo),
        success: function (response) {

            var span = document.getElementById("message");
            
            span.removeAttribute("hidden");
            if (response.status === 1000) {
                if (response.data) {
                    span.style.background = "lightgreen";
                    span.innerHTML = "Valid Card";
                } else {
                    span.style.background = "orange";
                    span.innerHTML = "Invalid Card";
                }               
            } else {
                span.innerHTML = response.message;
                span.style.background = "red";
            }
        },
        error: function (jqXHR, textStatus, errorThrown)
        {
        }
    });

}

$(document).ready(function () {
    $('#verify').click(function (e) {
        e.preventDefault();
        validate($, document);
    });
});