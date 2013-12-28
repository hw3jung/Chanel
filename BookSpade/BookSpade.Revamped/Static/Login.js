var loginForm = $("#loginForm");

$(document).ready(function () {
    loginForm.validate({
        onfocusout: function (element) { $(element).valid(); },
        errorClass: 'error',
        validClass: 'valid',
        rules: {
            UserName: {
                required: true,
                email : true
            },
            Password: {
                required: true
            }
        },
        ignore: [],
        errorPlacement: function (error, element) {
            // Set positioning based on the elements position in the form
            var elem = $(element),
                corners = ['left center', 'right center'];

            // Check we have a valid error message
            if (!error.is(':empty')) {
                // Apply the tooltip only if it isn't valid
                elem.filter(':not(.valid)').qtip({
                    overwrite: false,
                    content: error,
                    position: {
                        my: corners[0],
                        at: corners[1],
                        target: elem,
                        viewport: $(window)
                    },
                    show: {
                        event: false,
                        ready: true
                    },
                    hide: false,
                    style: {
                        classes: 'qtip-red' // Make it red... the classic error colour!
                    }
                })

                // If we have a tooltip on this element already, just update its content
                .qtip('option', 'content.text', error);
            }
            // If the error is empty, remove the qTip
            else { elem.qtip('destroy'); }
        },
        success: $.noop, // Odd workaround for errorPlacement not firing!
    });
});