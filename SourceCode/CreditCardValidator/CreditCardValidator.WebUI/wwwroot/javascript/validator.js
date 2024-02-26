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

        if (formattedValue.length > 18) {
            evt.preventDefault();
            return false;
        }

        return true;
    }

    evt.preventDefault();
    return false;
}

function empty() {
    document.getElementById("message").setAttribute("hidden", true);
    document.getElementById("cardNo").value = "";
}

function validate() {
    var creditCardNo = document.getElementById("cardNo").value.replace(/\s+/g, '');

    $.ajax({
        url: 'https://localhost:44323/api/CardValidator',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(creditCardNo),
        success: function (data) {

            var span = document.getElementById("message");
            span.innerHTML = data.message;
            span.removeAttribute("hidden");
            if (data.isValid) {
                span.style.background = "lightgreen";
            } else {
                span.style.background = "orange";
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
        }
    });

}