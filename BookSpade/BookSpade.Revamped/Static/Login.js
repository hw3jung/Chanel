var createPostForm = $("#createPostForm");

$(document).ready(function () {
    // existing textbook id is initialized to 0 by default
    // so unset it
    $("#TextBookId").val("");

    createPostForm.validate({
        onfocusout: function (element) { $(element).valid(); },
        errorClass: 'error',
        validClass: 'valid',
        rules: {
            Price: {
                required: true,
                maxlength: 9
            },
            TextBookId: {
                required: true
            }
        },
        messages: {
            TextBookId: {
                required: jQuery.format("Please choose your book")
            }
        },
        ignore: [],
        errorPlacement: function (error, element) {
            // Set positioning based on the elements position in the form
            var elem = $(element),
                corners = ['left center', 'right center'];

            // Hacky solution for validating existing book selection
            var dest = elem,
                existingBook = elem[0].id == "TextBookId";

            if (existingBook) {
                dest = $("#BookList");
                corners = ['right center', 'left center'];
            }

            // Check we have a valid error message
            if (!error.is(':empty')) {
                // Apply the tooltip only if it isn't valid
                elem.filter(':not(.valid)').qtip({
                    overwrite: false,
                    content: error,
                    position: {
                        my: corners[0],
                        at: corners[1],
                        target: dest,
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