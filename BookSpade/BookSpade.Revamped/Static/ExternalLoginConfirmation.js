function validateEmail() {
    var email = $("#UserName").val();

    if (email.length > 0) {
        $(".field-validation-error").css('display', 'none');
        $("#UserName").removeClass('input-validation-error');
    }
}

$(function () {
    $("#UserName").change(validateEmail);
});