var url = 'http://localhost:37059/CreditCardValidator';
function isNumberKey(evt) 
{
    let input = document.getElementById("cardNo");
    let charCode = (evt.which) ? evt.which : evt.keyCode;

    let keyChar = String.fromCharCode(charCode);

    if (/\d/.test(keyChar))
    {
        let value = input.value.replace(/\s/g, '');
        let formattedValue = '';

        for (let i = 0; i < value.length; i++)
        {
            if (i > 0 && i % 4 === 0)
            {
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

function popUpMessage(title, message, icon)
{
    Swal.fire(
    {
        title: title,
        text: message,
        icon: icon
    })
}

function updateResponse(response)
{
    if (response.status === ResponseStatus.Valid)
    {
        if (response.data)
            popUpMessage(PopupTitle.Valid, ValidationMessage.Valid, PopupIcon.Valid);
        else
            popUpMessage(PopupTitle.Invalid, ValidationMessage.Invalid, PopupIcon.Invalid);

    } else
        popUpMessage(PopupTitle.Error, ValidationMessage.Error, PopupIcon.Error);

}

function validate() 
{
    let creditCardNo = document.getElementById("cardNo").value.replace(/\s+/g, '');

    if (creditCardNo === "")
    {
        popUpMessage(PopupTitle.Empty, ValidationMessage.Empty, PopupIcon.Empty);
        return false;
    }
    $("#wait").css("display", "block");

    $.ajax({
        url: url,
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(creditCardNo),
        success: function (response)
        {
            updateResponse(response);
            $("#wait").css("display", "none");
        },
        error: function (jqXHR, textStatus, errorThrown)
        {
        }
    });
}

function clearCard() 
{
    document.getElementById("cardNo").value = "";
}