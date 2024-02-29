function isNumberKey(evt) 
{
    let input = document.getElementById("cardNo");
    let charCode = (evt.which) ? evt.which : evt.keyCode;

    let keyChar = String.fromCharCode(charCode);

    if (/\d/.test(keyChar)) {
        let value = input.value.replace(/\s/g, '');
        let formattedValue = '';

        for (let i = 0; i < value.length; i++) {
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

const RESPONSE_STATUS = {
    VALID: 1000,
    INVALID: 1001
}

const RESPONSE_COLOR = {
    VALID: "lightgreen",
    INVALID: "orange",
    ERROR: "red"
}

const VALIDATION_MESSAGE = {
    EMPTY: "Enter card number.",
    VALID: "Its a valid card.",
    INVALID: "Its an invalid card."
}

const VALIDATION_MESSAGE_TIMEOUT = {
    EMPTY_INPUT: 4000
}

function updateResponse(span, response)
{
    if (response.status === RESPONSE_STATUS.VALID)
    {
        if (response.data)
        {
            span.style.background = RESPONSE_COLOR.VALID;
            span.innerHTML = VALIDATION_MESSAGE.VALID;
        } else
        {
            span.style.background = RESPONSE_COLOR.INVALID;
            span.innerHTML = VALIDATION_MESSAGE.INVALID;
        }
    } else
    {
        span.style.background = RESPONSE_COLOR.ERROR;
        span.innerHTML = response.message;
    }
    span.style.display = "flex";

}

function CreditCardValidator($, document, url)
{
    this.$ = $;
    this.document = document;
    this.url = url || 'http://localhost:37059/CreditCardValidator';
}

CreditCardValidator.prototype.validate = function () 
{
    let creditCardNo = this.document.getElementById("cardNo").value.replace(/\s+/g, '');
    let self = this;
    let span = self.document.getElementById("message");     

    if (creditCardNo === "")
    {
        isEmptyMessage(span);
        hideIsEmptyMessage(span);
        return false;
    }

    $("#wait").css("display", "block");

    $.ajax({
        url: this.url,
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(creditCardNo),
        success: function (response) {
                            
            updateResponse(span, response);    
            $("#wait").css("display", "none");
        },
        error: function (jqXHR, textStatus, errorThrown)
        {
        }
    });
}

CreditCardValidator.prototype.clear = function() 
{
    let self = this;

    let span = self.document.getElementById("message");
    span.style.display = "none";
    span.style.background = "";
    span.innerHTML = "";

    self.document.getElementById("cardNo").value = "";
}

function isEmptyMessage(span)
{
    span.style.background = RESPONSE_COLOR.INVALID;
    span.innerHTML = VALIDATION_MESSAGE.EMPTY;
    span.style.display = "flex";
 
}

function hideIsEmptyMessage(span)
{
    setTimeout(function ()
    {
        span.style.display = "none";
        span.style.background = "";
        span.innerHTML = "";
    }, VALIDATION_MESSAGE_TIMEOUT.EMPTY_INPUT);
}


$(document).ready(function ()
{
    var validator = new CreditCardValidator($, document);    
    $('#verify').click(function (e) {
        e.preventDefault();
        validator.validate();
    });

    $('#clear').click(function (e)
    {
        e.preventDefault();
        validator.clear();
    })
});