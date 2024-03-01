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

const ResponseStatus = {
    Valid: 1000,
    Invalid: 1001
}

const ValidationMessage = {
    Empty: "Please enter card number..",
    Valid: "Its a valid card..",
    Invalid: "Its an invalid card..",
    Error: "Sorry, Something went wrong.."
}

const PopupTitle = {
    Valid: "Success..",
    Invalid: "Sorry..",
    Error: "Oops..",
    Empty: "Warning"
}

const PopupIcon = {
    Valid: "success",
    Invalid: "error",
    Error: "error",
    Empty: "warning"
}

function popUpMessage(title, message, icon)
{
    Swal.fire({
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
            popUpMessage(PopupTitle.Invalid, ValidationMessage.Invalid, PopupIcon.Invalid)

    } else
        popUpMessage(PopupTitle.Error, ValidationMessage.Error, PopupIcon.Error)

}

function CreditCardValidator($, document, url)
{
    this.$ = $;
    this.document = document;
    this.url = url || 'http://localhost:37059/CreditCardValidator';
}

CreditCardValidator.prototype.validate = function () 
{
    let self = this;
    let creditCardNo = self.document.getElementById("cardNo").value.replace(/\s+/g, '');

    if (creditCardNo === "")
    {
        popUpMessage(PopupTitle.Empty, ValidationMessage.Empty, PopupIcon.Empty);
        return false;
    }
    $("#wait").css("display", "block");

    $.ajax({
        url: self.url,
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

CreditCardValidator.prototype.clear = function () 
{
    let self = this;
    self.document.getElementById("cardNo").value = "";
}

$(document).ready(function ()
{
    var validator = new CreditCardValidator($, document);
    $('#verify').click(function (e)
    {
        e.preventDefault();
        validator.validate();
    });
    $('#clear').click(function (e)
    {
        e.preventDefault();
        validator.clear();
    })
});