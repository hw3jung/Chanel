
var comments_shown = false;

function toggleComments() {
    if (comments_shown) {
        $("#commentAngle").removeClass("fa-angle-up");
        $("#commentAngle").addClass("fa-angle-down");
        $("#commentAngleLabel").text("Show Chat");
        $("#commentArea").slideUp(function () {
            comments_shown = false;
        });
    } else {
        $("#commentAngle").removeClass("fa-angle-down");
        $("#commentAngle").addClass("fa-angle-up");
        $("#commentAngleLabel").text("Hide Chat");
        $("#commentArea").slideDown(function () {
            comments_shown = true;
        });
    }
}

// Unused
function updateCountdown() {
    var remaining = 225 - $('#sendComment').val().length;
    $('#countdown').text(remaining);
}

function confirmTransactionModal() {
    $('#confirmationModal').modal({});
}

function cancelTransactionModal() {
    $('#cancellationModal').modal({});
}

function confirmTransaction() {
    var finalPrice = $("#confirmFinalPrice").val();
    $("#FinalPrice").val(finalPrice);
    $('#confirmationModal').modal('hide');
    $("#confirmTransactionForm").submit();
}

function cancelTransaction() {
    $('#cancellationModal').modal('hide');
    $("#cancelTransactionForm").submit();
}

/* // Unused
function confirmFinalPrice() {
    var finalprice = $("#txtFinalPrice").val();

    alert(finalprice);
    $.ajax({
        url: '@Url.Action("setFinalPrice", "Transaction")',
        data: {
            transactionId: "@(Model.Details.transaction.TransactionId)",
            finalprice: parseFloat(finalprice).toFixed(2)
        },
        type: 'POST',
        success: function (result) {
            $("#txtFinalPrice").attr("disabled", "disabled");
            $("#confirmPrice").hide();
        }
    });
}
*/

//updateCountdown();
//$('#sendComment').change(updateCountdown);
//$('#sendComment').keyup(updateCountdown);

$(function () {
    $("#showComments").click(toggleComments);

    $("#confirmTransaction").click(confirmTransactionModal);
    $("#cancelTransaction").click(cancelTransactionModal);

    $("#proceedConfirmTransaction").click(confirmTransaction);
    $("#proceedCancelTransaction").click(cancelTransaction);

    $('#commentList').scrollTop(10000000);
});