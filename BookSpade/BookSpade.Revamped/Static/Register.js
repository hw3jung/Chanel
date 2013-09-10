function validateDisplayName() {
    var name = $("#DisplayName").val();

    if (name.length > 0) {
        $("#DisplayName").removeClass('input-validation-error');
    }
}

function validateEmail() {
    var email = $("#UserName").val();

    if (email.length > 0) {
        $("#UserName").removeClass('input-validation-error');
    }
}

function validatePassword() {
    var password = $("#Password").val();

    if (password.length > 0) {
        $("#Password").removeClass('input-validation-error');
    }
}

function validateConfirmPassword() {
    var confirmPassword = $("#ConfirmPassword").val();

    if (confirmPassword.length > 0) {
        $("#ConfirmPassword").removeClass('input-validation-error');
    }
}

// Attach event handlers
$("#DisplayName").change(validateDisplayName);
$("#UserName").change(validateEmail);
$("#Password").change(validatePassword);
$("#ConfirmPassword").change(validateConfirmPassword);